using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SemesterProject.Models;
using SemesterProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CreamyUtopia.Controllers
{
    public class AdminController : Controller
    {

       
        private readonly ApplicationDbcontext db;
        public AdminController(ApplicationDbcontext applicationDbContext)
        {
            db = applicationDbContext;
        }


        // GET: Admin/Inventory
        public IActionResult Inventory()
        {
            var menu = db.Menu.ToList();
            return View(menu);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMenu am)
        {
            var categoryExists = db.Category.Any(c => c.CategoryName == am.CategoryName);
            if (!categoryExists)
            {
                ModelState.AddModelError("CategoryName", "Selected category doesn't exist");
                return View(am);
            }

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(am);
                }

                if (am.ImageFile == null || am.ImageFile.Length == 0)
                {
                    ModelState.AddModelError("ImageFile", "Please select an image file");
                    return View(am);
                }

                using var memoryStream = new MemoryStream();
                await am.ImageFile.CopyToAsync(memoryStream);

                // Verify the image is valid
                if (memoryStream.Length == 0)
                {
                    ModelState.AddModelError("ImageFile", "The uploaded file is empty");
                    return View(am);
                }

                var menu = new Menu()
                {
                    Name = am.Name,
                    Description = am.Description,
                    Price = am.Price,
                    CategoryName = am.CategoryName,
                    Status = am.Status,
                    Image = memoryStream.ToArray()
                };

                db.Menu.Add(menu);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Inventory));
            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", "Database error: " + dbEx.InnerException?.Message);
                return View(am);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error: " + ex.Message);
                return View(am);
            }
        }
        [HttpPost]
        public IActionResult Edit(Menu product)
        {
            if (product == null || product.Mid == 0)
            {
                return RedirectToAction(nameof(Inventory));
            }

            try
            {
                var existingItem = db.Menu.FirstOrDefault(p => p.Mid == product.Mid);
                if (existingItem == null)
                {
                    return RedirectToAction(nameof(Inventory));
                }

                // Only update fields that are in the form
                existingItem.Name = product.Name;
                existingItem.Description = product.Description;
                existingItem.Price = product.Price;
                existingItem.CategoryName = product.CategoryName;
                existingItem.Status = product.Status;

                db.SaveChanges();
                return RedirectToAction(nameof(Inventory));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating product: " + ex.Message);
                return View(product);  // Return to the edit view with the model
            }
        }
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var product = db.Menu.FirstOrDefault(p => p.Mid == id);
            if (product == null)
            {
                return RedirectToAction(nameof(Inventory));
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(int id)  // Change parameter to match route
        {
            try
            {
                var item = db.Menu.FirstOrDefault(p => p.Mid == id);
                if (item == null)
                {
                    TempData["Error"] = "Item not found";
                    return RedirectToAction(nameof(Inventory));
                }

                db.Menu.Remove(item);
                db.SaveChanges();
                TempData["Success"] = "Item deleted successfully";
                return RedirectToAction(nameof(Inventory));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting item: " + ex.Message;
                return RedirectToAction(nameof(Inventory));
            }
        }


        public IActionResult Add()
        {
            return View();
        }
        // GET: Admin/Orders
        public ActionResult Orders()
        {
            var orders = db.Order
               .OrderByDescending(o => o.OrderDate)
               .ToList();

           
            var orderItems = db.Orderitems
                .Include(oi => oi.Menu) 
                .Where(oi => orders.Select(o => o.OrderId).Contains(oi.OrderId))
                .ToList()
                .GroupBy(oi => oi.OrderId)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewBag.Orders = orders;
            ViewBag.OrderItems = orderItems;

            return View(orders);
        }



        // GET: Admin/Reviews
        public ActionResult Reviews()
        {
            return View();
        }
        
        public FileResult GetImage(int id)
        {
            var image = db.Menu.FirstOrDefault(i => i.Mid == id);
            if (image == null) return null;

            return File(image.Image, "image/jpeg"); // You can default or detect type
        }

        [HttpPost]
        public ActionResult UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                var order = db.Order.FirstOrDefault(o => o.OrderId == orderId);
                if (order == null)
                {
                    return RedirectToAction(nameof(Orders));
                }

                // Update the status
                order.Status = status;
                db.SaveChanges();

                return RedirectToAction(nameof(Orders));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Orders));
            }
        }

    }
}
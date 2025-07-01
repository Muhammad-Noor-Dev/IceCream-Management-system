using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SemesterProject.Models;
using SemesterProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;


namespace CreamyUtopia.Controllers
{

    public class HomeController : Controller
    {
       
        private readonly ApplicationDbcontext db;
        public HomeController(ApplicationDbcontext applicationDbContext)
        {
            db = applicationDbContext;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var menuItems = db.Menu
                .Include(m => m.Category)
                .Where(m => m.Status != "Inactive")
                .ToList()
                .GroupBy(m => m.Category?.CategoryName ?? "Uncategorized")
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Add cart count if user is logged in
            if (HttpContext.Session.TryGetValue("userid", out _))
            {
                int userId = HttpContext.Session.GetInt32("userid").Value;
                ViewBag.CartCount = db.CartItems
                    .Where(c => c.CustomerId == userId)
                    .Sum(c => c.Quantity);
            }

            return View(menuItems);
        }

        public ActionResult SoftieIceCream()
        {
            var items = db.Menu
                .Include(m => m.Category)
                .Where(m => m.Status != "Inactive" && m.Category.CategoryName == "Softie Ice Cream")
                .ToList();

            ViewBag.Category = "Softie Ice Cream";
            return View("MenuCategory", items);
        }

        public ActionResult Gelatos()
        {
            var items = db.Menu
                .Include(m => m.Category)
                .Where(m => m.Status != "Inactive" && m.Category.CategoryName == "Gelatos")
                .ToList();

            ViewBag.Category = "Gelatos";
            return View("MenuCategory", items);
        }

        public ActionResult Slush()
        {
            var items = db.Menu
                .Include(m => m.Category)
                .Where(m => m.Status != "Inactive" && m.Category.CategoryName == "Slush")
                .ToList();

            ViewBag.Category = "Slush";
            return View("MenuCategory", items);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(addcontact ac)
        {
            try
            {
                var con = new Contact
                {
                    Name = ac.Name,
                    Message = ac.Message,
                    Number = ac.Number,
                    Topic = ac.Topic,
                    Email = ac.Email,
                };
                db.Contacts.Add(con);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
            
        }
        public ActionResult Order()
        {
            return View();
        }

       
            public ActionResult MyOrder()
            {
            if (!HttpContext.Session.TryGetValue("userid", out _))
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = HttpContext.Session.GetInt32("userid").Value;

            // Get orders for the current user with included order items and menu details
            var orders = db.Order
                .Where(o => o.Id == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            // Get all order items for these orders with menu information
            var orderItems = db.Orderitems
                .Include(oi => oi.Menu) // Include menu details
                .Where(oi => orders.Select(o => o.OrderId).Contains(oi.OrderId))
                .ToList()
                .GroupBy(oi => oi.OrderId)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewBag.Orders = orders;
            ViewBag.OrderItems = orderItems;

            return View(orders);
        }


        public ActionResult Reviews()
        {
            if (!HttpContext.Session.TryGetValue("userid", out _))
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = HttpContext.Session.GetInt32("userid").Value;

            // Get the latest delivered order for this customer
            var latestOrder = db.Order
                .Where(o => o.Id == userId && o.Status == "Delivered")
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefault();

            if (latestOrder != null)
            {
                // Get order items with menu details
                var orderItems = db.Orderitems
                    .Include(oi => oi.Menu)
                    .Where(oi => oi.OrderId == latestOrder.OrderId)
                    .ToList();

                ViewBag.LatestOrder = latestOrder;
                ViewBag.OrderItems = orderItems;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitReview(Reviews review, int orderId)
        {
            if (!HttpContext.Session.TryGetValue("userid", out _))
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                // Set additional review properties
                review.CreatedAt = DateTime.Now;

                // Save the review
                db.Reviews.Add(review);
                db.SaveChanges();

                // Optionally mark the order as reviewed
                var order = db.Order.Find(orderId);
              

                TempData["SuccessMessage"] = "Thank you for your review!";
                return RedirectToAction("Reviews");
            }

            // If we got this far, something failed, redisplay form
            int userId = HttpContext.Session.GetInt32("userid").Value;
            var latestOrder = db.Order
                .Where(o => o.Id == userId && o.Status == "Delivered")
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefault();

            if (latestOrder != null)
            {
                var orderItems = db.Orderitems
                    .Include(oi => oi.Menu)
                    .Where(oi => oi.OrderId == latestOrder.OrderId)
                    .ToList();

                ViewBag.LatestOrder = latestOrder;
                ViewBag.OrderItems = orderItems;
            }

            return View("Reviews", review);
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public ActionResult Cart()
        {
            int cid = HttpContext.Session.GetInt32("userid").Value;
            var Cartitem = db.CartItems.Include(me => me.Menu).Where(m => m.CustomerId == cid).ToList();
            return View(Cartitem);
        }
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            // Check if user is logged in
            if (!HttpContext.Session.TryGetValue("userid", out _))
            {
                return Json(new { redirect = Url.Action("Login", "Account") });
            }

            int customerId = HttpContext.Session.GetInt32("userid").Value;

            // Check if item exists in cart
            var existingItem = db.CartItems
                .FirstOrDefault(c => c.CustomerId == customerId && c.MenuId == id);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                var newItem = new CartItem
                {
                    CustomerId = customerId,
                    MenuId = id,
                    Quantity = 1,
                    AddedDate = DateTime.Now
                };
                db.CartItems.Add(newItem);
            }

            db.SaveChanges();

            // Get updated cart count
            var cartCount = db.CartItems
                .Where(c => c.CustomerId == customerId)
                .Sum(c => c.Quantity);

            return Json(new { success = true, cartCount });
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            // Check if user is logged in
            if (!HttpContext.Session.TryGetValue("userid", out _))
            {
                return RedirectToAction("Login", "Account");
            }

            int customerId = HttpContext.Session.GetInt32("userid").Value;

            var itemToRemove = db.CartItems
                .FirstOrDefault(c => c.CartId == id && c.CustomerId == customerId);

            if (itemToRemove != null)
            {
                if(itemToRemove.Quantity > 1)
                {
                    itemToRemove.Quantity -= 1;
                }
                else
                {
                    db.CartItems.Remove(itemToRemove);
                }

               
                db.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            if (!HttpContext.Session.TryGetValue("userid", out _))
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = HttpContext.Session.GetInt32("userid").Value;

            var cartItems = db.CartItems
                .Include(ci => ci.Menu)
                .Where(ci => ci.CustomerId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Cart");
            }

            var totalAmount = cartItems.Sum(ci => ci.Menu.Price * ci.Quantity);
            totalAmount = (int)(totalAmount * 1.10);

            var model = new AddOrder
            {
                CartItems = cartItems,
                TotalAmount = totalAmount
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult ConfirmOrder(AddOrder model)
        {
            if (!HttpContext.Session.TryGetValue("userid", out _))
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = HttpContext.Session.GetInt32("userid").Value;

            try
            {
                // Create the order
                var order = new Order
                {
                    Id = userId,
                    OrderDate = DateTime.Now,
                    Amount = (int)model.TotalAmount,
                    Status = "Pending",
                    Address = model.Address
                };

                db.Order.Add(order);
                db.SaveChanges();

                // Create order items
                foreach (var cartItem in model.CartItems)
                {
                    var orderItem = new Orderitems
                    {
                        OrderId = order.OrderId,
                        Mid = cartItem.MenuId
                    };

                    db.Orderitems.Add(orderItem);
                }

                // Clear the cart
                db.CartItems.RemoveRange(model.CartItems);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Your order has been placed successfully!";
                return RedirectToAction("MyOrder");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error creating order: {ex.Message}");
                TempData["ErrorMessage"] = "There was an error processing your order. Please try again.";
                return RedirectToAction("Checkout");
            }
        }


    }
}


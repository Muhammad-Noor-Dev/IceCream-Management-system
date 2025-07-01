using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SemesterProject.Models;
using SemesterProject.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CreamyUtopia.Controllers
{
    public class AccountController : Controller
    {

    

        private readonly ApplicationDbcontext db;
        public AccountController(ApplicationDbcontext applicationDbcontext)
        {
            db=applicationDbcontext;
        }
        // GET: Account/Login
        public ActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer u)
        {
            if(u.Email=="admin@gmail.com" && u.Password=="admin123")
            {
                return Redirect("/Admin/Inventory");
            }
            try
            {
                var customer = db.Customer.FirstOrDefault(c => c.Email == u.Email && c.Password == u.Password);
                if(customer!=null)
                {
                    HttpContext.Session.SetInt32("userid", customer.Id);
                    return View("~/Views/Home/Index.cshtml");

                }
                else
                {
                    TempData["ErrorMessage"] = "Incorrect email or password. Please try again.";
                    return View();
                }

            }
            catch
            {
                TempData["ErrorMessage"] = "An error occurred during login. Please try again.";
                return View();
            }
        }


        // GET: Account/Register
        public ActionResult Register()
        {
            return View("~/Views/Account/Register.cshtml");
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(addusers user)
        {
            
            try
            {
                Customer c = new Customer()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber
                };
                db.Customer.Add(c);
                db.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View("~/Views/Account/ForgotPassword.cshtml");
        }

        // POST: Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(checklogin u )
        {
            // This would handle password reset requests
            string email = u.Email;

            // TODO: Add actual password reset logic here

            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}


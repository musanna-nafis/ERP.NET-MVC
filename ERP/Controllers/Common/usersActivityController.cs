using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.Common
{
    public class usersActivityController : Controller
    {
        // GET: usersActivity
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login u)
        {
            if (ModelState.IsValid)
            {

                var db = new ERPEntities();
                var user = (from e in db.Users
                            where e.email.Equals(u.email)
                            select e).SingleOrDefault();
                if (user == null)
                {
                    TempData["Msg"] = "Invalid email";
                    return View(u);
                }
                if (user.password != u.password)
                {
                    TempData["Msg"] = "Incorrect Password";
                    return View(u);
                }

                if (user.user_status != "active")
                {
                    TempData["Msg"] = "You are not active user";
                    return View(u);
                }

                Session["username"] = user.username;
                Session["email"] = user.email;
                Session["userType"] = user.position;
                Session["id"] = user.id;
                Session["name"] = user.firstname + " " + user.lastname;

                if (user.position == "HR")
                {
                    Session["type"] = "HR";
                    return RedirectToAction("Home", "HR_Basic");
                }

                if (user.position == "Finance Manager")
                {
                    Session["type"] = "Finance Manager";
                    return RedirectToAction("Home", "FinanceManager_basic");
                }


                if (user.position.Equals("Product Manager"))
                {
                    Session["type"] = "Product Manager";
                    return RedirectToAction("Home","./ProductHome");
                }
            }
            return View(u);
        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}
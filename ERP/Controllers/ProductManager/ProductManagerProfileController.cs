using ERP.Auth;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.ProductManager
{
    [ProductManagerLogged]
    public class ProductManagerProfileController : Controller
    {
        // GET: ProductManagerProfile
        public ERPEntities db = new ERPEntities();
        [HttpGet]
        public ActionResult MyProfile()
        {
            int id = (int)Session["id"];
            var user = (from u in db.Users where u.id == id select u).SingleOrDefault();
            var employee = (from em in db.Employees where em.employee_id==user.id select em).SingleOrDefault();
            ViewBag.user = user;
            ViewBag.employee = employee;
            return View();
        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            int id = (int)Session["id"];
            var user=(from u in db.Users where u.id==id select u).SingleOrDefault();
            ViewBag.user = user;
            return View(user);
        }
        [HttpPost]
        public ActionResult EditProfile(User us)
        {
            int id = (int)Session["id"];
            var user=(from u in db.Users where u.id==id select u).SingleOrDefault();
            ViewBag.user = user;
            if(ModelState.IsValid)
            {
                if(us.password!=user.password)
                {
                    TempData["msge"] = "Wrong Current Password!";
                    return View(us);
                }
                user.firstname = us.firstname;
                user.lastname = us.lastname;
                user.phone = us.phone;
                user.email = us.email;
                Session["name"] = user.firstname + " " + user.lastname;
                Session["email"] = user.email;
                db.SaveChanges();
                TempData["msgs"] = "Profile Updated!";
                return View(us);
            }
            return View(us);
        }
    }
}
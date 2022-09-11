using ERP.Auth;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.HR
{
    [HrLogged]
    public class HR_ProfileController : Controller
    {
        // GET: HR_Profile
        public ActionResult ProfileDetails()
        {

            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            return View(user);


        }
        [HttpGet]
        public ActionResult ProfileEdit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProfileEdit(HR_ProfileEdit p)
        {

            if (ModelState.IsValid)
            {
                var uname = (string)Session["username"];
                var db = new ERPEntities();
                var user = (from e in db.Users
                            where e.username.Equals(uname)
                            select e).SingleOrDefault();


                var user1 = (from e in db.Users
                             where e.email.Equals(p.email)
                             select e).SingleOrDefault();
                if (user1 != null && (!user1.username.Equals(uname)))
                {
                    TempData["msg"] = "A user with this email address already exists";
                }
                else
                {
                    user.firstname = p.firstname;
                    user.lastname = p.lastname;
                    user.phone = p.phone;
                    user.email = p.email;
                    user.address = p.address;
                    db.SaveChanges();
                    TempData["msg"] = "Profile Updated successfully";
                    return RedirectToAction("ProfileDetails");
                }

            }
            return View(p);
        }


        [HttpGet]
        public ActionResult PasswordChange()
        {

            return View();
        }
        [HttpPost]
        public ActionResult PasswordChange(FormCollection form)
        {
            var old_password = (string)form["old_password"];
            var new_password = (string)form["new_password"];
            var confirm_new_password = (string)form["confirm_new_password"];
            var uname = (string)Session["username"];
            var db = new ERPEntities();
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            if (!user.password.Equals(old_password))
            {
                TempData["msg"] = "Wrong password!";
                return View();

            }
            else
            {
                if (new_password.Length < 4)
                {
                    TempData["msg"] = "New password must be at least 4 character";
                    return View();
                }
                else
                {
                    if (confirm_new_password.Equals(new_password))
                    {
                        TempData["msg"] = "Password Change successfull";
                        user.password = new_password;
                        db.SaveChanges();
                        return View();
                    }

                    else
                    {
                        TempData["msg"] = "Confirm Password and New Password does not match!";
                        return View();
                    }
                }



            }

        }

    }
}
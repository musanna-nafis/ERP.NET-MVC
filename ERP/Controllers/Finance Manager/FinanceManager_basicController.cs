using ERP.Auth;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.Finance_Manager
{
    [FinanceManagerLogged]
    public class FinanceManager_basicController : Controller
    {
        // GET: FinanceManager_basic
        public ActionResult Home()
        {

            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();

            var organization = (from f in db.Organizatons
                                where f.id == user.organization_id
                                select f).SingleOrDefault();

            var hr = (from g in db.Users
                      where g.organization_id == user.organization_id && g.position.Equals("HR")
                      select g).SingleOrDefault();

            var product = (from h in db.Users
                           where h.organization_id == user.organization_id && h.position.Equals("Product Manager")
                           select h).SingleOrDefault();

            var sales = (from i in db.Users
                         where i.organization_id == user.organization_id && i.position.Equals("Sales Manager")
                         select i).SingleOrDefault();

            ViewBag.organization = organization;
            ViewBag.hr = hr;
            ViewBag.product = product;
            ViewBag.sales = sales;
            return View(user);
        }
        public ActionResult ProfileInfo()
        {
            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            return View(user);
        }
        [HttpGet]
        public ActionResult UpdateInformantion()
        {
            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult UpdateInformantion(Finance_changeinfo ff)
        {
            if (ModelState.IsValid)
            {

                //find main user
                var uname = (string)Session["username"];

                var db = new ERPEntities();

                var user = (from e in db.Users
                            where e.username.Equals(uname)
                            select e).SingleOrDefault();

                var count = 0;
                var str = "";
                if (!user.username.Equals(ff.username))
                {
                    var user1 = (from e in db.Users
                                 where e.username.Equals(ff.username)
                                 select e).SingleOrDefault();
                    if (user1 != null)
                    {
                        TempData["msg"] = "This username already used by " + user1.firstname + " " + user1.lastname;
                        return View(ff);
                    }
                    else
                    {
                        count++;
                        str += "Username" + ",";
                    }
                }
                if (!user.email.Equals(ff.email))
                {
                    var user2 = (from e in db.Users
                                 where e.email.Equals(ff.email)
                                 select e).SingleOrDefault();
                    if (user2 != null)
                    {
                        TempData["msg"] = "This email already used by " + user2.firstname + " " + user2.lastname;
                        return View(ff);
                    }
                    else
                    {
                        count++;
                        str += "Email" + ",";
                    }
                }

                if (!user.phone.Equals(ff.phone))
                {
                    var user3 = (from e in db.Users
                                 where e.phone.Equals(ff.phone)
                                 select e).SingleOrDefault();
                    if (user3 != null)
                    {
                        TempData["msg"] = "This Mobile number already used by " + user3.firstname + " " + user3.lastname;
                        return View(ff);
                    }
                    else
                    {
                        count++;
                        str += "Mobile number" + ",";
                    }
                }
                if (user.address != (ff.address))
                {
                    count++;
                    str += "Address";
                }

                if (count == 0)
                {
                    TempData["msg"] = "You don't apply to update anything,Thank You!";
                    return View(ff);
                }
                else
                {
                    TempData["msg"] = str + " " + "Update successfully";
                    user.username = ff.username;
                    user.email = ff.email;
                    user.phone = ff.phone;
                    user.address = ff.address;
                    db.SaveChanges();
                    Session["username"] = ff.username;
                    Session["email"] = ff.email;

                    return View(ff);

                }
            }
            return View(ff);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            var a = (string)form["password"];
            var b = (string)form["npassword"];
            var c = (string)form["cpassword"];
            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            if (!user.password.Equals(a))
            {
                TempData["msg"] = "Your current password doesnot match";
                return View();
            }
            else
            {
                if (!b.Equals(c))
                {
                    TempData["msg"] = "Your new password and confirm new password doesnot match";
                    return View();
                }
                else
                {
                    if (b.Length < 7)
                    {
                        TempData["msg"] = "New password must be at least 7 character";
                        return View();
                    }
                    else if (a.Equals(b))
                    {
                        TempData["msg"] = "Current password and new password should not be match";
                        return View();
                    }
                    else
                    {
                        TempData["msg"] = "Password successfully updated";
                        user.password = b;
                        db.SaveChanges();
                        return View();
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult LeaveApplication()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LeaveApplication(FormCollection form)
        {
            return View();
        }
        public ActionResult LeaveRequests()
        {

            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();

            var requests = (from e in db.Leave_requests
                            where e.employee_id == user.id
                            select e).ToList();

            return View(requests);
        }
        public ActionResult DeleteApply(int id)
        {
            var db = new ERPEntities();
            var requests = (from e in db.Leave_requests
                            where e.id == id
                            select e).SingleOrDefault();
            db.Leave_requests.Remove(requests);
            db.SaveChanges();
            return RedirectToAction("LeaveRequests");
        }
        [HttpGet]
        public ActionResult LeaveApply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LeaveApply(Leave_requests l)
        {
            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();

            if (ModelState.IsValid)
            {

                var result = DateTime.Compare((DateTime)l.end_time, (DateTime)l.start_time);
                if (result < 0)
                {
                    TempData["msg"] = "End time is previous than start time";
                    return View(l);
                }
                var result1 = DateTime.Compare((DateTime)l.start_time, DateTime.Today);
                if (result1 < 0)
                {
                    TempData["msg"] = "Start time is previous than Today";
                    return View(l);
                }

                l.request_made = DateTime.Today;
                l.status = "pending";
                l.employee_id = user.id;
                db.Leave_requests.Add(l);
                db.SaveChanges();
                return RedirectToAction("LeaveRequests");

            }
            return View(l);
        }
    }
}
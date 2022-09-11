using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ERP.Controllers.Sales_Manager
{
    public class sales_Controller : Controller
    {
        //private object user;

        // GET: sales_
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult createCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createCustomer(Customer t)
        {

            if (ModelState.IsValid)
            {

                ERPEntities db = new ERPEntities();
                db.Customers.Add(t);
                db.SaveChanges();
                //return View();
                return RedirectToAction("viewCustomer");
            }
            return View(t);
        }
        public ActionResult viewCustomer()
        {
            ERPEntities db = new ERPEntities();
            var data = db.Customers.ToList();
            return View(data);
        }
        public ActionResult createOrder()
        {
            return View();
        }
        [HttpPost]

        public ActionResult createOrder(Order t)
        {
            if (ModelState.IsValid)
            {

                ERPEntities db = new ERPEntities();
                db.Orders.Add(t);
                db.SaveChanges();
                //return View();
                return RedirectToAction("viewOrder");
            }
            return View(t);

        }
        public ActionResult viewOrder()
        {
            ERPEntities db = new ERPEntities();
            var data = db.Orders.ToList();
            return View(data);

        }
        [HttpGet]
        public ActionResult loginSales()
        {
            return View();
        }
        [HttpPost]
        public ActionResult loginSales(User s)
        {

            ERPEntities db = new ERPEntities();
            var user = (from e in db.Users
                        where e.username.Equals(s.username) &&
                        e.password.Equals(s.password)
                        select e).SingleOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie("data", true);
                //Session["user_mobile"] = user.Mobile;
                Session["username"] = user.username;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Msg"] = "Username and Password is invalid";
                return View();
            }
        }
        public ActionResult profile()
        {
            ERPEntities db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            return View(user);
        }
        [HttpGet]
        public ActionResult updateProfile(int? id)
        {
            ERPEntities db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult updateProfile(sales_changeInfo s)
        {

            ERPEntities db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            Session["username"] = s.username;
            user.username = s.username;
            user.firstname = s.firstname;
            user.lastname = s.lastname;
            user.email = s.email;
            user.phone = s.phone;
            user.address = s.address;

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
                db.SaveChanges();
            }
            //return View();
            return RedirectToAction("profile");
            //return View(user);
        }
        [HttpGet]
        public ActionResult changePassword()
        {
            ERPEntities db = new ERPEntities();

            return View();
        }
        [HttpPost]
        public ActionResult changePassword(User s)
        {
            ERPEntities db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            if (user.password == s.password)
            {
                //user.password = s.new_password;
            }
            return View();
        }
        [HttpGet]
        public ActionResult customerEdit(int id)
        {
            var db = new ERPEntities();
            var customer = (from p in db.Customers where p.id == id select p).SingleOrDefault();
            Session["customerId"] = id;

            return View(customer);
        }
        [HttpPost]
        public ActionResult customerEdit(Customer s)
        {
            ERPEntities db = new ERPEntities();
            var customerid = (int)Session["customerId"];
            var customer = (from p in db.Customers where p.id == customerid select p).SingleOrDefault();
            customer.name = s.name;
            customer.email = s.email;
            customer.phone = s.phone;
            customer.delivery_point = s.delivery_point;
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("viewCustomer");
        }

        public ActionResult customerDelete(int id)
        {
            var db = new ERPEntities();
            var customer = (from p in db.Customers where p.id == id select p).SingleOrDefault();
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        // @Html.ActionLink("edit","customerEdit", new { id = s.id })  

        public ActionResult Orderdelete(int id)
        {
            var db = new ERPEntities();
            var order = (from p in db.Orders where p.id == id select p).SingleOrDefault();
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult orderEdit(int id)
        {
            var db = new ERPEntities();
            var order = (from p in db.Orders where p.id == id select p).SingleOrDefault();
            Session["orderId"] = id;

            return View(order);
        }

        [HttpPost]
        public ActionResult orderEdit(Order s)
        {
            ERPEntities db = new ERPEntities();
            var orderId = (int)Session["orderId"];
            var order = (from p in db.Orders where p.id == orderId select p).SingleOrDefault();
            order.customer_id = s.customer_id;
            order.order_description = s.order_description;
            order.total_amount = s.total_amount;
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("viewOrder");
        }
        [HttpGet]
        public ActionResult leaveRequest()
        {

            return View();
        }
        [HttpPost]

        public ActionResult leaveRequest(Leave_requests t)
        {
            if (ModelState.IsValid)
            {

                ERPEntities db = new ERPEntities();
                db.Leave_requests.Add(t);
                db.SaveChanges();
                //return View();
                return RedirectToAction("viewOrder");
            }
            return View(t);

        }
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("loginSales");
        }
        public ActionResult sendEmail()
        {
            var Email = "student.38040@gmail.com";
            var To = "shahidinfo.45@gmail.com";
            var Body = "This is test email";
            var Subject = "First mail";
            var Password = "dhyamycsibzwnbwf";
            using (MailMessage mm = new MailMessage(Email, To))
            {
                mm.Subject = Subject;
                mm.Body = Body;
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential cred = new NetworkCredential(Email,Password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = cred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    TempData["msg"] = "Email sent";
                }
            }

            return View();
        }
    }
}
using ERP.Auth;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.HR
{
    [HrLogged]
    public class HR_BasicController : Controller
    {
        // GET: HR_Basic
        public ActionResult Home()
        {
            var db = new ERPEntities();
            var male = (from e in db.Users
                        where e.gender.Equals("male")
                        select e).Count();

            var female = (from e in db.Users

                          where e.gender.Equals("female")
                          select e).Count();

            ViewBag.male = male;
            ViewBag.female = female;
            return View();
        }
        [HttpGet]
        public ActionResult SendEmail()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(HR_SendEmail email)
        {
            if (ModelState.IsValid)
            {


                var Email = "student.38040@gmail.com";
                var Password = "dhyamycsibzwnbwf";

                using (MailMessage mm = new MailMessage(Email, email.To))
                {
                    mm.Subject = email.Subject;
                    mm.Body = email.Body;
                    mm.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential cred = new NetworkCredential(Email, Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = cred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        TempData["msg"] = "Email sent";
                    }
                }

                return View();
            }
            return View(email);
        }




    }
}
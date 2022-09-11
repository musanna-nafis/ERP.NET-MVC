using ERP.Auth;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.Finance_Manager
{
    [FinanceManagerLogged]
    public class FinanceManager_ProfessionalController : Controller
    {
        //private object path;

        // GET: FinanceManager_Professional
        public ActionResult Payments()
        {
            return View();
        }
        public ActionResult HistoryPayment()
        {
            var uname = (string)Session["username"];

            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var history = (from e in db.Finance_payment_histories
                           where e.manager_id == user.id
                           select e).ToList();

            return View(history);
        }
        public ActionResult CustomerPayments()
        {
            var db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var invoice = (from e in db.Invoices
                           where e.manager_id == user.id && e.type.Equals("Customer") && e.status.Equals("Unadjusted")
                           select e).ToList();
            return View(invoice);
        }
        public ActionResult CustomerAdjust(int id)
        {
            var db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var invoice = (from e in db.Invoices
                           where e.id == id
                           select e).SingleOrDefault();

            invoice.status = "Adjusted";
            var asset = new Asset();
            asset.type = "Sales";
            asset.amount = invoice.total_amount;
            asset.manager_id = user.id;
            db.Assets.Add(asset);
            //add to payment history
            var payment_history = new Finance_payment_histories();
            payment_history.type = "Credit";
            payment_history.amount = invoice.total_amount;
            payment_history.manager_id = user.id;
            db.Finance_payment_histories.Add(payment_history);

            //Banks Balance add
            var bank = (from e in db.Banks
                        where e.manager_id == user.id
                        select e).SingleOrDefault();
            bank.balance += payment_history.amount;
            db.SaveChanges();


            return RedirectToAction("HistoryPayment");
        }
        public ActionResult SupplierPayments()
        {
            var db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var invoice = (from e in db.Invoices
                           where e.manager_id == user.id && e.type.Equals("Supplier") && e.status.Equals("Unadjusted")
                           select e).ToList();
            return View(invoice);
        }
        public ActionResult SupplierAdjust(int id)
        {
            var db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var invoice = (from e in db.Invoices
                           where e.id == id
                           select e).SingleOrDefault();

            invoice.status = "Adjusted";

            var asset = new Asset();
            asset.type = "Purchases";
            asset.amount = invoice.total_amount;
            asset.manager_id = user.id;
            db.Assets.Add(asset);

            //add to payment history
            var payment_history = new Finance_payment_histories();
            payment_history.type = "Debit";
            payment_history.amount = invoice.total_amount;
            payment_history.manager_id = user.id;
            db.Finance_payment_histories.Add(payment_history);

            //
            var liability = new Liability();
            liability.type = "Expenses";
            liability.amount = payment_history.amount;
            liability.manager_id = user.id;

            //Banks Balance add
            var bank = (from e in db.Banks
                        where e.manager_id == user.id
                        select e).SingleOrDefault();
            bank.balance -= payment_history.amount;

            db.SaveChanges();

            return RedirectToAction("HistoryPayment");
        }

        public ActionResult PaymentsChart()
        {
            var db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var Debit = (from e in db.Finance_payment_histories
                         where e.type.Equals("Debit") && e.manager_id == user.id
                         select e).Count();

            var Credit = (from e in db.Finance_payment_histories
                          where e.type.Equals("Credit") && e.manager_id == user.id
                          select e).Count();

            ViewBag.Debit = Debit;
            ViewBag.Credit = Credit;
            return View();
        }

        [HttpGet]
        public ActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendEmail(FinanceManager_Email email)
        {
            if (ModelState.IsValid)
            {


                var Email = "student.38040@gmail.com";

                using (MailMessage mm = new MailMessage(Email, email.To))
                {
                    mm.Subject = email.Subject;
                    mm.Body = email.Body;
                    mm.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential cred = new NetworkCredential(Email, email.Password);
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
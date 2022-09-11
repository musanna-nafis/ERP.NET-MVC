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
    public class FinanceManager_BudgetingController : Controller
    {
        // GET: FinanceManager_Budgeting
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ConnectedBanks()
        {
            var db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var bank = (from e in db.Banks
                        where e.manager_id == user.id
                        select e).ToList();
            return View(bank);
        }
        public ActionResult DisconnectBank(int id)
        {
            var db = new ERPEntities();
            var uname = (string)Session["username"];
            var user = (from e in db.Users
                        where e.username.Equals(uname)
                        select e).SingleOrDefault();
            var bank = (from e in db.Banks
                        where e.manager_id == user.id && e.id == id
                        select e).SingleOrDefault();
            db.Banks.Remove(bank);
            db.SaveChanges();
            return RedirectToAction("ConnectedBanks");
        }
        [HttpGet]
        public ActionResult AddBankAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBankAccount(Bank b)
        {
            if (ModelState.IsValid)
            {
                var db = new ERPEntities();
                var uname = (string)Session["username"];
                var user = (from e in db.Users
                            where e.username.Equals(uname)
                            select e).SingleOrDefault();
                var id = user.id;
                if (b.balance < 0)
                {
                    TempData["msg"] = "Balance can not be negative";
                    return View(b);
                }
                var bank = new Bank();
                bank.manager_id = id;
                bank.balance = b.balance;
                bank.name = b.name;
                bank.holder_name = b.holder_name;
                bank.account_no = b.account_no;
                db.Banks.Add(bank);
                db.SaveChanges();
                return RedirectToAction("ConnectedBanks");
            }
            return View(b);
        }
        [HttpGet]

        public ActionResult Expenses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Expenses(Asset a)
        {
            if (ModelState.IsValid)
            {
                var db = new ERPEntities();
                var uname = (string)Session["username"];
                var user = (from e in db.Users
                            where e.username.Equals(uname)
                            select e).SingleOrDefault();
                var id = user.id;
                if (a.amount < 0)
                {
                    TempData["msg"] = "Balance can not be negative";
                    return View(a);
                }
                var bank = (from e in db.Banks
                            where e.manager_id == id
                            select e).FirstOrDefault();

                if (bank.balance < a.amount)
                {
                    TempData["msg"] = "Bank Doesnot have Enough Money";
                    return View(a);
                }
                bank.balance -= a.amount;

                var expence = new Asset();
                expence.manager_id = id;
                expence.amount = a.amount;
                expence.type = a.type;
                db.Assets.Add(expence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a);
        }
        [HttpGet]
        public ActionResult Liability()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Liability(Liability a)
        {
            if (ModelState.IsValid)
            {
                var db = new ERPEntities();
                var uname = (string)Session["username"];
                var user = (from e in db.Users
                            where e.username.Equals(uname)
                            select e).SingleOrDefault();
                var id = user.id;
                if (a.amount < 0)
                {
                    TempData["msg"] = "Balance can not be negative";
                    return View(a);
                }
                var bank = (from e in db.Banks
                            where e.manager_id == id
                            select e).FirstOrDefault();

                if (bank.balance < a.amount)
                {
                    TempData["msg"] = "Bank Doesnot have Enough Money";
                    return View(a);
                }
                bank.balance -= a.amount;

                var expence = new Liability();
                expence.manager_id = id;
                expence.amount = a.amount;
                expence.type = a.type;
                db.Liabilities.Add(expence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a);
        }
        [HttpGet]
        public ActionResult Asset()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Asset(Asset a)
        {
            if (ModelState.IsValid)
            {
                var db = new ERPEntities();
                var uname = (string)Session["username"];
                var user = (from e in db.Users
                            where e.username.Equals(uname)
                            select e).SingleOrDefault();
                var id = user.id;
                if (a.amount < 0)
                {
                    TempData["msg"] = "Balance can not be negative";
                    return View(a);
                }
                var bank = (from e in db.Banks
                            where e.manager_id == id
                            select e).FirstOrDefault();


                bank.balance += a.amount;

                var expence = new Asset();
                expence.manager_id = id;
                expence.amount = a.amount;
                expence.type = a.type;
                db.Assets.Add(expence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(a);
        }

    }
}
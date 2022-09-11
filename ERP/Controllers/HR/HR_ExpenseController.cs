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
    public class HR_ExpenseController : Controller
    {
        // GET: HR_Expense
        [HttpGet]
        public ActionResult ExpenseReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExpenseReport(HR_ExpenseReport expense)
        {
            if (ModelState.IsValid)
            {
                var db = new ERPEntities();
                var expense1 = new Expens();


                expense1.name = expense.name;

                expense1.catagory = expense.catagory;
                expense1.amount = expense.amount;
                expense1.description = expense.description;
                expense1.expense_date = expense.expense_date;


                db.Expenses.Add(expense1);
                db.SaveChanges();
                TempData["msg"] = "Expense Report Added";

                return RedirectToAction("ExpenseReport");

            }
            return View();
        }
        public ActionResult ExpenseReportCreate()
        {
            return View();
        }
        public ActionResult ExpenseList()
        {
            var db = new ERPEntities();
            var expenses = db.Expenses.ToList();
            return View(expenses);

        }
        public ActionResult ExpenseEdit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ExpenseUpdate(int id)
        {
            var db = new ERPEntities();
            var req = (from e in db.Expenses
                       where e.id == id
                       select e).SingleOrDefault();

            return View(req);
        }
        [HttpPost]
        public ActionResult ExpenseUpdate(FormCollection e, int id)
        {
            var db = new ERPEntities();

            var emp = (from u in db.Expenses
                       where u.id == id
                       select u).SingleOrDefault();




            emp.name = e["name"];
            emp.catagory = e["catagory"];
            emp.description = e["description"];
            emp.amount = int.Parse(e["amount"]);
            emp.expense_date = DateTime.Parse(e["expense_date"]);







            db.SaveChanges();
            TempData["msg"] = "Updated";

            return RedirectToAction("ExpenseList");
        }
        [HttpGet]
        public ActionResult ExpenseDelete(int id)
        {
            var db = new ERPEntities();
            var expense = (from u in db.Expenses
                           where u.id == id
                           select u).SingleOrDefault();
            return View(expense);
        }
        [HttpPost]
        public ActionResult ExpenseDelete(int id, FormCollection u)
        {
            var db = new ERPEntities();
            var req = (from e in db.Expenses
                       where e.id == id
                       select e).SingleOrDefault();
            db.Expenses.Remove(req);
            db.SaveChanges();
            TempData["msg"] = "Deleted";
            return RedirectToAction("ExpenseList");
        }
        public ActionResult ExpenseDestroy()
        {
            return View();
        }
        public ActionResult ExpenseStatistic()
        {
            var db = new ERPEntities();
            var food = (from e in db.Expenses
                        where e.catagory.Equals("Food")
                        select e).Count();

            var transport = (from e in db.Expenses

                             where e.catagory.Equals("Transport")
                             select e).Count();

            ViewBag.food = food;
            ViewBag.transport = transport;
            return View();

        }
        public ActionResult ExpenseListExport()
        {
            return View();
        }
    }
}
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
    public class ProductIssueController : Controller
    {
        // GET: ProductIssue
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Issue isu)
        {
            if(ModelState.IsValid)
            {
                string username = Session["username"].ToString();
                isu.issued_by = username;
                isu.issue_time = DateTime.Now;
                isu.status = "Pending";
                var db = new ERPEntities();
                db.Issues.Add(isu);
                TempData["msg"] = "Issue sent to Administration Panel";
                db.SaveChanges();
                return View();
            }
            return View(isu);
        }
        public ActionResult MyIssues()
        {
            var db = new ERPEntities();
            string username = Session["username"].ToString();
            var myList = (from list in db.Issues where list.issued_by.Equals(username) select list).ToArray();
            ViewBag.MyIssues = myList;
            return View();
        }
    }
}
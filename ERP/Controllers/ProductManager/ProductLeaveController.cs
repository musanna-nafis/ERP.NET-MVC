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
    public class ProductLeaveController : Controller
    {
        // GET: ProductLeave
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Leave_requests lreq)
        {
            if(ModelState.IsValid)
            {
                if(lreq.start_time>lreq.end_time)
                {
                    TempData["msgd"] = "End time must be greater than start time!";
                    return View(lreq);
                }
                int id = (int)Session["id"];
                lreq.employee_id = id;
                lreq.request_made = DateTime.Now;
                lreq.status = "Pending";
                var db = new ERPEntities();
                db.Leave_requests.Add(lreq);
                TempData["msg"]= "Leave request sent to HR";
                db.SaveChanges();
                return View();
            }
            return View(lreq);
        }

        public ActionResult MyLeave()
        {
            var db = new ERPEntities();
            int id = (int)Session["id"];
            var myList = (from list in db.Leave_requests where list.employee_id == id select list).ToArray();
            ViewBag.myList = myList;
            return View();
        }
    }
}
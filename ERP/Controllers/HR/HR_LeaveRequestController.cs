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
    public class HR_LeaveRequestController : Controller
    {
        // GET: HR_LeaveRequest
        [HttpGet]
        public ActionResult LeaveRequest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LeaveRequest(HR_LeaveRequest r)
        {
            if (ModelState.IsValid)
            {
                var db = new ERPEntities();
                var req = new Leave_requests();


                req.type = r.type;

                req.request_description = r.request_description;
                req.start_time = r.start_time;
                req.end_time = r.end_time;





                db.Leave_requests.Add(req);
                db.SaveChanges();
                TempData["msg"] = "Created";

                return RedirectToAction("LeaveRequestList");

            }
            return View();
        }
        [HttpGet]
        public ActionResult LeaveRequestApprove(int id)
        {
            var db = new ERPEntities();
            var req = (from u in db.Leave_requests
                       where u.id == id
                       select u).SingleOrDefault();
            return View(req);
        }
        [HttpPost]
        public ActionResult LeaveRequestApprove(int id, FormCollection u)
        {
            var db = new ERPEntities();
            var req = (from r in db.Leave_requests
                       where r.id == id
                       select r).SingleOrDefault();
            if (req.status == "Pending")
            {
                req.status = "Approved";
                db.SaveChanges();
                TempData["msg"] = "Approved";

                return RedirectToAction("LeaveRequestList");
            }
            else

            {
                TempData["msg"] = "Failed!!!";

                return RedirectToAction("LeaveRequestList");

            }


        }
        public ActionResult LeaveRequestApproveVerify()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LeaveRequestReject(int id)
        {
            var db = new ERPEntities();
            var req = (from u in db.Leave_requests
                       where u.id == id
                       select u).SingleOrDefault();
            return View(req);
        }
        [HttpPost]
        public ActionResult LeaveRequestReject(FormCollection e, int id)
        {
            var db = new ERPEntities();
            var req = (from r in db.Leave_requests
                       where r.id == id
                       select r).SingleOrDefault();
            if (req.status == "Pending")
            {
                req.status = "Declined";
                db.SaveChanges();
                TempData["msg"] = "Declined";

                return RedirectToAction("LeaveRequestList");
            }
            else

            {
                TempData["msg"] = "Failed!!!";

                return RedirectToAction("LeaveRequestList");

            }
        }
        public ActionResult LeaveRequestRejectVerify()
        {
            return View();
        }
        public ActionResult LeaveRequestList()
        {
            var db = new ERPEntities();
            var reqs = db.Leave_requests.ToList();
            return View(reqs);
        }
    }
}
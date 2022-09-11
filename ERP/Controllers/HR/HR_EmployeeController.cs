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
    public class HR_EmployeeController : Controller
    {
        // GET: HR_Employee
        [HttpGet]
        public ActionResult EmployeeCreate()
            
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeCreate(HR_EmployeeCreate e)
        {

            var db = new ERPEntities();
            var emp = new Employee();


            emp.employee_id = e.employee_id;

            emp.employee_name = e.employee_name;
            emp.gender = e.gender;
            emp.supervisor = e.supervisor;
            emp.present_address = e.present_address;
            emp.phone = e.phone;
            emp.job_position = e.job_position;
            emp.employee_group = "N/A";
            emp.start_time = e.start_time;
            emp.end_time = e.end_time;
            emp.hours_worked = e.hours_worked;
            emp.employement_start_date = e.employement_start_date;



            var user = (from u in db.Users
                        where u.id == e.employee_id
                        select u).SingleOrDefault();





            if (user == null)
            {
                TempData["msg"] = "Invalid Employee Id";

                return View(emp);
            }
            else
            {

                db.Employees.Add(emp);
                db.SaveChanges();
                TempData["msg"] = "Employee Added";

                return RedirectToAction("EmployeeList");
            }


        }

        public ActionResult EmployeeList()
        {
            var db = new ERPEntities();
            var users = db.Employees.ToList();
            return View(users);

        }

        [HttpGet]
        public ActionResult EmployeeUpdate(int id)
        {
            var db = new ERPEntities();
            var req = (from e in db.Employees
                       where e.id == id
                       select e).SingleOrDefault();

            return View(req);
        }
        [HttpPost]
        public ActionResult EmployeeUpdate(FormCollection e, int id)
        {
            var db = new ERPEntities();

            var emp = (from u in db.Employees
                       where u.id == id
                       select u).SingleOrDefault();


            emp.employee_id = int.Parse(e["employee_id"]);


            emp.employee_name = e["employee_name"];
            emp.gender = e["gender"];
            emp.supervisor = e["supervisor"];
            emp.present_address = e["present_address"];
            emp.phone = e["phone"];
            emp.job_position = e["job_position"];
            emp.employee_group = "N/A";
            emp.start_time = TimeSpan.Parse(e["start_time"]);
            emp.end_time = TimeSpan.Parse(e["end_time"]);
            emp.hours_worked = int.Parse(e["hours_worked"]);
            int employee_id = int.Parse(e["employee_id"]);
            var user = (from u in db.Users
                        where u.id == employee_id
                        select u).SingleOrDefault();
            if (user == null)
            {
                TempData["msg"] = "Invalid Employee Id";

                return View(emp);
            }
            else
            {


                db.SaveChanges();
                TempData["msg"] = "Employee Information Updated";

                return RedirectToAction("EmployeeList");
            }

        }

        [HttpGet]
        public ActionResult EmployeeDelete(int id)
        {
            var db = new ERPEntities();
            var user = (from u in db.Employees
                        where u.id == id
                        select u).SingleOrDefault();
            return View(user);


        }
        [HttpPost]
        public ActionResult EmployeeDelete(int id, FormCollection u)
        {
            var db = new ERPEntities();
            var req = (from e in db.Employees
                       where e.id == id
                       select e).SingleOrDefault();
            db.Employees.Remove(req);
            db.SaveChanges();
            TempData["msg"] = "Employee Deleted";
            return RedirectToAction("EmployeeList");

        }


        [HttpGet]
        public ActionResult CreateEmployeeGroup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployeeGroup(HR_CreateEmployeeGroup c)
        {


            int id = c.employee_id;


            var group = c.employee_group;
            var db = new ERPEntities();
            var employee = (from e in db.Employees
                            where e.employee_id == id
                            select e).SingleOrDefault();

            if (employee == null)
            {
                TempData["msg"] = "Invalid Employee Id";
                return View();
            }
            else
            {

                if (employee.employee_group == "N/A")
                {

                    employee.employee_group = group;
                    db.SaveChanges();
                    TempData["msg"] = "Employee added to group Successfully";
                    return RedirectToAction("EmployeeList");
                }
                else
                {
                    TempData["msg"] = "Employee is alreay added to a group";
                    return View();
                }
            }


        }


        public ActionResult EmployeeSchedule()
        {
            var db = new ERPEntities();
            var users = db.Employees.ToList();
            return View(users);

        }

    }
}
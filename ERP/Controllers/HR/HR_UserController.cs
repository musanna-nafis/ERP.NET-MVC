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
    public class HR_UserController : Controller
    {
        // GET: HR_User
        [HttpGet]
        public ActionResult UserCreate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UserCreate(HR_UserCreate u)
        {
            if (ModelState.IsValid)
            {
                var db = new ERPEntities();

                var user = new User();

                user.firstname = u.firstname;

                user.lastname = u.lastname;
                user.username = u.username;
                user.organization_id = u.organization_id;
                user.password = u.password;
                user.gender = u.gender;
                user.type = u.type;
                user.phone = u.phone;
                user.address = u.address;
                user.email = u.email;
                user.position = u.position;
                int c = 0;
                var user1 = (from e in db.Users
                             where e.email.Equals(u.email)
                             select e).SingleOrDefault();
                if (user1 != null)
                {
                    TempData["msg"] = "A user with this email address already exists";

                    return View(user);
                }
                else
                {
                    c++;
                }
                if (!u.password.Equals(u.confirm_password))
                {
                    TempData["msg"] = "Confirm Password and Password do not match";

                    return View(user);
                }
                else
                {
                    c++;
                }
                if (u.password.Length < 4)
                {
                    TempData["msg"] = "password must be at least 4 character";

                    return View(user);
                }
                else
                {
                    c++;
                }
                var user2 = (from e in db.Users
                             where e.username.Equals(u.username)
                             select e).SingleOrDefault();
                if (user2 != null)
                {
                    TempData["msg"] = "A user with this username already exists";

                    return View(user);

                }
                else
                {
                    c++;
                }
                if (c == 4)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    TempData["msg"] = "User Added";

                    return RedirectToAction("UserList");

                }


            }


            return View();
        }

        [HttpGet]
        public ActionResult UserUpdate(int id)
        {
            var db = new ERPEntities();
            var req = (from e in db.Users
                       where e.id == id
                       select e).SingleOrDefault();

            return View(req);

        }
        [HttpPost]
        public ActionResult UserUpdate(FormCollection u, int id)
        {
            var db = new ERPEntities();

            var user = (from e in db.Users
                        where e.id == id
                        select e).SingleOrDefault();

            user.firstname = u["firstname"];

            user.lastname = u["lastname"];
            user.username = u["username"];
            user.password = u["password"];
            user.gender = u["gender"];
            user.type = u["type"];
            user.phone = u["phone"];
            user.address = u["address"];
            user.email = u["email"];
            int c = 0;
            if (u["organization_id"] != null)
            {
                user.organization_id = int.Parse(u["organization_id"]);
            }


            if (!u["password"].Equals(u["confirm_password"]))
            {
                TempData["msg"] = "Confirm Password and Password do not match";

                return View(user);
            }
            else
            {
                c++;
            }
            if (u["password"].Length < 4)
            {
                TempData["msg"] = "password must be at least 4 character";

                return View(user);
            }
            else
            {
                c++;
            }
            string email = u["email"];
            var user1 = (from e in db.Users
                         where e.email.Equals(email)
                         select e).SingleOrDefault();
            if (user1 != null && (user1.id != id))
            {
                TempData["msg"] = "A user with this email address already exists";

                return View(user);
            }
            else
            {
                c++;
            }
            string username = u["username"];
            var user2 = (from e in db.Users
                         where e.username.Equals(username)
                         select e).SingleOrDefault();
            if (user2 != null)
            {
                TempData["msg"] = "A user with this username already exists";

                return View(user);

            }
            else
            {
                c++;
            }

            if (c == 4)
            {

                db.SaveChanges();
                TempData["msg"] = "User Information Updated";

                return RedirectToAction("UserList");
            }
            return View(user);

        }










        [HttpGet]
        public ActionResult UserDelete(int id)
        {
            var db = new ERPEntities();
            var user = (from u in db.Users
                        where u.id == id
                        select u).SingleOrDefault();
            return View(user);

        }
        [HttpPost]
        public ActionResult UserDelete(int id, FormCollection u)
        {
            var db = new ERPEntities();
            var req = (from e in db.Users
                       where e.id == id
                       select e).SingleOrDefault();
            db.Users.Remove(req);
            db.SaveChanges();
            TempData["msg"] = "User Deleted";
            return RedirectToAction("UserList");

        }


        public ActionResult UserList()
        {

            var db = new ERPEntities();
            var users = db.Users.ToList();

            return View(users);



        }


    }
}
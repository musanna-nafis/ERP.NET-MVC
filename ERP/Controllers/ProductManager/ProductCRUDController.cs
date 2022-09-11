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
    public class ProductCRUDController : Controller
    {
        // GET: ProductCRUD
        [HttpGet]
        public ActionResult Create()
        {
            var db = new ERPEntities();
            var warehhouses = db.Warehouses.ToList();
            ViewBag.warehouses = warehhouses;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product pr)
        {
            var db = new ERPEntities();
            var warehhouses = db.Warehouses.ToList();
            ViewBag.warehouses = warehhouses;
            pr.product_condition = "good";
            if (ModelState.IsValid)
            {
                var w= (from wh in db.Warehouses where wh.name==pr.warehouse_name select wh).SingleOrDefault();
                if (pr.stock>w.remaining_quantity)
                {
                    TempData["msge"] = "Warehouse does not has enough capacity!";
                    return View(pr);
                }
                w.remaining_quantity -= pr.stock;
                db.Products.Add(pr);
                db.SaveChanges();
                TempData["msgs"] = "Product successfully added!";
                return RedirectToAction("Create");
            }
            else
            {
                return View(pr);
            }
        }
        public ActionResult List()
        {
            var db = new ERPEntities();
            var products = (from pr in db.Products where pr.product_condition.Equals("good") select pr).ToArray();
            if (Request["searchProduct"] != null)
            {
                string productName = Request["searchProduct"];
                products = (from pr in db.Products where pr.product_condition.Equals("good") && pr.product_name.Contains(productName) select pr).ToArray();
            }
            ViewBag.products = products;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ERPEntities();
            var product = (from pr in db.Products where pr.id == id select pr).SingleOrDefault();
            var warehhouses = db.Warehouses.ToList();
            ViewBag.warehouses = warehhouses;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product pro)
        {

            if (ModelState.IsValid)
            {
                var db = new ERPEntities();
                var product = (from pr in db.Products where pr.id == pro.id select pr).SingleOrDefault();
                var w = (from wh in db.Warehouses where wh.name == product.warehouse_name select wh).SingleOrDefault();
                if (pro.stock > w.remaining_quantity+product.stock)
                {
                    TempData["msge"] = "Warehouse does not has enough capacity!";
                    return View(pro);
                }
                w.remaining_quantity = product.stock - pro.stock;
                product.product_name = pro.product_name;
                product.product_description = pro.product_description;
                product.product_condition = pro.product_condition;
                product.nature = pro.nature;
                product.selling_price = pro.selling_price;
                product.status_purchase = pro.status_purchase;
                product.status_sale = pro.status_sale;
                product.stock = pro.stock;
                product.tax = pro.tax;
                product.weight = pro.weight;
                product.weight_unit = pro.weight_unit;
                product.dimention = pro.dimention;
                product.dimention_unit = pro.dimention_unit;
                db.SaveChanges();
                TempData["msgs"] = "Product successfully edited!";
                return RedirectToAction("List");
            }
            else
            {

                return View(pro);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new ERPEntities();
            var product = (from pr in db.Products where pr.id == id select pr).SingleOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(Product pr)
        {
            var db = new ERPEntities();
            int id = pr.id;
            var product = (from p in db.Products where p.id == id select p).SingleOrDefault();
            var w = (from wh in db.Warehouses where wh.name == product.warehouse_name select wh).SingleOrDefault();
            w.remaining_quantity += product.stock;
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Faulty()
        {
            var db = new ERPEntities();
            var products = (from pr in db.Products where pr.product_condition.Equals("faulty") select pr).ToArray();
            if (Request["searchProduct"] != null)
            {
                string productName = Request["searchProduct"];
                products = (from pr in db.Products where pr.product_condition.Equals("faulty") && pr.product_name.Contains(productName) select pr).ToArray();
            }
            ViewBag.products = products;
            return View();
        }
    }
}
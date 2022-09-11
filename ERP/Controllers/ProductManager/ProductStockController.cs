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
    public class ProductStockController : Controller
    {
        // GET: ProductStock
        [HttpGet]
        public ActionResult Index()
        {
            var db = new ERPEntities();
            var products = (from pr in db.Products where pr.product_condition.Equals("good") select pr).ToArray();
            ViewBag.products = products;
            return View();
        }

        [HttpPost]
        public JsonResult Search(string product_name)
        {
            var db = new ERPEntities();
            var products = (from pr in db.Products where pr.product_name.Contains(product_name) select pr);
            return Json(products.ToList());
        }
    }
}
using ERP.Auth;
using ERP.Form_Models;
using ERP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.ProductManager
{
    [ProductManagerLogged]
    public class ProductVisualizationController : Controller
    {
        // GET: ProductVisualization
        [HttpGet]
        public ActionResult PieChart()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Result()
        {
            var db= new ERPEntities();
            List<object> products = new List<object>();
            products.Add(new object[]
                        {
                            "Product Name", "Product Stock"
                        });
            var allProducts = db.Products.ToArray();
            foreach(var product in allProducts)
            {
                products.Add(new object[]
                        {
                            product.product_name, product.stock
                        });
            }
            return Json(products);
        }
    }
}
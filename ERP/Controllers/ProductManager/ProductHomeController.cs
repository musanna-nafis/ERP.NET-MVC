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
    public class ProductHomeController : Controller
    {
        // GET: ProductHome
        public ActionResult Home()
        {
            var db = new ERPEntities();
            var products = db.Products.ToList();
            var max_stocked_product = "";
            float max_stocked = -1;
            var most_expensive_product = "";
            float most_expensive = -1;
            var good_product_count = 0;
            var faulty_product_count = 0;
            
            foreach(var product in products)
            {
                if(product.stock>max_stocked)
                {
                    max_stocked =(float) product.stock;
                    max_stocked_product = product.product_name;
                }
                if(product.selling_price>most_expensive)
                {
                    most_expensive = (float)product.selling_price;
                    most_expensive_product = product.product_name;
                }
                if(product.product_condition.Equals("good"))
                {
                    good_product_count++;
                }
                if(product.product_condition.Equals("faulty"))
                {
                    faulty_product_count++;
                }

            }
            TempData["max_stocked_product"] = max_stocked_product;
            TempData["max_stocked"] = max_stocked;
            TempData["most_expensive_product"] = most_expensive_product;
            TempData["most_expensive"] = most_expensive;
            TempData["good_product_count"] = good_product_count;
            TempData["faulty_product_count"] = faulty_product_count;
            return View();
        }
    }
}
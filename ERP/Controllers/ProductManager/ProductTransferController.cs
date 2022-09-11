using ERP.Auth;
using ERP.Form_Models;
using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.ProductManager
{
    [ProductManagerLogged]
    public class ProductTransferController : Controller
    {
        // GET: ProductTransfer
        public ERPEntities db = new ERPEntities();
        [HttpGet]
        public ActionResult Index()
        {
            var warehhouses = (from wr in db.Warehouses where wr.status.Equals("open") select wr).ToArray();
            ViewBag.warehouses = warehhouses;
            return View();
        }
        [HttpPost]
        public ActionResult Index(Transfer tr)
        {
            var warehhouses = db.Warehouses.ToList();
            ViewBag.warehouses = warehhouses;
            if (ModelState.IsValid)
            {
                var product = (from pr in db.Products where pr.id == tr.product_id select pr).SingleOrDefault();
                if(product==null)
                {
                    TempData["msg"] = "Oops! Product Not Found";
                    return View(tr);
                }
                if (tr.warehouse.Equals(product.warehouse_name))
                {
                    TempData["curr_msg"] = "Oops! You selected the current warehouse";
                    return View(tr);
                }
                var wareHouse= (from wr in db.Warehouses where tr.warehouse.Equals(wr.name) select wr).SingleOrDefault();
                if (tr.product_quantity>wareHouse.remaining_quantity)
                {
                    TempData["wmsg"] = "Oops! Warehouse doesn't have that much quantity to store";
                    return View(tr);
                }
                if(tr.product_quantity>product.stock)
                {
                    TempData["qmsg"] = "Oops! You don't have that much product";
                    return View(tr);
                }
                var oldWarehouse= (from wr in db.Warehouses where wr.name.Equals(product.warehouse_name) select wr).SingleOrDefault();
                wareHouse.remaining_quantity -= tr.product_quantity;
                oldWarehouse.remaining_quantity += tr.product_quantity;
                product.stock -= tr.product_quantity;

                var newProduct = new Product();
                newProduct.product_name = product.product_name;
                newProduct.dimention = product.dimention;
                newProduct.dimention_unit = product.dimention_unit;
                newProduct.nature = product.nature;
                newProduct.product_condition = product.product_condition;
                newProduct.product_description = product.product_description;
                newProduct.selling_price = product.selling_price;
                newProduct.status_purchase = product.status_purchase;
                newProduct.status_sale = product.status_sale;
                newProduct.stock = tr.product_quantity;
                newProduct.tax = product.tax;
                newProduct.warehouse_name = tr.warehouse;
                newProduct.weight = product.weight;
                newProduct.weight_unit = product.weight_unit;
                db.Products.Add(newProduct);
                db.SaveChanges();
                return View();
            }
            return View(tr);
        }
    }
}
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
    public class WareHouseCRUDController : Controller
    {
        // GET: WareHouseCRUD
        public ERPEntities db = new ERPEntities();
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Warehouse wr)
        {
            if(ModelState.IsValid)
            {
                wr.remaining_quantity = wr.quantity;
                db.Warehouses.Add(wr);
                db.SaveChanges();
                TempData["msgs"] = "New warehouse successfully created!";
                return View();
            }
            return View(wr);
        }

        public ActionResult List()
        {
            var warehouses = db.Warehouses.ToArray();
            if (Request["searchWarehouse"] != null)
            {
                string warehouseName = Request["searchWarehouse"];
                warehouses = (from wr in db.Warehouses where wr.name.Contains(warehouseName) select wr).ToArray();
            }
            ViewBag.warehouses = warehouses;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var warehouse = (from wr in db.Warehouses where wr.id == id select wr).SingleOrDefault();
            return View(warehouse);
        }
        [HttpPost]
        public ActionResult Edit(Warehouse ware)
        {
            if (ModelState.IsValid)
            {
                var warehouse = (from wr in db.Warehouses where wr.id == ware.id select wr).SingleOrDefault();
                float used = (float)warehouse.quantity - (float)warehouse.remaining_quantity;
                if(ware.quantity<used)
                {
                    TempData["msgq"] = "Oops! can't decrease quantity that much!";
                    return View(ware);
                }
                warehouse.name = ware.name;
                warehouse.phone = ware.phone;
                warehouse.quantity = ware.quantity;
                warehouse.remaining_quantity =(float) ware.quantity-used;
                warehouse.status = ware.status;
                warehouse.zip_code = ware.zip_code;
                warehouse.description = ware.description;
                warehouse.country = ware.country;
                warehouse.city = ware.city;
                warehouse.address = ware.address;
                db.SaveChanges();
                TempData["msgs"] = "Warehouse successfully edited!";
                return View(ware);
            }
            else
            {
                return View(ware);
            }
        }

    }
}
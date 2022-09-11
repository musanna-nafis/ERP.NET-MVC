using ERP.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers.Finance_Manager
{
    [FinanceManagerLogged]
    public class FinanceManager_InvoiceController : Controller
    {
        // GET: FinanceManagerInvoice
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CustomerInvoiceList()
        {
            return View();
        }
        public ActionResult SupplierInvoiceList()
        {
            return View();
        }
        public ActionResult NewCustomerInvoice()
        {
            return View();
        }
        public ActionResult NewSupplierInvoice()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Auth
{
    public class ProductManagerLogged : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var value = httpContext.Session["type"];
            if (value == null)
            {
                return false;
            }
            var type = value.ToString();
            if(type.Equals("Product Manager"))
            {
                return true;
            }
            return false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeZona.Models;

namespace TimeZona.Controllers
{
    public class MagazaController : Controller
    {
        // GET: Magaza
        public ActionResult Index()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.PRODUCTS.ToList();
                return View(model);

            }
        }
    }
}
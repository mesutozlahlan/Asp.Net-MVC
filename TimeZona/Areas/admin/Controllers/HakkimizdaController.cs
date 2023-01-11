using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeZona.Models;
using TimeZona.App_Code;



namespace TimeZona.Areas.admin.Controllers
{
    public class HakkimizdaController : Controller
    {
        // GET: admin/Hakkimizda

        [Authorize]
        public ActionResult Index()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.ABOUTUS.ToList();
                return View(model);
            }

        }

        public ActionResult HakkimizdaGuncelle()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.ABOUTUS.First();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Kaydet(ABOUTUS GelenVeri)
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var GuncellenecekVeri = db.ABOUTUS.ToList();
                if (!ModelState.IsValid)
                {
                    return View("HakkimizdaGuncelle", GelenVeri);
                }

                db.Entry(GuncellenecekVeri).CurrentValues.SetValues(GelenVeri);
                db.SaveChanges();

                TempData["hakkimdaGuncelle"] = " ";
                return RedirectToAction("index","hakkimizda");
            }
        }
    }
}
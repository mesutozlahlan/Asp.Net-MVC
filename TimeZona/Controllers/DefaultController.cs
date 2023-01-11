using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TimeZona.Models;


namespace TimeZona.Controllers
{
    public class DefaultController : Controller
    {

        // GET: Default
        [Route("anasayfa")]
        public ActionResult Index()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.PRODUCTS.ToList();
                return View(model);

            }
        }
        [Route("magaza")]
        public ActionResult Magaza()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.PRODUCTS.ToList();
                return View(model);

            }
        }
        [Route("hakkimizda")]
        public ActionResult Hakkimizda()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.ABOUTUS.ToList();
                return View(model);

            }
        }

        [Route("urunler")]
        public ActionResult ÜrünDetayları()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.PRODUCTSDETAILS.ToList();
                return View(model);

            }
        }

        public ActionResult Blog()
        {
            return View();
        }
        [Route("iletisim")]
        public ActionResult İletisim()
        {
            return View();
        }

        [Route("giris")]
        public ActionResult Giriş()
        {
            return View();
        }

        public ActionResult GLogin(LOGİN kullaniciFormu)
        {
            if (!ModelState.IsValid)
            {
                return View("index", kullaniciFormu);
            }
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var kullaniciVarmi = db.LOGİN.FirstOrDefault(
                    x => x.NAMESURNAME == kullaniciFormu.NAMESURNAME && x.PASSWORD == kullaniciFormu.PASSWORD);
                if (kullaniciVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullaniciVarmi.NAMESURNAME, kullaniciFormu.BeniHatirla);
                    return RedirectToAction("/index", "magaza");
                }
                ViewBag.Hata = "Kullanıcı Adı veya Şifre Hatalı";
                return View("Giriş");
            }
        }

        [Route("sepetim")]
        public ActionResult Sepetim()
        {
            return View();
        }
    }
}


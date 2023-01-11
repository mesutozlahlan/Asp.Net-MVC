using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeZona.Models;
using System.Web.Security;
using TimeZona.Controllers;

namespace TimeZona.Areas.admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        //Şifreleri karmaşıklaştırma
        //public ActionResult Index2()
        //{
        //    return Content(Sifrele.MD5Olustur("1234" ) );
        //}       



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ALogin(ADMİNLOGİN kullaniciFormu, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("index", kullaniciFormu);
            }

            string sifre = Sifrele.MD5Olustur(kullaniciFormu.PASSWORD);

            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var kullaniciVarmi = db.ADMİNLOGİN.FirstOrDefault(x => x.NAMESURNAME == kullaniciFormu.NAMESURNAME && x.PASSWORD == sifre);
                if (kullaniciVarmi != null)
                {
                    FormsAuthentication.SetAuthCookie(kullaniciVarmi.NAMESURNAME, kullaniciFormu.BeniHatirla);

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("/index", "Hakkimizda");
                    }

                }
                ViewBag.Hata = "Kullanıcı Adı veya Şifre Hatalı";
                return View("index");
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("index");
        }
    }
}
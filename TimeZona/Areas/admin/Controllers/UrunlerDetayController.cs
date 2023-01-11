using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeZona.App_Code;
using TimeZona.Models;

namespace TimeZona.Areas.admin.Controllers
{
    public class UrunlerDetayController : Controller
    {
        public ActionResult Index()
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.PRODUCTSDETAILS.ToList();
                return View(model);

            }
        }

        public ActionResult YeniUrunlerDetay()
        {
            var model = new PRODUCTSDETAILS();
            return View("UrunDetayForm", model);
        }

        public ActionResult Guncelle(int ID)
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.PRODUCTSDETAILS.Find(ID);

                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("UrunDetayForm", model);
            }
        }

        public ActionResult Kaydet(PRODUCTSDETAILS gelenUrun)
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                if (!ModelState.IsValid) //formun doğru dolduruludu mu?
                {
                    return View("UrunDetayForm", gelenUrun);
                }

                if (gelenUrun.ID == 0)//Yeni ürün kaydı
                {
                    if (gelenUrun.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim Yükleyin";
                        return View("UrunDetayForm", gelenUrun);
                    }
                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);

                    gelenUrun.PHOTO = fotoAdi;
                    db.PRODUCTSDETAILS.Add(gelenUrun);
                    gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    TempData["urunEkle"] = "ekle";

                }
                else  //Güncelleme 
                {
                    var GuncellenecekVeri = db.PRODUCTSDETAILS.Find(gelenUrun.ID);
                    if (!ModelState.IsValid)
                    {
                        return View("UrunDetayForm", gelenUrun);
                    }

                    if (gelenUrun.fotoFile != null)
                    {
                        string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);

                        gelenUrun.PHOTO = fotoAdi;
                        //kaydetme
                        gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    }

                    //güncelleme
                    db.Entry(GuncellenecekVeri).CurrentValues.SetValues(gelenUrun);
                    TempData["urun"] = "güncelleme";
                }

                db.SaveChanges();
                return RedirectToAction("/index");
            }
        }

        public ActionResult Sil(int id)
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {

                var silincekVeri = db.PRODUCTSDETAILS.Find(id);

                if (silincekVeri == null)
                {
                    return HttpNotFound();
                }

                db.PRODUCTSDETAILS.Remove(silincekVeri);
                db.SaveChanges();


                TempData["urun"] = "silindi";
                return RedirectToAction("/index", "Urunler");
            }

        }
    }
}
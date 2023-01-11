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
    [Authorize]
    public class UrunlerController : Controller
    {
        // GET: admin/Urunler
        
        public ActionResult Index()
        {
            using(TimeZonaEntities db= new TimeZonaEntities() )
            {
                var model = db.PRODUCTS.ToList();
                return View(model);

            }  
        }
        public ActionResult YeniUrunler()
        {
            var model = new PRODUCTS();
            return View("UrunForm", model);
        }

        public ActionResult Guncelle(int ID)
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                var model = db.PRODUCTS.Find(ID);

                if (model == null)
                {
                    return HttpNotFound();
                }
                return View("UrunForm", model);
            }
        }

        public ActionResult Kaydet(PRODUCTSDETAILS gelenUrun)
        {
            using (TimeZonaEntities db = new TimeZonaEntities())
            {
                if (!ModelState.IsValid) //formun doğru dolduruludu mu?
                {
                    return View("UrunForm", gelenUrun);
                }

                if (gelenUrun.ID == 0)//Yeni ürün kaydı
                {
                    if (gelenUrun.fotoFile == null)
                    {
                        ViewBag.HataFoto = "Lütfen Resim Yükleyin";
                        return View("UrunForm", gelenUrun);
                    }
                    string fotoAdi = Seo.DosyaAdiDuzenle(gelenUrun.fotoFile.FileName);

                    gelenUrun.PHOTO = fotoAdi;
                    db.PRODUCTSDETAILS.Add(gelenUrun);
                    gelenUrun.fotoFile.SaveAs(Path.Combine(Server.MapPath("~/Content/img"), Path.GetFileName(fotoAdi)));
                    TempData["urun"] = "ekle";

                }
                else  //Güncelleme 
                {
                    var GuncellenecekVeri = db.PRODUCTSDETAILS.Find(gelenUrun.ID);
                    if (!ModelState.IsValid)
                    {
                        return View("UrunForm", gelenUrun);
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

                var silincekVeri = db.PRODUCTS.Find(id);

                if (silincekVeri == null)
                {
                    return HttpNotFound();
                }

                db.PRODUCTS.Remove(silincekVeri);
                db.SaveChanges();


                TempData["urun"] = "silindi";
                return RedirectToAction("/index", "UrunlerDetay");
            }

        }
    }


}
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game_Wave.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminIstatisticController : Controller
    {
        // GET: AdminIstatistic
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var satis = db.Sales.Count();
            ViewBag.satis = satis;

            var urun = db.Products.Count();
            ViewBag.urun = urun;

            var kategori = db.Products.Count();
            ViewBag.kategori = kategori;

            var sepet = db.Products.Count();
            ViewBag.sepet = sepet;

            var kullanici = db.Products.Count();
            ViewBag.kullanici = kullanici;

            return View();
        }
    }
}
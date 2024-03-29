﻿using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game_Wave.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart

        DataContext db = new DataContext();
        public ActionResult Index(decimal? Tutar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var username = User.Identity.Name;

                var kullanici = db.Users.FirstOrDefault(x => x.Email == username);

                var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();

                var kid = db.Carts.FirstOrDefault(x => x.UserId == kullanici.Id);

                if (model != null) 
                {
                    if (kid == null)
                    {
                        ViewBag.Tutar = "Sepetinizde ürün bulunamadı."; 
                    }
                    else if (kid != null)
                    {
                        Tutar = db.Carts.Where(x => x.UserId == kid.UserId).Sum(x => x.Product.Price * x.Quantity);

                        ViewBag.Tutar = "Toplam Tutar :" + Tutar + "$";
                    }
                    return View(model);
                }
            }

            return HttpNotFound();
        }

        public ActionResult AddCart(int id) 
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciAdi = User.Identity.Name;

                var model = db.Users.FirstOrDefault(x => x.Email == kullaniciAdi);

                var urun = db.Products.Find(id);

                var sepet = db.Carts.FirstOrDefault(x => x.UserId == model.Id && x.ProductId == id);

                if (model != null) 
                {
                    if (sepet != null) 
                    {
                        sepet.Quantity++;

                        sepet.Price = urun.Price * sepet.Quantity;

                        db.SaveChanges();

                        return RedirectToAction("Index","Cart");
                    }

                    var s = new Cart
                    {
                        UserId = model.Id,
                        ProductId = urun.Id,
                        Quantity = 1,
                        Image = urun.Image,
                        Price = urun.Price,
                        Date = DateTime.Now
                    };

                    db.Carts.Add(s);

                    db.SaveChanges();

                    return RedirectToAction("Index","Cart");
                }
            }

            return View();
        }

        public void DinamicQuantity(int id, int count) 
        {
            var model = db.Carts.Find(id);

            model.Quantity = count;

            model.Price = model.Price * model.Quantity;

            db.SaveChanges();
        }

        public ActionResult Decrease(int id) 
        {
            var model = db.Carts.Find(id);

            if (model.Quantity == 1)
            {
                db.Carts.Remove(model);

                db.SaveChanges();
            }

            model.Quantity--;

            model.Price = model.Price * model.Quantity;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Increase(int id)
        {
            var model = db.Carts.Find(id);

            model.Quantity++;

            model.Price = model.Price * model.Quantity;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult TotalCount(int? count) 
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = db.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

                count = db.Carts.Where(x => x.UserId == model.Id).Count();

                ViewBag.count = count;

                if (count == 0)
                {
                    ViewBag.count = "";
                }
            }

            return PartialView();
        }

        public ActionResult Delete(int id)
        {
            var sil = db.Carts.Find(id);

            db.Carts.Remove(sil);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteRange()
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullanici = User.Identity.Name;

                var model = db.Users.FirstOrDefault(x => x.Email == kullanici);

                var sil = db.Carts.Where(x => x.UserId == model.Id);

                db.Carts.RemoveRange(sil);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}
using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace Game_Wave.Controllers
{
    public class AdminProductController : Controller
    {
        // GET: AdminProduct
        ProductRepository productRepository = new ProductRepository();

        DataContext db = new DataContext();

        public ActionResult Index(int sayfa = 1)
        {
            return View(productRepository.List().ToPagedList(sayfa,6));
        }

        public ActionResult Create()
        {
            List<SelectListItem> deger1 = (from i in db.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Product data, HttpPostedFileBase File) 
        {
            if (!ModelState.IsValid)
            {
                string path = Path.Combine("~/Content/images/" + File.FileName);

                File.SaveAs(Server.MapPath(path));

                data.Image = File.FileName.ToString();

                productRepository.Insert(data);

                return RedirectToAction("Index");
            }

            return View(data);
        }

        public ActionResult Delete(int id) 
        {
            var delete = productRepository.GetById(id);

            productRepository.Delete(delete);

            return RedirectToAction("Index");
        }

        public ActionResult Update(int id) 
        {
            List<SelectListItem> deger1 = (from i in db.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()
                                           }).ToList();
            ViewBag.ktgr = deger1;

            var update = productRepository.GetById(id);

            return View(update);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(Product data, HttpPostedFileBase File) 
        {
            var update = productRepository.GetById(data.Id);

            if (!ModelState.IsValid) 
            {
                if (File == null)
                {
                    string path = Path.Combine("~/Content/images/" + File.FileName);

                    File.SaveAs(Server.MapPath(path));

                    update.Description = data.Description;

                    update.Name = data.Name;

                    update.Price = data.Price;

                    update.IsApproved = data.IsApproved;

                    update.Popular = data.Popular;

                    update.Stock = data.Stock;

                    update.Image = File.FileName.ToString();

                    update.CategoryId = data.CategoryId;

                    productRepository.Update(data);

                    return RedirectToAction("Index");
                }
                else
                {
                    string path = Path.Combine("~/Content/images/" + File.FileName);

                    File.SaveAs(Server.MapPath(path));

                    update.Description = data.Description;

                    update.Name = data.Name;

                    update.Price = data.Price;

                    update.IsApproved = data.IsApproved;

                    update.Popular = data.Popular;

                    update.Stock = data.Stock;

                    update.Image = File.FileName.ToString();

                    update.CategoryId = data.CategoryId;

                    productRepository.Update(data);

                    return RedirectToAction("Index");
                }
            }

            return View(update);
        }
    }
}
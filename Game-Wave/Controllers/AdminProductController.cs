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
            List<SelectListItem> categories = (from i in db.Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.Id.ToString()
                                               }).ToList();
            ViewBag.ktgr = categories;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Product data, HttpPostedFileBase File)
        {
            if (File != null && File.ContentLength > 0)
            {
                string fileName = Path.GetFileName(File.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);

                File.SaveAs(path);

                data.Image = fileName;

                productRepository.Insert(data);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Lütfen bir dosya seçin.");
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
                    update.Description = data.Description;

                    update.Name = data.Name;

                    update.Price = data.Price;

                    update.Popular = data.Popular;

                    update.Stock = data.Stock;

                    update.CategoryId = data.CategoryId;

                    productRepository.Update(data);

                    return RedirectToAction("Index");
                }
                else
                {
                    string path = Path.Combine("~/Content/images/", File.FileName);

                    File.SaveAs(Server.MapPath(path));

                    update.Description = data.Description;

                    update.Name = data.Name;

                    update.Price = data.Price;

                    update.Popular = data.Popular;

                    update.Stock = data.Stock;

                    update.Image = File.FileName.ToString();

                    update.youtubeLink = data.youtubeLink;

                    update.CategoryId = data.CategoryId;

                    productRepository.Update(update);

                    return RedirectToAction("Index");
                }    
            }
            return View(update);
        }
    }
}
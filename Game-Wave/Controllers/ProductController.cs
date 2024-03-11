using BusinessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game_Wave.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        ProductRepository productRepository = new ProductRepository();

        public PartialViewResult PopularProduct()
        {
            var product = productRepository.GetPopularProduct();

            ViewBag.popular = product;

            return PartialView();
        }

        public ActionResult ProductDetails(int id) 
        {
            var details = productRepository.GetById(id);

            return View(details);
        }
    }
}
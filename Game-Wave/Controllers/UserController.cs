using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game_Wave.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var username = (string)Session["Mail"];

            var degerler = db.Users.FirstOrDefault(x => x.Email == username);

            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index(User data) 
        {
            var username = (string)Session["Mail"];
            var user = db.Users.Where(x => x.Email == username).FirstOrDefault();

            user.Name = data.Name;
            user.Surname = data.Surname;
            user.Password = data.Password;
            user.UserName = data.UserName;

            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
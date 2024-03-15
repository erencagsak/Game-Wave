using DataAccessLayer.Context;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        public ActionResult SifreYenile() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult SifreYenile(string eposta)
        {
            var mail = db.Users.Where(x => x.Email == eposta).SingleOrDefault();

            if (mail != null)
            {
                Random random = new Random();
                int new_password = random.Next();
                User password = new User();
                mail.Password = Convert.ToString(new_password);
                db.SaveChanges();
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "-";
                WebMail.Password = "-";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta,"Giriş şifrenizi","Şifreniz" + new_password);
                ViewBag.uyari = "Şifreniz başarıyla gönderilmiştir.";
            }
            else
            {
                ViewBag.uyari = "Bir hata oluştu lütfen daha sonra tekrar deneyiniz.";
            }
            return View();
        }
    }
}
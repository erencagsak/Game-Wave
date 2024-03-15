using DataAccessLayer.Context;
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
            return View();
        }
    }
}
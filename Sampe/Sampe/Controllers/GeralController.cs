using Sampe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sampe.Controllers
{
    public class GeralController : System.Web.Mvc.Controller
    {
        private SampeContext db = new SampeContext();
        public ActionResult Menu()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
       /* [ChildActionOnly]
        public ActionResult Menu()
        {
            var menu = db.Usuarios.ToList();
            return PartialView(menu);
        }*/
    }
}
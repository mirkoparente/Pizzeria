using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Security;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
            ModelDbContext context = new ModelDbContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Pizze()
        {

            List<Articoli> a = context.Articoli.ToList();
            return View(a);
        }


        public ActionResult DettaglioPizza(int id)
        {
            Articoli u = context.Articoli.Find(id);
            return View(u);
        }


    }

    
}

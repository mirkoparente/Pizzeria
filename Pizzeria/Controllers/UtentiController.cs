using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pizzeria.Controllers
{
    public class UtentiController : Controller
    {
        ModelDbContext context = new ModelDbContext();

        // GET: Utenti
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Utenti u)
        {
            var user = context.Utenti.Where(m => m.Username == u.Username).FirstOrDefault();
            if (user.Username == u.Username)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                Session["id"] = user.IdUtenti;
            }
            else
            {
                return View(u);
            }
            return RedirectToAction("Index", "Home");
            
        }


        public ActionResult Registrati()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrati(Utenti u)
        {
            if (ModelState.IsValid)
            {

                u.Ruolo = "User";
                var user = context.Utenti.Add(u);
                context.SaveChanges();
                return RedirectToAction("index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie(".ASPXAUTH");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ListaClienti()
        {
            List<Utenti> utenti = context.Utenti.Where(m => m.Ruolo != "Admin").ToList();
            return View(utenti);
        }


        public ActionResult DettaglioClienti(int id)
        {
            Utenti u = context.Utenti.Find(id);
            return View(u);
        }


        public ActionResult ModificaCliente(int id)
        {
            Utenti u = context.Utenti.Find(id);
            return View(u);
        }

        [HttpPost]
        public ActionResult ModificaCliente(Utenti u)
        {
            if (ModelState.IsValid)
            {
                context.Entry(u).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ListaClienti");

            }
            return View(u);
        }



        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Utenti u = context.Utenti.Find(id);
            context.Utenti.Remove(u);
            context.SaveChanges();

            return RedirectToAction("ListaClienti");
        }

    }
}
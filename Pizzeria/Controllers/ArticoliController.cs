using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ArticoliController : Controller
    {
        ModelDbContext context = new ModelDbContext();

        // GET: Articoli
        public ActionResult ListaArticoli()
        {
            List<Articoli>a=context.Articoli.ToList();
            return View(a);
        }

        public ActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Add(Articoli u,HttpPostedFileBase Foto)
        {


            string source = Path.Combine(Server.MapPath("~/Content/img"), Foto.FileName);

            if (ModelState.IsValid)
            {
            if (Foto != null)
            {
                Foto.SaveAs(source);
                u.Foto=Foto.FileName;
            }

                context.Articoli.Add(u);
                context.SaveChanges();
                return RedirectToAction("ListaArticoli");
            }
            return View();
        }
        public ActionResult DettaglioArticoli(int id)
        {
            Articoli u = context.Articoli.Find(id);
            return View(u);
        }



        public ActionResult ModificaArticolo(int id)
        {
            Articoli u = context.Articoli.Find(id);
            return View(u);
        }

        [HttpPost]
        public ActionResult ModificaArticolo(Articoli u, HttpPostedFileBase Foto)
        {
            string source = Path.Combine(Server.MapPath("~/Content/img"), Foto.FileName);

            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    Foto.SaveAs(source);
                    u.Foto = Foto.FileName;
                }
                context.Entry(u).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ListaArticoli");

            }
            return View(u);
        }
        public ActionResult Delete(int id)
        {
            Articoli u = context.Articoli.Find(id);
            context.Articoli.Remove(u);
            context.SaveChanges();


            return RedirectToAction("ListaClienti");
        }


    }
}
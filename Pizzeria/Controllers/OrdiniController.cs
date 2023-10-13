using Microsoft.Ajax.Utilities;
using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrdiniController : Controller
    {
        ModelDbContext context = new ModelDbContext();


        public List<SelectListItem> Stato
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                SelectListItem item = new SelectListItem { Text = "In Preparazione", Value = "In Preparazione" };
                SelectListItem item1 = new SelectListItem { Text = "In Consegna", Value = "In Consegna" };
                list.Add(item);
                list.Add(item1);

                return list;
            }
        }


        // GET: Ordini
        public ActionResult ListaOrdini()
        {

            return View(context.Ordini.ToList());
        }
        public ActionResult ModificaOrdine(int id)
        {
            ViewBag.stato = Stato;
            Ordini u = context.Ordini.Find(id);
            return View(u);
        }

        [HttpPost]
        public ActionResult ModificaOrdine(Ordini u,string stato)
        {
            if (ModelState.IsValid)
            {
                u.Stato= stato;
                context.Entry(u).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ListaOrdini","Ordini");

            }
            return View(u);
        }



        public JsonResult OrdiniEvasi()
        {
            List<Ordini> ordini = context.Ordini.Where(o => o.Stato == "In Consegna").ToList();

            List<OrdiniToJson> ordiniTo = new List<OrdiniToJson>();
            foreach (Ordini o in ordini)
            {

                OrdiniToJson ordin = new OrdiniToJson();

                ordin.Stato = o.Stato;
                ordin.IdOrdine = o.IdOrdini;
                ordiniTo.Add(ordin);
                
            };
            return Json(ordiniTo,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IncassoGiornaliero(DateTime data)
        {
            List <Ordini> ordini= context.Ordini.Where(o=>o.Stato=="In Consegna"&& o.DataOrdine==data).ToList();
            List<OrdiniToJson> ordiniTo = new List<OrdiniToJson>();
                decimal? or = context.DettagliOrdine.Sum(t => t.Quantita * t.Articoli.Prezzo);
            foreach (Ordini o in ordini)
            {
                OrdiniToJson ordin = new OrdiniToJson();
                ordin.IdOrdine= o.IdOrdini;
                ordin.DataOrdine=o.DataOrdine;
                ordin.Prezzo=or;
                ordiniTo.Add(ordin);
            }

            return Json(ordiniTo);


        }

    }
}
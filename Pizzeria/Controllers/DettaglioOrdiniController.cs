using Pizzeria.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pizzeria.Controllers
{
    public class DettaglioOrdiniController : Controller
    {
        ModelDbContext context = new ModelDbContext();

        // GET: DettaglioOrdini
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddToCart(int IdArticoli, int quantita, string note)
        {

            if (User.Identity.IsAuthenticated)
            {
                if (Session["id"] == null)
                {
                    HttpCookie cookie = new HttpCookie(".ASPXAUTH");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Login", "Utenti");

                }
                else
                {
                    int userId = Convert.ToInt32(Session["id"].ToString());


                    Ordini ordine = context.Ordini.FirstOrDefault(o => o.IdUtenti == userId && o.Stato == "In Attesa");

                    DettagliOrdine dett = new DettagliOrdine();
                    Ordini nuovoOrdine = new Ordini();


                    if (ordine == null)
                    {


                        nuovoOrdine.DataOrdine = DateTime.Now;
                        nuovoOrdine.Stato = "In Attesa";
                        nuovoOrdine.IdUtenti = userId;
                        if (nuovoOrdine.Note == null)
                        {
                            nuovoOrdine.Note = "Nessuna Nota";
                        }
                        else
                        {

                            nuovoOrdine.Note = note;
                        }
                        context.Ordini.Add(nuovoOrdine);



                        dett.IdOrdini = nuovoOrdine.IdOrdini;
                        dett.IdArticoli = IdArticoli;
                        dett.Quantita = quantita;


                        context.DettagliOrdine.Add(dett);
                    }
                    else
                    {


                        DettagliOrdine cart = context.DettagliOrdine.FirstOrDefault(item => item.IdOrdini == ordine.IdOrdini && item.IdArticoli == IdArticoli);


                        if (cart == null)
                        {

                            dett.IdOrdini = ordine.IdOrdini;
                            dett.IdArticoli = IdArticoli;
                            dett.Quantita = quantita;



                            context.DettagliOrdine.Add(dett);
                        }
                        else
                        {

                            cart.Quantita += quantita;
                        }
                    }





                    context.SaveChanges();


                    return RedirectToAction("Pizze", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Utenti");
            }
        }




        public ActionResult ViewCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["id"] == null)
                {
                    HttpCookie cookie = new HttpCookie(".ASPXAUTH");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Login", "Utenti");

                }
                else
                {
                int userId = Convert.ToInt32(Session["id"].ToString());
                List<Ordini> or = context.Ordini.Where(o => o.IdUtenti == userId).ToList();

                return View(or);
                }
            }
            else
            {
                return RedirectToAction("Login", "Utenti");
            }

        }
        public ActionResult ConfermaOrdine()
        {
            int userId = Convert.ToInt32(Session["id"].ToString());
            Ordini ordine = context.Ordini.FirstOrDefault(o => o.IdUtenti == userId && o.Stato == "In Attesa");

            if (ordine != null)
            {
                ordine.Stato = "In Preparazione";

                List<DettagliOrdine> checkout = context.DettagliOrdine.Where(d => d.IdOrdini == ordine.IdOrdini).ToList();

                context.SaveChanges();
                return RedirectToAction("ViewCart");
            }
            return RedirectToAction("ViewCart");

        }

        public ActionResult RimuoviUno(int id)
        {
            int userId = Convert.ToInt32(Session["id"].ToString());
            DettagliOrdine oneOrdine = context.DettagliOrdine.FirstOrDefault(d => d.IdArticoli == id);
            if (oneOrdine != null)
            {
                context.DettagliOrdine.Remove(oneOrdine);
                context.SaveChanges();
            }

            return RedirectToAction("ViewCart");

        }

        public ActionResult RimuoviAll(int id)
        {
            int userId = Convert.ToInt32(Session["id"].ToString());
            List<Ordini> ordine = context.Ordini.Where(d => d.IdOrdini == id).ToList();
            List<DettagliOrdine>oneOrdine = context.DettagliOrdine.Where(d => d.IdOrdini == id).ToList();
            if (oneOrdine != null)
            {
                context.DettagliOrdine.RemoveRange(oneOrdine);
                context.SaveChanges();
            }
            if (ordine != null)
            {
                context.Ordini.RemoveRange(ordine);
                context.SaveChanges();
                return RedirectToAction("ViewCart");
            }
            return RedirectToAction("ViewCart");

        }
    }
}

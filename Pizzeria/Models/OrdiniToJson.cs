using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pizzeria.Models
{
    public class OrdiniToJson
    {

        public int IdOrdine {  get; set; }

        public string Stato { get; set; }

        public string Indirizzo { get; set; }

        public DateTime? DataOrdine { get; set; }
        public int? Quantita { get; set; }

        public decimal? Prezzo { get; set; }



    }
}
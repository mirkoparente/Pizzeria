namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliOrdine")]
    public partial class DettagliOrdine
    {
        [Key]
        public int IdDettagli { get; set; }

        public int IdOrdini { get; set; }

        public int IdArticoli { get; set; }

        public int? Quantita { get; set; }

        public virtual Articoli Articoli { get; set; }

        public virtual Ordini Ordini { get; set; }
    }
}

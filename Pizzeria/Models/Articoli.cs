namespace Pizzeria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Articoli")]
    public partial class Articoli
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Articoli()
        {
            DettagliOrdine = new HashSet<DettagliOrdine>();
        }

        [Key]
        public int IdArticoli { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        public string Foto { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal? Prezzo { get; set; }


        [Display(Name ="Tempo Preparazione")]
        public int? TempoConsegna { get; set; }

        public string Ingredienti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliOrdine> DettagliOrdine { get; set; }
    }
}

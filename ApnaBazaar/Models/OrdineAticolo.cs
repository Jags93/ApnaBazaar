using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApnaBazaar.Models
{
    public class OrdineAticolo
    {
        [Key]
        public int IdOa { get; set; }
        [ForeignKey("Ordine")]
        public int IdO { get; set; }
        public Ordine Ordine { get; set; }
        [ForeignKey("Articolo")]
        public int IdA { get; set; }
        public Articolo Articolo { get; set; }
        public int Quantita { get; set; }

      

    }
}
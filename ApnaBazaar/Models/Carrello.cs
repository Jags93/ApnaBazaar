using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApnaBazaar.Models
{
    public class Carrello
    {
        //creiamo il modello per il carello in base al modello dell'articolo

        [Key]
        public int IdC { get; set; }

        [ForeignKey("AccountUser")]
        public string UserId { get; set; }

        public List<ItemCarrello> Items { get; set; }

        public decimal? TotPrezzo { get; set; }

        public int? TotQuantita { get; set; }

        







    }

    public class ItemCarrello
    {
        [Key]
        public int IdI { get; set; }
        [ForeignKey("Articolo")]
        public int IdA { get; set; }
        public Articolo Articolo { get; set; }
        
        public int Quantita { get; set; }

        

    }
}

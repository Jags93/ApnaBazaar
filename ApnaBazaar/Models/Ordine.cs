using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApnaBazaar.Models
{
    public class Ordine
    {
        [Key]
        public int IdO { get; set; }
        [ForeignKey("Articoli")]
        public int IdA { get; set; }
        [ForeignKey("AccountUser")]
        public string UserId{ get; set; }



        public int Quantita { get; set; }

        public decimal Prezzo { get; set; }

        public string Stato { get; set; }

        public string? Note { get; set; }

        public DateTime Data { get; set; }
        [NotMapped]
        public decimal Totale { get; set; }






    }

    
}

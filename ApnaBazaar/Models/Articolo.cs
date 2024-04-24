using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ApnaBazaar.Models
{
    public class Articolo
    {
        [Key]
        public int IdA { get; set; }

        [Required(ErrorMessage = "Inserisci il nome dell'articolo")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Inserisci la descrizione dell'articolo")]
        public string Descrizione { get; set; }
        [Required(ErrorMessage = "Inserisci il prezzo dell'articolo")]
        public decimal Prezzo { get; set; }
        [Required(ErrorMessage = "Inserisci la quantità dell'articolo")]
        public int Quantita { get; set; }

        public string? Img1 { get; set; }
        public string? Img2 { get; set; }
        public string? Img3 { get; set; }

        [NotMapped]
        public IFormFile? ImmagineUpload1 { get; set; }
        [NotMapped]
        public IFormFile? ImmagineUpload2 { get; set; }
        [NotMapped]
        public IFormFile? ImmagineUpload3 { get; set; }
        
    }
}

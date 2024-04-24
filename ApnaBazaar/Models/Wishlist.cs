using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApnaBazaar.Models
{
    public class Wishlist
    {
        [Key]
        public int IdW { get; set; }
        [ForeignKey("AccountUser")]
        public string UserId { get; set; }
        public List<WishlistItem> Items { get; set; }
    }

    public class WishlistItem
    {
        [Key]
        public int IdWi { get; set; }
        [ForeignKey("Articoli")]
        public int IdA { get; set; }
        public Articolo Articolo { get; set; }
    }

}

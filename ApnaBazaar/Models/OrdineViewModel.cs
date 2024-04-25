namespace ApnaBazaar.Models
{
    public class OrdineViewModel
    {
        public Ordine Ordine { get; set; }
        public List<ItemCarrello> Items { get; set; }
        public decimal Prezzo { get; internal set; }
        public int Quantita { get; internal set; }

        public int IdA { get; internal set; }

        
    }
}

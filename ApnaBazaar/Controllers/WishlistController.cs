using ApnaBazaar.Areas.Identity.Data;
using ApnaBazaar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApnaBazaar.Controllers
{
    public class WishlistController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<WishlistController> _logger;

        public WishlistController(AppDbContext context, ILogger<WishlistController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = _context.Wishlists.Include(w => w.Items).ThenInclude(i => i.Articolo).FirstOrDefault(w => w.UserId == userId);
            return View(wishlist);
        }

        public IActionResult Add(int IdA) // IdA è l'Id dell'articolo
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // userId è l'Id dell'utente
            var wishlist = _context.Wishlists.Include(w => w.Items).ThenInclude(i => i.Articolo).FirstOrDefault(w => w.UserId == userId); // wishlist è la lista dei desideri dell'utente
            var articolo = _context.Articoli.Find(IdA); // articolo è l'articolo che l'utente vuole aggiungere alla lista dei desideri
            if (articolo == null) // se l'articolo non esiste
            {
                return NotFound(); // ritorna errore 404
            }

            if (wishlist == null) // se la lista dei desideri non esiste
            {
                wishlist = new Wishlist { UserId = userId }; // crea una nuova lista dei desideri
                _context.Wishlists.Add(wishlist); // aggiungi la lista dei desideri al database
                _context.SaveChanges(); // salva le modifiche
            }
            if (!wishlist.Items.Any(i => i.IdA == IdA)) // se l'articolo non è già presente nella lista dei desideri
            {

                var wishlistItem = new WishlistItem { IdA = IdA }; // crea un nuovo oggetto WishlistItem
                wishlist.Items.Add(wishlistItem); // aggiungi l'oggetto alla lista dei desideri
                _context.SaveChanges(); // salva le modifiche
                TempData["Success"] = "Articolo aggiunto alla lista dei desideri"; // mostra un messaggio di conferma
            }
            return RedirectToAction("Index"); // reindirizza alla pagina Index

        }


        public IActionResult Remove(int IdA)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = _context.Wishlists.Include(w => w.Items).FirstOrDefault(w => w.UserId == userId);
            var item = wishlist.Items.FirstOrDefault(i => i.IdA == IdA);
            if (item != null)
            {
                wishlist.Items.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        
    }
}

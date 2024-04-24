using ApnaBazaar.Areas.Identity.Data;
using ApnaBazaar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace ApnaBazaar.Controllers
{
    public class CarrelloController : Controller
    {
        // Dichiarazione delle variabili private
        private readonly AppDbContext _context;
        private readonly ILogger<CarrelloController> _logger;
        private readonly IConfiguration _configuration;
        private List<ItemCarrello> _items;

        // Costruttore del controller
        public CarrelloController(AppDbContext context, ILogger<CarrelloController> logger, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _context = context; // Inizializzazione del contesto del database
            _logger = logger; // Inizializzazione del logger
            _items = LoadCartItems(httpContextAccessor.HttpContext.Session); // Caricamento degli articoli nel carrello
            _configuration = configuration; // Inizializzazione della configurazione
        }

        // Metodo per caricare gli articoli nel carrello dalla sessione
        private List<ItemCarrello> LoadCartItems(ISession session)
        {
            if(session == null) // se la sessione è nulla
            {
                return new List<ItemCarrello>(); // restituisco una lista vuota
            }
            return session.Get<List<ItemCarrello>>("Cart") ?? new List<ItemCarrello>();
        }

        // Metodo per salvare gli articoli nel carrello nella sessione
        private void SaveCartItems(ISession session)
        {
            session.Set("Cart", _items);
        } 

        // Metodo per visualizzare il carrello
        public IActionResult Index()
        {
            return View(_items);
        }


       

        // Metodo per aggiungere un articolo al carrello
       public IActionResult AddToCart (int id)
        {
            var articolo = _context.Articoli.Find(id); // cerco l'articolo nel database
            if(articolo == null) // se l'articolo non esiste
            {
                TempData["Message"] = "Articolo non trovato!"; // messaggio di errore
                return RedirectToAction("Index", "Articoli"); // reindirizzo alla pagina degli articoli
            }
            if(articolo.Quantita < 1) // se la quantità dell'articolo è minore di 1
            {
                TempData["Message"] = "Quantità non disponibile!"; // messaggio di errore
                return RedirectToAction("Index", "Articoli"); // reindirizzo alla pagina degli articoli
            }
            var item = _items.Find(i => i.Articolo.IdA == id); // cerco l'articolo nel carrello
            if(item == null) // se l'articolo non è presente nel carrello 
            {
                _items.Add(new ItemCarrello 
                {
                    Articolo = articolo,
                    Quantita = 1
                }); // aggiungo l'articolo al carrello
            }
            else // se l'articolo è presente nel carrello
            {
                item.Quantita ++; // incremento la quantità dell'articolo
            }

            articolo.Quantita --; // decremento la quantità dell'articolo
            _context.Update(articolo); // aggiorno l'articolo nel database
            _context.SaveChanges(); // salvo le modifiche nel database
            SaveCartItems(HttpContext.Session); // salvo gli articoli nel carrello nella sessione
            TempData["Success"] = "Articolo aggiunto al carrello!"; // messaggio di successo
            return RedirectToAction("Index", "Articoli"); // reindirizzo alla pagina degli articoli
        }

        
        // Metodo per rimuovere un articolo dal carrello
        public IActionResult RemoveFromCart(int id)
        {
            var articolo = _context.Articoli.Find(id); // cerco l'articolo nel database
            if (articolo != null) // se l'articolo esiste
            {
                var item = _items.FirstOrDefault(i => i.Articolo.IdA == id); // cerco l'articolo nel carrello
                if (item != null) // se l'articolo è presente nel carrello 
                {
                    if (item.Quantita > 1) //Se la quantità dell'articolo nel carrello è maggiore di 1, decrementa la sua quantità
                    {
                        item.Quantita--;
                        articolo.Quantita++;
                    } 
                    else //Se la quantità dell'articolo nel carrello è uguale a 1, rimuovi l'articolo dal carrello
                    {
                        _items.Remove(item); // rimuovo l'articolo dal carrello
                        articolo.Quantita++; // incremento la quantità dell'articolo
                    }
                    _context.Update(articolo); // aggiorno l'articolo nel database
                    _context.SaveChanges(); // salvo le modifiche nel database
                }
            }
            SaveCartItems(HttpContext.Session); // salvo gli articoli nel carrello nella sessione
            TempData["Message"] = "Articolo rimosso dal carrello!"; // messaggio di successo
            return RedirectToAction("Index"); // reindirizzo alla pagina del carrello
        }
       
        // Metodo per incrementare la quantità di un articolo nel carrello
        public IActionResult IncrementQuantity(int id)
        {
            var articolo = _context.Articoli.Find(id); // cerco l'articolo nel database
            if (articolo != null) // se l'articolo esiste
            {
                var item = _items.FirstOrDefault(i => i.Articolo.IdA == id); // cerco l'articolo nel carrello
                if (item != null) // se l'articolo è presente nel carrello
                {
                    item.Quantita++; // incremento la quantità dell'articolo
                    articolo.Quantita--; // decremento la quantità dell'articolo
                    _context.Update(articolo); // aggiorno l'articolo nel database
                    _context.SaveChanges(); // salvo le modifiche nel database
                }
            }
            SaveCartItems(HttpContext.Session); // salvo gli articoli nel carrello nella sessione
            return RedirectToAction("Index"); // reindirizzo alla pagina del carrello
        }

        // Metodo per decrementare la quantità di un articolo nel carrello

        public IActionResult DecrementQuantity(int id)
        {
            var articolo = _context.Articoli.Find(id); // cerco l'articolo nel database
            if (articolo != null) // se l'articolo esiste
            {
                var item = _items.FirstOrDefault(i => i.Articolo.IdA == id); // cerco l'articolo nel carrello
                if (item != null) // se l'articolo è presente nel carrello
                {
                    if (item.Quantita > 1) // se la quantità dell'articolo è maggiore di 1
                    {
                        item.Quantita--; // decremento la quantità dell'articolo
                        articolo.Quantita++; // incremento la quantità dell'articolo
                        _context.Update(articolo); // aggiorno l'articolo nel database
                        _context.SaveChanges(); // salvo le modifiche nel database
                    }
                }
            }
            SaveCartItems(HttpContext.Session); // salvo gli articoli nel carrello nella sessione
            return RedirectToAction("Index"); // reindirizzo alla pagina del carrello
        }

        // Metodo del checkout che nonstante il pagamento non è ancora stato effettuato e sopratutto ti permette di cambiare dati spedizione e fatturazione perchè può essere che l'utente voglia cambiare indirizzo di spedizione o fatturazione

      
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(AccountUser user)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ottieni l'ID dell'utente corrente
            if (userId == null) // Se l'ID dell'utente è nullo
            {
                return RedirectToAction("Failure"); // Reindirizza alla pagina di errore
            }

            var userp = _context.Users.Find(userId); // Cerca l'utente nel database
            return RedirectToAction("ConfermaDatiSpedizione", "Ordini", userp); // Reindirizza alla pagina di conferma dei dati di spedizione

        }

        

        







       



        // adesso creiamo il metodo per far vedere l'ordine dopo aver cliccato su checkout ma deve ancora effettuare il pagamento

        public IActionResult Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ottieni l'ID dell'utente corrente
            if (userId == null) // Se l'ID dell'utente è nullo
            {
                return RedirectToAction("Failure"); // Reindirizza alla pagina di errore
            }

            // Salva l'ordine nel database
            Ordine ordine = new Ordine(); // Crea un nuovo ordine
            ordine.UserId = userId; // Assegna l'ID dell'utente all'ordine
            ordine.Data = DateTime.Now; // Assegna la data corrente all'ordine
            ordine.Stato = "Non Pagato"; // Assegna lo stato "Pagato" all'ordine
            ordine.Totale = _items.Sum(item => item.Articolo.Prezzo * item.Quantita); // Calcola il totale dell'ordine
            _context.Ordini.Add(ordine); // Aggiungi l'ordine al database
            _context.SaveChanges(); // Salva le modifiche nel database

            // Svuota il carrello
            _items.Clear(); // Svuota il carrello
            SaveCartItems(HttpContext.Session); // Salva gli articoli nel carrello nella sessione

            TempData["Message"] = "Ordine effettuato con successo!"; // Messaggio di successo

            return RedirectToAction("Index", "Articoli"); // Reindirizza alla pagina degli articoli
        }

       


        // Metodo per svuotare il carrello
        public IActionResult ClearCart()
        {
            _items.Clear(); // svuoto il carrello
            SaveCartItems(HttpContext.Session); // salvo gli articoli nel carrello nella sessione
            TempData["Message"] = "Carrello svuotato!"; // messaggio di successo
            return RedirectToAction("Index"); // reindirizzo alla pagina del carrello
        }


        [HttpGet]
        public IActionResult CartSummary() // Metodo per visualizzare il riepilogo del carrello
        { 
            var cartSummary = _items.Select(i => new
            {
                i.Articolo.Nome,
                i.Quantita,
                Subtotal = i.Articolo.Prezzo * i.Quantita
            }); // Seleziona gli articoli nel carrello

            return Json(cartSummary); // Restituisci il riepilogo del carrello in formato JSON
        }

    }
}


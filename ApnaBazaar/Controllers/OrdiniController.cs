using ApnaBazaar.Areas.Identity.Data;
using ApnaBazaar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Stripe;
using System.Security.Claims;

namespace ApnaBazaar.Controllers
{
    public class OrdiniController : Controller
    {
        private readonly ILogger<OrdiniController> _logger;
        private readonly AppDbContext _context;
        private List<ItemCarrello> _items;
        private readonly IConfiguration _configuration;

        public OrdiniController(AppDbContext context, ILogger<OrdiniController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            
            _configuration = configuration;
            _items = new List<ItemCarrello>();
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            
        }

        public IActionResult SomeAction()
        {
            _items = LoadCartItems(HttpContext.Session);
            return View(_items);
        }

        private List<ItemCarrello> LoadCartItems(ISession session)
        {
            if (session == null)
            {
                return new List<ItemCarrello>();
            }
            return session.Get<List<ItemCarrello>>("Cart") ?? new List<ItemCarrello>();
        }

        private void SaveCartItems(ISession session)
        {
            session.Set("Cart", _items);
        }

        public IActionResult Index()
        {
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier); // recupero l'ID dell'utente loggato
            if (!User.Identity.IsAuthenticated) // se l'utente non è loggato, lo reindirizzo alla pagina di login
            {
                return Challenge();
            }

            var ordini = _context.Ordini.Where(o => o.UserId == userId).ToList(); // recupero tutti gli ordini dell'utente loggato
            return View(ordini); // mostro la vista con gli ordini
           
        }

        

        // facciamo il metodo per vedere i dettagli dell'ordine
        public IActionResult Details(int id)
        {
            var ordine = _context.Ordini.Find(id); // recupero l'ordine dal database
            if (ordine == null) // se l'ordine non esiste
            {
                return NotFound(); // ritorno un errore 404
            }
            return View(ordine); // mostro la vista con i dettagli dell'ordine
        }

        [Authorize(Roles = "Admin, Venditore")] // solo l'admin può vedere tutti gli ordini
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _context.Ordini.ToListAsync());
        }






        public IActionResult ConfermaDatiSpedizione()
        {
            
            string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier); // recupero l'ID dell'utente loggato
            if(!User.Identity.IsAuthenticated) // se l'utente non è loggato, lo reindirizzo alla pagina di login
            {
                return Challenge();
            }

            var userInDb = _context.Users.Find(userId); // recupero l'utente dal database
            if(userInDb != null) // se l'utente è presetne nel db
            {
                return View(userInDb); // mostro la vista con i dati dell'utente
            }
            return View(userInDb); // mostro la vista con i dati dell'utente
            
            
        }

        [HttpPost]
        public IActionResult ConfermaDatiSpedizione(AccountUser model)
        {
            if(ModelState.IsValid)
            {
                AccountUser newUser = new AccountUser // creo un nuovo oggetto AccountUser con i dati dell'utente
                {
                    Nome = model.Nome,
                    Cognome = model.Cognome,
                    Indirizzo = model.Indirizzo,
                    Citta = model.Citta,
                    CAP = model.CAP,
                    Provincia = model.Provincia,
                    Email = model.Email,
                    Cellulare = model.Cellulare,
                    

                };

                _context.Users.Add(newUser); // aggiungo il nuovo utente al database
                _context.SaveChanges(); // salvo i cambiamenti
                TempData["Success"] = "Dati salvati con successo!"; // messaggio di successo
                return RedirectToAction("ConfermaDatiSpedizioneSuccess", newUser); // reindirizzo alla pagina di checkout 

            }
            return View(model); // se i dati non sono validi, rimango sulla stessa pagina

        }

        // metodo per il successo della conferma dei dati

        public IActionResult ConfermaDatiSPedizioneSuccess(AccountUser model)
        {
            
            return View(model);


        }

        /*  [HttpPost]
          public IActionResult PagaConStripe(string stripeEmail, string stripeToken) // metodo per il pagamento con Stripe
          {
              ViewData["PublishableKey"] = _configuration["Stripe:PublishableKey"]; // recupero la chiave pubblica di Stripe
              var customers = new CustomerService(); // creo un nuovo servizio per i clienti
              var charges = new ChargeService(); // creo un nuovo servizio per i pagamenti

              var customer = customers.Create(new CustomerCreateOptions // creo un nuovo cliente
              {
                  Email = stripeEmail, // email del cliente
                  Source = stripeToken // token di Stripe
              });

              var charge = charges.Create(new ChargeCreateOptions // creo un nuovo pagamento
              {
                  Amount = 500, // Ad esempio, 500 centesimi = 5 euro 
                  Description = "Test Payment", // descrizione del pagamento
                  Currency = "eur", 
                  Customer = customer.Id // ID del cliente
              });

              if (charge.Status == "succeeded")
              {

                  // Il pagamento è stato effettuato con successo, quindi qui puoi salvare l'ordine nel tuo database
                  // e fare altre operazioni come inviare una email di conferma, ecc.
                  return View("Success");
              }
              else
              {
                  // Il pagamento non è riuscito, quindi qui puoi gestire l'errore e mostrare un messaggio all'utente
                  return View("Failure");
              }
              TempData["Message"] = "Pagamento effettuato con successo!";
          } */

        // creiamo un metodo per simulare che il pagamento sia statò effettuato da parte dell'utente e che mostri nell'index degli ordini sia la quantità ordinata che il prezzo totale
        /* [HttpPost]
         public IActionResult PagaConStripe()
         {
             string userId = User?.FindFirstValue(ClaimTypes.NameIdentifier); // recupero l'ID dell'utente loggato
             if (!User.Identity.IsAuthenticated) // se l'utente non è loggato, lo reindirizzo alla pagina di login
             {
                 return Challenge();
             }

             var userInDb = _context.Users.Find(userId); // recupero l'utente dal database
             if (userInDb != null) // se l'utente è presetne nel db
             {
                 return View(userInDb); // mostro la vista con i dati dell'utente
             }
             return View(userInDb); // mostro la vista con i dati dell'utente

         } */

        [HttpPost]
        public IActionResult PagaConStripe(OrdineViewModel model)
        {
           
           
            // Crea un nuovo ordine
            var nuovoOrdine = new Ordine
            {
                // Imposta le proprietà dell'ordine qui
                // Ad esempio, potresti voler copiare le proprietà dall'ordine passato come parametro

                Stato = "Pagato", // Imposta lo stato dell'ordine a "Pagato"
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), // Imposta l'ID dell'utente
                // Altre proprietà dell'ordine tipo prezzo totale, data di creazione e la quantita' ordinata
                Data = DateTime.Now,
                Prezzo = model.Prezzo,
                Quantita = model.Quantita
                
            };
            nuovoOrdine.Totale = model.Prezzo * model.Quantita; // Calcola il totale dell'ordine

            // Aggiungi l'ordine al database
            _context.Ordini.Add(nuovoOrdine);
            _context.SaveChanges();

            // Reindirizza l'utente alla pagina dell'ordine con i dettagli dell'ordine
            return RedirectToAction("Index", new { id = nuovoOrdine.IdO });

        }





    }




























}


using ApnaBazaar.Areas.Identity.Data;
using ApnaBazaar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            _items = LoadCartItems(HttpContext.Session);
            IEnumerable<Ordine> objOrdini = _context.Ordini;
            return View(objOrdini);
        }

        // facciamo il metodo per vedere i dettagli dell'ordine
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var objOrdine = _context.Ordini.FirstOrDefault(u => u.IdO == id);
            if (objOrdine == null)
            {
                return NotFound();
            }

            return View(objOrdine);
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
                    Cellulare = model.Cellulare

                };

                _context.Users.Add(newUser); // aggiungo il nuovo utente al database
                _context.SaveChanges(); // salvo i cambiamenti
                return RedirectToAction("ConfermaDatiSpedizioneSuccess", newUser); // reindirizzo alla pagina di checkout 

            }
            return View(model); // se i dati non sono validi, rimango sulla stessa pagina

        }

        // metodo per il successo della conferma dei dati

        public IActionResult ConfermaDatiSPedizioneSuccess(AccountUser model)
        {
            return View(model);


        }

        [HttpPost]
        public IActionResult PagaConStripe(string stripeEmail, string stripeToken)
        {
            ViewData["PublishableKey"] = _configuration["Stripe:PublishableKey"];
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500, // Ad esempio, 500 centesimi = 5 euro
                Description = "Test Payment",
                Currency = "eur",
                Customer = customer.Id
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
        }





    }
    



























}


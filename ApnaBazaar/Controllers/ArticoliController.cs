using ApnaBazaar.Areas.Identity.Data;
using ApnaBazaar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ApnaBazaar.Controllers
{
    public class ArticoliController : Controller
    {
      
        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostingEnviroment;
        private ILogger<ArticoliController> _logger;

        public ArticoliController(AppDbContext context, IWebHostEnvironment hostingEnviroment, ILogger<ArticoliController> logger)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
            _logger = logger;

        }

        public IActionResult Index()
        {
            IEnumerable<Articolo> objArticoli;
            if(ViewBag.SearchResults != null) // se ci sono articoli trovati
            {
                objArticoli = ViewBag.SearchResults; // prendiamo gli articoli trovati
            }
            else
            {
                objArticoli = _context.Articoli; // prendiamo tutti gli articoli dal db
            }
            return View(objArticoli);
        }


        // creiamo il metodo per la ricerca degli articoli
        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<IActionResult> Search(string query) // prendiamo il nome dell'articolo che l'utente ha inserito nel form
        {
            var articoli = from a in _context.Articoli select a; // prendiamo tutti gli articoli dal db
            if (!String.IsNullOrEmpty(query)) // se l'utente ha inserito un nome
            {
                articoli = articoli.Where(s => s.Nome.Contains(query)); // prendiamo gli articoli che contengono il nome inserito dall'utente
            }

            ViewBag.SearchResults= await articoli.ToListAsync(); // mettiamo gli articoli trovati in una variabile ViewBag

            return View("Index", ViewBag.SearchResults); // ritorniamo la vista con gli articoli trovati
        }



        //GET - CREATE
        [Authorize(Roles = "Admin, Venditore")]
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Articolo obj) // prendiamo l'oggetto articolo che l'utente ha inserito nel form
        {
            if(obj.Nome == null || obj.Descrizione == null || obj.Prezzo == 0 || obj.Quantita == 0) // se uno dei campi è vuoto o il prezzo o la quantità è 0
            {
                ModelState.AddModelError("", "Tutti i campi sono obbligatori"); // ritorniamo un errore
            }
            if (ModelState.IsValid) // se il modello è valido
            {
                if(obj.ImmagineUpload1 != null) // se l'utente ha inserito un'immagine
                {
                    string cartella = Path.Combine(_hostingEnviroment.WebRootPath, "img"); // prendiamo la cartella img
                   
                    string nomeFile = Guid.NewGuid().ToString() + "_" + obj.ImmagineUpload1.FileName; // creiamo un nome univoco per l'immagine
                    string percorso = Path.Combine(cartella, nomeFile); // creiamo il percorso dell'immagine
                    obj.Img1 =  "img/" + nomeFile; // salviamo il percorso dell'immagine nel db
                    using (var fileStream = new FileStream(percorso, FileMode.Create)) // creiamo un filestream per salvare l'immagine
                    {
                        obj.ImmagineUpload1.CopyTo(fileStream); // copiamo l'immagine nel filestream
                    }
                }
                if (obj.ImmagineUpload2 != null)
                {
                    string cartella = Path.Combine(_hostingEnviroment.WebRootPath, "img");
                    
                    string nomeFile = Guid.NewGuid().ToString() + "_" + obj.ImmagineUpload2.FileName;
                    string percorso = Path.Combine(cartella, nomeFile);
                    obj.Img2 = "img/" + nomeFile;
                    using (var fileStream = new FileStream(percorso, FileMode.Create))
                    {
                        obj.ImmagineUpload2.CopyTo(fileStream);
                    }
                }
                if (obj.ImmagineUpload3 != null)
                {
                    string cartella = Path.Combine(_hostingEnviroment.WebRootPath, "img");
                    
                    string nomeFile = Guid.NewGuid().ToString() + "_" + obj.ImmagineUpload3.FileName;
                    string percorso = Path.Combine(cartella, nomeFile);
                    obj.Img3 = "img/" + nomeFile;
                    using (var fileStream = new FileStream(percorso, FileMode.Create))
                    {
                        obj.ImmagineUpload3.CopyTo(fileStream);
                    }
                }
                _context.Articoli.Add(obj); // aggiungiamo l'articolo al db
                _context.SaveChanges(); // salviamo le modifiche
                TempData["Success"] = "L'articolo è stato aggiunto correttamente"; // mostriamo un messaggio di successo
                return RedirectToAction("Index"); // ritorniamo alla pagina principale
            }
            return View(obj); // se il modello non è valido ritorniamo la vista con l'articolo inserito
        }

        // adesso facciamo l'edit dell'articolo
        // prima facciamo il get dell'articolo tramite l'id
        //GET - EDIT
        [Authorize(Roles = "Admin, Venditore")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context.Articoli.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // adesso facciamo il post dell'articolo tramite l'id che abbiamo preso dal get dell'articolo e salviamo eventuali modifiche senza creare un nuovo articolo ma modificando quello esistente, senza che ogni campo sia obbligatorio

        //POST - EDIT
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Articolo obj)
        {
            if (obj.IdA == 0) // se l'id dell'articolo è 0
            {
                ModelState.AddModelError("", "Identificativo articolo non valido."); // ritorniamo un errore
                return View(obj); // ritorniamo la vista con l'articolo
            } 

            var articoloEsistente = _context.Articoli.Find(obj.IdA); // prendiamo l'articolo dal db tramite l'id
            if (articoloEsistente == null) // se l'articolo non esiste
            {
                return NotFound(); // ritorniamo un errore
            }

            articoloEsistente.Nome = obj.Nome ?? articoloEsistente.Nome; // se l'utente non ha inserito un nome, prendiamo il nome dell'articolo esistente
            articoloEsistente.Descrizione = obj.Descrizione ?? articoloEsistente.Descrizione; // se l'utente non ha inserito una descrizione, prendiamo la descrizione dell'articolo esistente
            articoloEsistente.Prezzo = obj.Prezzo > 0 ? obj.Prezzo : articoloEsistente.Prezzo; // se l'utente non ha inserito un prezzo, prendiamo il prezzo dell'articolo esistente
            articoloEsistente.Quantita = obj.Quantita > 0 ? obj.Quantita : articoloEsistente.Quantita; // se l'utente non ha inserito una quantità, prendiamo la quantità dell'articolo esistente

            if (!ModelState.IsValid) // se il modello non è valido
            {
                return View(obj); // ritorniamo la vista con l'articolo
            }

            try // proviamo a salvare le modifiche
            {
                _context.SaveChanges(); // salviamo le modifiche
                TempData["Success"] = "L'articolo è stato modificato correttamente."; // mostriamo un messaggio di successo
                return RedirectToAction("Index"); // ritorniamo alla pagina principale
            }
            catch (Exception ex) // se c'è un errore
            {
                ModelState.AddModelError("", "Errore durante il salvataggio: " + ex.Message); // ritorniamo un errore
                return View(obj); // ritorniamo la vista con l'articolo
            }
        }











        //GET - DELETE
        [Authorize(Roles = "Admin, Venditore")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context.Articoli.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - DELETE
        
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteArticolo(int? id)
        {
            var obj = _context.Articoli.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Articoli.Remove(obj);
            _context.SaveChanges();
            TempData["Success"] = "L'articolo è stato eliminato correttamente";
            return RedirectToAction("Index");
        }

        // creiamo il metodo per la visualizzazione dei dettagli dell'articolo
        
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _context.Articoli.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

       



       





        
       
        



       
      
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ApnaBazaar.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AccountUser class
public class AccountUser : IdentityUser
{
    public string Nome { get; set; }
    public string Cognome { get; set; }

    [Display(Name = "Data di Nascita")]
    public DateOnly DataDiNascita { get; set; }

    public string Indirizzo { get; set; }

    public string Citta { get; set; }

    public string Provincia { get; set; }

    public string CAP { get; set; }

    public string Cellulare { get; set; }

    public string? PIVA { get; set; }

    public bool? Venditore { get; set; } 


    // voglio che un un utente deve essere un venditore per poter inserire un articolo ed età maggiore di 18 anni
    public bool IsVenditore()
    {
        return Venditore == true && (DateTime.Now.Year - DataDiNascita.Year) >= 18;
    }

   

} 


using ApnaBazaar.Areas.Identity.Data;
using ApnaBazaar.Controllers;
using ApnaBazaar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApnaBazaar.Areas.Identity.Data;

public class AppDbContext : IdentityDbContext<AccountUser>
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articolo>(entity =>
        {
            entity.Property(e => e.IdA)
                .ValueGeneratedOnAdd();
                
        });

        modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Articolo> Articoli { get; set; }

    public DbSet<Ordine> Ordini { get; set; }

    public DbSet<Carrello> Carrello { get; set; }

    public DbSet<ItemCarrello> ItemCarrello { get; set; }

    public DbSet<AccountUser> AccountUser { get; set; }

}
public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AccountUser>
{
   

    public void Configure(EntityTypeBuilder<AccountUser> builder)
    {

        builder.Property(u => u.Nome)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.Cognome)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.DataDiNascita)
            .IsRequired();
        builder.Property(u => u.Indirizzo)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.Citta)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.Provincia)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(u => u.CAP)
            .HasMaxLength(5)
            .IsRequired();
        builder.Property(u => u.Cellulare)
            .HasMaxLength(10)
            .IsRequired();
        builder.Property(u => u.PIVA)
            .HasMaxLength(11);
        builder.Property(u => u.Venditore);
           

        
        
    }

   
 

    

    
}

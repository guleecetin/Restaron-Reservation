using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestoranRezervasyonu.Models;

namespace RestoranRezervasyonu.Utility
{
    public class UygulamaDbContext:IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options)
        { }
        public DbSet<RezervasyonTuru>RezervasyonTurleri {get; set;}
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<Masa>Masalar { get; set; }

    }
}

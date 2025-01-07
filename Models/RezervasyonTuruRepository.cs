using RestoranRezervasyonu.Utility;

namespace RestoranRezervasyonu.Models
{
    public class RezervasyonTuruRepository : Repository<RezervasyonTuru>, IRezervasyonTuruRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public RezervasyonTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(RezervasyonTuru rezervasyonTuru)
        {
            _uygulamaDbContext.Update(rezervasyonTuru);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
          
        }
    }
}

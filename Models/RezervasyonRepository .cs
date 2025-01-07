using RestoranRezervasyonu.Utility;

namespace RestoranRezervasyonu.Models
{
    public class RezervasyonRepository : Repository<Rezervasyon>, IRezervasyonRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public RezervasyonRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Rezervasyon rezervasyon)
        {
            _uygulamaDbContext.Update(rezervasyon);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
          
        }
    }
}

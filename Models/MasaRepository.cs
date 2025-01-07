using RestoranRezervasyonu.Utility;

namespace RestoranRezervasyonu.Models
{
    public class MasaRepository : Repository<Masa>, IMasaRepository
    {
        private readonly UygulamaDbContext _uygulamaDbContext;

        public MasaRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Masa masa)
        {
            _uygulamaDbContext.Update(masa);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}

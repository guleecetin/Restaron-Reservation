namespace RestoranRezervasyonu.Models
{
    public interface IRezervasyonTuruRepository:IRepository<RezervasyonTuru>
    {
        void Guncelle(RezervasyonTuru rezervasyonTuru);
        void Kaydet();
    }
}

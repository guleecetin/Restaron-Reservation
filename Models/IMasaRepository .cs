namespace RestoranRezervasyonu.Models
{
    public interface IMasaRepository:IRepository<Masa>
    {
        void Guncelle(Masa masa);
        void Kaydet();
    }
}

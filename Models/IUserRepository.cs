namespace RestoranRezervasyonu.Models
{
    public interface IUserRepository: IRepository<ApplicationUser>
    {
        void Kaydet();
    }
}

namespace RestoranRezervasyonu.Models
{
    public interface IUserRepository: IRepository<User>
    {
        void Kaydet();
    }
}

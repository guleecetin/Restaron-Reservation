using RestoranRezervasyonu.Utility;
using static RestoranRezervasyonu.Models.UserRepository;

namespace RestoranRezervasyonu.Models
{
    public class UserRepository : Repository<User>, IUserRepository
    {
       
            private readonly UygulamaDbContext _uygulamaDbContext;
            public UserRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
            {
                _uygulamaDbContext = uygulamaDbContext;
            }
        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();

        }
    }
}

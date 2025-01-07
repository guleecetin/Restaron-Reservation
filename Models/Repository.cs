using Microsoft.EntityFrameworkCore;
using RestoranRezervasyonu.Utility;
using System.Linq.Expressions;

namespace RestoranRezervasyonu.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbSet;
        public Repository(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
             this.dbSet = _uygulamaDbContext.Set<T>();
            _uygulamaDbContext.Rezervasyonlar.Include(k => k.RezervasyonTuru).Include(k => k.RezervasyonTuruId);
        }
        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            // Null kontrolü
            if (filtre == null)
            {
                throw new ArgumentNullException(nameof(filtre), "Filtre null olamaz.");
            }

            IQueryable<T> sorgu = dbSet ?? throw new InvalidOperationException("dbSet null olamaz!");

            // Filtreyi uygula
            sorgu = sorgu.Where(filtre);

            // includeProps kontrolü ve Include işlemleri
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }

            // Sonuç döndürme
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps=null)
        {
            IQueryable<T> sorgu = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    sorgu = sorgu.Include(includeProp);
                }
            }
            return sorgu.ToList();
        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);

        }
    }
}

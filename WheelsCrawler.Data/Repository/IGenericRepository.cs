using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelsCrawler.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        Task CreateAsync(TEntity entity);
        void Update(int id, TEntity entity);
        Task Delete(int id);
        Task<bool> SaveAll();
        void Dispose();
    }
}

using WheelsCrawler.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.Repository
{
    //used this resources : https://codingblast.com/entity-framework-core-generic-repository/
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly WheelsCrawlerDbContext _dbContext;

        public GenericRepository()
        {
            _dbContext = new WheelsCrawlerDbContext();            
        }
        public GenericRepository(WheelsCrawlerDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .Where(e => e.Id == id)
                        .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }
        public async Task<bool> SaveAll()
        {
                return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task Update(int id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

    }
}

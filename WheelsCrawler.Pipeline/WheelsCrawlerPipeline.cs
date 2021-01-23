using System.Collections.Generic;
using System.Threading.Tasks;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Pipeline
{
    public class WheelsCrawlerPipeline<TEntity> : IWheelsCrawlerPipeline<TEntity> where TEntity : class, IEntity
    {
        private readonly IGenericRepository<TEntity> _repository;

        public WheelsCrawlerPipeline()
        {
            _repository = new GenericRepository<TEntity>();
        }

        public async Task Run(IEnumerable<TEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                await _repository.CreateAsync(entity);
                if (await _repository.SaveAll())
                    continue;

            }
        }
    }
}
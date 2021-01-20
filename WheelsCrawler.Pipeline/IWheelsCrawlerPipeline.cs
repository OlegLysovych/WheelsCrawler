using System.Collections.Generic;
using System.Threading.Tasks;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Pipeline
{
    public interface IWheelsCrawlerPipeline<TEntity> where TEntity : class, IEntity
    {
        Task Run(IEnumerable<TEntity> entity);
    }
}
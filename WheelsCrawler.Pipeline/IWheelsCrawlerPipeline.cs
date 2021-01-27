using System.Collections.Generic;
using System.Threading.Tasks;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Pipeline
{
    public interface IWheelsCrawlerPipeline<NEntity> where NEntity : class, IEntity
    {
        Task Run(IEnumerable<NEntity> entity);
    }
}
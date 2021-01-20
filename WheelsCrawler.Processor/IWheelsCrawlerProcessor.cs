using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Processor
{
    public interface IWheelsCrawlerProcessor<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> Process(HtmlDocument document);
    }
}
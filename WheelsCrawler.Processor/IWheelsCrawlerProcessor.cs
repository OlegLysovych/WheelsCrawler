using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Processor
{
    public interface IWheelsCrawlerProcessor<TEntity, NEntity> where TEntity : class, IEntity where NEntity : class
    {
        Task<IEnumerable<NEntity>> Process(HtmlDocument document);
    }
}
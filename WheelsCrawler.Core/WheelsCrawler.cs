using System.Threading.Tasks;
using WheelsCrawler.Data.Repository;
using WheelsCrawler.Downloader;
using WheelsCrawler.Pipeline;
using WheelsCrawler.Processor;
using WheelsCrawler.Request;
using WheelsCrawler.Scheduler;

namespace WheelsCrawler.Core
{
    public class WheelsCrawler<TEntity> : IWheelsCrawler where TEntity : class, IEntity
    {
        public IWheelsCrawlerRequest Request { get; private set; }
        public IWheelsCrawlerDownloader Downloader { get; private set; }
        public IWheelsCrawlerProcessor<TEntity> Processor { get; private set; }
        public IWheelsCrawlerScheduler Scheduler { get; private set; }
        public IWheelsCrawlerPipeline<TEntity> Pipeline { get; private set; }

        public WheelsCrawler()
        {

        }

        public WheelsCrawler<TEntity> AddRequest(IWheelsCrawlerRequest request)
        {
            Request = request;
            return this;
        }

        public WheelsCrawler<TEntity> AddDownloader(IWheelsCrawlerDownloader downloader)
        {
            Downloader = downloader;
            return this;
        }

        public WheelsCrawler<TEntity> AddProcessor(IWheelsCrawlerProcessor<TEntity> processor)
        {
            Processor = processor;
            return this;
        }

        public WheelsCrawler<TEntity> AddScheduler(IWheelsCrawlerScheduler scheduler)
        {
            Scheduler = scheduler;
            return this;
        }

        public WheelsCrawler<TEntity> AddPipeline(IWheelsCrawlerPipeline<TEntity> pipeline)
        {
            Pipeline = pipeline;
            return this;
        }

        public async Task Crawle()
        {
            var linkReader = new WheelsCrawlerPageLinkReader(Request);
            var links = await linkReader.GetLinks(Request.Url, 0);

            foreach (var url in links)
            {
                var document = await Downloader.Download(url);
                var entity = await Processor.Process(document);
                await Pipeline.Run(entity);
            }
        }

    }
}
using System.Threading.Tasks;

namespace WheelsCrawler.Scheduler
{
    public interface IWheelsCrawlerScheduler
    {
        long RetryTime { get; set; }
        Task Schedule();
    }
}
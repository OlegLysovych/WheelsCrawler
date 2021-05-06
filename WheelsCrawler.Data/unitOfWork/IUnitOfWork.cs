using System.Threading.Tasks;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.unitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;

        IAccountRepository Users { get; }
        ICarRepository Cars { get; }
    }
}
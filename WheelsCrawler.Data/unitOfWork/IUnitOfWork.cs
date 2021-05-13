using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.unitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;

        IAccountRepository Users { get; }
        ICarRepository Cars { get; }
        IModelRepository Models { get; }
        IBrandRepository Brands { get; }
        IUrlRepository Urls { get; }
    }
}
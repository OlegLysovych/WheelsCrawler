using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Models.Account;
using WheelsCrawler.Data.Repository;

namespace WheelsCrawler.Data.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WheelsCrawlerDbContext _dbContext;
        private Hashtable _repositories;
        private IAccountRepository accountRepository;
        private ICarRepository carRepository;
        private IModelRepository modelRepository;
        private IBrandRepository brandRepository;
        private IUrlRepository urlRepository;

        private IAccManager userManager;
        private AppRole roleManager;
        public UnitOfWork(WheelsCrawlerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public IAccountRepository Users
        {
            get
            {
                if (accountRepository == null)
                    accountRepository = new AccountRepository(_dbContext);
                return accountRepository;
            }
        }
        public ICarRepository Cars
        {
            get
            {
                if (carRepository == null)
                    carRepository = new CarRepository(_dbContext);
                return carRepository;
            }
        }

        public IModelRepository Models
        {
            get
            {
                if (modelRepository == null)
                    modelRepository = new ModelRepository(_dbContext);
                return modelRepository;
            }
        }
        public IBrandRepository Brands
        {
            get
            {
                if (brandRepository == null)
                    brandRepository = new BrandRepository(_dbContext);
                return brandRepository;
            }
        }
        public IUrlRepository Urls
        {
            get
            {
                if (urlRepository == null)
                    urlRepository = new UrlRepository(_dbContext);
                return urlRepository;
            }
        }

    }
}
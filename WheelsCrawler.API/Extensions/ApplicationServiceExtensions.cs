using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WheelsCrawler.API.Interfaces;
using WheelsCrawler.API.Services;
using WheelsCrawler.Data.Dto;
using WheelsCrawler.Data.Models;
using WheelsCrawler.Data.Repository;
using WheelsCrawler.Data.unitOfWork;

namespace WheelsCrawler.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<WheelsCrawlerDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlite(config.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WheelsCrawler.API"));
            });
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            WheelsCrawlerDbContext wheelsDbContext = serviceProvider.GetService<WheelsCrawlerDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(uow => new UnitOfWork(wheelsDbContext));

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
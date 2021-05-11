using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
// using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Reflection;
using WheelsCrawler.Data.Models.Account;

namespace WheelsCrawler.Data.Models
{
    public partial class WheelsCrawlerDbContext : IdentityDbContext<User, AppRole, int,
                                                                    IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
                                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public WheelsCrawlerDbContext()
        {

        }
        public WheelsCrawlerDbContext(DbContextOptions<WheelsCrawlerDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarType> CarTypes { get; set; }
        public virtual DbSet<CarBrand> CarBrands { get; set; }
        public virtual DbSet<CarFuel> CarFuels { get; set; }
        public virtual DbSet<CarGearbox> CarGearboxes { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<Url> Urls { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 // optionsBuilder.UseSqlServer("Server=(localdb)//mssqllocaldb;Database=WheelsCrawler.CarsDb;Trusted_Connection=True;");
//                 optionsBuilder.UseSqlite("Data Source = C:/Users/PC/source/repos/WheelsCrawler/WheelsCrawler.Data/WheelsCrawler.db", options =>
//              {
//                  options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
//                  options.UseRelationalNulls(false);
//              });
//                 base.OnConfiguring(optionsBuilder);
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
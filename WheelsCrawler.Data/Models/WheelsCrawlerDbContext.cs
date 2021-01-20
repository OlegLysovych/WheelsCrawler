using Microsoft.EntityFrameworkCore;
// using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Reflection;

namespace WheelsCrawler.Data.Models
{
    public partial class WheelsCrawlerDbContext : DbContext
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                // optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WheelsCrawler.CarsDb;Trusted_Connection=True;");
                optionsBuilder.UseSqlite("Data Source=WheelsCrawler.Cars.db", options =>
             {
                 options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
             });
            base.OnConfiguring(optionsBuilder);
            }
        }
        
        // protected override void OnModelCreating(DbModelBuilder modelBuilder)
        // {
        //     throw new UnintentionalCodeFirstException();
        //     modelBuilder.Entity<Car>(entity =>
        //     {
        //         entity.HasKey(k => k.Id);
        //         entity.HasIndex(e => e.CarBrandId);
        //         entity.HasIndex(e => e.CarTypeId);
        //         entity.Property(e => e.Id)
        //     })
        // }
    }
}
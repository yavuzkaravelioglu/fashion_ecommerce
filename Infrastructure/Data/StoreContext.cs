using System.Linq;
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext // Entitiy Framework Class that  Microsoft Create
    {
        public StoreContext(DbContextOptions <StoreContext> options) : base(options)
        {

        }

        public DbSet <Product> Products { get; set; }
        public DbSet <ProductBrand> ProductBrand { get; set; }
        public DbSet <ProductType> ProductType { get; set; }

        /*
        public DbSet <ProductDetailPictures> ProductDetailPictures {get; set;}
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder) // we override this method to look for our configuration file. (Configurations about entitity constrains, which is Infrastructure/Data/Config)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if ( Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach ( var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties()
                        .Where( p => p.PropertyType == typeof(decimal));

                    foreach ( var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion<double>();
                    }
                }
            }

        }

    }
}
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Enities;

namespace Infrastructure.Data
{
    public class ProdContextSeed
    {
        public static async Task SeedAsync(ProdContext prodContext, ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (prodContext.Database.IsSqlServer())
                {
                    prodContext.Database.Migrate();
                }


                if (!await prodContext.Brands.AnyAsync())
                {
                    await prodContext.Brands.AddRangeAsync(
                        GetPreconfiguredBrands());

                    await prodContext.SaveChangesAsync();
                }

                if (!await prodContext.Categories.AnyAsync())
                {
                    await prodContext.Categories.AddRangeAsync(
                        GetPreconfiguredCategories());

                    await prodContext.SaveChangesAsync();
                }

                if (!await prodContext.Products.AnyAsync())
                {
                    await prodContext.Products.AddRangeAsync(
                        GetPreconfiguredProducts());

                    await prodContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 10) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(prodContext, logger, retryForAvailability);
                throw;
            }
        }

        private static IEnumerable<Brand> GetPreconfiguredBrands()
        {
            return new List<Brand>()
            {
                new("Zaza"),
                new("Rizz"),
                new("Skib")
            };
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                new("PC"),
                new("Smartphone"),
                new("Laptop")
            };
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new("PC 1000", "High performance PC", 1200, "http://productbaseurltobereplaced/images/products/2.png", 1, 1),
                new("PC 2000", "Gaming PC", 1500, "http://productbaseurltobereplaced/images/products/3.png", 1, 1),
                new("Smartphone A1", "Latest model smartphone", 800, "http://productbaseurltobereplaced/images/products/4.png", 2, 2),
                new("Smartphone B1", "Mid-range smartphone", 500, "http://productbaseurltobereplaced/images/products/5.png", 2, 2),
                new("Laptop X1", "Ultrabook laptop", 1000, "http://productbaseurltobereplaced/images/products/6.png", 3, 1),
                new("Laptop Y1", "Gaming laptop", 1300, "http://productbaseurltobereplaced/images/products/7.png", 3, 1)
            };
        }
    }
}

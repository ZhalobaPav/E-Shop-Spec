using ApplicationCore.Enities;
using ApplicationCore.Specifications;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class ProductFilterPaginatedSpecificationTest
    {
        [Fact]
        public void ReturnAllProducts()
        {
            var spec = new ProductFilterPaginatedSpecification(0, 8, null, null);

            var prods = spec.Evaluate(GetTestItemCollection()).ToList();

            Xunit.Assert.NotNull(prods);
            Xunit.Assert.Equal(5, prods.Count());
        }
        [Fact]
        public void Return3Products()
        {
            var spec = new ProductFilterPaginatedSpecification(0, 8, 1, 1);

            var prods = spec.Evaluate(GetTestItemCollection()).ToList();

            Xunit.Assert.NotNull(prods);
            Xunit.Assert.Equal(3, prods.Count());
        }

        [Fact]
        public void Return2Products()
        {
            var spec = new ProductFilterPaginatedSpecification(0, 2, 1, 1);

            var prods = spec.Evaluate(GetTestItemCollection()).ToList();

            Xunit.Assert.NotNull(prods);
            Xunit.Assert.Equal(2, prods.Count());
        }
        public List<Product> GetTestItemCollection()
        {
            return new List<Product>()
            {
                new Product("Name",   "Description",   0,   "FakePath", 1, 1),
                new Product("Name 2", "Description 2", 0, "FakePath", 1, 1),
                new Product("Name 3", "Description 3 ", 0,"FakePath", 3, 1),
                new Product("Name 4", "Description 4", 0, "FakePath", 1, 1),
                new Product("Name 5", "Description 5", 0, "FakePath", 2, 2),
            };
        }
    }
}

using ApplicationCore.Enities;
using ApplicationCore.Specifications;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class ProductFilterSpecificationTest
    {
        [Xunit.Theory]
        [InlineData(null, null, 5)]
        [InlineData(1, null, 3)]
        [InlineData(2, null, 2)]
        [InlineData(null, 1, 2)]
        [InlineData(null, 3, 1)]
        [InlineData(1, 3, 1)]
        [InlineData(2, 3, 0)]
        public void MatchesExpectedNumberOfItems(int? brandId, int? categoryId, int expectedCount)
        {
            var spec = new ProductFilterSpecification(brandId, categoryId);

            var result = spec.Evaluate(GetTestItemCollection()).ToList();

            Xunit.Assert.Equal(expectedCount, result.Count);
        }
        public List<Product> GetTestItemCollection()
        {
            return new List<Product>()
            {
                new Product("Name",   "Description",   0,   "FakePath", 1, 1),
                new Product("Name 2", "Description 2", 0, "FakePath", 2, 1),
                new Product("Name 3", "Description 3 ", 0,"FakePath", 3, 1),
                new Product("Name 4", "Description 4", 0, "FakePath", 1, 2),
                new Product("Name 5", "Description 5", 0, "FakePath", 2, 2),
            };
        }
    }
}

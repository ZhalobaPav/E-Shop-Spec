using ApplicationCore.Enities;
using ApplicationCore.Specifications;
using NSubstitute;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class ProductSpecificationTest
    {
        [Fact]
        public void MatchesSpecificProduct()
        {
            var productIds = new int[] { 1 };
            var spec = new ProductSpecification(productIds);

            var result = spec.Evaluate(GetTestCollection()).ToList();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Single(result);
        }
        [Fact]
        public void MathchesAllProducts()
        {
            var productIds = new int[] { 1, 2 };
            var spec = new ProductSpecification(productIds);

            var result = spec.Evaluate(GetTestCollection()).ToList();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(2, result.ToList().Count());
        }
        private List<Product> GetTestCollection()
        {
            var catalogItems = new List<Product>();

            var mockCatalogItem1 = Substitute.For<Product>("Item 1",  "Item 1 description",  1.5m, "Item1Uri", 1, 1);
            mockCatalogItem1.Id.Returns(1);

            var mockCatalogItem3 = Substitute.For<Product>("Item 2", "Item 2 description", 2.5m, "Item1Uri", 2, 2);
            mockCatalogItem3.Id.Returns(2);

            catalogItems.Add(mockCatalogItem1);
            catalogItems.Add(mockCatalogItem3);

            return catalogItems;
        }
    }
}

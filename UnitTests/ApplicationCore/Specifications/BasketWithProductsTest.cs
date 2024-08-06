using ApplicationCore.Enities.Basket;
using ApplicationCore.Specifications;
using NSubstitute;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications
{
    public class BasketWithProductsTest
    {
        private readonly int _testBasketId = 123;
        private readonly string _buyerId = "Test buyerId";
        [Fact]
        public void MatchesBasketWithGivenBasketId()
        {
            var spec = new BasketWithProductsSpecification(_testBasketId);

            var result = spec.Evaluate(GetTestBasketCollection()).FirstOrDefault();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(_testBasketId, result.Id);
        }
        [Fact]
        public void MatchesNoBasketsIfBasketIdNotPresent()
        {
            int badBasketId = -1;
            var spec = new BasketWithProductsSpecification(badBasketId);

            var result = spec.Evaluate(GetTestBasketCollection()).Any();

            Xunit.Assert.False(result);
        }
        [Fact]
        public void MatchesBasketWithGivenBuyerId()
        {
            var spec = new BasketWithProductsSpecification(_buyerId);

            var result = spec.Evaluate(GetTestBasketCollection()).FirstOrDefault();

            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(_buyerId, result.BuyerId);
        }
        [Fact]
        public void MatchesNoBasketsIfBuyerIdNotPresent()
        {
            string badBuyerId = "badBuyerId";
            var spec = new BasketWithProductsSpecification(badBuyerId);

            var result = spec.Evaluate(GetTestBasketCollection()).Any();

            Xunit.Assert.False(result);
        }
        public List<Basket> GetTestBasketCollection()
        {
            var basket1Mock = Substitute.For<Basket>(_buyerId);
            basket1Mock.Id.Returns(1);
            var basket2Mock = Substitute.For<Basket>(_buyerId);
            basket2Mock.Id.Returns(2);
            var basket3Mock = Substitute.For<Basket>(_buyerId);
            basket3Mock.Id.Returns(_testBasketId);

            return new List<Basket>()
            {
                basket1Mock,
                basket2Mock,
                basket3Mock
            };
        }
    }
}

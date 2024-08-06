using ApplicationCore.Enities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.BasketTests
{
    public class BasketAddProduct
    {
        private readonly int _testProductId = 123;
        private readonly decimal _testUnitPrice = 1.23m;
        private readonly int _testQuantity = 2;
        private readonly string _buyerId = "Test buyerId";
        [Fact]
        public void AddsBasketItemIfNotPresent()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);

            var firstItem = basket.BasketProducts.Single();

            Xunit.Assert.Equal(_testProductId, firstItem.ProductId);
            Xunit.Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
            Xunit.Assert.Equal(_testQuantity, firstItem.Quantity);
        }
        [Fact]
        public void AddQuantityOfItem()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);

            var firstItem = basket.BasketProducts.Single();
            Xunit.Assert.Equal(_testQuantity * 2, firstItem.Quantity);
        }

        [Fact]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testProductId, _testUnitPrice * 2, _testQuantity);

            var firstItem = basket.BasketProducts.Single();
            Xunit.Assert.Equal(_testUnitPrice, firstItem.UnitPrice);
        }
        [Fact]
        public void DefaultsToQuantityOfOne()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice);

            var firstItem = basket.BasketProducts.Single();
            Xunit.Assert.Equal(1, firstItem.Quantity);
        }
        [Fact]
        public void CantGiveNegativeQuantity()
        {
            var basket = new Basket(_buyerId);

            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => basket.AddItem(_testProductId, _testUnitPrice, -1));
        }
        [Fact]
        public void CantModifyQuantityToNegativeNumber()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testProductId, _testUnitPrice);

            Xunit.Assert.Throws<ArgumentOutOfRangeException>(() => basket.AddItem(_testProductId, _testUnitPrice, -2));
        }
    }
}

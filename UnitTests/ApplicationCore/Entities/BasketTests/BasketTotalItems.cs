using ApplicationCore.Enities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.BasketTests
{
    public class BasketTotalItems
    {
        private readonly int _testProductId = 123;
        private readonly decimal _testUnitPrice = 1.23m;
        private readonly int _testQuantity = 2;
        private readonly string _buyerId = "Test buyerId";
        [Fact]
        public void ReturnsTotalQuantityWithOneItem()
        {
            var basket = new Basket(_buyerId);

            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);

            Xunit.Assert.Equal(basket.TotalItems, _testQuantity);
        }
        [Fact]
        public void ReturnsTotalQuantityWithThreeItems()
        {
            var basket = new Basket(_buyerId);

            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity);
            basket.AddItem(_testProductId, _testUnitPrice, _testQuantity * 2);

            Xunit.Assert.Equal(basket.TotalItems, _testQuantity * 3);
        }
    }
}

using ApplicationCore.Enities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.OrderTests
{
    public class OrderTotal
    {
        private decimal _testUnitPrice = 42m;
        [Fact]
        public void IsZeroForNewItem()
        {
            var order = new OrderBuilder().WithNoItems();

            Xunit.Assert.Equal(0, order.Total());
        }
        [Fact]
        public void IsCorrectGiven1Item()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderProductDetails>()
            {
                new OrderProductDetails(builder.TestProductOrder, _testUnitPrice, 1)
            };
            var order = new OrderBuilder().WithItems(items);
            Xunit.Assert.Equal(_testUnitPrice, order.Total());
        }

        [Fact]
        public void IsCorrectGiven3Items()
        {
            var builder = new OrderBuilder();
            var order = builder.WithDefaultValues();

            Xunit.Assert.Equal(builder.TestUnitPrice * builder.TestUnits, order.Total());
        }
    }
}

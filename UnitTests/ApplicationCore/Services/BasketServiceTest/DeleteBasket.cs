using ApplicationCore.Enities.Basket;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Logging;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.ApplicationCore.Services.BasketServiceTest
{
    public class DeleteBasket
    {
        private readonly string _buyerId = "Test buyerId";
        private readonly IRepository<Basket> _mockBasketRepository = Substitute.For<IRepository<Basket>>();
        private readonly ICustomLogger<BasketService> _mockLogger = Substitute.For<ICustomLogger<BasketService>>();
        [Fact]
        public async Task ShouldInvokeBasketRepositoryDeleteAsyncOnce()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(1, 1.5m);
            basket.AddItem(2, 1.1m, 1);
            _mockBasketRepository.GetByIdAsync(Arg.Any<int>(), default).Returns(basket);
            var basketService = new BasketService(_mockBasketRepository, _mockLogger);
            await basketService.DeleteBasketAsync(1);
            await _mockBasketRepository.Received().DeleteAsync(Arg.Any<Basket>(), default);
        }

    }
}

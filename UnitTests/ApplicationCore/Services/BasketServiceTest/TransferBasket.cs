using ApplicationCore.Enities.Basket;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.Specifications;
using Infrastructure.Logging;
using NSubstitute;
using Xunit;

namespace UnitTests.ApplicationCore.Services.BasketServiceTest
{
    public class TransferBasket
    {
        private readonly string _nonexistentAnonymousBasketBuyerId = "nonexistent-anonymous-basket-buyer-id";
        private readonly string _existentAnonymousBasketBuyerId = "existent-anonymous-basket-buyer-id";
        private readonly string _nonexistentUserBasketBuyerId = "newuser@microsoft.com";
        private readonly string _existentUserBasketBuyerId = "testuser@microsoft.com";
        private readonly IRepository<Basket> _mockBasketRepo = Substitute.For<IRepository<Basket>>();
        private readonly ICustomLogger<BasketService> _mockLogger = Substitute.For<ICustomLogger<BasketService>>();
        public class Results<T>
        {
            private readonly Queue<Func<T>> values = new Queue<Func<T>>();
            public Results(T result) => values.Enqueue(() => result);
            
            public Results<T> Then(T value) => Then(() => value); 
            public Results<T> Then(Func<T> value)
            {
                values.Enqueue(value);
                return this;
            }
            public T Next() => values.Dequeue()();
        }
        [Fact]
        public async Task InvokesBasketRepositoryFirstOrDefaultAsyncOnceIfAnonymousBasketNotExists()
        {
            var anonymousBasket = null as Basket;
            var userBasket = new Basket(_existentUserBasketBuyerId);
            var results = new Results<Basket?>(anonymousBasket)
                .Then(userBasket);
            _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithProductsSpecification>(), default).Returns(x => results.Next());
            var basketService = new BasketService(_mockBasketRepo, _mockLogger);
            await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
            await _mockBasketRepo.Received().FirstOrDefaultAsync(Arg.Any<BasketWithProductsSpecification>(), default);
        }
        [Fact]
        public async Task TransferAnonymousBasketItemsWhilePreservingExistingUserBasketItems()
        {
            var userBasket = new Basket(_existentUserBasketBuyerId);
            userBasket.AddItem(1, 33, 3);
            userBasket.AddItem(2, 43, 5);
            var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
            userBasket.AddItem(1, 33, 2);
            userBasket.AddItem(3, 32, 2);
            var results = new Results<Basket?>(anonymousBasket).Then(userBasket);

            _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithProductsSpecification>(), default).Returns(x=>results.Next());
            var basketService = new BasketService(_mockBasketRepo, _mockLogger);
            await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
            await _mockBasketRepo.Received().UpdateAsync(userBasket, default);

            Xunit.Assert.Equal(3, userBasket.BasketProducts.Count);
            Xunit.Assert.Contains(userBasket.BasketProducts, x => x.ProductId == 1 && x.UnitPrice == 33 && x.Quantity == 5);
            Xunit.Assert.Contains(userBasket.BasketProducts, x => x.ProductId == 2 && x.UnitPrice == 43 && x.Quantity == 5);
            Xunit.Assert.Contains(userBasket.BasketProducts, x => x.ProductId == 3 && x.UnitPrice == 32 && x.Quantity == 2);
        }
        [Fact]
        public async Task RemovesAnonymousBasketAfterUpdatingUserBasket()
        {
            var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
            var userBasket = new Basket(_existentUserBasketBuyerId);

            var results = new Results<Basket>(anonymousBasket)
                            .Then(userBasket);
            _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithProductsSpecification>(), default).Returns(x=>results.Next());
            var basketService = new BasketService(_mockBasketRepo, _mockLogger);
            await basketService.TransferBasketAsync(_nonexistentAnonymousBasketBuyerId, _existentUserBasketBuyerId);
            await _mockBasketRepo.Received().UpdateAsync(userBasket, default);
            await _mockBasketRepo.Received().DeleteAsync(anonymousBasket, default);
        }
        [Fact]
        public async Task CreatesNewUserBasketIfNotExists()
        {
            var anonymousBasket = new Basket(_existentAnonymousBasketBuyerId);
            var userBasket = null as Basket;
            var results = new Results<Basket?>(anonymousBasket).Then(userBasket);
            _mockBasketRepo.FirstOrDefaultAsync(Arg.Any<BasketWithProductsSpecification>(), default).Returns(x => results.Next());
            var basketService = new BasketService(_mockBasketRepo, _mockLogger);
            await basketService.TransferBasketAsync(_existentAnonymousBasketBuyerId, _nonexistentUserBasketBuyerId);
            await _mockBasketRepo.Received().AddAsync(Arg.Is<Basket>(x=>x.BuyerId == _nonexistentUserBasketBuyerId), default);
        }
    }
}

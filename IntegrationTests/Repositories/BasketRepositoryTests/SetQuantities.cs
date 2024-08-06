using ApplicationCore.Enities.Basket;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using UnitTests.Builders;
using Xunit;

namespace IntegrationTests.Repositories.BasketRepositoryTests
{
    public class SetQuantities
    {
        private readonly ProdContext _prodContext;
        private readonly EFRepository<Basket> _basketRepository;
        private readonly BasketBuilder BasketBuilder = new BasketBuilder();
        public SetQuantities()
        {
            var dbOptions = new DbContextOptionsBuilder<ProdContext>().UseInMemoryDatabase(databaseName: "TestProducts").Options;
            _prodContext = new ProdContext(dbOptions);
            _basketRepository = new EFRepository<Basket>(_prodContext);
        }
        [Fact]
        public async Task RemoveEmptyQuantities()
        {
            var basket = BasketBuilder.WithOneBasketItem();
            var basketService = new BasketService(_basketRepository, null);
            await _basketRepository.AddAsync(basket);
            await basketService.SetQuantities(BasketBuilder.BasketId, new Dictionary<string, int>() { { BasketBuilder.BasketId.ToString(), 0 } });
            Xunit.Assert.Equal(0, basket.BasketProducts.Count);
        }
    }
}

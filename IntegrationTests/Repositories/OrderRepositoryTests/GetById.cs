using ApplicationCore.Enities.Order;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using UnitTests.Builders;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.OrderRepositoryTests
{
    public class GetById
    {
        private readonly ProdContext _prodContext;
        private readonly EFRepository<Order> _orderRepository;
        private OrderBuilder OrderBuilder { get; } = new OrderBuilder();
        private readonly ITestOutputHelper _output;
        public GetById(ITestOutputHelper testOutputHelper)
        {
            var dbContextOptions = new DbContextOptionsBuilder<ProdContext>().UseInMemoryDatabase("Products");
            _prodContext = new ProdContext(dbContextOptions.Options);
            _output = testOutputHelper;
            _orderRepository = new EFRepository<Order>(_prodContext);
        }
        [Fact]
        public async Task GetExistingOrder()
        {
            var existingOrder = OrderBuilder.WithDefaultValues();
            _prodContext.Add(existingOrder);
            _prodContext.SaveChanges();
            int existingOrderId = existingOrder.Id;
            _output.WriteLine($"OrderId: {existingOrderId}");

            var orderFromRepo = await _orderRepository.GetByIdAsync(existingOrderId);
            Assert.Equal(OrderBuilder.TestBuyerId, orderFromRepo.BuyerId);

            var firstItem = orderFromRepo.OrderDetails.FirstOrDefault();
            Assert.Equal(OrderBuilder.TestUnits, firstItem.Units);
        }
    }
}

using ApplicationCore.Enities;
using ApplicationCore.Enities.Basket;
using ApplicationCore.Enities.Order;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Basket> basketRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IUriComposer uriComposer;

        public OrderService(IRepository<Product> productRepository,
                            IRepository<Basket> basketRepository,
                            IRepository<Order> orderRepository,
                            IUriComposer uriComposer)
        {
            this.productRepository = productRepository;
            this.basketRepository = basketRepository;
            this.orderRepository = orderRepository;
            this.uriComposer = uriComposer;
        }
        public async Task CreateOrderAsync(int basketId, Address address)
        {
            var basketSpec = new BasketWithProductsSpecification(basketId);
            var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);

            Guard.Against.Null(basket, nameof(basket));
            Guard.Against.EmptyBasketOnCheckout(basket.BasketProducts);

            var productSpec = new ProductSpecification(basket.BasketProducts.Select(item => item.ProductId).ToArray());
            var products = await productRepository.ListAsync(productSpec);

            var items = basket.BasketProducts.Select(basketItem =>
            {
                var product = products.First(p => p.Id == basketItem.ProductId);
                var productOrdered = new ProductOrder(product.Id, product.Name, uriComposer.ComposePicUri(product.PictureUri));
                var orderItem = new OrderProductDetails(productOrdered, basketItem.UnitPrice, basketItem.Quantity);
                return orderItem;
            }).ToList();
            var order = new Order(basket.BuyerId, address, items);
            await orderRepository.AddAsync(order);
        }
    }
}

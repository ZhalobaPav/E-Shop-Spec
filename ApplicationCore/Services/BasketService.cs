using ApplicationCore.Enities.Basket;
using ApplicationCore.Helper;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Infrastructure.Logging;

namespace ApplicationCore.Services
{
    public class BasketService : IBasketService
    {
        private readonly IRepository<Basket> basketRepository;
        private readonly ICustomLogger<BasketService> customLogger;

        public BasketService(IRepository<Basket> basketRepository, ICustomLogger<BasketService> customLogger)
        {
            this.basketRepository = basketRepository;
            this.customLogger = customLogger;
        }
        public async Task<Basket> AddItemToBasket(BasketProductRequest basketProductRequest)
        {
            var basketSpec = new BasketWithProductsSpecification(basketProductRequest.UserName);
            var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);
            if (basket == null)
            {
                basket = new Basket(basketProductRequest.UserName);
                await basketRepository.AddAsync(basket);
            }
            basket.AddItem(basketProductRequest.ProductId, basketProductRequest.Price, basketProductRequest.Quantity);
            await basketRepository.UpdateAsync(basket);
            return basket;
        }
        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await basketRepository.GetByIdAsync(basketId);
            Guard.Against.Null(basket, nameof(basket));
            await basketRepository.DeleteAsync(basket);
        }
        public async Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities)
        {
            var basketSpec = new BasketWithProductsSpecification(basketId);
            var basket = await basketRepository.GetByIdAsync(basketId);
            foreach (var item in basket.BasketProducts)
            {
                if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
                {
                    if (customLogger != null) customLogger.LogInformation($"Updating quantity with id {item.Id} to {quantity}");
                    item.SetQuantity(quantity);
                }
            }
            basket.RemoveEmpty();
            await basketRepository.UpdateAsync(basket);
            return basket;
        }
        public async Task TransferBasketAsync(string anonymousId, string userName)
        {
            var anonymousSpec = new BasketWithProductsSpecification(anonymousId);
            var anonymousBasket = await basketRepository.FirstOrDefaultAsync(anonymousSpec);
            if (anonymousBasket == null)
            {
                return;
            }
            var userBasketSpec = new BasketWithProductsSpecification(userName);
            var userBasket = await basketRepository.FirstOrDefaultAsync(userBasketSpec);
            if (userBasket == null)
            {
                userBasket = new Basket(userName);
                await basketRepository.AddAsync(userBasket);
            }
            foreach (var item in anonymousBasket.BasketProducts)
            {
                userBasket.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
            }
            await basketRepository.UpdateAsync(userBasket);
            await basketRepository.DeleteAsync(anonymousBasket);
        }
    }
}

using ApplicationCore.Enities.Basket;
using ApplicationCore.Helper;
using Ardalis.Result;

namespace ApplicationCore.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> AddItemToBasket(BasketProductRequest basketProductRequest);
        Task DeleteBasketAsync(int basketId);
        Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
        Task TransferBasketAsync(string anonymousId, string userName);
    }
}
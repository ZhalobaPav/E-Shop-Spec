using ApplicationCore.Enities.Basket;
using ViewModels.Basket;

namespace ApplicationCore.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<int> CountTotalProducts(string userName);
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
        Task<BasketViewModel> Map(Basket basket);
    }
}
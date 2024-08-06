
namespace Infrastructure.Data.Querries
{
    public interface IBasketQueryService
    {
        Task<int> CountTotalProducts(string userName);
    }
}
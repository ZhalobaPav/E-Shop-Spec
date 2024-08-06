using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Querries
{
    public class BasketQueryService : IBasketQueryService
    {
        private readonly ProdContext dbContext;

        public BasketQueryService(ProdContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> CountTotalProducts(string userName)
        {
            var totalItems = await dbContext.Baskets
                .Where(p => p.BuyerId == userName)
                .SelectMany(p => p.BasketProducts)
                .SumAsync(sum => sum.Quantity);
            return totalItems;
        }
    }
}

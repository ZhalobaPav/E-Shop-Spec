using ApplicationCore.Enities.Basket;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class BasketWithProductsSpecification:Specification<Basket>
    {
        public BasketWithProductsSpecification(int basketId)
        {
            Query
                .Where(b=>b.Id==basketId)
                .Include(b=>b.BasketProducts);
        }
        public BasketWithProductsSpecification(string buyerId)
        {
            Query
            .Where(b => b.BuyerId == buyerId)
            .Include(b => b.BasketProducts);
        }
    }
}

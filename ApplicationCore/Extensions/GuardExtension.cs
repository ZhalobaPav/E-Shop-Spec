using ApplicationCore.Enities.Basket;
using ApplicationCore.Exceptions;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Extensions
{
    public static class GuardExtension
    {
        public static void EmptyBasketOnCheckout(this IGuardClause guardClause, IReadOnlyCollection<BasketProduct> basketProducts)
        {
            if (!basketProducts.Any())
                throw new EmptyBasketOnCheckoutException();
        }
    }
}

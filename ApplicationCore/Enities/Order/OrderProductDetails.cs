using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities.Order
{
    public class OrderProductDetails:BaseEntity
    {
        #pragma warning disable CS8618
        private OrderProductDetails()
        {
            
        }
        public ProductOrder ProductOrder { get;private set; }
        public decimal UnitPrice { get; private set; }
        public int Units { get; private set; }
        public OrderProductDetails(ProductOrder product, decimal unitPrice, int units)
        {
            Guard.Against.NegativeOrZero(unitPrice, nameof(unitPrice));
            Guard.Against.NegativeOrZero(units, nameof(units));
            Guard.Against.Null(product, nameof(product));
            ProductOrder = product;
            UnitPrice = unitPrice;
            Units = units;
        }
    }
}

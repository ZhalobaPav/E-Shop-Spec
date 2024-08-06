using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities.Order
{
    public class Order:BaseEntity, IAggreagateRoot
    {
        #pragma warning disable CS8618
        private Order()
        {
            
        }
        public string BuyerId { get;private set; }
        public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
        public Address Address { get; private set; }
        private readonly List<OrderProductDetails> _orderDetails = new List<OrderProductDetails>();
        public IReadOnlyCollection<OrderProductDetails> OrderDetails => _orderDetails.AsReadOnly();
        public Order(string buyerId, Address address, List<OrderProductDetails> productOrders)
        {
            Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
            BuyerId = buyerId;
            Address = address;
            _orderDetails = productOrders;
        }
        public decimal Total()
        {
            var total = 0m;
            foreach(var item in _orderDetails)
            {
                total += item.UnitPrice * item.Units;
            }
            return total;
        }

    }
}

using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities.Basket
{
    public class Basket:BaseEntity, IAggreagateRoot
    {
        public string BuyerId { get;private set; }
        private readonly List<BasketProduct> _basketProducts = new List<BasketProduct>();
        public IReadOnlyCollection<BasketProduct> BasketProducts => _basketProducts.AsReadOnly();
        public int TotalItems => _basketProducts.Sum(i => i.Quantity);
        private Basket()
        {
            
        }
        public Basket(string buyerId)
        {
            BuyerId = buyerId;
        }
        public void AddItem(int productId, decimal unitPrice, int quantity = 1)
        {
            if(!BasketProducts.Any(i=>i.ProductId == productId))
            {
                _basketProducts.Add(new BasketProduct(productId, quantity, unitPrice));
                return;
            }
            var existingProd = BasketProducts.First(bp => bp.ProductId == productId);
            existingProd.AddQuantity(quantity);
        }
        public void RemoveEmpty()
        {
            _basketProducts.RemoveAll(i=>i.Quantity == 0);
        }
        public void SetNewBuyerId(string buyerId)
        {
            BuyerId = buyerId;
        }
    }
}

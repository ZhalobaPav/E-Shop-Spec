using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities.Order
{
    public class ProductOrder
    {
        public int ProductId { get;private set; }
        public string ProductName { get; private set;}
        public string PictureUri { get; private set;}
        public ProductOrder(int productId, string productName, string pictureUri)
        {
            Guard.Against.NullOrEmpty(productName, nameof(productName));
            Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));
            Guard.Against.OutOfRange(productId, nameof(productId), 1, int.MaxValue);
            ProductId = productId;
            ProductName = productName;
            PictureUri = pictureUri;
        }
    }
}

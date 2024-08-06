using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Enities
{
    public class Product:BaseEntity, IAggreagateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string PictureUri { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public int BrandId { get; private set; }
        public Brand Brand { get; private set; }
        public bool? HotSeller { get; set; }
        public bool? BestSeller { get; set; }
        public Product(string name, 
            string description, 
            decimal price, 
            string pictureUri, 
            int categoryId, 
            int brandId)
        {
             Name = name;
            Description = description;
            Price = price;
            PictureUri = pictureUri;
            CategoryId = categoryId;
            BrandId = brandId;
        }
        public void UpdateDetails(ProductDetails details)
        {
            Guard.Against.Null(details.Name, nameof(details.Name));
            Guard.Against.Null(details.Description, nameof(details.Description));
            Guard.Against.NegativeOrZero(details.Price, nameof(details.Price));
            Name = details.Name;
            Description = details.Description;
            Price = details.Price;
        }
        public void UpdateBrand(int brandId)
        {
            Guard.Against.Zero(brandId, nameof(brandId));
            BrandId = brandId;
        }
        public void UpdateCategory(int categoryId)
        {
            Guard.Against.Zero(categoryId, nameof(categoryId));
            CategoryId = categoryId;
        }
        public void UpdatePictureUri(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                PictureUri = string.Empty;
                return;
            }
            PictureUri = $"images\\products\\{pictureName}?{new DateTime().Ticks}";
        }
        public readonly record struct ProductDetails
        {
            public string? Name { get; }
            public string? Description { get; }
            public decimal Price { get; }
            public ProductDetails(string? name, string? description, decimal price)
            {
                Name =name; 
                Description =description;
                Price = price;
            }
        }
    }
}

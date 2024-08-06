using E_Js.Base;
using System.ComponentModel.DataAnnotations;

namespace E_Js.Requests.BasketRequest
{
    public class BasketOrderRequest:BaseRequest
    {
        public int Id { get; set; }
        public int CatalogItemId { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OldUnitPrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be bigger than 0")]
        public int Quantity { get; set; }

        public string? PictureUrl { get; set; }
    }
}

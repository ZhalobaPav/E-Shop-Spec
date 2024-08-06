using E_Js.Base;

namespace E_Js.Requests.ProductRequests
{
    public class CreateProductRequest : BaseRequest
    {
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public string PictureBase64 { get; set; }
        public string PictureName { get; set; }
        public decimal Price { get; set; }
    }
}

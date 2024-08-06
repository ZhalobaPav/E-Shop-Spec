using E_Js.Base;
using E_Js.Dtos;

namespace E_Js.Responses.Product
{
    public class CreateProductResponse : BaseResponse
    {
        public CreateProductResponse(Guid correlationId) : base(correlationId)
        {

        }
        public CreateProductResponse()
        {

        }
        public ProductDto productDto { get; set; }
    }
}

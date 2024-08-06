using E_Js.Base;
using E_Js.Dtos;

namespace E_Js.Responses.Product
{
    public class UpdateProductResponse : BaseResponse
    {
        public UpdateProductResponse()
        {

        }
        public UpdateProductResponse(Guid correlationId) : base(correlationId)
        {

        }
        public ProductDto Product { get; set; }
    }
}

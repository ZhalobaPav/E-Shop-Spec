using ApplicationCore.Enities;
using E_Js.Base;
using E_Js.Dtos;

namespace E_Js.Responses.Product
{
    public class GetByIdProductResponse : BaseResponse
    {
        public GetByIdProductResponse()
        {
        }
        public GetByIdProductResponse(Guid correlationId) : base(correlationId)
        {

        }
        public ProductDto Product { get; set; }
    }
}

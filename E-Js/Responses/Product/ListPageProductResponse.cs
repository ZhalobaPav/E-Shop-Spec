using E_Js.Base;
using E_Js.Dtos;

namespace E_Js.Responses.Product
{
    public class ListPageProductResponse : BaseResponse
    {
        public ListPageProductResponse(Guid correlationId) : base(correlationId)
        {
        }
        public ListPageProductResponse()
        {
        }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
        public int PageCount { get; set; }
    }
}

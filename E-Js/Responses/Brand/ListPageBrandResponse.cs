using E_Js.Base;
using E_Js.Dtos;

namespace E_Js.Responses.Brand
{
    public class ListPageBrandResponse : BaseResponse
    {
        public ListPageBrandResponse(Guid correlationId) : base(correlationId)
        {

        }
        public ListPageBrandResponse()
        {

        }
        public List<BrandDto> brandDtos { get; set; } = new List<BrandDto>();
    }
}

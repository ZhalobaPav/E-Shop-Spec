using ApplicationCore.Enities;
using E_Js.Base;
using E_Js.Dtos;

namespace E_Js.Responses.Category
{
    public class ListPageCategoryResponse : BaseResponse
    {
        public ListPageCategoryResponse(Guid correlationId) : base(correlationId)
        {

        }
        public ListPageCategoryResponse()
        {

        }
        public List<CategoryDto> categories { get; set; } = new List<CategoryDto>();
    }
}

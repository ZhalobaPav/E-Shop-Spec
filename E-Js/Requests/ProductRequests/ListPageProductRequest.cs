using E_Js.Base;
using E_Js.Helper;

namespace E_Js.Requests.ProductRequests
{
    public class ListPageProductRequest : BaseRequest
    {
        public int PageSize { get; init; }
        public int PageIndex { get; init; }
        public int? BrandId { get; init; }
        public int? CategoryId { get; init; }
        public SortParam Sort { get; set; } = new SortParam();
        public string SearchTerm { get; set; } = null;
        public ListPageProductRequest(int? pageSize, int? pageIndex, int? brandId, int? categoryId, SortParam sortParam)
        {
            PageSize = pageSize ?? 0;
            PageIndex = pageIndex ?? 0;
            BrandId = brandId;
            CategoryId = categoryId;
            Sort = sortParam;
        }
    }
}

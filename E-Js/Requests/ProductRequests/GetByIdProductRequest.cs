using E_Js.Base;

namespace E_Js.Requests.ProductRequests
{
    public class GetByIdProductRequest : BaseRequest
    {
        public GetByIdProductRequest(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; init; }
    }
}

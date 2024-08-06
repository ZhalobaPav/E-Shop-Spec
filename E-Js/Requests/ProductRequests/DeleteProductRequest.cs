using E_Js.Base;

namespace E_Js.Requests.ProductRequests
{
    public class DeleteProductRequest : BaseRequest
    {
        public int ProductId { get; init; }

        public DeleteProductRequest(int productId)
        {
            ProductId = productId;
        }
    }
}

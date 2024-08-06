using E_Js.Base;

namespace E_Js.Responses.Product
{
    public class DeleteProductResponse : BaseResponse
    {
        public DeleteProductResponse(Guid correlationId) : base(correlationId)
        {

        }
        public DeleteProductResponse()
        {

        }
        public string Status { get; set; } = "Deleted";
    }
}

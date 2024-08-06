using E_Js.Base;

namespace E_Js.Responses
{
    public class RegisterResponse : BaseResponse
    {
        public RegisterResponse(Guid correlationId) : base(correlationId)
        {

        }
        public RegisterResponse()
        {

        }
        public bool Success { get; set; }
    }
}

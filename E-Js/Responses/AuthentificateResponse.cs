using E_Js.Base;

namespace E_Js.Responses
{
    public class AuthentificateResponse : BaseResponse
    {
        public AuthentificateResponse(Guid correlationId) : base(correlationId)
        {

        }
        public AuthentificateResponse()
        {

        }
        public bool Result { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool IsLockedOut { get; set; } = false;
        public bool IsNotAllowed { get; set; } = false;
        public bool RequiresTwoFactor { get; set; } = false;
    }
}

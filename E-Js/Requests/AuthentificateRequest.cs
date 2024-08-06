using E_Js.Base;

namespace E_Js.Requests
{
    public class AuthentificateRequest : BaseRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

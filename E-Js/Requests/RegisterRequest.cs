using E_Js.Base;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Shared.Enums;

namespace E_Js.Requests
{
    public class RegisterRequest : BaseRequest
    {
        public UserType UserType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Password { get; set; }
        public ApplicationUser ToEntity()
        {
            return new ApplicationUser()
            {
                UserType = UserType,
                UserName = Email,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Password = Password
            };
        }
    }
}

using E_Js.Base;
using Infrastructure.Identity;

namespace E_Js.Responses
{
    public class ListGetUsersResponse:BaseResponse
    {
        public ListGetUsersResponse(Guid correlationId):base(correlationId) 
        {
            
        }
        public ListGetUsersResponse()
        {
            
        }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}

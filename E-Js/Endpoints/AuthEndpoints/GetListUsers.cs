using ApplicationCore.Interfaces;
using E_Js.Responses;
using Infrastructure.Identity;
using MinimalApi.Endpoint;

namespace E_Js.Endpoints.AuthEndpoints
{
    public class GetListUsers : IEndpoint<IResult, IRepository<ApplicationUser>>
    {
        private readonly UserService userService;

        public GetListUsers(UserService userService)
        {
            this.userService = userService;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/users", async (IRepository<ApplicationUser> userRepository) =>
            {
               return await HandleAsync(userRepository);
            }).Produces<List<ApplicationUser>>()
              .WithTags("AuthEndpoints");
        }

        public async Task<IResult> HandleAsync(IRepository<ApplicationUser> userRepository)
        {
            var response  = new ListGetUsersResponse();
            var items = await userService.GetUsers();
            response.Users.AddRange(items);
            return Results.Ok(items);
        }
    }
}

using ApplicationCore.Constants;
using ApplicationCore.Exceptions;
using Ardalis.ApiEndpoints;
using E_Js.Requests;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace E_Js.Endpoints.AuthEndpoints
{
    public class RegisterEndpoint : EndpointBaseAsync
        .WithRequest<RegisterRequest>
        .WithActionResult<bool>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterEndpoint(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost("api/register")]
        [SwaggerOperation(Tags = new[]
        {
            "AuthEndpoints"
        })]
        public override async Task<ActionResult<bool>> HandleAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var createdUser = await userManager.CreateAsync(request.ToEntity(), request.Password);
            if (!createdUser.Succeeded)
            {
                throw new BadRequestException(new Dictionary<string, string>(createdUser.Errors
                    .Select(e => new KeyValuePair<string, string>(e.Code, e.Description))));
            }
            ApplicationUser user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                user = await userManager.FindByNameAsync(request.Email);
            }
            var addUserToRole = await userManager.AddToRoleAsync(user, user.UserType.ToString());
            if (!addUserToRole.Succeeded)
            {
                throw new BadRequestException(new Dictionary<string, string>(
                    addUserToRole.Errors.Select(e => new KeyValuePair<string, string>(e.Code, e.Description))));
            }
            return user != null;
        }
    }
}

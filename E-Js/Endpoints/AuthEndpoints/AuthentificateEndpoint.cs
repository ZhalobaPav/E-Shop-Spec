using ApplicationCore.Interfaces;
using Ardalis.ApiEndpoints;
using E_Js.Requests;
using E_Js.Responses;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace E_Js.Endpoints.AuthEndpoints
{
    public class AuthentificateEndpoint : EndpointBaseAsync
        .WithRequest<AuthentificateRequest>
        .WithActionResult<AuthentificateResponse>
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenClaimService tokenClaimService;

        public AuthentificateEndpoint(SignInManager<ApplicationUser> signInManager, ITokenClaimService tokenClaimService)
        {
            this.signInManager = signInManager;
            this.tokenClaimService = tokenClaimService;
        }
        [HttpPost("api/authenticate")]
        [SwaggerOperation(Summary = "Authentificates user",
        Description = "Authenticates a user",
        OperationId = "auth.authenticate",
        Tags = new[] { "AuthEndpoints" })]
        public override async Task<ActionResult<AuthentificateResponse>> HandleAsync(AuthentificateRequest request, CancellationToken cancellationToken = default)
        {
            var response = new AuthentificateResponse(request.CorreltaionId());
            var result = await signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);
            response.Result = result.Succeeded;
            response.IsLockedOut = result.IsLockedOut;
            response.IsNotAllowed = result.IsNotAllowed;
            response.RequiresTwoFactor = result.RequiresTwoFactor;
            response.Username = request.Username;
            if (result.Succeeded)
            {
                response.Token = await tokenClaimService.GetTokenAsync(request.Username);
            }
            return response;
        }
    }
}

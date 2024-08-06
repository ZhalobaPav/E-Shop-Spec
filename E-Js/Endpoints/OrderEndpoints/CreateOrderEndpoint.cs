
using ApplicationCore.Constants;
using ApplicationCore.Enities.Order;
using ApplicationCore.Interfaces;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Endpoint;
using System.Security.Claims;
using ViewModels.Basket;
namespace E_Js.Endpoints.OrderEndpoints
{
    public class CreateOrderEndpoint : IEndpoint<IResult, IEnumerable<BasketProductViewModel>>
    {
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;
        private readonly IBasketViewModelService basketViewModelService;
        private readonly ICustomLogger<CreateOrderEndpoint> logger;
        private readonly HttpContext httpContext;
        public BasketViewModel BasketModel { get; set; } = new BasketViewModel();

        public CreateOrderEndpoint(
            IBasketService basketService, 
            IOrderService orderService, 
            IBasketViewModelService basketViewModelService, 
            ICustomLogger<CreateOrderEndpoint> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            this.basketService = basketService;
            this.orderService = orderService;
            this.basketViewModelService = basketViewModelService;
            this.logger = logger;
            this.httpContext = httpContextAccessor.HttpContext;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapPost("api/orders", [Authorize(Roles = AuthorizationConstants.CUSTOMER, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (List<BasketProductViewModel> items) =>
            {
                await HandleAsync(items);
            }).WithTags("OrderEndpoints");
        }

        public async Task<IResult> HandleAsync(IEnumerable<BasketProductViewModel> items)
        {
            string userId = GetUserIdFromClaims();

            if (string.IsNullOrEmpty(userId))
            {
                logger.LogWarning("User ID not found in claims.");
                return Results.BadRequest("User not authenticated.");
            }

            await SetBasketModelAsync(userId);

            if (!BasketModel.Items.Any())
            {
                logger.LogWarning("Basket is empty during checkout.");
                return Results.BadRequest("Basket is empty.");
            }

            var updatedModel = items.ToDictionary(b => b.Id.ToString(), b => b.Quantity);
            await basketService.SetQuantities(BasketModel.Id, updatedModel);
            await orderService.CreateOrderAsync(BasketModel.Id, new Address("123 Main St.", "Kent", "OH", "United States", "44240"));
            await basketService.DeleteBasketAsync(BasketModel.Id);

            return Results.Ok();
        }
        private async Task SetBasketModelAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            BasketModel = await basketViewModelService.GetOrCreateBasketForUser(userId);
        }
        private string GetUserIdFromClaims()
        {
            var claimsIdentity = httpContext.User.Identity as ClaimsIdentity;
            return claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}

using ApplicationCore.Constants;
using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using E_Js.Requests.ProductRequests;
using E_Js.Responses.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Endpoint;

namespace E_Js.Endpoints.ProductEndpoints
{
    public class DeleteProductEndpoint : IEndpoint<IResult, DeleteProductRequest, IRepository<Product>>
    {
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapDelete("products/{productId}", [Authorize(Roles = AuthorizationConstants.ADMINISTRATOR, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
            async (int productId, IRepository<Product> productrepository) =>
            {
                await HandleAsync(new DeleteProductRequest(productId), productrepository); 
            })
                .Produces<DeleteProductResponse>()
                .WithTags("ProductEndpoints");
        }

        public async Task<IResult> HandleAsync(DeleteProductRequest request, IRepository<Product> productRepository)
        {
            var response = new DeleteProductResponse(request.CorreltaionId());
            var itemToDelete = await productRepository.GetByIdAsync(request.ProductId);
            if(itemToDelete is null) 
            {
                return Results.NotFound();
            }
            await productRepository.DeleteAsync(itemToDelete);
            return Results.Ok(response);
        }
    }
}

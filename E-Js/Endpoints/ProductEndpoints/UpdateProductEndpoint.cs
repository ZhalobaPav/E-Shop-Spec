using ApplicationCore.Constants;
using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using E_Js.Dtos;
using E_Js.Requests.ProductRequests;
using E_Js.Responses.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Endpoint;

namespace E_Js.Endpoints.ProductEndpoints
{
    public class UpdateProductEndpoint : IEndpoint<IResult, UpdateProductRequest, IRepository<Product>>
    {
        private readonly IUriComposer uriComposer;

        public UpdateProductEndpoint(IUriComposer uriComposer)
        {
            this.uriComposer = uriComposer;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapPut("api/products/{productId}", [Authorize(Roles = AuthorizationConstants.ADMINISTRATOR, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (UpdateProductRequest request, IRepository<Product> productRepository) =>
            {
                await HandleAsync(request, productRepository);
            }).Produces<UpdateProductResponse>()
                .WithTags("ProductEndpoints");
        }

        public async Task<IResult> HandleAsync(UpdateProductRequest request, IRepository<Product> productRepository)
        {
            var response = new UpdateProductResponse(request.CorreltaionId());
            var existingItem = await productRepository.GetByIdAsync(request.Id);
            if(existingItem == null) 
            {
                return Results.NotFound();
            }
            Product.ProductDetails productDetails = new Product.ProductDetails(request.Name, request.Description, request.Price);
            existingItem.UpdateDetails(productDetails);
            existingItem.UpdateBrand(request.BrandId);
            existingItem.UpdateCategory(request.CategoryId);
            await productRepository.UpdateAsync(existingItem);
            var dto = new ProductDto()
            {
                Id = existingItem.Id,
                Name = existingItem.Name,
                Description = existingItem.Description,
                Price = existingItem.Price,
                BrandId = existingItem.BrandId,
                CategoryId = existingItem.CategoryId,
                PictureUri = uriComposer.ComposePicUri(existingItem.PictureUri)
            };
            response.Product = dto;
            return Results.Ok(response);
        }
    }
}

using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using E_Js.Requests.ProductRequests;
using E_Js.Responses.Product;
using Microsoft.CodeAnalysis;
using MinimalApi.Endpoint;

namespace E_Js.Endpoints.ProductEndpoints
{
    public class ProductGetByIdEndpoint : IEndpoint<IResult, GetByIdProductRequest, IRepository<Product>>
    {
        private readonly IUriComposer uriComposer;

        public ProductGetByIdEndpoint(IUriComposer uriComposer)
        {
            this.uriComposer = uriComposer;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/products/{productId}", async (int productId, IRepository<Product> productRepository) =>
            {
                return await HandleAsync(new GetByIdProductRequest(productId), productRepository);
            }).Produces<GetByIdProductResponse>().WithTags("ProductEndpoints");
        }

        public async Task<IResult> HandleAsync(GetByIdProductRequest request, IRepository<Product> productRepository)
        {
            var response = new GetByIdProductResponse(request.CorreltaionId());
            var item = await productRepository.GetByIdAsync(request.ProductId);
            if(item is null)
            {
                return Results.NotFound();
            }
            response.Product = new Dtos.ProductDto()
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                BrandId = item.BrandId,
                Description = item.Description,
                Name = item.Name,
                PictureUri = uriComposer.ComposePicUri(item.PictureUri),
                Price = item.Price,
            };
            return Results.Ok(response);
        }
    }
}

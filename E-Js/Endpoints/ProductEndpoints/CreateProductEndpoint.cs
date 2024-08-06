using ApplicationCore.Constants;
using ApplicationCore.Enities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using E_Js.Dtos;
using E_Js.Requests.ProductRequests;
using E_Js.Responses.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Endpoint;

namespace E_Js.Endpoints.ProductEndpoints
{
    public class CreateProductEndpoint : IEndpoint<IResult, CreateProductRequest, IRepository<Product>>
    {
        private readonly IUriComposer uriComposer;

        public CreateProductEndpoint(IUriComposer uriComposer)
        {
            this.uriComposer = uriComposer;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapPost("api/products", [Authorize(Roles = AuthorizationConstants.ADMINISTRATOR, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async 
                (CreateProductRequest request, IRepository<Product> productRepository) =>
            {
                await HandleAsync(request, productRepository);
            }).Produces<CreateProductResponse>()
                .WithTags("ProductEndpoints");
        }

        public async Task<IResult> HandleAsync(CreateProductRequest request, IRepository<Product> productRepository)
        {
            var response = new CreateProductResponse(request.CorreltaionId());
            var productNameSpec = new ProductFilterNameSpecification(request.Name);
            var existingProduct = await productRepository.CountAsync(productNameSpec);
            if(existingProduct > 0)
            {
                throw new DuplicateException($"A product with name{request.Name} already exists");
            }
            var newItem = new Product(request.Name, request.Description,request.Price, request.PictureUri, request.CategoryId, request.BrandId);
            newItem = await productRepository.AddAsync(newItem);
            var dto = new ProductDto()
            {
                Id = newItem.Id, 
                Name = newItem.Name,
                Description = newItem.Description,
                Price = newItem.Price,
                PictureUri = uriComposer.ComposePicUri(newItem.PictureUri),
                CategoryId = newItem.CategoryId,
                BrandId = newItem.BrandId,
            };
            response.productDto = dto;
            return Results.Created($"api/products/{dto.Id}",response);
        }
    }
}

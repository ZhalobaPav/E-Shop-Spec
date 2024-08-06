using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using AutoMapper;
using E_Js.Dtos;
using E_Js.Helper;
using E_Js.Requests.ProductRequests;
using E_Js.Responses.Product;
using MinimalApi.Endpoint;
using Shared.Enums;

namespace E_Js.Endpoints.ProductEndpoints
{
    public class ListPageProductEndpoint : IEndpoint<IResult, ListPageProductRequest, IRepository<Product>>
    {
        private readonly IMapper mapper;
        private readonly IUriComposer uriComposer;

        public ListPageProductEndpoint(IMapper mapper, IUriComposer uriComposer)
        {
            this.mapper = mapper;
            this.uriComposer = uriComposer;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/products", async (int? pageSize, int? pageIndex, int? brandId, int? categoryId, string? sortBy, byte? sortDirection, IRepository <Product> productRepository) =>
            {
                return await HandleAsync(new ListPageProductRequest(pageSize, pageIndex, brandId, categoryId, new SortParam(sortBy, sortDirection)), productRepository);
            }).Produces<ListPageProductResponse>().WithTags("ProductEndpoints");
        }

        public async Task<IResult> HandleAsync(ListPageProductRequest request, IRepository<Product> productRepository)
        {
            await Task.Delay(1000);
            var response = new ListPageProductResponse(request.CorreltaionId());
            var filterSpec = new ProductFilterSpecification(request.BrandId, request.CategoryId);
            int totalItems = await productRepository.CountAsync(filterSpec);
            var pageSpec = new ProductFilterPaginatedSpecification(skip: request.PageIndex * request.PageSize, take: request.PageSize,
                    brandId: request.BrandId, categoryId: request.CategoryId);
            var products = await productRepository.ListAsync(pageSpec);
            products = SortHelper<Product>.Sort(products, request.Sort);
            response.Products.AddRange(products.Select(mapper.Map<ProductDto>));
            foreach(ProductDto item in response.Products)
            {
                item.PictureUri = uriComposer.ComposePicUri(item.PictureUri);
            }
            if (request.PageSize > 0)
            {
                response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
            }
            else
            {
                response.PageCount = totalItems > 0 ? 1 : 0;
            }
            return Results.Ok(response);
        }
    }
}

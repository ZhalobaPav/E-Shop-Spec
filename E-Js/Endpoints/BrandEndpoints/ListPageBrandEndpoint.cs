using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using AutoMapper;
using E_Js.Dtos;
using E_Js.Responses.Brand;
using MinimalApi.Endpoint;

namespace E_Js.Endpoints.BrandEndpoints
{
    public class ListPageBrandEndpoint : IEndpoint<IResult, IRepository<Brand>>
    {
        private readonly IMapper mapper;

        public ListPageBrandEndpoint(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/brands", async (IRepository<Brand> brandRepository) =>
            {
                return await HandleAsync(brandRepository);
            }).Produces<ListPageBrandResponse>()
               .WithTags("BrandEndpoints");
        }

        public async Task<IResult> HandleAsync(IRepository<Brand> brandRepository)
        {
            var response = new ListPageBrandResponse();
            var items = await brandRepository.ListAsync();
            response.brandDtos.AddRange(items.Select(mapper.Map<BrandDto>));
            return Results.Ok(response);
        }
    }
}

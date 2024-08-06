using ApplicationCore.Enities;
using ApplicationCore.Interfaces;
using AutoMapper;
using E_Js.Dtos;
using E_Js.Responses.Category;
using MinimalApi.Endpoint;
namespace E_Js.Endpoints.CategoryEndpoints
{
    public class ListPageCategoryEndpoint : IEndpoint<IResult, IRepository<Category>>
    {
        private readonly IMapper mapper;

        public ListPageCategoryEndpoint(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public void AddRoute(IEndpointRouteBuilder app)
        {
            app.MapGet("api/categories", async (IRepository<Category> categoryRepository) =>
            {
                return await HandleAsync(categoryRepository);
            }).Produces<ListPageCategoryResponse>()
            .WithTags("CategoryEndpoints");
        }

        public async Task<IResult> HandleAsync(IRepository<Category> categoryRepository)
        {
            var response = new ListPageCategoryResponse();
            var items = await categoryRepository.ListAsync();
            response.categories.AddRange(items.Select(mapper.Map<CategoryDto>));
            return Results.Ok(response);
        }
    }
}

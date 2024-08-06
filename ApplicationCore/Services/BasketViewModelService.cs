using ApplicationCore.Enities;
using ApplicationCore.Enities.Basket;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Infrastructure.Data.Querries;
using ViewModels.Basket;

namespace ApplicationCore.Services
{
    public class BasketViewModelService : IBasketViewModelService
    {
        private readonly IRepository<Basket> basketRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IUriComposer uriComposer;
        private readonly IBasketQueryService basketQueryService;

        public BasketViewModelService(
            IRepository<Basket> basketRepository,
            IRepository<Product> productRepository,
            IUriComposer uriComposer, IBasketQueryService
            basketQueryService)
        {
            this.basketRepository = basketRepository;
            this.productRepository = productRepository;
            this.uriComposer = uriComposer;
            this.basketQueryService = basketQueryService;
        }
        public async Task<BasketViewModel> GetOrCreateBasketForUser(string userName)
        {
            var basketSpec = new BasketWithProductsSpecification(userName);
            var basket = await basketRepository.FirstOrDefaultAsync(basketSpec);
            if (basket == null)
            {
                return await CreateBasketForUser(userName);
            }
            var viewModel = await Map(basket);
            return viewModel;
        }
        public async Task<int> CountTotalProducts(string userName)
        {
            var counter = await basketQueryService.CountTotalProducts(userName);
            return counter;
        }
        private async Task<BasketViewModel> CreateBasketForUser(string userId)
        {
            var basket = new Basket(userId);
            await basketRepository.AddAsync(basket);
            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
            };
        }
        public async Task<BasketViewModel> Map(Basket basket)
        {
            return new BasketViewModel()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                Items = await GetProducts(basket.BasketProducts)
            };
        }

        private async Task<List<BasketProductViewModel>> GetProducts(IReadOnlyCollection<BasketProduct> basketItems)
        {
            var productsSpec = new ProductSpecification(basketItems.Select(b => b.ProductId).ToArray());
            var products = await productRepository.ListAsync(productsSpec);
            var items = basketItems.Select(basketItem =>
            {
                var product = products.First(p => p.Id == basketItem.ProductId);
                var basketItemViewModel = new BasketProductViewModel()
                {
                    Id = basketItem.Id,
                    UnitPrice = basketItem.UnitPrice,
                    Quantity = basketItem.Quantity,
                    ProductId = basketItem.ProductId,
                    PictureUrl = uriComposer.ComposePicUri(product.PictureUri),
                    ProductName = product.Name
                };
                return basketItemViewModel;
            }).ToList();
            return items;
        }
    }
}

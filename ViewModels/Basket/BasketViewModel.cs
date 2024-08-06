namespace ViewModels.Basket
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public List<BasketProductViewModel> Items { get; set; } = new List<BasketProductViewModel>();
        public string? BuyerId { get; set; }

        public decimal Total()
        {
            return Math.Round(Items.Sum(x => x.UnitPrice * x.Quantity), 2);
        }
    }
}

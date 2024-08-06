using ApplicationCore.Enities.Order;

namespace UnitTests.Builders
{
    public class OrderBuilder
    {
        private Order _order;
        public string TestBuyerId => "12345";
        public int TestProductId => 234;
        public string TestProductName => "Test Product Name";
        public string TestPictureUri => "http://test.com/image.jpg";
        public decimal TestUnitPrice = 1.23m;
        public int TestUnits = 3;
        public ProductOrder TestProductOrder { get; }
        public OrderBuilder()
        {
            TestProductOrder = new ProductOrder(TestProductId, TestProductName, TestPictureUri);
            _order = WithDefaultValues();
        }
        public Order WithNoItems()
        {
            _order = new Order(TestBuyerId, new AddressBuilder().WithDefaultValues(), new List<OrderProductDetails>());
            return _order;
        }
        public Order WithItems(List<OrderProductDetails> items)
        {
            _order = new Order(TestBuyerId, new AddressBuilder().WithDefaultValues(), items);
            return _order;
        }
        public Order WithDefaultValues()
        {
            var orderItem = new OrderProductDetails(TestProductOrder, TestUnitPrice, TestUnits);
            var itemList = new List<OrderProductDetails>() { orderItem };
            _order = new Order(TestBuyerId, new AddressBuilder().WithDefaultValues(), itemList);
            return _order;
        }
    }
}

using System;
using Xunit;

namespace CapgeminiSample.Tests
{
    public class CustomersControllerTests
    {
        [Fact]
        public void GetAllCustomers_ShouldReturnAllProducts()
        {
            var testProducts = GetTestProducts();
            var controller = new SimpleProductController(testProducts);

            var result = controller.GetAllProducts() as List<Product>;
            Assert.AreEqual(testProducts.Count, result.Count);
        }
    }
}

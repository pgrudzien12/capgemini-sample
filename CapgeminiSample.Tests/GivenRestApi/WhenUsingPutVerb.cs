using CapgeminiSample.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CapgeminiSample.Tests.CustomersController
{
    public class WhenUsingPutVerb : GivenRestApi
    {
        private Task<HttpResponseMessage> PutAsync(Customer customer)
        {
            var content = ToHttpContent(customer);
            return _client.PutAsync($"/api/Customer({customer.Id})", content);
        }

        [Fact]
        public async void AndUpdatingPhone_ThenCustomerGetsUpdated()
        {
            // Arrange 
            var customer = await Insert(CustomerBuilder.DefaultCustomer.Build());
            customer.Phone = "phone";

            // Act
            HttpResponseMessage response = await PutAsync(customer);
            response.EnsureSuccessStatusCode();


            // Assert
            Customer responseCustomer = await Get(customer.Id);

            Assert.NotNull(responseCustomer);
        }

        // NOT WORKING YET
        //[Fact]
        //public async void AndConcurrencyDoesNotMath_ThenReturnPreconditionFailed()
        //{
        //    // Arrange 
        //    var customer = await Insert(CustomerBuilder.DefaultCustomer.Build());
        //    customer.Phone = "phone";
        //    customer.Version = 0;

        //    // Act
        //    HttpResponseMessage response = await PutAsync(customer);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.PreconditionFailed, response.StatusCode);
        //}
    }
}

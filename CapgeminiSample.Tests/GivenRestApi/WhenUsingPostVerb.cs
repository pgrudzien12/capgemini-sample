using CapgeminiSample.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CapgeminiSample.Tests.CustomersController
{
    public class WhenUsingPostVerb : GivenRestApi
    {
        [Fact]
        public async void AndARegularCustomer_ThenReturnCustomerObject()
        {
            // Arrange 
            var customer = CustomerBuilder.DefaultCustomer.Build();

            // Act
            HttpResponseMessage response = await PostAsync(customer);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Customer responseCustomer = JsonConvert.DeserializeObject<Customer>(responseString);

            Assert.NotNull(responseCustomer);
            Assert.True(responseCustomer.Id > 0);
        }

        [Fact]
        public async void AndACustomerWithoutName_ThenReturnBadRequest()
        {
            // Arrange 
            var customer = CustomerBuilder.DefaultCustomer.WithoutName().Build();

            // Act
            HttpResponseMessage response = await PostAsync(customer);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private Task<HttpResponseMessage> PostAsync(Customer customer)
        {
            var content = ToHttpContent(customer);
            return _client.PostAsync("/api/Customer", content);
        }
    }
}

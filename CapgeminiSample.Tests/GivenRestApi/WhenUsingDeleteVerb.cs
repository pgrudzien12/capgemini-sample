using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CapgeminiSample.Tests.CustomersController
{
    public class WhenUsingDeleteVerb : GivenRestApi
    {
        private Task<HttpResponseMessage> DeleteAsync(int id)
        {
            return _client.DeleteAsync($"/api/Customer({id})"); ;
        }

        [Fact]
        public async void AndDeletingNonExistingUser_ThenReturnNotFound()
        {
            // No Arrange

            // Act
            var response = await DeleteAsync(200);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async void AndDeletingExistingUser_ThenReturnNoContent()
        {
            // Arrange
            var customer = CustomerBuilder.DefaultCustomer.Build();
            var responseCustomer = await Insert(customer);

            // Act
            var response = await DeleteAsync(responseCustomer.Id);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

    }
}

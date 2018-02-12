using CapgeminiSample.Controllers;
using CapgeminiSample.Model;
using CapgeminiSample.Tests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using Xunit;

namespace CapgeminiSample.Tests
{
    public class CustomersControllerShould
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CustomersControllerShould()
        {
            // Arrange
            // TODO: Use test startup that uses mem db and mocked data
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async void ReturnCustomers()
        {
            // Act
            var response = await _client.GetAsync("/api/Customer");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // TODO: check for something meningful...
            Assert.NotEmpty(responseString);
        }

        [Fact]
        public async void DeleteCustomer()
        {
            // Act
            var response = await _client.DeleteAsync("/api/Customer/0");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // TODO: check for something meningful...
            Assert.NotEmpty(responseString);
        }
    }
}

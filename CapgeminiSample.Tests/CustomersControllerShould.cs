using CapgeminiSample.Controllers;
using CapgeminiSample.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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

        //[Fact]
        //public async void DeleteCustomer()
        //{
        //    // Act
        //    var response = await _client.DeleteAsync("/api/Customer/0");
        //    response.EnsureSuccessStatusCode();

        //    var responseString = await response.Content.ReadAsStringAsync();

        //    // TODO: check for something meningful...
        //    Assert.NotEmpty(responseString);
        //}

        //[Fact]
        //public async void InsertCustomer()
        //{
        //    // Arrange 
        //    var c = new Customer() { Name = "abc", Surname = "bdc" };
        //    var jsonString = JsonConvert.SerializeObject(c);
        //    var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _client.PostAsync("/api/Customer", content);
        //    response.EnsureSuccessStatusCode();

        //    var responseString = await response.Content.ReadAsStringAsync();

        //    // TODO: check for something meningful...
        //    Assert.NotEmpty(responseString);
        //}
    }
}

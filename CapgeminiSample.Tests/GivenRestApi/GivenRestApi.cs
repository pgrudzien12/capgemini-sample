using CapgeminiSample.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CapgeminiSample.Tests.CustomersController
{
    public class GivenRestApi
    {
        protected readonly TestServer _server;
        protected readonly HttpClient _client;

        public GivenRestApi()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        protected static StringContent ToHttpContent(Customer c)
        {
            var jsonString = JsonConvert.SerializeObject(c);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return content;
        }

        protected async Task<Customer> Insert(Customer customer)
        {
            var response = await _client.PostAsync("/api/Customer", ToHttpContent(customer));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Customer>(await response.Content.ReadAsStringAsync());
        }

        protected async Task<Customer> Get(int id)
        {
            var response = await _client.GetAsync($"/api/Customer({id})");
            response.EnsureSuccessStatusCode();
            var getResponseAsString = await response.Content.ReadAsStringAsync();
            JObject jobject = JObject.Parse(getResponseAsString);
            string customer = jobject["value"][0].ToString();
            return JsonConvert.DeserializeObject<Customer>(customer);
        }
    }
}

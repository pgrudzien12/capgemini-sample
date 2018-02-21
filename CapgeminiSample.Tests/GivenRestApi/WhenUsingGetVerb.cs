using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CapgeminiSample.Tests.CustomersController
{
    public class WhenUsingGetVerb : GivenRestApi
    {
        [Fact]
        public async void AndQueryByKey_ThenReturnProperCustomer()
        {
            // Arrange 
            var customer = await Insert(CustomerBuilder.DefaultCustomer.Build());

            // Act
            var result = await Get(customer.Id);

            Assert.NotNull(result);
        }
    }
}

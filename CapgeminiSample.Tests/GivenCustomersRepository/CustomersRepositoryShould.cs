using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CapgeminiSample.Tests.GivenCustomersRepository
{
    public class EfCustomersRepositoryShould
    {
        [Fact]
        public void ReturnIdOfCustomerAfterCreation()
        {
            // Arrange
            DbContextOptions options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("InMemDb").Options;
            EfCustomerRepository repository = new EfCustomerRepository(new CapgeminiDbContext(options));

            // Act
            int id = repository.Create(new Model.Customer());

            // Assert
            Assert.True(id > 0);
        }
    }
}

using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapgeminiSample
{
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly CapgeminiDbContext dbContext;

        public EfCustomerRepository(CapgeminiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IQueryable<Customer> AsQueryable()
        {
            return dbContext.Customers.AsQueryable();
        }
    }
}

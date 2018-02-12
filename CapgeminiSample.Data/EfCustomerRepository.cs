using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapgeminiSample
{
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly CapgeminiDbContext dbContext;

        public EfCustomerRepository(CapgeminiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Customer customer)
        {
            dbContext.Customers.Add(customer);
        }

        public IQueryable<Customer> AsQueryable()
        {
            return dbContext.Customers.AsQueryable();
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}

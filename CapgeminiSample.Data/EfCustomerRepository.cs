using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using Microsoft.EntityFrameworkCore;
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

        public int Create(Customer customer)
        {
            return dbContext.Customers.Add(customer).Entity.Id;
        }

        public IQueryable<Customer> AsQueryable()
        {
            return dbContext.Customers.AsQueryable();
        }

        public Task<Customer> FindById(int id)
        {
            return dbContext.Customers.FindAsync(id);
        }

        public void Remove(Customer customer)
        {
            dbContext.Remove(customer);
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public void Update(Customer customerUpdate)
        {
            dbContext.Attach(customerUpdate);
            dbContext.Entry(customerUpdate).State = EntityState.Modified;
        }
    }
}

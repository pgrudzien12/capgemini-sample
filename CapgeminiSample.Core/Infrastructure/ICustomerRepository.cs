using CapgeminiSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapgeminiSample.Infrastructure
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> AsQueryable();
        int Create(Customer customer);
        Task<int> SaveChangesAsync();
        Task<Customer> FindById(int id);
        void Remove(Customer product);
        Task Update(Customer customerUpdate);
    }
}

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
        void Add(Customer customer);
        Task<int> SaveChangesAsync();
        Task<Customer> FindbyId(int id);
        void Remove(Customer product);
        void Update(Customer customerUpdate);
    }
}

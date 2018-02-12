using CapgeminiSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapgeminiSample.Infrastructure
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> AsQueryable();
    }
}

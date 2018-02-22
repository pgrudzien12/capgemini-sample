using AutoMapper;
using CapgeminiSample.Exceptions;
using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapgeminiSample.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository repository;
        private readonly IMapper mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IQueryable<CustomerDTO> Get()
        {
            IQueryable<Customer> customers = repository.AsQueryable();
            return customers.Select(c => mapper.Map<CustomerDTO>(c));
        }

        public async Task<CustomerDTO> SaveAndGet(CustomerDTO customer)
        {
            var customerId = repository.Create(mapper.Map<Customer>(customer));
            await repository.SaveChangesAsync();

            var result = await repository.FindById(customerId);
            return mapper.Map<CustomerDTO>(result);
        }

        public async Task<CustomerDTO> Update(CustomerDTO customerUpdate)
        {
            await repository.Update(mapper.Map<Customer>(customerUpdate));
            await repository.SaveChangesAsync();
            return customerUpdate;
        }

        public async Task Delete(int key)
        {
            var customer = await repository.FindById(key);
            if (customer == null)
            {
                throw new NotFoundException(nameof(Customer));
            }
            repository.Remove(customer);
            await repository.SaveChangesAsync();
        }
    }
}

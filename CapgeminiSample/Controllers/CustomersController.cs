using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CapgeminiSample.Controllers
{
    public class CustomerController : ODataController
    {
        private readonly ICustomerRepository repository;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(ICustomerRepository repository, ILogger<CustomerController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(repository.AsQueryable());
        }

        public async Task<IActionResult> Post(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Add(customer);
            await repository.SaveChangesAsync();
            return Created(customer);
        }
    }
}

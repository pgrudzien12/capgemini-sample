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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Add(customer);
            await repository.SaveChangesAsync();
            return Created(customer);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, Customer customerUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != customerUpdate.Id)
            {
                return BadRequest();
            }

            repository.Update(customerUpdate);

            try
            {
                await repository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ue)
            {
                logger.LogError(ue, "Db update error");
                return StatusCode(409);
            }
            catch (DbUpdateException ue)
            {
                logger.LogError(ue, "Db update error");
            }

            return Updated(customerUpdate);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var customer = await repository.FindbyId(key);
            if (customer == null)
            {
                return NotFound();
            }
            repository.Remove(customer);
            await repository.SaveChangesAsync();
            return NoContent();
        }
    }
}

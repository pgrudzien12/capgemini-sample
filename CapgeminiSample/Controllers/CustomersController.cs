using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
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

            var customerId = repository.Create(customer);
            await repository.SaveChangesAsync();

            var result = await repository.FindById(customerId);
            return Created(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Customer customerUpdate)
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
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
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
            if (key <= 0)
                return BadRequest("Not a valid customer id");
            var customer = await repository.FindById(key);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using CapgeminiSample.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CapgeminiSample.Controllers
{
    public class CustomerController : ODataController
    {
        private CustomerService Service { get; }
        private readonly ILogger<CustomerController> logger;

        public CustomerController(CustomerService customerService, ILogger<CustomerController> logger)
        {
            Service = customerService;
            this.logger = logger;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<CustomerDTO> Get()
        {
            return Service.Get();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Created(await Service.SaveAndGet(customer));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] CustomerDTO customerUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != customerUpdate.Id)
            {
                return BadRequest();
            }

            try
            {
                var afterUpdate = await Service.Update(customerUpdate);
                return Updated(afterUpdate);
            }
            catch (DbUpdateConcurrencyException ue)
            {
                logger.LogError(ue, "Db update error");
                return StatusCode((int)HttpStatusCode.PreconditionFailed);
            }
            catch (DbUpdateException ue)
            {
                logger.LogError(ue, "Db update error");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            if (key <= 0)
                return BadRequest("Not a valid customer id");

            await Service.Delete(key);
            
            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapgeminiSample.Model;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CapgeminiSample.Controllers
{
    public class CustomerController : ODataController
    {
        private readonly CapgeminiDbContext db;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(CapgeminiDbContext sampleODataDbContext, ILogger<CustomerController> logger)
        {
            this.db = sampleODataDbContext;
            this.logger = logger;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(db.Customers.AsQueryable());
        }

    }
}

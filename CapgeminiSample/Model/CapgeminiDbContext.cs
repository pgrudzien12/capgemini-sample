using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapgeminiSample.Model
{
    public class CapgeminiDbContext : DbContext
    {
        public CapgeminiDbContext(DbContextOptions options) : base(options)
        {
        }

        protected CapgeminiDbContext()
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}

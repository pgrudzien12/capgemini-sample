using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CapgeminiSample.Exceptions;
using CapgeminiSample.Infrastructure;
using CapgeminiSample.Model;
using CapgeminiSample.Services;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CapgeminiSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<CustomerDTO, Customer>();
            });
            services.AddSingleton(sp => config.CreateMapper());
            services.AddScoped<ICustomerRepository, EfCustomerRepository>()
                .AddScoped<CustomerService>()
                .AddDbContext<CapgeminiDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDb");
                });
            //Adding OData middleware.
            services.AddOData();
            services.AddMvc(setup =>
            {
                setup.Filters.Add<ApplicationLayerExceptionFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Adding Model class to OData
            var builder = new ODataConventionModelBuilder(app.ApplicationServices);
            builder.EntitySet<CustomerDTO>(nameof(Customer));
            //Enabling OData routing.
            app.UseMvc(routes =>
            {
                //routes.MapRoute("default", "api/{controller}/{action}/{id?}");
                routes.MapODataServiceRoute("odata", "api", builder.GetEdmModel());
            });
        }
    }
}

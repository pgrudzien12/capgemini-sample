using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CapgeminiSample.Exceptions
{
    public class DbLayerExceptionFilter : IAsyncActionFilter
    {
        private readonly ILogger logger;

        public DbLayerExceptionFilter(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger("database");
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // do something before the action executes
            var resultContext = await next();
            if (resultContext.Exception == null)
                return;

            if (resultContext.Exception is DbUpdateConcurrencyException)
            {
                resultContext.Result = new StatusCodeResult((int)HttpStatusCode.PreconditionFailed);
            }
            else if (resultContext.Exception is DbUpdateException)
                resultContext.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);

            if (resultContext.Exception is DbUpdateException)
            {
                logger.LogError(resultContext.Exception, "Db update error");
                resultContext.ExceptionHandled = true;
            }
        }
    }
}

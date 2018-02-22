using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CapgeminiSample.Exceptions
{
    public class ApplicationLayerExceptionFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // do something before the action executes
            var resultContext = await next();
            if (resultContext.Exception != null && resultContext.Exception is ApplicationLayerException)
                resultContext.ExceptionHandled = true;
            else
                return;

            resultContext.Result = new StatusCodeResult((int)HttpStatusCode.NotFound);
        }
    }
}

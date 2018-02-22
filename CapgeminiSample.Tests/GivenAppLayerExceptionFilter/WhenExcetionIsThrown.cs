using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FakeItEasy;

namespace CapgeminiSample.Tests.GivenAppLayerExceptionFilter
{
    public class WhenExcetionIsThrown : GivenAppLayerExceptionFilter
    {
        [Fact]
        public async void _OfTypeAppExcetion_ThenReturnNotFoundStatus()
        {
            // Arrange
            // Arrange
            var serviceProviderMock = A.Fake<IServiceProvider>();
            
            A.CallTo(() => serviceProviderMock.GetService(typeof(ILoggerFactory)))
                .Returns(Mock.Of<ILogger<ValidationFilterAttribute>>());
            var httpContext = new DefaultHttpContext();
            httpContext.RequestServices = serviceProviderMock.Object;
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var actionExecutingContext = new ActionExecutingContext(
                actionContext,
                filters: new List<IFilterMetadata>(), // for majority of scenarios you need not worry about populating this parameter
                actionArguments: new Dictionary<string, object>(), // if the filter uses this data, add some data to this dictionary
                controller: null); // since the filter being tested here does not use the data from this parameter, just provide null
            var validationFilter = new ValidationFilterAttribute();


            ActionExecutingContext context = A.Fake<ActionExecutingContext>(options=>options.);
            var exectuedContext = A.Fake<ActionExecutedContext>();
            A.CallTo(() => exectuedContext.Exception).Returns(new ApplicationException());

            ActionExecutionDelegate next = () => Task.FromResult(exectuedContext);

            // Act
            await Subject.OnActionExecutionAsync(context, next);

            Assert.True(exectuedContext.ExceptionHandled);
            Assert.IsType<StatusCodeResult>(exectuedContext.Result);
            Assert.Equal(404, ((StatusCodeResult)exectuedContext.Result).StatusCode);
        }
    }
}

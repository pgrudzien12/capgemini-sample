using CapgeminiSample.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapgeminiSample.Tests.GivenAppLayerExceptionFilter
{
    public class GivenAppLayerExceptionFilter
    {
        public GivenAppLayerExceptionFilter()
        {
            this.Subject = new ApplicationLayerExceptionFilter();
        }

        protected ApplicationLayerExceptionFilter Subject { get; }
    }
}

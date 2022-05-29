using ExMvvmCore.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMvvmCore.Middlewares
{
    public static class UnobservedTaskExceptionMiddlware
    {
        public static IApplicatoinBuilder UseUnobservedTaskException(this IApplicatoinBuilder app)
        {
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                string errorMsg = string.Join("\r\n", e.Exception.InnerExceptions.Select(c => c.Message));
                app._Logger?.Critical(errorMsg);
                e.SetObserved();
            };
            return app;
        }
    }
}

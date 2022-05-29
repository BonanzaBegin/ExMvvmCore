using ExMvvmCore.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMvvmCore.Middlewares
{
    public static class DispatcherUnhandledExceptionMiddlware
    {
        public static IApplicatoinBuilder UseDispatcherUnhandledException(this IApplicatoinBuilder app)
        {
            app.Current.DispatcherUnhandledException += (sender, e) =>
            {
                app._Logger?.Critical($"Application Error,exception message:{e.Exception.Message},ex:{e.Exception}");
                e.Handled = true;
            };
            return app;
        }
    }
}

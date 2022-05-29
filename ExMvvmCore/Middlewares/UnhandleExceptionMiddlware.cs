using ExMvvmCore.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMvvmCore.Middlewares
{
    public static class UnhandleExceptionMiddlware
    {
        public static IApplicatoinBuilder UseUnhandleException(this IApplicatoinBuilder app)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => 
            {
                Exception error = (Exception)e.ExceptionObject;
                app._Logger?.Critical($"Some error occurred,the operation has been abandon,please try again,if try again can not solve the problem,please contact the administrator");

                app._Logger?.Critical($"Application will exit:{e.IsTerminating},Error Message:{error.Message},StackTrace:{error.StackTrace}");
            };
            return app;
        }

     
    }
}

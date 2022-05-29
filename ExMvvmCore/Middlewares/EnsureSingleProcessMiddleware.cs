using ExMvvmCore.AppServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ExMvvmCore.Middlewares
{
    public static class EnsureSingleProcessMiddleware
    {
        private static Mutex mutex;
        public static IApplicatoinBuilder UseEnsureSingleProcess(this IApplicatoinBuilder app)
        {
            Process current=Process.GetCurrentProcess();
            mutex = new Mutex(true,current.ProcessName,out bool createNew);
            if (createNew == false)
            {
                MessageBox.Show($"当前程序正在运行,不允许重复开启");
                app.Current.Shutdown();
            }
            return app;
        }
    }
}

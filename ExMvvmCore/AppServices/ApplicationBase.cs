using ExMvvmCore.IServices;
using ExMvvmCore.Middlewares;
using SimpleIoc;
using SimpleIoc.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ExMvvmCore.AppServices
{
    public class ApplicationBase:Application
    {
        private IServiceCollection _IServiceCollection=new ServiceCollection();
        public IServiceProvider _Services { get; set; }
        public ApplicationBase()
        {
            ConfigureDefaultService(_IServiceCollection);

            ConfigureServices(_IServiceCollection);

            _Services=_IServiceCollection.BuildServiceProvider();
        }

        private void ConfigureDefaultService(IServiceCollection services)
        {
            services.AddSingleton<IApplicatoinBuilder, ApplicationBuilder>();
            services.AddSingleton<Application>(this);
            services.AddSingleton<ILogService, DefaultLogService>();
            services.AddSingleton<Dispatcher>(Current.Dispatcher);
        }

        protected virtual void ConfigureServices(IServiceCollection services) { }

        protected override void OnStartup(StartupEventArgs e)
        {
            Configure(_Services.GetService<IApplicatoinBuilder>());
            ILogService _LogService=_Services.GetService<ILogService>();

            _LogService.Info("***************************************************************************");
            _LogService.Info($"SYSTEM:{Process.GetCurrentProcess().ProcessName},Version:{GetVersion()} START NOW :{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            _LogService.Info("***************************************************************************");

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ILogService _LogService = _Services.GetService<ILogService>();

            _LogService.Info("***************************************************************************");
            _LogService.Info($"SYSTEM:{Process.GetCurrentProcess().ProcessName},Version:{GetVersion()} EXIT NOW :{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff")}");
            _LogService.Info("***************************************************************************");
            base.OnExit(e);
        }


        protected virtual void Configure(IApplicatoinBuilder app)
        {
            app.UseUnhandleException();
            app.UseUnobservedTaskException()
                .UseEnsureSingleProcess()
                .UseDispatcherUnhandledException() ;
        }

        private string GetVersion()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;
            return $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}

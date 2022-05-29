using ExMvvmCore.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExMvvmCore.AppServices
{
    public interface IApplicatoinBuilder
    {
        Application Current { get; }
        ILogService _Logger { get; }


    }

    public class ApplicationBuilder : IApplicatoinBuilder
    {
        public Application Current { get; }
        public ILogService _Logger { get; }
        public ApplicationBuilder(Application app, ILogService logger)
        {
            Current = app;
            _Logger = logger;
        }
    }
}

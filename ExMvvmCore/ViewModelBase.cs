using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExMvvmCore
{
    public abstract class ViewModelBase : ObservableObject
    {
        public ICommand LoadCommand { get; set; }
        public ICommand CleanCommand { get; set; }
        public ViewModelBase()
        {
            LoadCommand = new RelayCommand(DoLoad);
            CleanCommand = new RelayCommand(DoClean);
        }

        protected virtual void DoLoad() { }
        protected virtual void DoClean() { }
    }
}

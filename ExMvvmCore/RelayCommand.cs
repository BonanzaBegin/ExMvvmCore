using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ExMvvmCore
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        private Action _execute;
        private Func<bool> _canExecute;
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null || canExecute == null) throw new Exception($"execute or canexecute can not be null");
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action execute) : this(execute, () => true) { }


        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }

        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        private Action<T> _execute;
        private Func<T, bool> _canExecute;
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null || canExecute == null) throw new Exception($"execute or canexecute can not be null");
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<T> execute) : this(execute, s => true) { }


    
        //"123" 123
       

        public bool CanExecute(object parameter)=> _canExecute.Invoke(GetParameter(parameter));

        public void Execute(object parameter)=> _execute?.Invoke(GetParameter(parameter));

        private T GetParameter(object parameter)
        {
            T value = default;
            try
            {
                value = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(parameter.ToString());
            }
            catch
            {
                value = (T)parameter;
            }
            return value;
        }

        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }


}

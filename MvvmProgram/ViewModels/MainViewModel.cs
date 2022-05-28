using ExMvvmCore;
using ExMvvmCore.ExMessenger;
using ExMvvmCore.Extensions;
using ExMvvmCore.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MvvmProgram.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private int currentValue = 150;

        [RangeValidation(100, 300)]
        public int CurrentValue
        {
            get => currentValue;
            set
            {
                // currentValue = value;
                //this.RaisePropertyChanged();
                Set(ref currentValue, value, nameof(CurrentValue));
            }
        }

        public MainViewModel()
        {
            Messages.Add("11111111111111111");
            Messages.Add("22222222222222222");
            Messages.Add("33333333333333333");

            //Task.Factory.StartNew(() =>
            //{

            //    while (true)
            //    {
            //        Task.Delay(1000).Wait();
            //        //App.Current?.Dispatcher?.Invoke(() =>
            //        //{
            //        //});
            //        Messages.Add("9999999999999999999999999");
            //    }
            //});
        }



        //public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();
        //public DispatcherObservableCollection<string> Messages { get; set; } = new DispatcherObservableCollection<string>();
        public ExObservableCollection<string> Messages { get; set; } = new ExObservableCollection<string>();





        //public static event PropertyChangedEventHandler StaticPropertyChanged;

        //private static string staticValue = "123456";
        //public static string StaticValue
        //{
        //    get => staticValue;
        //    set
        //    {
        //        staticValue = value;
        //        //StaticPropertyChanged?.Invoke(null,new PropertyChangedEventArgs(nameof(StaticValue)));
        //        RaiseStaticPropertyChanged(nameof(StaticValue));
        //    }
        //}

        private bool canUse = true;
        private RelayCommand changeValueCommand;
        public RelayCommand ChangeValueCommand
        {
            get
            {
                if (changeValueCommand == null)
                {
                    changeValueCommand = new RelayCommand(() =>
                    {
                        StaticValue = "命令中改变";
                        canUse = false;
                    }, () => canUse);
                }
                return changeValueCommand;
            }
        }

        private RelayCommand<int> stringTestCommand;
        public RelayCommand<int> StringTestCommand
        {
            get
            {
                if (stringTestCommand == null)
                {
                    stringTestCommand = new RelayCommand<int>(str =>
                    {
                        MessengerProvider.Default.Send("test",str.ToString());
                    });
                }
                return stringTestCommand;
            }
        }
    }
}

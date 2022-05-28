using ExMvvmCore.ExMessenger;
using MvvmProgram.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.MainViewModel();


            MessengerProvider.Default.Register<string>("test", s => 
            {
                MessageBox.Show($"传入的参数为:{s}");
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MainViewModel).CurrentValue = 100;

            MainViewModel.StaticValue = "999999";
        }
    }
}

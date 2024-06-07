using System.Windows;
using HamsterRating.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace HamsterRating
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = App.ServiceProvider.GetService<MainWindowViewModel>();
            DataContext = viewModel;
            Loaded += (object sender, RoutedEventArgs e) => Task.Factory.StartNew(viewModel.OnLoaded);
        }
    }
}

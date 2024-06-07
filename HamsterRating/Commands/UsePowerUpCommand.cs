using System.Windows;
using System.Windows.Input;
using HamsterRating.Core.Data;
using HamsterRating.ViewModels;

namespace HamsterRating.Commands
{
    public class UsePowerUpCommand : ICommand
    {
        private readonly IPowerUpRepository _powerUpRepository;

        public UsePowerUpCommand(IPowerUpRepository powerUpRepository)
        {
            _powerUpRepository = powerUpRepository;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public async void Execute(object parameter)
        {
            var data = (PowerUpViewModel)parameter;
            MessageBox.Show(data.Name);
        }
    }
}

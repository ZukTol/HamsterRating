using System.Collections.ObjectModel;
using HamsterRating.Core.Data;

namespace HamsterRating.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IPowerUpRepository _powerUpRepository;

        private ObservableCollection<PowerUpViewModel> _data;
        public ObservableCollection<PowerUpViewModel> Data { get => _data; private set { _data = value; OnPropertyChanged(); } }

        public MainWindowViewModel(IPowerUpRepository powerUpRepository)
        {
            _powerUpRepository = powerUpRepository;
        }

        public async Task OnLoaded()
        {
            var models = await _powerUpRepository.GetPowerUpsAsync();
            Data = new ObservableCollection<PowerUpViewModel>(models.Select(x => new PowerUpViewModel(x)).OrderBy(x=>x.CostOfUpgrade));
        }
    }
}

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
            await LoadData();
        }

        private async Task LoadData()
        {
            var models = await _powerUpRepository.GetPowerUpsAsync();
            Data = new ObservableCollection<PowerUpViewModel>(models.Select(x =>
            {
                var res = new PowerUpViewModel(x);
                res.UseCommandExecuted += Res_UseCommandExecuted;
                return res;
            }).OrderBy(x => x.CostOfUpgrade));
        }

        private async void Res_UseCommandExecuted(PowerUpViewModel obj)
        {

            foreach (var item in Data)
            {
                item.UseCommandExecuted -= Res_UseCommandExecuted;
            }
            await LoadData();
        }
    }
}

using System.Globalization;
using System.Windows.Input;
using HamsterRating.Commands;
using HamsterRating.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HamsterRating.ViewModels
{
    public class PowerUpViewModel : ViewModelBase
    {
        private string _name;
        private decimal _price;
        private string _value;
        private string _group;
        private decimal _profitability;

        public string Name { get => _name; private set { _name = value; OnPropertyChanged(); } }
        public decimal Price { get => _price; private set { _price = value; OnPropertyChanged(); } }
        public string Value { get => _value; private set { _value = value; OnPropertyChanged(); } }
        public string Group { get => _group; private set { _group = value; OnPropertyChanged(); } }
        public decimal CostOfUpgrade { get => _profitability; private set { _profitability = value; OnPropertyChanged(); } }

        public ICommand UseCommand { get => App.ServiceProvider.GetService<UsePowerUpCommand>(); }

        public PowerUpViewModel(PowerUp model)
        {
            _name = model.Name;
            _price = model.Price;
            _value = model.Value;
            _group = model.Group;
            _profitability = CalculateProfit(model);
        }

        private decimal CalculateProfit(PowerUp model) => model.Price / ParseValue(model.Value);

        private decimal ParseValue(string valueStr)
        {
            if (decimal.TryParse(valueStr, out var parsed))
                return parsed;

            if (valueStr.EndsWith("K", StringComparison.OrdinalIgnoreCase))
                return decimal.Parse(valueStr.Substring(0, valueStr.Length - 1), CultureInfo.InvariantCulture) * 1000;
            if (valueStr.EndsWith("M", StringComparison.OrdinalIgnoreCase))
                return decimal.Parse(valueStr.Substring(0, valueStr.Length - 1), CultureInfo.InvariantCulture) * 1000000;
            throw new ArgumentException("Can't parse");
        }
    }
}

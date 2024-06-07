using System.Text.Json;
using HamsterRating.Core.Models;
using HamsterRating.Options;
using Microsoft.Extensions.Options;

namespace HamsterRating.Core.Data
{
    public class PowerUpRepository(IOptions<FileStorage> storageOptions) : IPowerUpRepository
    {
        private readonly FileStorage _fileStorage = storageOptions.Value;

        public async Task<PowerUp> GetPowerUpAsync(string name)
        {
            var powerUps = await GetPowerUpsAsync();
            return powerUps.FirstOrDefault(x => x.Name == name);
        }
        public async Task<IReadOnlyCollection<PowerUp>> GetPowerUpsAsync()
        {
            var data = await GetStoredData();
            var groups = data.Groups.ToDictionary(x => x.Id, x => x.Name);
            var powerUps = data.PowerUps;
            foreach (var powerUp in powerUps)
            {
                powerUp.Group = groups[powerUp.GroupId];
            }
            return powerUps;
        }
        public async Task UpdatePowerUpAsync(PowerUp powerUp)
        {
            var data = await GetStoredData();
            var item = data.PowerUps.First(x=>x.Name == powerUp.Name);
            item.Update(powerUp);
            var str = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_fileStorage.Path, str);
        }

        private async Task<Storage> GetStoredData()
        {
            var dataStr = await File.ReadAllTextAsync(_fileStorage.Path);
            var data = JsonSerializer.Deserialize<Storage>(dataStr);
            return data;
        }
    }
}


using HamsterRating.Core.Models;

namespace HamsterRating.Core.Data;

public interface IPowerUpRepository
{
    public Task<IReadOnlyCollection<PowerUp>> GetPowerUpsAsync();

    public Task<PowerUp> GetPowerUpAsync(string name);

    public Task UpdatePowerUpAsync(PowerUp powerUp);
}

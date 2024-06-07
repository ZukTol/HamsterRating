using System.Text.Json.Serialization;

namespace HamsterRating.Core.Models;

public class PowerUp
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Value { get; set; }
    public int GroupId { get; set; }
    [JsonIgnore]
    public string Group { get; set; }

    public void Update(PowerUp powerUp)
    {
        if (powerUp.Price > 0)
            Price = powerUp.Price;
        if (!string.IsNullOrEmpty(powerUp.Value))
            Value = powerUp.Value;
    }
}

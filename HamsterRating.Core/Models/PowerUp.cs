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
}

using System.Text.Json.Serialization;

namespace TomeKeeper.Models
{
    public class DamageInfo
    {
        [JsonPropertyName("damage_type")]
        public ApiReference? DamageType { get; set; }

        // Keys are slot levels as strings (e.g. "2", "3", ...).
        [JsonPropertyName("damage_at_slot_level")]
        public Dictionary<string, string> DamageAtSlotLevel { get; set; } = new();
    }
}

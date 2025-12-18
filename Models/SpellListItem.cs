using System.Text.Json.Serialization;

namespace TomeKeeper.Models
{
    public sealed class SpellListItem
    {
        [JsonConstructor]
        public SpellListItem(string index, string name, short level, string url)
        {
            Index = index;
            Name = name;
            Level = level;
            Url = url;
        }

        [JsonPropertyName("index")]
        public string Index { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public short Level { get; set; } = 0;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}

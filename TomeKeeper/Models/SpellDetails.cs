using System.Text.Json.Serialization;

namespace TomeKeeper.Models
{
    public class SpellDetails
    {
        //Empty constructor for deserialization
        public SpellDetails() { }

//        [JsonConstructor]
//        public SpellDetails(string index, string name, List<string>? desc, List<string>? higherLevel,
//            string range, List<string> components, string? material, bool ritual,
//            string duration, bool concentration, string? castingTime, int level,
//            string? attackType, DamageInfo? damage, ApiReference? school,
//            List<ApiReference> classes, List<ApiReference> subclasses,
//            string url, DateTimeOffset? updatedAt)
//        {
//            Index = index;
//            Name = name;
//            Desc = desc ?? new List<string>();
//            HigherLevel = higherLevel ?? new List<string>();
//            Range = range;
//            Components = components;
//            Material = material;
//            Ritual = ritual;
//            Duration = duration;
//            Concentration = concentration;
//            CastingTime = castingTime;
//            Level = level;
//            AttackType = attackType;
//            Damage = damage;
//            School = school;
//            Classes = classes;
//            Subclasses = subclasses;
//            Url = url;
//            UpdatedAt = updatedAt;
//            Description = string.Join("\n", Desc);
//        }

        [JsonPropertyName("index")]
        public string Index { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("desc")]
        public List<string> Desc { get; set; } = new();

        [JsonPropertyName("higher_level")]
        public List<string>? HigherLevel { get; set; } = new();

        [JsonPropertyName("range")]
        public string Range { get; set; } = string.Empty;

        [JsonPropertyName("components")]
        public List<string> Components { get; set; } = new();

        [JsonPropertyName("material")]
        public string? Material { get; set; }

        [JsonPropertyName("ritual")]
        public bool Ritual { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; } = string.Empty;

        [JsonPropertyName("concentration")]
        public bool Concentration { get; set; }

        [JsonPropertyName("casting_time")]
        public string? CastingTime { get; set; } = string.Empty;

        [JsonPropertyName("level")]
        public int Level { get; set; }

        [JsonPropertyName("attack_type")]
        public string? AttackType { get; set; }

        [JsonPropertyName("damage")]
        public DamageInfo? Damage { get; set; }

        [JsonPropertyName("school")]
        public ApiReference? School { get; set; }

        [JsonPropertyName("classes")]
        public List<ApiReference> Classes { get; set; } = new();

        [JsonPropertyName("subclasses")]
        public List<ApiReference> Subclasses { get; set; } = new();

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        [JsonPropertyName("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}

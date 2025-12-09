namespace TomeKeeper.Models
{
    internal sealed class Spell
    {
        public Spell() { }
        public string Index { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string HigherLevel { get; set; } = string.Empty;
        public string Range { get; set; } = string.Empty;
        public string Components { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public bool Ritual { get; set; } = false;
        public string Duration { get; set; } = string.Empty;
        public bool Concentration { get; set; } = false;
        public string CastingTime { get; set; } = string.Empty;
        public short Level { get; set; } = 0;
        public string AttackType { get; set; } = string.Empty;
        public Damage Damage { get; set; } = new Damage();
        public School School { get; set; } = new School();
        public IList<Header>Classes { get; set; } = new List<Header>();
        public IList<Header> Subclasses { get; set; } = new List<Header>();
        public string Url { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
    }
}

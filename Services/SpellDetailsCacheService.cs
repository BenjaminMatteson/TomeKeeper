using TomeKeeper.ViewModels;

namespace TomeKeeper.Services
{
    public class SpellDetailsCacheService
    {
        private readonly Dictionary<string, SpellDetailsViewModel> _cachedSpells = new();

        public bool TryGetSpell(string index, out SpellDetailsViewModel? spell)
        {
            return _cachedSpells.TryGetValue(index, out spell);
        }

        public void AddSpell(SpellDetailsViewModel spell)
        {
            if (!string.IsNullOrEmpty(spell.Index) && !_cachedSpells.ContainsKey(spell.Index))
            {
                _cachedSpells[spell.Index] = spell;
            }
        }

        public bool ContainsSpell(string index)
        {
            return _cachedSpells.ContainsKey(index);
        }
    }
}

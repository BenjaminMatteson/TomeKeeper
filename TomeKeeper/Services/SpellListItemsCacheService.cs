using TomeKeeper.Models;

namespace TomeKeeper.Services
{
    public class SpellListItemsCacheService
    {
        public List<SpellListItem> CachedSpellListItems { get; set; } = new List<SpellListItem>();
    }
}

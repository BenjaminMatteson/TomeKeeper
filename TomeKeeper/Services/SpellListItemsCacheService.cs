using TomeKeeper.Models;

namespace TomeKeeper.Services
{
    public class SpellListItemsCacheService : ISpellListItemsCacheService
    {
        public IList<SpellListItem> CachedSpellList { get; set; } = new List<SpellListItem>();

    }
}

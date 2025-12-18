using TomeKeeper.Models;

namespace TomeKeeper.Services
{
    public interface ISpellListItemsCacheService
    {
        public IList<SpellListItem> CachedSpellList { get; set; }
    }
}

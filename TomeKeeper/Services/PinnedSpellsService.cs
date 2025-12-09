using TomeKeeper.Models;
namespace TomeKeeper.Services
{
    public class PinnedSpellsService
    {
        private ISpellListItemsCacheService _spellListItemsCacheService { get; set; }

        public PinnedSpellsService(ISpellListItemsCacheService spellListItemsCacheService)
        {
            _spellListItemsCacheService = spellListItemsCacheService;
        }

        public void AddSpell(string spellIndex)
        {
            // Prevent duplicates and invalid entries
            if (string.IsNullOrWhiteSpace(spellIndex) ||
                SavedSpellListItems.Any(x => x.Index == spellIndex))
            {
                return;
            }

            var cachedSpellListItem = _spellListItemsCacheService.CachedSpellList
                .FirstOrDefault(x => x.Index == spellIndex);
            if (cachedSpellListItem == null)
            {
                //TODO: Get the spell from the API or throw an error
                return;
            }
            SavedSpellListItems.Add(cachedSpellListItem);
        }

        public static List<SpellListItem> SavedSpellListItems { get; set; } = new List<SpellListItem>();
    }
}

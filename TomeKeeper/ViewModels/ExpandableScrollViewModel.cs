using System.ComponentModel;
using TomeKeeper.Enums;
using TomeKeeper.Models;
using TomeKeeper.Services;

namespace TomeKeeper.ViewModels
{
    public class ExpandableScrollViewModel : IExpandableScrollViewModel
    {
        private SpellDetailsCacheService _spellDetailsCacheService;
        private IAPIService _apiService;
        public ExpandableScrollMode ScrollMode { get; set; }

        public SpellListItem SpellListItem { get; set; }

        public SpellDetailsViewModel SpellDetailsViewModel { get; set; } = new();
        public ScrollState ScrollState
        {
            get
            {
                if (SavedSpellsService.SavedSpellListItems != null)
                {

                    if (SpellListItem.Index == string.Empty)
                        return ScrollState.NoAction;

                    var spellIsSaved =
                        SavedSpellsService.SavedSpellListItems.FirstOrDefault(x => x.Index == SpellListItem.Index) != null ?
                        true : false;

                    if (spellIsSaved)
                    {
                        return ScrollMode == ExpandableScrollMode.All ? ScrollState.Added : ScrollState.Remove;
                    }

                    // Spell isn't saved yet, expose option to Add
                    return ScrollState.Add;
                }
                return ScrollState.NoAction;
            }
        }

        public ExpandableScrollViewModel(
            SpellDetailsCacheService spellDetailsCacheService,
            SavedSpellsService savedSpellsService,
            IAPIService apiService)
        {
            _spellDetailsCacheService = spellDetailsCacheService;
            _apiService = apiService;
        }

        public async Task SetSpellDetailsViewModel(string spellIndex)
        {
            var cachedSpellDetails = _spellDetailsCacheService.CachedSpells.FirstOrDefault(x => x.Index == spellIndex);
            if (cachedSpellDetails != null)
            {
                SpellDetailsViewModel = cachedSpellDetails;
                return;
            }

            await GetFreshSpellDetails(spellIndex);
        }

        private async Task GetFreshSpellDetails(string spellIndex)
        {
            var spellDetails = await _apiService.GetSpellDetails(spellIndex);
            SpellDetailsViewModel = new SpellDetailsViewModel(spellDetails);
            if (!_spellDetailsCacheService.CachedSpells.Contains(SpellDetailsViewModel))
            {
                _spellDetailsCacheService.CachedSpells.Add(SpellDetailsViewModel);
            }
        }
    }
}

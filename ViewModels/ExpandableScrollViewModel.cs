using TomeKeeper.Enums;
using TomeKeeper.Models;
using TomeKeeper.Services;

namespace TomeKeeper.ViewModels
{
    public class ExpandableScrollViewModel : IExpandableScrollViewModel
    {
        private SpellDetailsCacheService _spellDetailsCacheService;
        private SavedSpellsService _savedSpellsService;
        private IAPIService _apiService;
        public ExpandableScrollMode ScrollMode { get; set; }

        public SpellListItem? SpellListItem { get; set; }

        public SpellDetailsViewModel SpellDetailsViewModel { get; set; } = new();
        public ScrollState ScrollState
        {
            get
            {
                if (_savedSpellsService.SavedSpellListItems != null && SpellListItem != null)
                {
                    if (SpellListItem.Index == string.Empty)
                        return ScrollState.NoAction;

                    var spellIsSaved = _savedSpellsService.SavedSpellListItems.Any(x => x.Index == SpellListItem.Index);

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

        public Action<SpellListItem>? OnSpellRemoved { get; set; }

        public ExpandableScrollViewModel(
            SpellDetailsCacheService spellDetailsCacheService,
            SavedSpellsService savedSpellsService,
            IAPIService apiService)
        {
            _spellDetailsCacheService = spellDetailsCacheService;
            _savedSpellsService = savedSpellsService;
            _apiService = apiService;
        }

        public async Task SetSpellDetailsViewModel(string spellIndex)
        {
            if (_spellDetailsCacheService.TryGetSpell(spellIndex, out var cachedSpellDetails) && cachedSpellDetails != null)
            {
                SpellDetailsViewModel = cachedSpellDetails;
                return;
            }

            await GetFreshSpellDetails(spellIndex);
        }

        public async Task HandleSpellAction()
        {
            if (ScrollState == ScrollState.NoAction ||
                ScrollState == ScrollState.Added ||
                SpellListItem == null ||
                string.IsNullOrEmpty(SpellListItem.Index))
                return;

            if (ScrollState == ScrollState.Remove)
            {
                await _savedSpellsService.RemoveSpell(SpellListItem.Index);
                OnSpellRemoved?.Invoke(SpellListItem);
                return;
            }

            if (ScrollState == ScrollState.Add)
                await _savedSpellsService.AddSpell(SpellListItem.Index);
        }

        private async Task GetFreshSpellDetails(string spellIndex)
        {
            var spellDetails = await _apiService.GetSpellDetails(spellIndex);
            SpellDetailsViewModel = new SpellDetailsViewModel(spellDetails);
            _spellDetailsCacheService.AddSpell(SpellDetailsViewModel);
        }
    }
}

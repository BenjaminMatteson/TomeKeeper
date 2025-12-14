using TomeKeeper.Services;

namespace TomeKeeper.ViewModels
{
    public class ExpandableScrollViewModel : IExpandableScrollViewModel
    {
        private SpellDetailsCacheService _spellDetailsCacheService;
        private IAPIService _apiService;

        public SpellDetailsViewModel SpellDetailsViewModel { get; set; } = new();

        public ExpandableScrollViewModel(
            SpellDetailsCacheService spellDetailsCacheService,
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

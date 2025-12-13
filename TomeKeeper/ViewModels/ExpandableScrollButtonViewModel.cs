
using System.Threading.Tasks;
using TomeKeeper.Services;

namespace TomeKeeper.ViewModels
{
    public class ExpandableScrollButtonViewModel : IExpandableScrollButtonViewModel
    {
        private SpellDetailsCacheService _spellDetailsCacheService;
        private IAPIService _apiService;

        private SpellDetailsViewModel _spellDetailsViewModel = new();
        public SpellDetailsViewModel SpellDetailsViewModel
        {
            get { return _spellDetailsViewModel; }
            set { _spellDetailsViewModel = value; }
        }

        //This is just used for UI
        public bool IsActive { get; set; }

        private string _selectedSpellIndex = string.Empty;
        public string SelectedSpellIndex
        {
            get { return _selectedSpellIndex; }
            set { _selectedSpellIndex = value; }
        }

        public ExpandableScrollButtonViewModel(
            SpellDetailsCacheService spellDetailsCacheService,
            SavedSpellsService savedSpellsService,
            IAPIService apiService)
        {
            _spellDetailsCacheService = spellDetailsCacheService;
            _apiService = apiService;
        }

        public async Task SetSpellDetailsViewModel(string spellIndex)
        {
            if (SelectedSpellIndex == spellIndex)
                return;

            var spellDetails = await _apiService.GetSpellDetails(spellIndex);
            _spellDetailsViewModel = new SpellDetailsViewModel(spellDetails);
            if (!_spellDetailsCacheService.CachedSpells.Contains(_spellDetailsViewModel))
            {
                _spellDetailsCacheService.CachedSpells.Add(_spellDetailsViewModel);
            }

            _selectedSpellIndex = spellIndex;
            _spellDetailsViewModel.isActive = true;
        }

        public void ClearSpellDetails()
        {
            _selectedSpellIndex = string.Empty;
            _spellDetailsViewModel = new();
        }

    }
}

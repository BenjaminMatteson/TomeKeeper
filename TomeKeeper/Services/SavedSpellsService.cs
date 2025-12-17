using Microsoft.JSInterop;
using System.Text.Json;
using TomeKeeper.Models;

namespace TomeKeeper.Services
{
    public class SavedSpellsService
    {
        private readonly IJSRuntime _jsRuntime;

        private ISpellListItemsCacheService _spellListItemsCacheService { get; set; }

        public SavedSpellsService(ISpellListItemsCacheService spellListItemsCacheService, IJSRuntime jsRuntime)
        {
            _spellListItemsCacheService = spellListItemsCacheService;
            _jsRuntime = jsRuntime;
        }
        public async Task InitializeSavedSpellsAsync()
        {
            var jsonString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "UserSpellList");
            if (!string.IsNullOrEmpty(jsonString))
            {
                var items = JsonSerializer.Deserialize<List<SpellListItem>>(jsonString);
                if (items != null)
                {
                    SavedSpellListItems = items;
                }
            }
        }

        public async Task AddSpell(string spellIndex)
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

            var jsonString = JsonSerializer.Serialize(SavedSpellListItems);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "UserSpellList", jsonString);
        }

        public static List<SpellListItem> SavedSpellListItems { get; set; } = new List<SpellListItem>();
    }
}

using System.Text.Json;
using TomeKeeper.Models;

namespace TomeKeeper.Services
{
    public class StorageService
    {
        public string ConvertSpellListToJson(List<SpellListItem> spellList)
        {
            var jsonString = JsonSerializer.Serialize(spellList);
            return jsonString;
        }
    }
}

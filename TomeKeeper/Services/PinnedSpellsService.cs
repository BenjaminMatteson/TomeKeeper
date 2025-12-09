using TomeKeeper.ViewModels;

namespace TomeKeeper.Services
{
    public class PinnedSpellsService
    {
        public static void AddSpell(string spellIndex)
        {
            if (!SavedSpellIndices.Exists(s => s == spellIndex))
            {
                SavedSpellIndices.Add(spellIndex);
            }
        }

        public static List<string> SavedSpellIndices { get; set; } = new List<string>();
    }
}

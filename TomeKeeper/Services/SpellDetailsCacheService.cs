using TomeKeeper.ViewModels;

namespace TomeKeeper.Services
{
    public class SpellDetailsCacheService
    {
        public List<SpellDetailsViewModel> CachedSpells { get; set; } = new List<SpellDetailsViewModel>();
    }
}

using TomeKeeper.Models;

namespace TomeKeeper.Services
{
    public interface IAPIService
    {
        public Task<IList<SpellListItem>> GetSpellsList(CancellationToken cancellationToken = default);
        public Task<SpellDetails> GetSpellDetails(string spellIndex, CancellationToken cancellationToken = default);
    }
}

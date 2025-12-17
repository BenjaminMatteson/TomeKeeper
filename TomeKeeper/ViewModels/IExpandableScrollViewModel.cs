using TomeKeeper.Enums;
using TomeKeeper.Models;

namespace TomeKeeper.ViewModels
{
    public interface IExpandableScrollViewModel
    {
        public SpellListItem SpellListItem { get; set; }
        public SpellDetailsViewModel SpellDetailsViewModel { get; set; }
        public Task SetSpellDetailsViewModel(string spellIndex);
        public Task HandleSpellAction();
        public ExpandableScrollMode ScrollMode { get; set; }
        public ScrollState ScrollState { get; }
    }
}
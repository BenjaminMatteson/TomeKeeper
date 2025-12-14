namespace TomeKeeper.ViewModels
{
    public interface IExpandableScrollViewModel
    {
        public SpellDetailsViewModel SpellDetailsViewModel { get; set; }
        public Task SetSpellDetailsViewModel(string spellIndex);
    }
}
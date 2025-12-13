
namespace TomeKeeper.ViewModels
{
    public interface IExpandableScrollButtonViewModel
    {
        public SpellDetailsViewModel SpellDetailsViewModel { get; set; }
        public string SelectedSpellIndex { get; set; }
        public bool IsActive { get; set; }
        public void ClearSpellDetails();
        public Task SetSpellDetailsViewModel(string spellIndex);
    }
}

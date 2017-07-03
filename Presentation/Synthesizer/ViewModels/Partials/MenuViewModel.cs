using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class MenuViewModel : ViewModelBase
    {
        public MenuItemViewModel DocumentTree { get; set; }
        public MenuItemViewModel DocumentProperties { get; set; }
        public MenuItemViewModel DocumentList { get; set; }
        public MenuItemViewModel CurrentInstrument { get; set; }
        public MenuItemViewModel DocumentClose { get; set; }
    }
}

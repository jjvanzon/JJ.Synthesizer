using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class MenuViewModel
    {
        public MenuItemViewModel DocumentsMenuItem { get; set; }
        public MenuItemViewModel DocumentCloseMenuItem { get; set; }
        public MenuItemViewModel DocumentTreeMenuItem { get; set; }
        public MenuItemViewModel DocumentSaveMenuItem { get; set; }
    }
}

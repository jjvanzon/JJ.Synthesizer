﻿using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ViewModels.Partials
{
    public sealed class MenuViewModel : ViewModelBase
    {
        public MenuItemViewModel DocumentTree { get; set; }
        public MenuItemViewModel CurrentPatches { get; set; }
        public MenuItemViewModel DocumentSave { get; set; }
        public MenuItemViewModel DocumentClose { get; set; }
        public MenuItemViewModel DocumentList { get; set; }
        public MenuItemViewModel DocumentProperties { get; set; }
    }
}
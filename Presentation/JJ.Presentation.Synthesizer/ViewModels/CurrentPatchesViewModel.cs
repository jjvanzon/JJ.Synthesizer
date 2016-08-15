using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurrentPatchesViewModel : ViewModelBase
    {
        public bool CanShowAutoPatchPolyphonic { get; set; }
        public IList<CurrentPatchItemViewModel> List { get; set; }
    }
}

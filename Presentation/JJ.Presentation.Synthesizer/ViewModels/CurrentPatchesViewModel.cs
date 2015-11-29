using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurrentPatchesViewModel
    {
        public IList<CurrentPatchItemViewModel> List { get; set; }
        public bool Visible;
    }
}

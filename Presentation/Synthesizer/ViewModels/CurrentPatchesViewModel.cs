using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurrentPatchesViewModel : ViewModelBase
    {
        public bool CanShowAutoPatchPolyphonic { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}

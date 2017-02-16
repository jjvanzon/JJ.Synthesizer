using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurrentPatchesViewModel : ViewModelBase
    {
        public IList<IDAndName> List { get; set; }
    }
}

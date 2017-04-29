using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurrentInstrumentViewModel : ViewModelBase
    {
        public IList<IDAndName> List { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}

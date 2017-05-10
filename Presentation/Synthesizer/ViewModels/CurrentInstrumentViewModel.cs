using System.Collections.Generic;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurrentInstrumentViewModel : ViewModelBase
    {
        public IList<IDAndName> List { get; set; }
        internal int? OutletIDToPlay { get; set; }
    }
}

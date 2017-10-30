using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class CurrentInstrumentViewModel : ViewModelBase
    {
        public int DocumentID { get; set; }
        public IList<CurrentInstrumentItemViewModel> List { get; set; }
        public bool CanPlay { get; set; }
        public bool CanExpand { get; set; }
        internal int? OutletIDToPlay { get; set; }
    }
}

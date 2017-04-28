using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SampleGridViewModel : ViewModelBase
    {
        public int DocumentID { get; set; }
        public IList<SampleListItemViewModel> List { get; set; }
        public int? OutletIDToPlay { get; set; }
    }
}

using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SampleGridViewModel
    {
        public int DocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<SampleListItemViewModel> List { get; set; }
        public bool Successful { get; set; }
    }
}

using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class SampleGridViewModel
    {
        public int DocumentID { get; set; }
        public bool Visible { get; set; }
        public IList<SampleListItemViewModel> List { get; set; }
    }
}

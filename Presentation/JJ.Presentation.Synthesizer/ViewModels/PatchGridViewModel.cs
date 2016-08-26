using System;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class PatchGridViewModel : ViewModelBase
    {
        public int DocumentID { get; set; }
        public string Group { get; set; }
        public IList<IDAndName> List { get; set; }
    }
}

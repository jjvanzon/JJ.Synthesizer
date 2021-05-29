using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForPatchOutlet : OperatorPropertiesViewModelBase
    {
        public int Position { get; set; }
        public bool NameOrDimensionHidden { get; set; }
        public bool IsRepeating { get; set; }

        public IDAndName Dimension { get; set; }
        public IList<IDAndName> DimensionLookup { get; set; }
    }
}

using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForPatchInlet : OperatorPropertiesViewModelBase
    {
        public int Position { get; set; }
        public string DefaultValue { get; set; }
        public bool WarnIfEmpty { get; set; }
        public bool NameOrDimensionHidden { get; set; }

        public IDAndName Dimension { get; set; }
        public IList<IDAndName> DimensionLookup { get; set; }
    }
}

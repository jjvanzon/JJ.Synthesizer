using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForBundle : OperatorPropertiesViewModelBase
    {
        public int PatchID { get; internal set; }
        public string Name { get; set; }
        public int InletCount { get; set; }
        public IDAndName Dimension { get; set; }
        public IList<IDAndName> DimensionLookup { get; set; }
    }
}

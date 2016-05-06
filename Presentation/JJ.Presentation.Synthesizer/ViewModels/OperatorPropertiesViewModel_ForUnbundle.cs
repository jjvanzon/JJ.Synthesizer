using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForUnbundle : OperatorPropertiesViewModelBase
    {
        public int PatchID { get; internal set; }
        public string Name { get; set; }
        public int OutletCount { get; set; }
        public IDAndName Dimension { get; set; }
        public IList<IDAndName> DimensionLookup { get; set; }
    }
}

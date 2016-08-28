using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_WithDimensionAndOutletCount : OperatorPropertiesViewModelBase
    {
        public IDAndName OperatorType { get; set; }
        public int OutletCount { get; set; }
    }
}

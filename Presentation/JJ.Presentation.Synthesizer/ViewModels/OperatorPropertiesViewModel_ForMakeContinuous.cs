using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForMakeContinuous : OperatorPropertiesViewModel
    {
        public int InletCount { get; set; }
        public IDAndName Interpolation { get; set; }
        public IList<IDAndName> InterpolationLookup { get; set; }
    }
}
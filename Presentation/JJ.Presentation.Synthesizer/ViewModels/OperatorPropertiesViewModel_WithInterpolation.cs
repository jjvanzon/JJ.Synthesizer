using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_WithInterpolation : OperatorPropertiesViewModel
    {
        public IDAndName Interpolation { get; set; }
        public IList<IDAndName> InterpolationLookup { get; set; }
    }
}
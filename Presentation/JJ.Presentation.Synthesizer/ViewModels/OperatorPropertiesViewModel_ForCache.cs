using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForCache : OperatorPropertiesViewModelBase
    {
        public string Name { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public int SamplingRate { get; set; }
        public IDAndName Interpolation { get; set; }
        public IList<IDAndName> InterpolationLookup { get; set; }
    }
}

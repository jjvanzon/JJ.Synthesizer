using JJ.Data.Canonical;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForAggregate : OperatorPropertiesViewModelBase
    {
        public OperatorTypeViewModel OperatorType { get; set; }

        public int PatchID { get; internal set; }
        public string Name { get; set; }
        public double TimeSliceDuration { get; set; }
        public int SampleCount { get; set; }
    }
}

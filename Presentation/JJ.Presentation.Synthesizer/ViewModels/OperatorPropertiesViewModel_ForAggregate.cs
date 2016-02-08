using JJ.Data.Canonical;
using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForAggregate : ViewModelBase
    {
        public int ID { get; set; }
        public OperatorTypeViewModel OperatorType { get; set; }

        public string Name { get; set; }
        public double TimeSliceDuration { get; set; }
        public int SampleCount { get; set; }
    }
}

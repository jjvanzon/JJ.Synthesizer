using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForSpectrum : ViewModelBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public int FrequencyCount { get; set; }
    }
}

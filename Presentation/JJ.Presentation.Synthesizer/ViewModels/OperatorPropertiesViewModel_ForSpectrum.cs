using JJ.Data.Canonical;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel_ForSpectrum
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public int FrequencyCount { get; set; }

        public bool Visible { get; set; }
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }
    }
}

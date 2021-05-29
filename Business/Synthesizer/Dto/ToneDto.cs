using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Business.Synthesizer.Dto
{
    public class ToneDto : ITone
    {
        /// <inheritdoc />
        public int Octave { get; set; }
        /// <inheritdoc />
        public double Value { get; set; }
        public int Ordinal { get; set; }
        public double Frequency { get; set; }
        public ScaleTypeEnum ScaleTypeEnum { get; set; }
        public double ScaleBaseFrequency { get; set; }
    }
}

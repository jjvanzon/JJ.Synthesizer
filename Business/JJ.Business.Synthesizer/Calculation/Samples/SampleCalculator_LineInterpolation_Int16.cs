using JJ.Data.Synthesizer;
using System.IO;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class SampleCalculator_LineInterpolation_Int16 : SampleCalculator_LineInterpolation_Base
    {
        private const double VALUE_DIVIDER = (double)-short.MinValue;

        public SampleCalculator_LineInterpolation_Int16(Sample sample, byte[] bytes)
            : base(sample, bytes)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            short shrt = binaryReader.ReadInt16();
            double value = (double)shrt / VALUE_DIVIDER;
            return value;
        }
    }
}
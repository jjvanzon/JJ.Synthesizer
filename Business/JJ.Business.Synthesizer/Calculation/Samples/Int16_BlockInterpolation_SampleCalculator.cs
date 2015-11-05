using JJ.Data.Synthesizer;
using System.IO;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Int16_BlockInterpolation_SampleCalculator : BlockInterpolation_SampleCalculatorBase
    {
        private const double VALUE_DIVIDER = (double)-short.MinValue;

        public Int16_BlockInterpolation_SampleCalculator(Sample sample, byte[] bytes)
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
using JJ.Data.Synthesizer;
using System.IO;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Byte_BlockInterpolation_SampleCalculator : BlockInterpolation_SampleCalculatorBase
    {
        private const double VALUE_DIVIDER = 128.0;

        public Byte_BlockInterpolation_SampleCalculator(Sample sample, byte[] bytes)
            : base(sample, bytes)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            byte b = binaryReader.ReadByte();
            double value = b - 128.0;
            value /= VALUE_DIVIDER;
            return value;
        }
    }
}
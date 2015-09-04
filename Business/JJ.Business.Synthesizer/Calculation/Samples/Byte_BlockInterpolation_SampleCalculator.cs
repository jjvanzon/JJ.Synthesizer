using JJ.Data.Synthesizer;
using System.IO;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Byte_BlockInterpolation_SampleCalculator : BlockInterpolation_SampleCalculatorBase
    {
        public Byte_BlockInterpolation_SampleCalculator(Sample sample, byte[] bytes)
            : base(sample, bytes)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            return binaryReader.ReadByte() - 128;
        }
    }
}
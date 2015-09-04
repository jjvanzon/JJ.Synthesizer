using JJ.Data.Synthesizer;
using System.IO;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Int16_BlockInterpolation_SampleCalculator : BlockInterpolation_SampleCalculatorBase
    {
        public Int16_BlockInterpolation_SampleCalculator(Sample sample, byte[] bytes)
            : base(sample, bytes)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            return binaryReader.ReadInt16();
        }
    }
}
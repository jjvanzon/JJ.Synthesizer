using JJ.Framework.IO;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Byte_BlockInterpolation_SampleCalculator : BlockInterpolation_SampleCalculatorBase
    {
        public Byte_BlockInterpolation_SampleCalculator(SampleChannel sampleChannel)
            : base(sampleChannel, 1)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            return binaryReader.ReadByte() - 128;
        }
    }
}
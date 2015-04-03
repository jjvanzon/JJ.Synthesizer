using JJ.Framework.IO;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Byte_LineInterpolation_SampleCalculator : LineInterpolation_SampleCalculatorBase
    {
        public Byte_LineInterpolation_SampleCalculator(Sample sample)
            : base(sample)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            return binaryReader.ReadByte() - 128;
        }
    }
}
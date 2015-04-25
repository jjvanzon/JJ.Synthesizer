using JJ.Framework.IO;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Int16_LineInterpolation_SampleCalculator : LineInterpolation_SampleCalculatorBase
    {
        public Int16_LineInterpolation_SampleCalculator(Sample sample)
            : base(sample)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            return binaryReader.ReadInt16();
        }
    }
}
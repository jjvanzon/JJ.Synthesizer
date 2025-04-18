﻿using JJ.Framework.IO;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.Samples
{
    internal class Float32_BlockInterpolation_SampleCalculator : BlockInterpolation_SampleCalculatorBase
    {
        public Float32_BlockInterpolation_SampleCalculator(Sample sample)
            : base(sample)
        { }

        protected override double ReadValue(BinaryReader binaryReader)
        {
            return binaryReader.ReadSingle();
        }
    }
}
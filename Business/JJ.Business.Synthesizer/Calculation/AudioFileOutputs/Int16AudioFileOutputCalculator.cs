using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class Int16AudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public Int16AudioFileOutputCalculator(AudioFileOutput audioFileOutput, ICurveRepository curveRepository, ISampleRepository sampleRepository)
            : base(audioFileOutput, curveRepository, sampleRepository)
        { }

        protected override void WriteValue(BinaryWriter binaryWriter, double value)
        {
            short convertedValue = (short)value;
            binaryWriter.Write(convertedValue);
        }
    }
}

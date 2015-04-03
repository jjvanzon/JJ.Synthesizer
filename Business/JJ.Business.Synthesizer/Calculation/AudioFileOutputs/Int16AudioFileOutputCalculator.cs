using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class Int16AudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public Int16AudioFileOutputCalculator(AudioFileOutput audioFileOutput, string filePath)
            : base(audioFileOutput, filePath)
        { }

        protected override void WriteValue(BinaryWriter binaryWriter, double value)
        {
            short convertedValue = (short)value;
            binaryWriter.Write(convertedValue);
        }
    }
}

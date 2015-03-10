using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class Float32AudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public Float32AudioFileOutputCalculator(AudioFileOutput audioFileOutput, string filePath)
            : base(audioFileOutput, filePath)
        { }

        protected override void WriteValue(BinaryWriter binaryWriter, double value)
        {
            float convertedValue = (float)value;
            binaryWriter.Write(convertedValue);
        }
    }
}
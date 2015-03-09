using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Structs;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class ByteAudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public ByteAudioFileOutputCalculator(AudioFileOutput audioFileOutput, string filePath)
            : base(audioFileOutput, filePath)
        { }

        protected override void WriteValue(BinaryWriter binaryWriter, double value)
        {
            value += 128;
            byte convertedValue = (byte)value;
            binaryWriter.Write(convertedValue);
        }
    }
}
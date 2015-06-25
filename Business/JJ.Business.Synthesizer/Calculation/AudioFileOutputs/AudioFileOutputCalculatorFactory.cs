using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal static class AudioFileOutputCalculatorFactory
    {
        public static IAudioFileOutputCalculator CreateAudioFileOutputCalculator(ICurveRepository curveRepository, ISampleRepository sampleRepository, AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            SampleDataTypeEnum sampleDataTypeEnum = audioFileOutput.GetSampleDataTypeEnum();
            switch (sampleDataTypeEnum)
            {
                case SampleDataTypeEnum.Int16:
                    return new Int16AudioFileOutputCalculator(audioFileOutput, curveRepository, sampleRepository);

                case SampleDataTypeEnum.Byte:
                    return new ByteAudioFileOutputCalculator(audioFileOutput, curveRepository, sampleRepository);

                default:
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }
    }
}

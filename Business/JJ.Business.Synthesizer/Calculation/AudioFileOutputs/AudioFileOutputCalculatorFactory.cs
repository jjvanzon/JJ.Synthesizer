using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal static class AudioFileOutputCalculatorFactory
    {
        public static IAudioFileOutputCalculator CreateAudioFileOutputCalculator(
            AudioFileOutput audioFileOutput, 
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository, 
            IPatchRepository patchRepository)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            SampleDataTypeEnum sampleDataTypeEnum = audioFileOutput.GetSampleDataTypeEnum();
            switch (sampleDataTypeEnum)
            {
                case SampleDataTypeEnum.Int16:
                    return new Int16AudioFileOutputCalculator(curveRepository, sampleRepository, patchRepository);

                case SampleDataTypeEnum.Byte:
                    return new ByteAudioFileOutputCalculator(curveRepository, sampleRepository, patchRepository);

                default:
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }
    }
}

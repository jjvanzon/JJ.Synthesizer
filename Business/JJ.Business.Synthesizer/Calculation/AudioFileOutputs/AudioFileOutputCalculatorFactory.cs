using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal static class AudioFileOutputCalculatorFactory
    {
        public static IAudioFileOutputCalculator CreateAudioFileOutputCalculator(
            AudioFileOutput audioFileOutput,
            IPatchCalculator patchCalculator,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
            if (patchCalculator == null) throw new NullException(() => patchCalculator);

            SampleDataTypeEnum sampleDataTypeEnum = audioFileOutput.GetSampleDataTypeEnum();
            switch (sampleDataTypeEnum)
            {
                case SampleDataTypeEnum.Int16:
                    return new Int16AudioFileOutputCalculator(patchCalculator, curveRepository, sampleRepository, patchRepository);

                case SampleDataTypeEnum.Byte:
                    return new ByteAudioFileOutputCalculator(patchCalculator, curveRepository, sampleRepository, patchRepository);

                default:
                    throw new ValueNotSupportedException(sampleDataTypeEnum);
            }
        }
    }
}

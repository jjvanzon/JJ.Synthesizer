using System.Collections.Generic;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
	internal static class AudioFileOutputCalculatorFactory
	{
		public static IAudioFileOutputCalculator CreateAudioFileOutputCalculator(
			AudioFileOutput audioFileOutput,
			IList<IPatchCalculator> patchCalculators)
		{
			if (audioFileOutput == null) throw new NullException(() => audioFileOutput);
			if (patchCalculators == null) throw new NullException(() => patchCalculators);

			SampleDataTypeEnum sampleDataTypeEnum = audioFileOutput.GetSampleDataTypeEnum();
			switch (sampleDataTypeEnum)
			{
				case SampleDataTypeEnum.Int16:
					return new Int16AudioFileOutputCalculator(patchCalculators);

				case SampleDataTypeEnum.Byte:
					return new ByteAudioFileOutputCalculator(patchCalculators);

				default:
					throw new ValueNotSupportedException(sampleDataTypeEnum);
			}
		}
	}
}

using System.IO;
using JJ.Business.Synthesizer.Calculation.Patches;
using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
	internal class ByteAudioFileOutputCalculator : AudioFileOutputCalculatorBase
	{
		public ByteAudioFileOutputCalculator(IList<IPatchCalculator> patchCalculators)
			: base(patchCalculators)
		{ }

		protected override void WriteValue(BinaryWriter binaryWriter, double value)
		{
			value += 128;

			if (value < byte.MinValue) value = byte.MinValue;
			if (value > byte.MaxValue) value = byte.MaxValue;

			byte convertedValue = (byte)value;
			binaryWriter.Write(convertedValue);
		}

		protected override double GetAmplifierAdjustedToSampleDataType(AudioFileOutput audioFileOutput)
		{
			return audioFileOutput.Amplifier * 127.0;
		}
	}
}
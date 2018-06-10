using System.Collections.Generic;
using System.IO;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
	internal class Int16AudioFileOutputCalculator : AudioFileOutputCalculatorBase
	{
		public Int16AudioFileOutputCalculator(IList<IPatchCalculator> patchCalculators)
			: base(patchCalculators)
		{ }

		protected override void WriteValue(BinaryWriter binaryWriter, double value)
		{
			if (value < short.MinValue) value = short.MinValue;
			if (value > short.MaxValue) value = short.MaxValue;

			short convertedValue = (short)value;
			binaryWriter.Write(convertedValue);
		}

		protected override double GetAmplifierAdjustedToSampleDataType(AudioFileOutput audioFileOutput) => audioFileOutput.Amplifier * short.MaxValue;
	}
}

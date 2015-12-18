using System;
using System.IO;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class Int16AudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public Int16AudioFileOutputCalculator(
            IPatchCalculator patchCalculator,
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository)
            : base(patchCalculator, curveRepository, sampleRepository, patchRepository)
        { }

        protected override void WriteValue(BinaryWriter binaryWriter, double value)
        {
            if (value < short.MinValue) value = short.MinValue;
            if (value > short.MaxValue) value = short.MaxValue;

            short convertedValue = (short)value;
            binaryWriter.Write(convertedValue);
        }

        protected override double GetAmplifierAdjustedToSampleDataType(AudioFileOutput audioFileOutput)
        {
            return audioFileOutput.Amplifier * short.MaxValue;
        }
    }
}

using JJ.Data.Synthesizer;
using System.IO;
using JJ.Business.Synthesizer.Calculation.Patches;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class ByteAudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public ByteAudioFileOutputCalculator(IPatchCalculator patchCalculator)
            : base(patchCalculator)
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
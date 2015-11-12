using System;
using System.IO;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class Int16AudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public Int16AudioFileOutputCalculator(AudioFileOutput audioFileOutput, ICurveRepository curveRepository, ISampleRepository sampleRepository, IDocumentRepository documentRepository)
            : base(audioFileOutput, curveRepository, sampleRepository, documentRepository)
        { }

        protected override void WriteValue(BinaryWriter binaryWriter, double value)
        {
            short convertedValue = (short)value;
            binaryWriter.Write(convertedValue);
        }

        protected override double GetAmplifierAdjustedToSampleDataType(AudioFileOutput audioFileOutput)
        {
            return audioFileOutput.Amplifier * short.MaxValue;
        }
    }
}

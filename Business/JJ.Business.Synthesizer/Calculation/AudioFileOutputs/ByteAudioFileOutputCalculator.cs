using JJ.Data.Synthesizer;
using System.IO;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal class ByteAudioFileOutputCalculator : AudioFileOutputCalculatorBase
    {
        public ByteAudioFileOutputCalculator(AudioFileOutput audioFileOutput, ICurveRepository curveRepository, ISampleRepository sampleRepository, IDocumentRepository documentRepository)
            : base(audioFileOutput, curveRepository, sampleRepository, documentRepository)
        { }

        protected override void WriteValue(BinaryWriter binaryWriter, double value)
        {
            value += 128;
            byte convertedValue = (byte)value;
            binaryWriter.Write(convertedValue);
        }
    }
}
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal interface IAudioFileOutputCalculator
    {
        void WriteFile(AudioFileOutput audioFileOutput);
    }
}

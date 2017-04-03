using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Calculation.AudioFileOutputs
{
    internal interface IAudioFileOutputCalculator
    {
        void WriteFile(AudioFileOutput audioFileOutput);
    }
}

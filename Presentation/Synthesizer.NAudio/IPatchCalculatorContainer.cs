using System.Threading;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public interface IPatchCalculatorContainer
    {
        ReaderWriterLockSlim Lock { get; }
        IPatchCalculator Calculator { get; }

        void RecreateCalculator(
            Patch patch, 
            int samplingRate,
            int channelCount,
            int maxConcurrentNotes);
    }
}

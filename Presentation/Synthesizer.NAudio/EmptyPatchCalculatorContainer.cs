using System.Collections.Generic;
using System.Threading;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class EmptyPatchCalculatorContainer : IPatchCalculatorContainer
    {
        public IPatchCalculator Calculator => null;

        public ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

        public void RecreateCalculator(IList<Patch> patches, AudioOutput audioOutput, RepositoryWrapper repositories)
        {
            // Do nothing.        
        }
    }
}

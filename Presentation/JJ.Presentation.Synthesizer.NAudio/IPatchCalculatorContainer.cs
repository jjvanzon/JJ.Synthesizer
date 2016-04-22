using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public interface IPatchCalculatorContainer
    {
        ReaderWriterLockSlim Lock { get; }
        IPatchCalculator Calculator { get; }

        // TODO: Needs to be refactored.
        void RecreateCalculator(
            IList<Patch> patches, 
            int maxConcurrentNotes, 
            RepositoryWrapper repositories,
            AudioOutput audioOutput);
    }
}

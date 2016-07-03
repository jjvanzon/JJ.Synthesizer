using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class SingleThreadedPatchCalculatorContainer : IPatchCalculatorContainer
    {
        // TODO: Low Priority: This class cannot work multi-channel.
        private const int DEFAULT_CHANNEL_INDEX = 0;

        public ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

        /// <summary> null if RecreateCalculator is not yet called. </summary>
        public IPatchCalculator Calculator { get; private set; }

        /// <summary> 
        /// You must call this on the thread that keeps the IContext open. 
        /// Will automatically use a WriteLock.
        /// </summary>
        public void RecreateCalculator(IList<Patch> patches, AudioOutput audioOutput, RepositoryWrapper repositories)
        {
            if (audioOutput == null) throw new NullException(() => audioOutput);

            var patchManager = new PatchManager(new PatchRepositories(repositories));
            Outlet autoPatchOutlet = patchManager.AutoPatchPolyphonic(patches, audioOutput.MaxConcurrentNotes);

            IPatchCalculator patchCalculator = patchManager.CreateCalculator(
                autoPatchOutlet, 
                audioOutput.SamplingRate,
                audioOutput.GetChannelCount(), 
                DEFAULT_CHANNEL_INDEX, 
                new CalculatorCache());

            Lock.EnterWriteLock();
            try
            {
                if (Calculator != null)
                {
                    patchCalculator.CloneValues(Calculator);
                }

                Calculator = patchCalculator;
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
    }
}

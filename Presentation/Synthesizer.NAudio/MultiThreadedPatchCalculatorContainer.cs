using System.Collections.Generic;
using System.Threading;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class MultiThreadedPatchCalculatorContainer : IPatchCalculatorContainer
    {
        private readonly NoteRecycler _noteRecycler;

        public ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

        /// <summary> null if RecreateCalculator is not yet called. </summary>
        public IPatchCalculator Calculator { get; private set; }

        public MultiThreadedPatchCalculatorContainer(NoteRecycler noteRecycler)
        {
            _noteRecycler = noteRecycler ?? throw new NullException(() => noteRecycler);
        }

        /// <summary> 
        /// You must call this on the thread that keeps the IContext open. 
        /// Will automatically use a WriteLock.
        /// </summary>
        public void RecreateCalculator(IList<Patch> patches, AudioOutput audioOutput, RepositoryWrapper repositories)
        {
            var patchManager = new PatchManager(new PatchRepositories(repositories));

            // Auto-Patch
            patchManager.AutoPatch(patches);
            Patch autoPatch = patchManager.Patch;

            var newCalculator = new MultiThreadedPatchCalculator(
                autoPatch,
                audioOutput, 
                _noteRecycler, 
                repositories);

            Lock.EnterWriteLock();
            try
            {
                if (Calculator != null)
                {
                    newCalculator.CloneValues(Calculator);
                }

                Calculator = newCalculator;
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
    }
}

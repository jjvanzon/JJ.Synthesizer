using System.Collections.Generic;
using System.Threading;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
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
            if (noteRecycler == null) throw new NullException(() => noteRecycler);

            _noteRecycler = noteRecycler;
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

            var newPolyphonyCalculator = new MultiThreadedPatchCalculator(
                autoPatch,
                audioOutput, 
                _noteRecycler, 
                repositories);

            Lock.EnterWriteLock();
            try
            {
                if (Calculator != null)
                {
                    newPolyphonyCalculator.CloneValues(Calculator);
                }

                Calculator = newPolyphonyCalculator;
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
    }
}

using System.Threading;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Presentation.Synthesizer.NAudio
{
    internal class MultiThreadedPatchCalculatorContainer : IPatchCalculatorContainer
    {
        private readonly RepositoryWrapper _repositories;
        private readonly NoteRecycler _noteRecycler;

        public ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

        /// <summary>
        /// Not thread-safe on its own. You need to use the Lock property
        /// to manage thread-safety yourself.
        /// null if RecreateCalculator is not yet called.
        /// </summary>
        public IPatchCalculator Calculator { get; private set; }

        public MultiThreadedPatchCalculatorContainer(NoteRecycler noteRecycler, RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _noteRecycler = noteRecycler ?? throw new NullException(() => noteRecycler);
        }

        /// <summary>
        /// You must call this on the thread that keeps the IContext open. 
        /// Thread-safe: Will automatically use a WriteLock.
        /// </summary>
        public void RecreateCalculator(Patch patch, int samplingRate, int channelCount, int maxConcurrentNotes)
        {
            var newCalculator = new MultiThreadedPatchCalculator(
                patch,
                samplingRate,
                channelCount,
                maxConcurrentNotes,
                _noteRecycler,
                _repositories);

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
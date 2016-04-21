using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class MultiThreadedPatchCalculatorContainer : IPatchCalculatorContainer
    {
        private readonly NoteRecycler _noteRecycler;
        private readonly int _threadCount;
        private readonly double _sampleDuration;
        private readonly int _bufferSize;

        public ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

        /// <summary> null if RecreateCalculator is not yet called. </summary>
        public IPatchCalculator Calculator { get; private set; }

        public MultiThreadedPatchCalculatorContainer(NoteRecycler noteRecycler, int maxThreadCount, AudioOutput audioOutput)
        {
            if (noteRecycler == null) throw new NullException(() => noteRecycler);
            if (audioOutput == null) throw new NullException(() => audioOutput);

            _noteRecycler = noteRecycler;
            _sampleDuration = audioOutput.GetSampleDuration();

            _bufferSize = 2205; // TODO: Manage that it is the same as what SampleProvider needs. Try doing it in an orderly fashion.

            int numberOfHardwareThreads = Environment.ProcessorCount;
            int threadCount = numberOfHardwareThreads;
            if (threadCount > maxThreadCount)
            {
                threadCount = maxThreadCount;
            }

            _threadCount = maxThreadCount;
        }

        private MultiThreadedPatchCalculator CreateCalculator()
        {
            var polyphonyCalculator = new MultiThreadedPatchCalculator(_threadCount, _bufferSize, _sampleDuration, _noteRecycler);

            return polyphonyCalculator;
        }

        /// <summary> 
        /// You must call this on the thread that keeps the IContext open. 
        /// Will automatically use a WriteLock.
        /// </summary>
        public void RecreateCalculator(
            IList<Patch> patches,
            int maxConcurrentNotes,
            PatchRepositories repositories)
        {
            var patchManager = new PatchManager(repositories);

            var calculatorCache = new CalculatorCache();

            MultiThreadedPatchCalculator newPolyphonyCalculator = CreateCalculator();

            var patchCalculators = new List<IPatchCalculator>(maxConcurrentNotes);

            for (int i = 0; i < maxConcurrentNotes; i++)
            {
                patchManager.AutoPatch(patches);
                Patch autoPatch = patchManager.Patch;
                Outlet signalOutlet = autoPatch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                               .Where(x => x.Result.GetDimensionEnum() == DimensionEnum.Signal)
                                               .SingleOrDefault();
                if (signalOutlet == null)
                {
                    signalOutlet = patchManager.Number(0.0);
#if DEBUG
                    signalOutlet.Operator.Name = "Dummy operator, because Auto-Patch has no signal outlets.";
#endif
                }

                IPatchCalculator patchCalculator = patchManager.CreateCalculator(calculatorCache, signalOutlet);
                patchCalculators.Add(patchCalculator);
            }

            newPolyphonyCalculator.AddPatchCalculators(patchCalculators);

            Lock.EnterWriteLock();
            try
            {
                if (Calculator != null)
                {
                    newPolyphonyCalculator.CloneValues(Calculator);

                    //var disposable = Calculator as IDisposable;
                    //if (disposable != null)
                    //{
                    //    disposable.Dispose();
                    //}
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

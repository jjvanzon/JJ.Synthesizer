using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JJ.Business.Synthesizer;
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
        public ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

        /// <summary> null if RecreateCalculator is not yet called. </summary>
        public IPatchCalculator Calculator { get; private set; }

        private MultiThreadedPatchCalculator CreatePolyphonyCalculator()
        {
            int numberOfHardwareThreads = Environment.ProcessorCount;

            int threadCount = numberOfHardwareThreads;
            int bufferSize = 2205; // TODO: Manage that it is the same as what SampleProvider needs. Try doing it in an orderly fashion.
            double sampleDuration = 1.0 / 44100; // TODO: Manage that it is the same as what SampleProvider needs. Try doing it in an orderly fashion.

            var polyphonyCalculator = new MultiThreadedPatchCalculator(threadCount, bufferSize, sampleDuration);

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

            MultiThreadedPatchCalculator newPolyphonyCalculator = CreatePolyphonyCalculator();

            var patchCalculators = new List<IPatchCalculator>(maxConcurrentNotes);

            for (int i = 0; i < maxConcurrentNotes; i++)
            {
                patchManager.AutoPatch(patches);
                Patch autoPatch = patchManager.Patch;
                Outlet signalOutlet = autoPatch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                               .Where(x => x.Input.GetOutletTypeEnum() == OutletTypeEnum.Signal)
                                               .SingleOrDefault();

                if (signalOutlet == null)
                {
                    signalOutlet = patchManager.Number(0.0);
                }

                IPatchCalculator patchCalculator = patchManager.CreateOptimizedCalculator(signalOutlet);
                patchCalculators.Add(patchCalculator);
            }

            newPolyphonyCalculator.AddPatchCalculators(patchCalculators);

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

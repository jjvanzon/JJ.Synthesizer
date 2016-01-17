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
    public static class PolyphonyCalculatorContainer
    {
        public static ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

        /// <summary> null if RecreateCalculator is not yet called. </summary>
        public static PolyphonyCalculator Calculator { get; private set; }

        private static PolyphonyCalculator CreatePolyphonyCalculator()
        {
            int threadCount = 1; // Less threads for testing.
            //int threadCount = 4; // TODO: Get from system information
            int bufferSize = 2205; // TODO: Manage that it is the same as what SampleProvider needs. Try doing it in an orderly fashion.
            double sampleDuration = 1.0 / 44100; // TODO: Manage that it is the same as what SampleProvider needs. Try doing it in an orderly fashion.

            var polyphonyCalculator = new PolyphonyCalculator(threadCount, bufferSize, sampleDuration);

            return polyphonyCalculator;
        }

        /// <summary> 
        /// You must call this on the thread that keeps the IContext open. 
        /// Will automatically use a WriteLock.
        /// </summary>
        public static void RecreateCalculator(
            IList<Patch> patches, 
            int maxConcurrentNotes, 
            PatchRepositories repositories)
        {
            var patchManager = new PatchManager(repositories);

            PolyphonyCalculator newPolyphonyCalculator = CreatePolyphonyCalculator();

            for (int i = 0; i < maxConcurrentNotes; i++)
            {
                patchManager.AutoPatch(patches);
                Patch autoPatch = patchManager.Patch;
                Outlet signalOutlet = autoPatch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                               .Where(x => x.Input.GetOutletTypeEnum() == OutletTypeEnum.Signal)
                                               .Single();

                IPatchCalculator patchCalculator = patchManager.CreateOptimizedCalculator(signalOutlet);
                newPolyphonyCalculator.AddPatchCalculator(patchCalculator, i);
            }

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

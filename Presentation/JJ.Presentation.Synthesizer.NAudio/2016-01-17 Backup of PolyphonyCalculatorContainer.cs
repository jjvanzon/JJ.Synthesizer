//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using JJ.Business.Synthesizer;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Presentation.Synthesizer.NAudio
//{
//    public static class PolyphonyCalculatorContainer
//    {
//        public static ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

//        public static PolyphonyCalculator Calculator { get; } = CreatePolyphonyCalculator();

//        private static PolyphonyCalculator CreatePolyphonyCalculator()
//        {
//            int threadCount = 4; // TODO: Get from system information
//            int bufferSize = 2205; // TODO: Manage that it is the same as what SampleProvider needs. Try doing it in an orderly fashion.
//            double sampleDuration = 1.0 / 44100; // TODO: Manage that it is the same as what SampleProvider needs. Try doing it in an orderly fashion.

//            var polyphonyCalculator = new PolyphonyCalculator(threadCount, bufferSize, sampleDuration);

//            return polyphonyCalculator;
//        }

//        /// <summary> 
//        /// You must call this on the thread that keeps the IContext open. 
//        /// Will automatically use a WriteLock.
//        /// </summary>
//        public static void ReplacePatchCalculator(
//            IPatchCalculator oldPatchCalculator,
//            IList<Patch> patches, 
//            int maxConcurrentNotes, 
//            PatchRepositories repositories)
//        {
//            if (oldPatchCalculator == null) throw new NullException(() => oldPatchCalculator);

//            var patchManager = new PatchManager(repositories);
//            Outlet autoPatchOutlet = patchManager.AutoPatchPolyphonic(patches, maxConcurrentNotes);
//            IPatchCalculator patchCalculator = patchManager.CreateOptimizedCalculator(autoPatchOutlet);

//            Lock.EnterWriteLock();
//            try
//            {
//                if (Calculator != null)
//                {
//                    patchCalculator.CloneValues(oldPatchCalculator);

//                    Calculator.RemoveCalculator(oldPatchCalculator);
//                    Calculator.AddCalculator(patchCalculator);
//                }
//            }
//            finally
//            {
//                Lock.ExitWriteLock();
//            }
//        }
//    }
//}

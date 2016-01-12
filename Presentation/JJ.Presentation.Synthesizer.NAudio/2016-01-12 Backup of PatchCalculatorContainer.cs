//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using JJ.Business.Synthesizer;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer;

//namespace JJ.Presentation.Synthesizer.NAudio
//{
//    public static class PatchCalculatorContainer
//    {
//        // TODO: Kind of temporary (2016-01-11) to test something for the NoteStart delays. It used to be a local variable
//        //private static Outlet _autoPatchOutlet;

//        public static ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();

//        /// <summary> null if CreatePatchCalculator is not yet called. </summary>
//        public static IPatchCalculator PatchCalculator { get; private set; }

//        /// <summary> 
//        /// You must call this on the thread that keeps the IContext open. 
//        /// Will automatically use a WriteLock.
//        /// </summary>
//        public static void RecreatePatchCalculator(
//            IList<Patch> patches, 
//            int maxConcurrentNotes, 
//            PatchRepositories repositories)
//        {
//            // TODO: Yield over values from old to new patch calculator.

//            var patchManager = new PatchManager(repositories);
//            Outlet autoPatchOutlet = patchManager.AutoPatchPolyphonic(patches, maxConcurrentNotes);

//            IPatchCalculator patchCalculator = patchManager.CreateOptimizedCalculator(autoPatchOutlet);

//            Lock.EnterWriteLock();
//            try
//            {
//                PatchCalculator = patchCalculator;
//            }
//            finally
//            {
//                Lock.ExitWriteLock();
//            }
//        }

//        //// TODO: Kind of temporary (2016-01-11) to test something for the NoteStart delays.
//        //public static Outlet GetMonophonicCalculator(int listIndex)
//        //{
//        //    // HACK: Quite an assumption that it is an adder
//        //    var wrapper = new Adder_OperatorWrapper(_autoPatchOutlet.Operator);
//        //    return wrapper.Operands[listIndex];
//        //}
//    }
//}

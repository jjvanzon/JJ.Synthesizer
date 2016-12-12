using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal static class PatchCalculationHelper
    {
        /// <summary>
        /// Returns the corresponding Outlet (of the PatchOutlet) in the Underlying Patch 
        /// after having assigned the Underlying Patch's (PatchInlets') Inlets.
        /// The returned outlet is then ready to be used like any other operator.
        /// 
        /// Note that even though a CustomOperator can have multiple outlets, you will only be using one at a time in your calculations.
        /// </summary>
        public static Outlet TryApplyCustomOperatorToUnderlyingPatch(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
        {
            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Operator source_CustomOperator = source_CustomOperator_Outlet.Operator;
            CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
            Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;

            if (dest_UnderlyingPatch == null)
            {
                return null;
            }

            foreach (Inlet source_CustomOperator_Inlet in source_CustomOperator.Inlets)
            {
                Operator dest_UnderlyingPatch_PatchInlet = InletOutletMatcher.GetPatchInlet(source_CustomOperator_Inlet, patchRepository);

                PatchInlet_OperatorWrapper dest_UnderlyingPatch_PatchInlet_Wrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet);
                dest_UnderlyingPatch_PatchInlet_Wrapper.Inlet.LinkTo(source_CustomOperator_Inlet.InputOutlet);
            }

            Operator dest_UnderlyingPatch_PatchOutlet = InletOutletMatcher.GetPatchOutlet(source_CustomOperator_Outlet, patchRepository);

            PatchOutlet_OperatorWrapper dest_UnderlyingPatch_PatchOutlet_Wrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet);
            return dest_UnderlyingPatch_PatchOutlet_Wrapper.Result;
        }
    }
}

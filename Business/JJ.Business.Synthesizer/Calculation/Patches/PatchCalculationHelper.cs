using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

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

            var source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
            Patch destUnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;

            if (destUnderlyingPatch == null)
            {
                return null;
            }

            // Cross reference custom operator's inlets with the Underlying Patch's PatchInlets.
            var tuples = from source_CustomOperator_Inlet in source_CustomOperator.Inlets
                         join dest_UnderlyingPatch_PatchInlet in destUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                         // The PatchToOperatorConverter and the OperatorValidator_CustomOperator guarantee that the names match.
                         on source_CustomOperator_Inlet.Name equals dest_UnderlyingPatch_PatchInlet.Name
                         select new
                         {
                             Source_CustomOperator_Inlet = source_CustomOperator_Inlet,
                             Dest_UnderlyingPatch_PatchInlet = dest_UnderlyingPatch_PatchInlet
                         };

            // Each custom operator's inlet has an input outlet. 
            // This input outlet should be assigned as the corresponding Underlying Patch Inlet.
            foreach (var tuple in tuples)
            {
                Operator dest_UnderlyingPatch_PatchInlet = tuple.Dest_UnderlyingPatch_PatchInlet;
                Inlet sourceCustomOperatorInlet = tuple.Source_CustomOperator_Inlet;

                var dest_UnderlyingPatch_PatchInlet_Wrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet);
                dest_UnderlyingPatch_PatchInlet_Wrapper.Inlet.InputOutlet = sourceCustomOperatorInlet.InputOutlet;
            }

            // Use the (custom operator's) outlet name and look it up in the Underlying Patch's outlets.
            Operator dest_UnderlyingPatch_PatchOutlet = destUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                                                           .Where(x => String.Equals(x.Name, source_CustomOperator_Outlet.Name))
                                                                           .First();

            // Return the result of that Underlying Patch's outlet.
            var dest_UnderlyingPatch_PatchOutlet_Wrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet);
            return dest_UnderlyingPatch_PatchOutlet_Wrapper.Result;
        }
    }
}

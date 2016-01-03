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
        public static Outlet TryApplyCustomOperatorToUnderlyingPatch(Outlet customOperatorOutlet, IPatchRepository patchRepository)
        {
            if (customOperatorOutlet == null) throw new NullException(() => customOperatorOutlet);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Operator customOperator = customOperatorOutlet.Operator;

            var customOperatorWrapper = new CustomOperator_OperatorWrapper(customOperator, patchRepository);
            Patch underlyingPatch = customOperatorWrapper.UnderlyingPatch;

            if (underlyingPatch == null)
            {
                return null;
            }

            // Cross reference custom operator's inlets with the Underlying Patch's PatchInlets.
            var tuples = from customOperatorInlet in customOperator.Inlets
                         join underlyingPatchInlet in underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                         // The PatchToOperatorConverter and the OperatorValidator_CustomOperator guarantee that the names match.
                         on customOperatorInlet.Name equals underlyingPatchInlet.Name
                         select new { CustomOperatorInlet = customOperatorInlet, UnderlyingPatchInlet = underlyingPatchInlet };

            // Each custom operator's inlet has an input outlet. 
            // This input outlet should be assigned as the corresponding Underlying Patch Inlet.
            foreach (var tuple in tuples)
            {
                Operator underlyingPatchInlet = tuple.UnderlyingPatchInlet;
                Inlet customOperatorInlet = tuple.CustomOperatorInlet;

                var underlyingPatchInletWrapper = new PatchInlet_OperatorWrapper(underlyingPatchInlet);
                Inlet patchInlet_InputInlet = OperatorHelper.GetInlet(underlyingPatchInlet, OperatorConstants.PATCH_INLET_INPUT_INDEX);
                patchInlet_InputInlet.DefaultValue = underlyingPatchInletWrapper.Inlet.DefaultValue;
                patchInlet_InputInlet.InputOutlet = customOperatorInlet.InputOutlet;
            }

            // Use the (custom operator's) outlet name and look it up in the Underlying Patch's outlets.
            Operator underlyingPatchOutlet = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                                            .Where(x => String.Equals(x.Name, customOperatorOutlet.Name))
                                                            .First();

            // Return the result of that Underlying Patch's outlet.
            var underlyingPatchOutletWrapper = new PatchOutlet_OperatorWrapper(underlyingPatchOutlet);
            return underlyingPatchOutletWrapper.Result;
        }
    }
}

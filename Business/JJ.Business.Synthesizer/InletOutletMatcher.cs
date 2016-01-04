using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer
{
    internal static class InletOutletMatcher
    {
        public static Inlet TryGetCustomOperatorInlet(IList<Inlet> destCustomOperatorInlets, Operator sourcePatchInlet)
        {
            if (destCustomOperatorInlets == null) throw new NullException(() => destCustomOperatorInlets);
            if (sourcePatchInlet == null) throw new NullException(() => sourcePatchInlet);

            // Try match by name
            foreach (Inlet destCustomOperatorInlet in destCustomOperatorInlets)
            {
                if (String.Equals(destCustomOperatorInlet.Name, sourcePatchInlet.Name))
                {
                    return destCustomOperatorInlet;
                }
            }

            // Try match by type
            foreach (Inlet destCustomOperatorInlet in destCustomOperatorInlets)
            {
                // TODO: I should really only match if it is unique.
                var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                if (destCustomOperatorInlet.GetInletTypeEnum() == sourcePatchInletWrapper.Inlet.GetInletTypeEnum())
                {
                    return destCustomOperatorInlet;
                }
            }

            // Try match by list index
            foreach (Inlet destInlet in destCustomOperatorInlets)
            {
                var wrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);
                if (destInlet.ListIndex == wrapper.ListIndex)
                {
                    return destInlet;
                }
            }

            return null;
        }

        public static Outlet TryGetCustomOperatorOutlet(IList<Outlet> destCustomOperatorOutlets, Operator sourcePatchOutlet)
        {
            if (destCustomOperatorOutlets == null) throw new NullException(() => destCustomOperatorOutlets);
            if (sourcePatchOutlet == null) throw new NullException(() => sourcePatchOutlet);

            // Try match by name
            foreach (Outlet destCustomOperatorOutlet in destCustomOperatorOutlets)
            {
                if (String.Equals(destCustomOperatorOutlet.Name, sourcePatchOutlet.Name))
                {
                    return destCustomOperatorOutlet;
                }
            }

            // Try match by type
            foreach (Outlet destOutlet in destCustomOperatorOutlets)
            {
                // TODO: I should really only match if it is unique.
                var wrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);
                if (destOutlet.GetOutletTypeEnum() == wrapper.Result.GetOutletTypeEnum())
                {
                    return destOutlet;
                }
            }

            // Try match by list index
            foreach (Outlet destOutlet in destCustomOperatorOutlets)
            {
                var wrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);
                if (destOutlet.ListIndex == wrapper.ListIndex)
                {
                    return destOutlet;
                }
            }

            return null;
        }

        public static Operator GetPatchInlet(Inlet source_CustomOperator_Inlet, IPatchRepository patchRepository)
        {
            if (source_CustomOperator_Inlet == null) throw new NullException(() => source_CustomOperator_Inlet);

            Operator dest_UnderlyingPatch_PatchInlet = TryGetPatchInlet(source_CustomOperator_Inlet, patchRepository);

            if (dest_UnderlyingPatch_PatchInlet == null)
            {
                throw new Exception(String.Format(
                    "PatchInlet not found in UnderlyingPatch. CustomOperator Inlet: Name = '{0}', InletTypeEnum = '{1}', ListIndex = '{2}'.",
                    source_CustomOperator_Inlet.Name,
                    source_CustomOperator_Inlet.GetInletTypeEnum(),
                    source_CustomOperator_Inlet.ListIndex));
            }

            return dest_UnderlyingPatch_PatchInlet;
        }

        public static Operator TryGetPatchInlet(Inlet source_CustomOperator_Inlet, IPatchRepository patchRepository)
        {
            if (source_CustomOperator_Inlet == null) throw new NullException(() => source_CustomOperator_Inlet);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Operator source_CustomOperator = source_CustomOperator_Inlet.Operator;
            CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
            Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;

            IList<Operator> dest_UnderlyingPatch_PatchInlets = dest_UnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            // Try match by name
            foreach (Operator dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets)
            {
                if (String.Equals(dest_UnderlyingPatch_PatchInlet.Name, source_CustomOperator_Inlet.Name))
                {
                    return dest_UnderlyingPatch_PatchInlet;
                }
            }

            // Try match by type
            foreach (Operator dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets)
            {
                var dest_UnderlyingPatch_PatchInlet_Wrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet);
                if (dest_UnderlyingPatch_PatchInlet_Wrapper.Inlet.GetInletTypeEnum() == source_CustomOperator_Inlet.GetInletTypeEnum())
                {
                    return dest_UnderlyingPatch_PatchInlet;
                }
            }

            // Try match by list index
            foreach (Operator dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets)
            {
                var dest_UnderlyingPatch_PatchInlet_Wrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet);
                if (dest_UnderlyingPatch_PatchInlet_Wrapper.ListIndex == source_CustomOperator_Inlet.ListIndex)
                {
                    return dest_UnderlyingPatch_PatchInlet;
                }
            }

            return null;
        }

        public static Operator GetPatchOutlet(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
        {
            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);

            Operator dest_UnderlyingPatch_PatchOutlet = TryGetPatchOutlet(source_CustomOperator_Outlet, patchRepository);

            if (dest_UnderlyingPatch_PatchOutlet == null)
            {
                throw new Exception(String.Format(
                    "PatchOutlet not found in UnderlyingPatch. CustomOperator Outlet: Name = '{0}', OutletTypeEnum = '{1}', ListIndex = '{2}'.",
                    source_CustomOperator_Outlet.Name,
                    source_CustomOperator_Outlet.GetOutletTypeEnum(),
                    source_CustomOperator_Outlet.ListIndex));
            }

            return dest_UnderlyingPatch_PatchOutlet;
        }

        public static Operator TryGetPatchOutlet(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
        {
            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Operator source_CustomOperator = source_CustomOperator_Outlet.Operator;
            CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
            Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;

            IList<Operator> dest_UnderlyingPatch_PatchOutlets = dest_UnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

            // Try match by name
            foreach (Operator dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets)
            {
                if (String.Equals(dest_UnderlyingPatch_PatchOutlet.Name, source_CustomOperator_Outlet.Name))
                {
                    return dest_UnderlyingPatch_PatchOutlet;
                }
            }

            // Try match by type
            foreach (Operator dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets)
            {
                var dest_UnderlyingPatch_PatchOutlet_Wrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet);
                if (dest_UnderlyingPatch_PatchOutlet_Wrapper.Result.GetOutletTypeEnum() == source_CustomOperator_Outlet.GetOutletTypeEnum())
                {
                    return dest_UnderlyingPatch_PatchOutlet;
                }
            }

            // Try match by list index
            foreach (Operator dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets)
            {
                var dest_UnderlyingPatch_PatchOutlet_Wrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet);
                if (dest_UnderlyingPatch_PatchOutlet_Wrapper.ListIndex == source_CustomOperator_Outlet.ListIndex)
                {
                    return dest_UnderlyingPatch_PatchOutlet;
                }
            }

            return null;
        }

        public static bool AreMatch(Outlet outlet, Inlet inlet)
        {
            if (outlet == null) throw new NullException(() => outlet);
            if (inlet == null) throw new NullException(() => inlet);

            // First match by OutletType / InletType.
            OutletTypeEnum outletTypeEnum = outlet.GetOutletTypeEnum();
            if (outletTypeEnum != OutletTypeEnum.Undefined)
            {
                InletTypeEnum inletTypeEnum = inlet.GetInletTypeEnum();
                if (inletTypeEnum != InletTypeEnum.Undefined)
                {
                    string outletTypeString = outletTypeEnum.ToString();
                    string inletTypeString = inletTypeEnum.ToString();

                    if (String.Equals(outletTypeString, inletTypeString))
                    {
                        return true;
                    }
                }
            }

            // Then match by name
            if (String.Equals(outlet.Name, inlet.Name))
            {
                return true;
            }

            // Do not match by list index, because that would result in something arbitrary.

            return false;
        }
    }
}

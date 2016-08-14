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
        public static Inlet TryGetCustomOperatorInlet(Operator sourcePatchInlet, IList<Inlet> destCustomOperatorInlets)
        {
            if (destCustomOperatorInlets == null) throw new NullException(() => destCustomOperatorInlets);
            if (sourcePatchInlet == null) throw new NullException(() => sourcePatchInlet);

            var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);

            // Try match by name
            bool nameIsFilledIn = !String.IsNullOrEmpty(sourcePatchInlet.Name);
            if (nameIsFilledIn)
            {
                foreach (Inlet destCustomOperatorInlet in destCustomOperatorInlets)
                {
                    bool namesAreEqual = String.Equals(
                        destCustomOperatorInlet.Name,
                        sourcePatchInlet.Name,
                        StringComparison.OrdinalIgnoreCase);

                    if (namesAreEqual)
                    {
                        return destCustomOperatorInlet;
                    }
                }
            }

            // Try match by Dimension (if unique)
            DimensionEnum sourceDimensionEnum = sourcePatchInletWrapper.Inlet.GetDimensionEnum();

            if (sourceDimensionEnum != DimensionEnum.Undefined)
            {
                IList<Inlet> destCustomOperatorInlets_WithMatchingDimension =
                    destCustomOperatorInlets.Where(x => x.GetDimensionEnum() == sourceDimensionEnum).ToArray();

                if (destCustomOperatorInlets_WithMatchingDimension.Count == 1)
                {
                    return destCustomOperatorInlets_WithMatchingDimension[0];
                }
            }

            // Try match by list index
            foreach (Inlet destInlet in destCustomOperatorInlets)
            {
                if (destInlet.ListIndex == sourcePatchInletWrapper.ListIndex)
                {
                    return destInlet;
                }
            }

            return null;
        }

        public static Outlet TryGetCustomOperatorOutlet(Operator sourcePatchOutlet, IList<Outlet> destCustomOperatorOutlets)
        {
            if (destCustomOperatorOutlets == null) throw new NullException(() => destCustomOperatorOutlets);
            if (sourcePatchOutlet == null) throw new NullException(() => sourcePatchOutlet);

            var sourcePatchOutletWrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);

            // Try match by name
            bool nameIsFilledIn = !String.IsNullOrEmpty(sourcePatchOutlet.Name);
            if (nameIsFilledIn)
            {
                foreach (Outlet destCustomOperatorOutlet in destCustomOperatorOutlets)
                {
                    bool namesAreEqual = String.Equals(
                        destCustomOperatorOutlet.Name,
                        sourcePatchOutlet.Name,
                        StringComparison.OrdinalIgnoreCase);

                    if (namesAreEqual)
                    {
                        return destCustomOperatorOutlet;
                    }
                }
            }

            // Try match by Dimension (if unique)
            DimensionEnum sourceDimensionEnum = sourcePatchOutletWrapper.Result.GetDimensionEnum();

            if (sourceDimensionEnum != DimensionEnum.Undefined)
            {
                IList<Outlet> destCustomOperatorOutlets_WithMatchingDimension =
                    destCustomOperatorOutlets.Where(x => x.GetDimensionEnum() == sourceDimensionEnum).ToArray();

                if (destCustomOperatorOutlets_WithMatchingDimension.Count == 1)
                {
                    return destCustomOperatorOutlets_WithMatchingDimension[0];
                }
            }

            // Try match by list index
            foreach (Outlet destOutlet in destCustomOperatorOutlets)
            {
                if (destOutlet.ListIndex == sourcePatchOutletWrapper.ListIndex)
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
                    "PatchInlet not found in UnderlyingPatch. CustomOperator Inlet: Name = '{0}', DimensionEnum = '{1}', ListIndex = '{2}'.",
                    source_CustomOperator_Inlet.Name,
                    source_CustomOperator_Inlet.GetDimensionEnum(),
                    source_CustomOperator_Inlet.ListIndex));
            }

            return dest_UnderlyingPatch_PatchInlet;
        }

        public static Operator TryGetPatchInlet(Inlet source_CustomOperator_Inlet, IPatchRepository patchRepository)
        {
            if (source_CustomOperator_Inlet == null) throw new NullException(() => source_CustomOperator_Inlet);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            IList<Operator> dest_UnderlyingPatch_PatchInlets;
            {
                Operator source_CustomOperator = source_CustomOperator_Inlet.Operator;
                CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
                Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;
                dest_UnderlyingPatch_PatchInlets = dest_UnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);
            }

            // Try match by name
            bool nameIsFilledIn = !String.IsNullOrEmpty(source_CustomOperator_Inlet.Name);
            if (nameIsFilledIn)
            {
                foreach (Operator dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets)
                {
                    bool namesAreEqual = String.Equals(
                        dest_UnderlyingPatch_PatchInlet.Name,
                        source_CustomOperator_Inlet.Name,
                        StringComparison.OrdinalIgnoreCase);

                    if (namesAreEqual)
                    {
                        return dest_UnderlyingPatch_PatchInlet;
                    }
                }
            }

            // Try match by Dimension (if unique)
            DimensionEnum sourceDimensionEnum = source_CustomOperator_Inlet.GetDimensionEnum();

            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                var dest_Underlying_PatchInlets_WithMatchingDimension = new List<Operator>();

                foreach (Operator dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets)
                {
                    var destWrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet);

                    bool dimensionsAreEqual = destWrapper.Inlet.GetDimensionEnum() == sourceDimensionEnum;
                    if (dimensionsAreEqual)
                    {
                        dest_Underlying_PatchInlets_WithMatchingDimension.Add(dest_UnderlyingPatch_PatchInlet);
                    }
                }

                if (dest_Underlying_PatchInlets_WithMatchingDimension.Count == 1)
                {
                    return dest_Underlying_PatchInlets_WithMatchingDimension[0];
                }
            }

            // Try match by list index
            foreach (Operator dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets)
            {
                var destWrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet);
                if (destWrapper.ListIndex == source_CustomOperator_Inlet.ListIndex)
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
                    "PatchOutlet not found in UnderlyingPatch. CustomOperator Outlet: Name = '{0}', DimensionEnum = '{1}', ListIndex = '{2}'.",
                    source_CustomOperator_Outlet.Name,
                    source_CustomOperator_Outlet.GetDimensionEnum(),
                    source_CustomOperator_Outlet.ListIndex));
            }

            return dest_UnderlyingPatch_PatchOutlet;
        }

        public static Operator TryGetPatchOutlet(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
        {
            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            IList<Operator> dest_UnderlyingPatch_PatchOutlets;
            {
                Operator source_CustomOperator = source_CustomOperator_Outlet.Operator;
                CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
                Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;
                dest_UnderlyingPatch_PatchOutlets = dest_UnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);
            }

            // Try match by name
            bool nameIsFilledIn = !String.IsNullOrEmpty(source_CustomOperator_Outlet.Name);
            if (nameIsFilledIn)
            {
                foreach (Operator dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets)
                {
                    bool namesAreEqual = String.Equals(
                        dest_UnderlyingPatch_PatchOutlet.Name,
                        source_CustomOperator_Outlet.Name,
                        StringComparison.OrdinalIgnoreCase);

                    if (namesAreEqual)
                    {
                        return dest_UnderlyingPatch_PatchOutlet;
                    }
                }
            }

            // Try match by Dimension (if unique)
            DimensionEnum sourceDimensionEnum = source_CustomOperator_Outlet.GetDimensionEnum();

            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                var dest_UnderlyingPatch_PatchOutlets_WithMatchingDimension = new List<Operator>();

                foreach (Operator dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets)
                {
                    var destWrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet);

                    bool dimensionsAreEqual = destWrapper.Result.GetDimensionEnum() == sourceDimensionEnum;
                    if (dimensionsAreEqual)
                    {
                        dest_UnderlyingPatch_PatchOutlets_WithMatchingDimension.Add(dest_UnderlyingPatch_PatchOutlet);
                    }
                }

                if (dest_UnderlyingPatch_PatchOutlets_WithMatchingDimension.Count == 1)
                {
                    return dest_UnderlyingPatch_PatchOutlets_WithMatchingDimension[0];
                }
            }

            // Try match by list index
            foreach (Operator dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets)
            {
                var destWrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet);
                if (destWrapper.ListIndex == source_CustomOperator_Outlet.ListIndex)
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

            // Try match by name
            bool nameIsfilledIn = !String.IsNullOrEmpty(outlet.Name);
            if (nameIsfilledIn)
            {
                bool namesAreEqual = String.Equals(outlet.Name, inlet.Name, StringComparison.OrdinalIgnoreCase);
                if (namesAreEqual)
                {
                    return true;
                }
            }

            // Try match by Dimension (be tollerant towards non-unicity, for more chance to get a match).
            DimensionEnum outletDimensionEnum = outlet.GetDimensionEnum();
            if (outletDimensionEnum != DimensionEnum.Undefined)
            {
                DimensionEnum inletDimensionEnum = inlet.GetDimensionEnum();
                if (inletDimensionEnum != DimensionEnum.Undefined)
                {
                    if (outletDimensionEnum == inletDimensionEnum)
                    {
                        return true;
                    }
                }
            }

            // Do not match by list index, because that would result in something arbitrary.

            return false;
        }
    }
}

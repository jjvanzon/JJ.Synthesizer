using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer
{
    internal static class InletOutletMatcher
    {
        // Patch to CustomOperator (e.g. for Converters)

        /// <summary> Returned tuples can contain null-elements. </summary>
        public static IList<(Operator sourcePatchInlet, Inlet destCustomOperatorInlet)> Match_PatchInlets_With_CustomOperatorInlets(
            IList<Operator> sourcePatchInlets,
            IList<Inlet> destCustomOperatorInlets)
        {
            IList<Operator> sourceSortedPatchInlets = SortPatchInlets(sourcePatchInlets);
            IList<Inlet> destCandidateCustomOperatorInlets = SortInlets(destCustomOperatorInlets);

            var tuples = new List<(Operator sourcePatchInlet, Inlet destCustomOperatorInlet)>();

            foreach (Operator sourcePatchInlet in sourceSortedPatchInlets)
            {
                Inlet destCustomOperatorInlet = TryGetCustomOperatorInlet(sourcePatchInlet, destCandidateCustomOperatorInlets);

                tuples.Add((sourcePatchInlet, destCustomOperatorInlet));

                destCandidateCustomOperatorInlets.Remove(destCustomOperatorInlet);
            }

            return tuples;
        }

        private static Inlet TryGetCustomOperatorInlet(Operator sourcePatchInlet, IList<Inlet> destCandicateCustomOperatorInlets)
        {
            if (destCandicateCustomOperatorInlets == null) throw new NullException(() => destCandicateCustomOperatorInlets);

            var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);

            // Try match by name
            if (NameHelper.IsFilledIn(sourcePatchInletWrapper.Name))
            {
                string canonicalSourceName = NameHelper.ToCanonical(sourcePatchInletWrapper.Name);

                foreach (Inlet destCustomOperatorInlet in destCandicateCustomOperatorInlets)
                {
                    string canonicalDestName = NameHelper.ToCanonical(destCustomOperatorInlet.Name);

                    bool namesAreEqual = string.Equals(canonicalDestName, canonicalSourceName);
                    if (namesAreEqual)
                    {
                        return destCustomOperatorInlet;
                    }
                }
            }

            // Try match by Dimension
            DimensionEnum sourceDimensionEnum = sourcePatchInletWrapper.DimensionEnum;
            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                Inlet destCustomOperatorInlet_WithMatchingDimension =
                    destCandicateCustomOperatorInlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);

                if (destCustomOperatorInlet_WithMatchingDimension != null)
                {
                    return destCustomOperatorInlet_WithMatchingDimension;
                }
            }

            // Try match by list index
            Inlet destCustomOperatorInlet_WithMatchingListIndex =
                destCandicateCustomOperatorInlets.FirstOrDefault(destInlet => destInlet.ListIndex == sourcePatchInletWrapper.ListIndex);

            return destCustomOperatorInlet_WithMatchingListIndex;
        }

        /// <summary> Returned tuples can contain null-elements. </summary>
        public static IList<(Operator sourcePatchOutlet, Outlet destCustomOperatorOutlet)> Match_PatchOutlets_With_CustomOperatorOutlets(
            IList<Operator> sourcePatchOutlets, 
            IList<Outlet> destCustomOperatorOutlets)
        {
            IList<Operator> sourceSortedPatchOutlets = SortPatchOutlets(sourcePatchOutlets);
            IList<Outlet> destCandidateCustomOperatorOutlets = SortOutlets(destCustomOperatorOutlets);

            var tuples = new List<(Operator sourcePatchOutlet, Outlet destCustomOperatorOutlet)>();

            foreach (Operator sourcePatchOutlet in sourceSortedPatchOutlets)
            {
                Outlet destCustomOperatorOutlet = TryGetCustomOperatorOutlet(sourcePatchOutlet, destCandidateCustomOperatorOutlets);

                tuples.Add((sourcePatchOutlet, destCustomOperatorOutlet));

                destCandidateCustomOperatorOutlets.Remove(destCustomOperatorOutlet);
            }

            return tuples;
        }

        private static Outlet TryGetCustomOperatorOutlet(Operator sourcePatchOutlet, IList<Outlet> destCandicateCustomOperatorOutlets)
        {
            if (destCandicateCustomOperatorOutlets == null) throw new NullException(() => destCandicateCustomOperatorOutlets);

            var sourcePatchOutletWrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);

            // Try match by name
            if (NameHelper.IsFilledIn(sourcePatchOutletWrapper.Name))
            {
                string canonicalSourceName = NameHelper.ToCanonical(sourcePatchOutletWrapper.Name);

                foreach (Outlet destCustomOperatorOutlet in destCandicateCustomOperatorOutlets)
                {
                    string canonicalDestName = NameHelper.ToCanonical(destCustomOperatorOutlet.Name);

                    bool namesAreEqual = string.Equals(canonicalDestName, canonicalSourceName);
                    if (namesAreEqual)
                    {
                        return destCustomOperatorOutlet;
                    }
                }
            }

            // Try match by Dimension
            DimensionEnum sourceDimensionEnum = sourcePatchOutletWrapper.DimensionEnum;
            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                Outlet destCustomOperatorOutlet_WithMatchingDimension =
                    destCandicateCustomOperatorOutlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);

                if (destCustomOperatorOutlet_WithMatchingDimension != null)
                {
                    return destCustomOperatorOutlet_WithMatchingDimension;
                }
            }

            // Try match by list index
            Outlet customOperatorOutlet_WithMatchingListIndex =
                destCandicateCustomOperatorOutlets.FirstOrDefault(destOutlet => destOutlet.ListIndex == sourcePatchOutletWrapper.ListIndex);

            return customOperatorOutlet_WithMatchingListIndex;
        }
        
        // CustomOperator to Patch (e.g. for Validation)

        /// <summary> destPatchInlet in returned tuples can be null. </summary>
        public static IList<(Inlet sourceCustomOperatorInlet, Operator destPatchInlet)> Match_CustomOperator_With_UnderlyingPatchInlets(
            Operator sourceCustomOperator, 
            IPatchRepository patchRepository)
        {
            IList<Inlet> sourceCustomOperatorInlets = sourceCustomOperator.Inlets.ToList();
            IList<Operator> destPatchInlets = GetUnderlyingPatchInlets(sourceCustomOperator, patchRepository);

            return Match_CustomOperatorInlets_With_UnderlyingPatchInlets(sourceCustomOperatorInlets, destPatchInlets);
        }

        /// <summary> destPatchInlet in returned tuples can be null. </summary>
        private static IList<(Inlet sourceCustomOperatorInlet, Operator destPatchInlet)> Match_CustomOperatorInlets_With_UnderlyingPatchInlets(
            IList<Inlet> sourceCustomOperatorInlets, 
            IList<Operator> destPatchInlets)
        {
            IList<Inlet> sourceSortedCustomOperatorInlets = SortInlets(sourceCustomOperatorInlets);
            IList<Operator> destCandidatePatchInlets = SortPatchInlets(destPatchInlets);

            var tuples = new List<(Inlet sourceCustomOperatorInlet, Operator destPatchInlet)>();

            foreach (Inlet sourceCustomOperatorInlet in sourceSortedCustomOperatorInlets)
            {
                Operator destPatchInlet = TryGetPatchInlet(sourceCustomOperatorInlet, destCandidatePatchInlets);

                tuples.Add((sourceCustomOperatorInlet, destPatchInlet));

                destCandidatePatchInlets.Remove(destPatchInlet);
            }

            return tuples;
        }

        private static IList<Operator> GetUnderlyingPatchInlets(Operator sourceCustomOperator, IPatchRepository patchRepository)
        {
            CustomOperator_OperatorWrapper sourceCustomOperatorWrapper = new CustomOperator_OperatorWrapper(sourceCustomOperator, patchRepository);

            Patch destUnderlyingPatch = sourceCustomOperatorWrapper.UnderlyingPatch;

            if (destUnderlyingPatch == null)
            {
                return new List<Operator>();
            }

            IList<Operator> destUnderlyingPatchInlets = destUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            return destUnderlyingPatchInlets;
        }

        private static Operator TryGetPatchInlet([NotNull] Inlet sourceCustomOperatorInlet, [NotNull] IList<Operator> destUnderlyingPatchInlets)
        {
            if (sourceCustomOperatorInlet == null) throw new NullException(() => sourceCustomOperatorInlet);
            if (destUnderlyingPatchInlets == null) throw new NullException(() => destUnderlyingPatchInlets);

            // Try match by name
            if (NameHelper.IsFilledIn(sourceCustomOperatorInlet.Name))
            {
                string canonicalSourceName = NameHelper.ToCanonical(sourceCustomOperatorInlet.Name);

                foreach (Operator destUnderlyingPatchInlet in destUnderlyingPatchInlets)
                {
                    string canonicalDestName = NameHelper.ToCanonical(destUnderlyingPatchInlet.Name);

                    bool namesAreEqual = string.Equals(canonicalDestName, canonicalSourceName);
                    if (namesAreEqual)
                    {
                        return destUnderlyingPatchInlet;
                    }
                }
            }

            // Try match by Dimension
            DimensionEnum sourceDimensionEnum = sourceCustomOperatorInlet.GetDimensionEnum();
            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                Operator destUnderlyingPatchInlet_WithMatchingDimension =
                    (from destUnderlyingPatchInlet in destUnderlyingPatchInlets
                     let destWrapper = new PatchInlet_OperatorWrapper(destUnderlyingPatchInlet)
                     let dimensionsAreEqual = destWrapper.DimensionEnum == sourceDimensionEnum
                     where dimensionsAreEqual
                     select destUnderlyingPatchInlet).FirstOrDefault();

                if (destUnderlyingPatchInlet_WithMatchingDimension != null)
                {
                    return destUnderlyingPatchInlet_WithMatchingDimension;
                }
            }

            // Try match by list index
            Operator destUnderlyingPatchInlet_WithMatchingListIndex =
                (from destUnderlyingPatchInlet in destUnderlyingPatchInlets
                 let destWrapper = new PatchInlet_OperatorWrapper(destUnderlyingPatchInlet)
                 where destWrapper.ListIndex == sourceCustomOperatorInlet.ListIndex
                 select destUnderlyingPatchInlet).FirstOrDefault();

            return destUnderlyingPatchInlet_WithMatchingListIndex;
        }

        public static IList<(Outlet sourceCustomOperatorOutlet, Operator destPatchOutlet)> Match_CustomOperator_With_UnderlyingPatchOutlets(
            Operator sourceCustomOperator, 
            IPatchRepository patchRepository)
        {
            IList<Outlet> sourceCustomOperatorOutlets = sourceCustomOperator.Outlets.ToList();
            IList<Operator> destUnderlyingPatchOutlet = GetUnderlyingPatchOutlets(sourceCustomOperator, patchRepository);

            return Match_CustomOperatorOutlets_With_UnderlyingPatchOutlets(sourceCustomOperatorOutlets, destUnderlyingPatchOutlet);
        }

        private static IList<(Outlet sourceCustomOperatorOutlet, Operator destPatchOutlet)> Match_CustomOperatorOutlets_With_UnderlyingPatchOutlets(
            IList<Outlet> sourceCustomOperatorOutlets, 
            IList<Operator> destPatchOutlets)
        {
            IList<Outlet> sourceSortedCustomOperatorOutlets = SortOutlets(sourceCustomOperatorOutlets);
            IList<Operator> destCandidatePatchOutlets = SortPatchOutlets(destPatchOutlets);

            var tuples = new List<(Outlet sourceCustomOperatorOutlet, Operator destPatchOutlet)>();
            foreach (Outlet sourceCustomOperatorOutlet in sourceSortedCustomOperatorOutlets)
            {
                Operator destPatchOutlet = TryGetPatchOutlet(sourceCustomOperatorOutlet, destCandidatePatchOutlets);

                tuples.Add((sourceCustomOperatorOutlet, destPatchOutlet));

                destCandidatePatchOutlets.Remove(destPatchOutlet);
            }

            return tuples;
        }

        private static IList<Operator> GetUnderlyingPatchOutlets(Operator sourceCustomOperator, IPatchRepository patchRepository)
        {
            var sourceCustomOperatorWrapper = new CustomOperator_OperatorWrapper(sourceCustomOperator, patchRepository);
            Patch destUnderlyingPatch = sourceCustomOperatorWrapper.UnderlyingPatch;
            IList<Operator> destUnderlyingPatchOutlets = destUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);
            return destUnderlyingPatchOutlets;
        }

        private static Operator TryGetPatchOutlet([NotNull] Outlet sourceCustomOperatorOutlet, [NotNull] IList<Operator> destUnderlyingPatchOutlets)
        {
            if (sourceCustomOperatorOutlet == null) throw new NullException(() => sourceCustomOperatorOutlet);
            if (destUnderlyingPatchOutlets == null) throw new NullException(() => destUnderlyingPatchOutlets);

            // Try match by name
            if (NameHelper.IsFilledIn(sourceCustomOperatorOutlet.Name))
            {
                string canonicalSourceName = NameHelper.ToCanonical(sourceCustomOperatorOutlet.Name);

                foreach (Operator destUnderlyingPatchOutlet in destUnderlyingPatchOutlets)
                {
                    string canonicalDestName = NameHelper.ToCanonical(destUnderlyingPatchOutlet.Name);

                    bool namesAreEqual = string.Equals(canonicalDestName, canonicalSourceName);
                    if (namesAreEqual)
                    {
                        return destUnderlyingPatchOutlet;
                    }
                }
            }

            // Try match by Dimension
            DimensionEnum sourceDimensionEnum = sourceCustomOperatorOutlet.GetDimensionEnum();
            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                var destUnderlyingPatchOutlet_WithMatchingDimension = (
                    from destUnderlyingPatchOutlet in destUnderlyingPatchOutlets
                    let destWrapper = new PatchOutlet_OperatorWrapper(destUnderlyingPatchOutlet)
                    let dimensionsAreEqual = destWrapper.DimensionEnum == sourceDimensionEnum
                    where dimensionsAreEqual
                    select destUnderlyingPatchOutlet).FirstOrDefault();

                if (destUnderlyingPatchOutlet_WithMatchingDimension != null)
                {
                    return destUnderlyingPatchOutlet_WithMatchingDimension;
                }
            }

            // Try match by list index
            Operator destUnderlyingPatchOutlet_WithMatchingListIndex =
                (from destUnderlyingPatchOutlet in destUnderlyingPatchOutlets
                 let destWrapper = new PatchOutlet_OperatorWrapper(destUnderlyingPatchOutlet)
                 where destWrapper.ListIndex == sourceCustomOperatorOutlet.ListIndex
                 select destUnderlyingPatchOutlet).FirstOrDefault();

            return destUnderlyingPatchOutlet_WithMatchingListIndex;
        }

        // Fill in PatchInlet.Inlet and PatchOutlet.Outlet (For Calculations)

        /// <summary>
        /// Used as a helper in producing the output calculation structure.
        ///
        /// Returns the corresponding Outlet (of the PatchOutlet) in the Underlying Patch 
        /// after having assigned the Underlying Patch's (PatchInlets') Inlets.
        /// The returned outlet is then ready to be used like any other operator.
        /// 
        /// Note that even though a CustomOperator can have multiple outlets, you will only be using one at a time in your calculations.
        /// </summary>
        public static Outlet ApplyCustomOperatorToUnderlyingPatch(Outlet sourceCustomOperatorOutlet, IPatchRepository patchRepository)
        {
            Outlet destPatchOutletOutlet = TryApplyCustomOperatorToUnderlyingPatch(sourceCustomOperatorOutlet, patchRepository);
            if (destPatchOutletOutlet == null)
            {
                // TODO: Low priority: This is a vague error message. Can it be made more specific?
                throw new Exception($"{nameof(destPatchOutletOutlet)} was null after {nameof(TryApplyCustomOperatorToUnderlyingPatch)}.");
            }

            return destPatchOutletOutlet;
        }

        /// <summary>
        /// Used as a helper in producing the output calculation structure.
        /// 
        /// Returns the corresponding Outlet (of the PatchOutlet) in the Underlying Patch 
        /// after having assigned the Underlying Patch's (PatchInlets') Inlets.
        /// The returned outlet is then ready to be used like any other operator.
        /// 
        /// Note that even though a CustomOperator can have multiple outlets, you will only be using one at a time in your calculations.
        /// </summary>
        private static Outlet TryApplyCustomOperatorToUnderlyingPatch(Outlet sourceCustomOperatorOutlet, IPatchRepository patchRepository)
        {
            if (sourceCustomOperatorOutlet == null) throw new NullException(() => sourceCustomOperatorOutlet);
            if (patchRepository == null) throw new NullException(() => patchRepository);

            Operator sourceCustomOperator = sourceCustomOperatorOutlet.Operator;
            CustomOperator_OperatorWrapper sourceCustomOperatorWrapper = new CustomOperator_OperatorWrapper(sourceCustomOperator, patchRepository);
            Patch destUnderlyingPatch = sourceCustomOperatorWrapper.UnderlyingPatch;

            if (destUnderlyingPatch == null)
            {
                return null;
            }

            IList<(Inlet sourceCustomOperatorInlet, Operator destPatchInlet)> inletTuples =
                Match_CustomOperator_With_UnderlyingPatchInlets(sourceCustomOperator, patchRepository);

            foreach ((Inlet sourceCustomOperatorInlet, Operator destPatchInlet) tuple in inletTuples)
            {
                Inlet sourceCustomOperatorInlet = tuple.sourceCustomOperatorInlet;

                Operator destUnderlyingPatchInlet = tuple.destPatchInlet;
                // ReSharper disable once InvertIf
                if (destUnderlyingPatchInlet != null)
                {
                    new PatchInlet_OperatorWrapper(destUnderlyingPatchInlet)
                    {
                        Input = sourceCustomOperatorInlet.InputOutlet
                    };
                }
            }

            IList<(Outlet sourceCustomOperatorOutlet, Operator destPatchOutlet)> outletTuples =
                Match_CustomOperator_With_UnderlyingPatchOutlets(sourceCustomOperator, patchRepository);
            Operator destUnderlyingPatchOutlet = outletTuples.Where(x => x.sourceCustomOperatorOutlet == sourceCustomOperatorOutlet).Single().destPatchOutlet;
            // ReSharper disable once InvertIf
            if (destUnderlyingPatchOutlet != null)
            {
                var destUnderlyingPatchOutletWrapper = new PatchOutlet_OperatorWrapper(destUnderlyingPatchOutlet);
                return destUnderlyingPatchOutletWrapper.Outlet;
            }

            return null;
        }

        // Inlet-Outlet Matching (e.g. for Auto-Patching)

        public static bool AreMatch(Outlet outlet, Inlet inlet)
        {
            if (outlet == null) throw new NullException(() => outlet);
            if (inlet == null) throw new NullException(() => inlet);

            // Try match by name
            if (NameHelper.IsFilledIn(outlet.Name))
            {
                if (NameHelper.AreEqual(outlet.Name, inlet.Name))
                {
                    return true;
                }
            }

            // Try match by Dimension (be tollerant towards non-unicity, for more chance to get a match).
            DimensionEnum outletDimensionEnum = outlet.GetDimensionEnum();
            // ReSharper disable once InvertIf
            if (outletDimensionEnum != DimensionEnum.Undefined)
            {
                DimensionEnum inletDimensionEnum = inlet.GetDimensionEnum();
                // ReSharper disable once InvertIf
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

        // Helpers

        private static IList<Operator> SortPatchInlets(IList<Operator> patchInlets)
        {
            return patchInlets.Select(x => new PatchInlet_OperatorWrapper(x))
                              .OrderBy(x => x.ListIndex)
                              .ThenBy(x => x.DimensionEnum == DimensionEnum.Undefined)
                              .ThenBy(x => x.DimensionEnum)
                              .ThenBy(x => string.IsNullOrWhiteSpace(x.Name))
                              .ThenBy(x => x.Name)
                              .Select(x => x.WrappedOperator)
                              .ToList();
        }

        private static IList<Operator> SortPatchOutlets(IList<Operator> patchOutlets)
        {
            return patchOutlets.Select(x => new PatchOutlet_OperatorWrapper(x))
                               .OrderBy(x => x.ListIndex)
                               .ThenBy(x => x.DimensionEnum == DimensionEnum.Undefined)
                               .ThenBy(x => x.DimensionEnum)
                               .ThenBy(x => string.IsNullOrWhiteSpace(x.Name))
                               .ThenBy(x => x.Name)
                               .Select(x => x.WrappedOperator)
                               .ToList();
        }

        private static IList<Inlet> SortInlets(IList<Inlet> inlets)
        {
            return inlets.OrderBy(x => x.ListIndex)
                         .ThenBy(x => x.GetDimensionEnum() == DimensionEnum.Undefined)
                         .ThenBy(x => x.GetDimensionEnum())
                         .ThenBy(x => string.IsNullOrWhiteSpace(x.Name))
                         .ThenBy(x => x.Name)
                         //.ThenBy(x => x.IsObsolete)
                         .ToList();
        }

        private static IList<Outlet> SortOutlets(IList<Outlet> outlets)
        {
            return outlets.OrderBy(x => x.ListIndex)
                          .ThenBy(x => x.GetDimensionEnum() == DimensionEnum.Undefined)
                          .ThenBy(x => x.GetDimensionEnum())
                          .ThenBy(x => string.IsNullOrWhiteSpace(x.Name))
                          .ThenBy(x => x.Name)
                          //.ThenBy(x => x.IsObsolete)
                          .ToList();
        }
    }
}
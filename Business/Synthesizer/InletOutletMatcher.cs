using System;
using System.Collections.Generic;
using System.Linq;
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
        // Patch to CustomOperator (e.g. for Converters and validators)

        public static IList<InletTuple> Match_PatchInlets_With_CustomOperatorInlets(Operator customOperator, IPatchRepository patchRepository)
        {
            IList<Inlet> customOperatorInlets = customOperator.Inlets.ToList();
            IList<Operator> underlyingPatchInlets = GetUnderlyingPatchInlets(customOperator, patchRepository);

            return Match_PatchInlets_With_CustomOperatorInlets(underlyingPatchInlets, customOperatorInlets);
        }

        /// <summary> Returned tuples can contain null-customOperatorInlets. </summary>
        public static IList<InletTuple> Match_PatchInlets_With_CustomOperatorInlets(IList<Operator> underlyingPatchInlets, IList<Inlet> customOperatorInlets)
        {
            IList<Operator> sourceSortedPatchInlets = SortPatchInlets(underlyingPatchInlets);
            IList<Inlet> destCandidateCustomOperatorInlets = SortInlets(customOperatorInlets);

            var tuples = new List<InletTuple>();

            foreach (Operator underlyingPatchInlet in sourceSortedPatchInlets)
            {
                Inlet customOperatorInlet = TryGetCustomOperatorInlet(underlyingPatchInlet, destCandidateCustomOperatorInlets);

                tuples.Add(new InletTuple(underlyingPatchInlet, customOperatorInlet));

                destCandidateCustomOperatorInlets.Remove(customOperatorInlet);
            }

            return tuples;
        }

        private static Inlet TryGetCustomOperatorInlet(Operator underlyingPatchInlet, IList<Inlet> destCandicateCustomOperatorInlets)
        {
            if (destCandicateCustomOperatorInlets == null) throw new NullException(() => destCandicateCustomOperatorInlets);

            var underlyingPatchInletWrapper = new PatchInlet_OperatorWrapper(underlyingPatchInlet);

            // Try match by name
            if (NameHelper.IsFilledIn(underlyingPatchInletWrapper.Name))
            {
                string canonicalSourceName = NameHelper.ToCanonical(underlyingPatchInletWrapper.Name);

                foreach (Inlet customOperatorInlet in destCandicateCustomOperatorInlets)
                {
                    string canonicalDestName = NameHelper.ToCanonical(customOperatorInlet.Name);

                    bool namesAreEqual = string.Equals(canonicalDestName, canonicalSourceName, StringComparison.Ordinal);
                    if (namesAreEqual)
                    {
                        return customOperatorInlet;
                    }
                }
            }

            // Try match by Dimension
            DimensionEnum sourceDimensionEnum = underlyingPatchInletWrapper.DimensionEnum;
            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                Inlet customOperatorInlet_WithMatchingDimension =
                    destCandicateCustomOperatorInlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);

                if (customOperatorInlet_WithMatchingDimension != null)
                {
                    return customOperatorInlet_WithMatchingDimension;
                }
            }

            // Try match by list index
            Inlet customOperatorInlet_WithMatchingListIndex =
                destCandicateCustomOperatorInlets.FirstOrDefault(destInlet => destInlet.ListIndex == underlyingPatchInletWrapper.ListIndex);

            return customOperatorInlet_WithMatchingListIndex;
        }

        public static IList<OutletTuple> Match_PatchOutlets_With_CustomOperatorOutlets(Operator customOperator, IPatchRepository patchRepository)
        {
            IList<Outlet> customOperatorOutlets = customOperator.Outlets.ToList();
            IList<Operator> underlyingPatchOutlets = GetUnderlyingPatchOutlets(customOperator, patchRepository);

            return Match_PatchOutlets_With_CustomOperatorOutlets(underlyingPatchOutlets, customOperatorOutlets);
        }

        /// <summary> Returned tuples can contain null-elements. </summary>
        public static IList<OutletTuple> Match_PatchOutlets_With_CustomOperatorOutlets(IList<Operator> underlyingPatchOutlets, IList<Outlet> customOperatorOutlets)
        {
            IList<Operator> sourceSortedPatchOutlets = SortPatchOutlets(underlyingPatchOutlets);
            IList<Outlet> destCandidateCustomOperatorOutlets = SortOutlets(customOperatorOutlets);

            var tuples = new List<OutletTuple>();

            foreach (Operator underlyingPatchOutlet in sourceSortedPatchOutlets)
            {
                Outlet customOperatorOutlet = TryGetCustomOperatorOutlet(underlyingPatchOutlet, destCandidateCustomOperatorOutlets);

                tuples.Add(new OutletTuple(underlyingPatchOutlet, customOperatorOutlet));

                destCandidateCustomOperatorOutlets.Remove(customOperatorOutlet);
            }

            return tuples;
        }

        private static Outlet TryGetCustomOperatorOutlet(Operator underlyingPatchOutlet, IList<Outlet> destCandicateCustomOperatorOutlets)
        {
            if (destCandicateCustomOperatorOutlets == null) throw new NullException(() => destCandicateCustomOperatorOutlets);

            var underlyingPatchOutletWrapper = new PatchOutlet_OperatorWrapper(underlyingPatchOutlet);

            // Try match by name
            if (NameHelper.IsFilledIn(underlyingPatchOutletWrapper.Name))
            {
                string canonicalSourceName = NameHelper.ToCanonical(underlyingPatchOutletWrapper.Name);

                foreach (Outlet customOperatorOutlet in destCandicateCustomOperatorOutlets)
                {
                    string canonicalDestName = NameHelper.ToCanonical(customOperatorOutlet.Name);

                    bool namesAreEqual = string.Equals(canonicalDestName, canonicalSourceName, StringComparison.Ordinal);
                    if (namesAreEqual)
                    {
                        return customOperatorOutlet;
                    }
                }
            }

            // Try match by Dimension
            DimensionEnum sourceDimensionEnum = underlyingPatchOutletWrapper.DimensionEnum;
            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
            if (dimensionIsFilledIn)
            {
                Outlet customOperatorOutlet_WithMatchingDimension =
                    destCandicateCustomOperatorOutlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);

                if (customOperatorOutlet_WithMatchingDimension != null)
                {
                    return customOperatorOutlet_WithMatchingDimension;
                }
            }

            // Try match by list index
            Outlet customOperatorOutlet_WithMatchingListIndex =
                destCandicateCustomOperatorOutlets.FirstOrDefault(destOutlet => destOutlet.ListIndex == underlyingPatchOutletWrapper.ListIndex);

            return customOperatorOutlet_WithMatchingListIndex;
        }

        private static IList<Operator> GetUnderlyingPatchInlets(Operator sourceCustomOperator, IPatchRepository patchRepository)
        {
            var sourceCustomOperatorWrapper = new CustomOperator_OperatorWrapper(sourceCustomOperator, patchRepository);

            Patch destUnderlyingPatch = sourceCustomOperatorWrapper.UnderlyingPatch;

            if (destUnderlyingPatch == null)
            {
                return new List<Operator>();
            }

            IList<Operator> destUnderlyingPatchInlets = destUnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);

            return destUnderlyingPatchInlets;
        }

        private static IList<Operator> GetUnderlyingPatchOutlets(Operator customOperator, IPatchRepository patchRepository)
        {
            var customOperatorWrapper = new CustomOperator_OperatorWrapper(customOperator, patchRepository);

            Patch underlyingPatch = customOperatorWrapper.UnderlyingPatch;

            if (underlyingPatch == null)
            {
                return new List<Operator>();
            }

            IList<Operator> underlyingPatchOutlets = underlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

            return underlyingPatchOutlets;
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
        private static Outlet TryApplyCustomOperatorToUnderlyingPatch(Outlet customOperatorOutlet, IPatchRepository patchRepository)
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

            IList<InletTuple> inletTuples = Match_PatchInlets_With_CustomOperatorInlets(customOperator, patchRepository);

            foreach (InletTuple tuple in inletTuples)
            {
                Inlet customOperatorInlet = tuple.CustomOperatorInlet;
                Operator destUnderlyingPatchInlet = tuple.UnderlyingPatchInlet;

                // ReSharper disable once InvertIf
                if (destUnderlyingPatchInlet != null)
                {
                    new PatchInlet_OperatorWrapper(destUnderlyingPatchInlet)
                    {
                        Input = customOperatorInlet.InputOutlet
                    };
                }
            }

            IList<OutletTuple> outletTuples = Match_PatchOutlets_With_CustomOperatorOutlets(customOperator, patchRepository);

            Operator destUnderlyingPatchOutlet = outletTuples.Where(x => x.CustomOperatorOutlet == customOperatorOutlet).Single().UnderlyingPatchOutlet;
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

            // Try match by Dimension
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
                         .ThenBy(x => x.IsObsolete)
                         .ToList();
        }

        private static IList<Outlet> SortOutlets(IList<Outlet> outlets)
        {
            return outlets.OrderBy(x => x.ListIndex)
                          .ThenBy(x => x.GetDimensionEnum() == DimensionEnum.Undefined)
                          .ThenBy(x => x.GetDimensionEnum())
                          .ThenBy(x => string.IsNullOrWhiteSpace(x.Name))
                          .ThenBy(x => x.Name)
                          .ThenBy(x => x.IsObsolete)
                          .ToList();
        }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JetBrains.Annotations;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.RepositoryInterfaces;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer
//{
//    internal static class InletOutletMatcher
//    {
//        // Patch to CustomOperator (e.g. for Converters)

//        /// <summary> Returned tuples can contain null-elements. </summary>
//        public static IList<(Operator sourcePatchInlet, Inlet destCustomOperatorInlet)>
//            Match_PatchInlets_With_CustomOperatorInlets(IList<Operator> sourceItems, IList<Inlet> destCandidateItems)
//        {
//            // Copy input list
//            destCandidateItems = destCandidateItems.ToList();

//            var tuples = new List<(Operator sourcePatchInlet, Inlet destCustomOperatorInlet)>();

//            foreach (Operator sourceItem in sourceItems)
//            {
//                Inlet destItem = TryGetCustomOperatorInlet(sourceItem, destCandidateItems);

//                tuples.Add((sourceItem, destItem));

//                destCandidateItems.Remove(destItem);
//            }

//            return tuples;
//        }

//        private static Inlet TryGetCustomOperatorInlet(Operator sourcePatchInlet, IList<Inlet> destCandicateCustomOperatorInlets)
//        {
//            if (destCandicateCustomOperatorInlets == null) throw new NullException(() => destCandicateCustomOperatorInlets);

//            var sourcePatchInletWrapper = new PatchInlet_OperatorWrapper(sourcePatchInlet);

//            // Try match by name
//            if (NameHelper.IsFilledIn(sourcePatchInletWrapper.Name))
//            {
//                string canonical_SourcePatchInlet_Name = NameHelper.ToCanonical(sourcePatchInletWrapper.Name);

//                foreach (Inlet destCustomOperatorInlet in destCandicateCustomOperatorInlets)
//                {
//                    string canonical_DestCustomOperatorInlet_Name = NameHelper.ToCanonical(destCustomOperatorInlet.Name);

//                    bool namesAreEqual = string.Equals(
//                        canonical_DestCustomOperatorInlet_Name,
//                        canonical_SourcePatchInlet_Name);

//                    if (namesAreEqual)
//                    {
//                        return destCustomOperatorInlet;
//                    }
//                }
//            }

//            // Try match by Dimension
//            DimensionEnum sourceDimensionEnum = sourcePatchInletWrapper.DimensionEnum;
//            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
//            if (dimensionIsFilledIn)
//            {
//                Inlet destCustomOperatorInlet_WithMatchingDimension =
//                    destCandicateCustomOperatorInlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);

//                if (destCustomOperatorInlet_WithMatchingDimension != null)
//                {
//                    return destCustomOperatorInlet_WithMatchingDimension;
//                }
//            }

//            // Try match by list index
//            Inlet destCustomOperatorInlet_WithMatchingListIndex = 
//                destCandicateCustomOperatorInlets.FirstOrDefault(destInlet => destInlet.ListIndex == sourcePatchInletWrapper.ListIndex);

//            return destCustomOperatorInlet_WithMatchingListIndex;
//        }

//        /// <summary> Returned tuples can contain null-elements. </summary>
//        public static IList<(Operator sourcePatchOutlet, Outlet destCustomOperatorOutlet)>
//            Match_PatchOutlets_With_CustomOperatorOutlets(IList<Operator> sourceItems, IList<Outlet> destCandidateItems)
//        {
//            // Copy input list
//            destCandidateItems = destCandidateItems.ToList();

//            var tuples = new List<(Operator sourcePatchOutlet, Outlet destCustomOperatorOutlet)>();

//            foreach (Operator sourceItem in sourceItems)
//            {
//                Outlet destItem = TryGetCustomOperatorOutlet(sourceItem, destCandidateItems);

//                tuples.Add((sourceItem, destItem));

//                destCandidateItems.Remove(destItem);
//            }

//            return tuples;
//        }

//        private static Outlet TryGetCustomOperatorOutlet(Operator sourcePatchOutlet, IList<Outlet> destCandicateCustomOperatorOutlets)
//        {
//            if (destCandicateCustomOperatorOutlets == null) throw new NullException(() => destCandicateCustomOperatorOutlets);

//            var sourcePatchOutletWrapper = new PatchOutlet_OperatorWrapper(sourcePatchOutlet);

//            // Try match by name
//            if (NameHelper.IsFilledIn(sourcePatchOutletWrapper.Name))
//            {
//                string canonical_SourcePatchOutlet_Name = NameHelper.ToCanonical(sourcePatchOutletWrapper.Name);

//                foreach (Outlet destCustomOperatorOutlet in destCandicateCustomOperatorOutlets)
//                {
//                    string canonical_DestCustomOperatorOutlet_Name = NameHelper.ToCanonical(destCustomOperatorOutlet.Name);

//                    bool namesAreEqual = string.Equals(
//                        canonical_DestCustomOperatorOutlet_Name,
//                        canonical_SourcePatchOutlet_Name);

//                    if (namesAreEqual)
//                    {
//                        return destCustomOperatorOutlet;
//                    }
//                }
//            }

//            // Try match by Dimension
//            DimensionEnum sourceDimensionEnum = sourcePatchOutletWrapper.DimensionEnum;
//            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
//            if (dimensionIsFilledIn)
//            {
//                Outlet destCustomOperatorOutlet_WithMatchingDimension =
//                    destCandicateCustomOperatorOutlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);

//                if (destCustomOperatorOutlet_WithMatchingDimension != null)
//                {
//                    return destCustomOperatorOutlet_WithMatchingDimension;
//                }
//            }

//            // Try match by list index
//            Outlet customOperatorOutlet_WithMatchingListIndex = 
//                destCandicateCustomOperatorOutlets.FirstOrDefault(destOutlet => destOutlet.ListIndex == sourcePatchOutletWrapper.ListIndex);

//            return customOperatorOutlet_WithMatchingListIndex;
//        }

//        // CustomOperator to Patch (e.g. for Validation)

//        public static IList<(Inlet sourceCustomOperatorInlet, Operator destPatchInlet)>
//            MatchPatchInlets(Operator sourceCustomOperator, Patch destPatch, IPatchRepository patchRepository)
//        {
//            var tuples = new List<(Inlet sourceCustomOperatorInlet, Operator destPatchInlet)>();

//            IList<Inlet> sourceItems = sourceCustomOperator.Inlets.ToList();
//            IList<Operator> destCandidateItems = destPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

//            foreach (Inlet sourceItem in sourceItems)
//            {
//                Operator destItem = TryGetPatchInlet(sourceItem, destCandidateItems);

//                tuples.Add((sourceItem, destItem));

//                destCandidateItems.Remove(destItem);
//            }

//            return tuples;
//        }

//        public static Operator GetPatchInlet(Inlet source_CustomOperator_Inlet, IPatchRepository patchRepository)
//        {
//            if (source_CustomOperator_Inlet == null) throw new NullException(() => source_CustomOperator_Inlet);

//            Operator dest_UnderlyingPatch_PatchInlet = TryGetPatchInlet(source_CustomOperator_Inlet, patchRepository);

//            // ReSharper disable once InvertIf
//            if (dest_UnderlyingPatch_PatchInlet == null)
//            {
//                var identifier = new
//                {
//                    source_CustomOperator_Inlet.Name,
//                    DimensionEnum =source_CustomOperator_Inlet.GetDimensionEnum(),
//                    source_CustomOperator_Inlet.ListIndex
//                };

//                throw new Exception($"PatchInlet not found in UnderlyingPatch. {nameof(source_CustomOperator_Inlet)}: {identifier}");
//            }

//            return dest_UnderlyingPatch_PatchInlet;
//        }

//        public static Operator TryGetPatchInlet(Inlet source_CustomOperator_Inlet, IPatchRepository patchRepository)
//        {
//            if (source_CustomOperator_Inlet == null) throw new NullException(() => source_CustomOperator_Inlet);

//            IList<Operator> dest_UnderlyingPatch_PatchInlets;
//            {
//                Operator source_CustomOperator = source_CustomOperator_Inlet.Operator;
//                CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
//                Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;
//                dest_UnderlyingPatch_PatchInlets = dest_UnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet);
//            }

//            return TryGetPatchInlet(source_CustomOperator_Inlet, dest_UnderlyingPatch_PatchInlets);
//        }

//        private static Operator TryGetPatchInlet([NotNull] Inlet source_CustomOperator_Inlet, [NotNull] IList<Operator> dest_UnderlyingPatch_PatchInlets)
//        {
//            if (source_CustomOperator_Inlet == null) throw new NullException(() => source_CustomOperator_Inlet);
//            if (dest_UnderlyingPatch_PatchInlets == null) throw new NullException(() => dest_UnderlyingPatch_PatchInlets);

//            // Try match by name
//            if (NameHelper.IsFilledIn(source_CustomOperator_Inlet.Name))
//            {
//                string canonical_Source_CustomOperator_Inlet_Name = NameHelper.ToCanonical(source_CustomOperator_Inlet.Name);

//                foreach (Operator dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets)
//                {
//                    string canonical_Dest_UnderlyingPatch_PatchInlet_Name = NameHelper.ToCanonical(dest_UnderlyingPatch_PatchInlet.Name);

//                    bool namesAreEqual = string.Equals(
//                        canonical_Dest_UnderlyingPatch_PatchInlet_Name,
//                        canonical_Source_CustomOperator_Inlet_Name);

//                    if (namesAreEqual)
//                    {
//                        return dest_UnderlyingPatch_PatchInlet;
//                    }
//                }
//            }

//            // Try match by Dimension
//            DimensionEnum sourceDimensionEnum = source_CustomOperator_Inlet.GetDimensionEnum();
//            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
//            if (dimensionIsFilledIn)
//            {
//                Operator dest_Underlying_PatchInlet_WithMatchingDimension =
//                    (from dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets
//                     let destWrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet)
//                     let dimensionsAreEqual = destWrapper.DimensionEnum == sourceDimensionEnum
//                     where dimensionsAreEqual
//                     select dest_UnderlyingPatch_PatchInlet).FirstOrDefault();

//                if (dest_Underlying_PatchInlet_WithMatchingDimension != null)
//                {
//                    return dest_Underlying_PatchInlet_WithMatchingDimension;
//                }
//            }

//            // Try match by list index
//            Operator dest_Underlying_PatchInlet_WithMatchingListIndex =
//                (from dest_UnderlyingPatch_PatchInlet in dest_UnderlyingPatch_PatchInlets
//                 let destWrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet)
//                 where destWrapper.ListIndex == source_CustomOperator_Inlet.ListIndex
//                 select dest_UnderlyingPatch_PatchInlet).FirstOrDefault();

//            return dest_Underlying_PatchInlet_WithMatchingListIndex;
//        }

//        public static IList<(Outlet sourceCustomOperatorOutlet, Operator destPatchOutlet)>
//            MatchPatchOutlets(Operator sourceCustomOperator, Patch destPatch, IPatchRepository patchRepository)
//        {
//            var tuples = new List<(Outlet sourceCustomOperatorOutlet, Operator destPatchOutlet)>();

//            IList<Outlet> sourceItems = sourceCustomOperator.Outlets.ToList();
//            IList<Operator> destCandidateItems = destPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);

//            foreach (Outlet sourceItem in sourceItems)
//            {
//                Operator destItem = TryGetPatchOutlet(sourceItem, destCandidateItems);

//                tuples.Add((sourceItem, destItem));

//                destCandidateItems.Remove(destItem);
//            }

//            return tuples;
//        }

//        public static Operator GetPatchOutlet(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
//        {
//            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);

//            Operator dest_UnderlyingPatch_PatchOutlet = TryGetPatchOutlet(source_CustomOperator_Outlet, patchRepository);

//            // ReSharper disable once InvertIf
//            if (dest_UnderlyingPatch_PatchOutlet == null)
//            {
//                var identifier = new
//                {
//                    source_CustomOperator_Outlet.Name,
//                    DimensionEnum = source_CustomOperator_Outlet.GetDimensionEnum(),
//                    source_CustomOperator_Outlet.ListIndex
//                };

//                throw new Exception($"PatchOutlet not found in UnderlyingPatch. {nameof(source_CustomOperator_Outlet)}: {identifier}.");
//            }

//            return dest_UnderlyingPatch_PatchOutlet;
//        }

//        public static Operator TryGetPatchOutlet(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
//        {
//            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);

//            IList<Operator> dest_UnderlyingPatch_PatchOutlets;
//            {
//                Operator source_CustomOperator = source_CustomOperator_Outlet.Operator;
//                CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
//                Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;
//                dest_UnderlyingPatch_PatchOutlets = dest_UnderlyingPatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet);
//            }

//            return TryGetPatchOutlet(source_CustomOperator_Outlet, dest_UnderlyingPatch_PatchOutlets);
//        }

//        private static Operator TryGetPatchOutlet([NotNull] Outlet source_CustomOperator_Outlet, [NotNull] IList<Operator> dest_UnderlyingPatch_PatchOutlets)
//        {
//            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);
//            if (dest_UnderlyingPatch_PatchOutlets == null) throw new NullException(() => dest_UnderlyingPatch_PatchOutlets);

//            // Try match by name
//            if (NameHelper.IsFilledIn(source_CustomOperator_Outlet.Name))
//            {
//                string canonical_Source_CustomOperator_Outlet_Name = NameHelper.ToCanonical(source_CustomOperator_Outlet.Name);

//                foreach (Operator dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets)
//                {
//                    string canonical_Dest_UnderlyingPatch_PatchOutlet_Name = NameHelper.ToCanonical(dest_UnderlyingPatch_PatchOutlet.Name);

//                    bool namesAreEqual = string.Equals(
//                        canonical_Dest_UnderlyingPatch_PatchOutlet_Name,
//                        canonical_Source_CustomOperator_Outlet_Name);

//                    if (namesAreEqual)
//                    {
//                        return dest_UnderlyingPatch_PatchOutlet;
//                    }
//                }
//            }

//            // Try match by Dimension
//            DimensionEnum sourceDimensionEnum = source_CustomOperator_Outlet.GetDimensionEnum();
//            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
//            if (dimensionIsFilledIn)
//            {
//                var dest_UnderlyingPatch_PatchOutlet_WithMatchingDimension = (
//                    from dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets
//                    let destWrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet)
//                    let dimensionsAreEqual = destWrapper.DimensionEnum == sourceDimensionEnum
//                    where dimensionsAreEqual
//                    select dest_UnderlyingPatch_PatchOutlet).FirstOrDefault();

//                if (dest_UnderlyingPatch_PatchOutlet_WithMatchingDimension != null)
//                {
//                    return dest_UnderlyingPatch_PatchOutlet_WithMatchingDimension;
//                }
//            }

//            // Try match by list index
//            Operator dest_UnderlyingPatch_PatchOutlet_WithMatchingListIndex =
//                (from dest_UnderlyingPatch_PatchOutlet in dest_UnderlyingPatch_PatchOutlets
//                 let destWrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet)
//                 where destWrapper.ListIndex == source_CustomOperator_Outlet.ListIndex
//                 select dest_UnderlyingPatch_PatchOutlet).FirstOrDefault();

//            return dest_UnderlyingPatch_PatchOutlet_WithMatchingListIndex;
//        }

//        // Inlet-Outlet Matching (e.g. for Auto-Patching)

//        public static bool AreMatch(Outlet outlet, Inlet inlet)
//        {
//            if (outlet == null) throw new NullException(() => outlet);
//            if (inlet == null) throw new NullException(() => inlet);

//            // Try match by name
//            if (NameHelper.IsFilledIn(outlet.Name))
//            {
//                if (NameHelper.AreEqual(outlet.Name, inlet.Name))
//                {
//                    return true;
//                }
//            }

//            // Try match by Dimension (be tollerant towards non-unicity, for more chance to get a match).
//            DimensionEnum outletDimensionEnum = outlet.GetDimensionEnum();
//            // ReSharper disable once InvertIf
//            if (outletDimensionEnum != DimensionEnum.Undefined)
//            {
//                DimensionEnum inletDimensionEnum = inlet.GetDimensionEnum();
//                // ReSharper disable once InvertIf
//                if (inletDimensionEnum != DimensionEnum.Undefined)
//                {
//                    if (outletDimensionEnum == inletDimensionEnum)
//                    {
//                        return true;
//                    }
//                }
//            }

//            // Do not match by list index, because that would result in something arbitrary.

//            return false;
//        }

//        // Fill in PatchInlet.Inlet and PatchOutlet.Outlet (For Calculations)

//        /// <summary>
//        /// Used as a helper in producing the output calculation structure.
//        ///
//        /// Returns the corresponding Outlet (of the PatchOutlet) in the Underlying Patch 
//        /// after having assigned the Underlying Patch's (PatchInlets') Inlets.
//        /// The returned outlet is then ready to be used like any other operator.
//        /// 
//        /// Note that even though a CustomOperator can have multiple outlets, you will only be using one at a time in your calculations.
//        /// </summary>
//        public static Outlet ApplyCustomOperatorToUnderlyingPatch(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
//        {
//            Outlet dest_PatchOutlet_Outlet = TryApplyCustomOperatorToUnderlyingPatch(source_CustomOperator_Outlet, patchRepository);
//            if (dest_PatchOutlet_Outlet == null)
//            {
//                // TODO: Low priority: This is a vague error message. Can it be made more specific?
//                throw new Exception($"{nameof(dest_PatchOutlet_Outlet)} was null after {nameof(TryApplyCustomOperatorToUnderlyingPatch)}.");
//            }

//            return dest_PatchOutlet_Outlet;
//        }

//        /// <summary>
//        /// Used as a helper in producing the output calculation structure.
//        /// 
//        /// Returns the corresponding Outlet (of the PatchOutlet) in the Underlying Patch 
//        /// after having assigned the Underlying Patch's (PatchInlets') Inlets.
//        /// The returned outlet is then ready to be used like any other operator.
//        /// 
//        /// Note that even though a CustomOperator can have multiple outlets, you will only be using one at a time in your calculations.
//        /// </summary>
//        public static Outlet TryApplyCustomOperatorToUnderlyingPatch(Outlet source_CustomOperator_Outlet, IPatchRepository patchRepository)
//        {
//            if (source_CustomOperator_Outlet == null) throw new NullException(() => source_CustomOperator_Outlet);
//            if (patchRepository == null) throw new NullException(() => patchRepository);

//            Operator source_CustomOperator = source_CustomOperator_Outlet.Operator;
//            CustomOperator_OperatorWrapper source_CustomOperator_Wrapper = new CustomOperator_OperatorWrapper(source_CustomOperator, patchRepository);
//            Patch dest_UnderlyingPatch = source_CustomOperator_Wrapper.UnderlyingPatch;

//            if (dest_UnderlyingPatch == null)
//            {
//                return null;
//            }

//            foreach (Inlet source_CustomOperator_Inlet in source_CustomOperator.Inlets)
//            {
//                Operator dest_UnderlyingPatch_PatchInlet = GetPatchInlet(source_CustomOperator_Inlet, patchRepository);

//                PatchInlet_OperatorWrapper dest_UnderlyingPatch_PatchInlet_Wrapper = new PatchInlet_OperatorWrapper(dest_UnderlyingPatch_PatchInlet);
//                dest_UnderlyingPatch_PatchInlet_Wrapper.Inlet.LinkTo(source_CustomOperator_Inlet.InputOutlet);
//            }

//            Operator dest_UnderlyingPatch_PatchOutlet = GetPatchOutlet(source_CustomOperator_Outlet, patchRepository);

//            PatchOutlet_OperatorWrapper dest_UnderlyingPatch_PatchOutlet_Wrapper = new PatchOutlet_OperatorWrapper(dest_UnderlyingPatch_PatchOutlet);
//            return dest_UnderlyingPatch_PatchOutlet_Wrapper.Outlet;
//        }
//    }
//}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.Interfaces;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer
//{
//    internal static class InletOutletMatcher
//    {
//        private class RepetitionInfo
//        {
//            public RepetitionInfo(bool isFromNonRepeatingToRepeating, bool isFromRepeatingToNonRepeating, bool repeatingToNonRepeatingAreMixed)
//            {
//                IsFromNonRepeatingToRepeating = isFromNonRepeatingToRepeating;
//                IsFromRepeatingToNonRepeating = isFromRepeatingToNonRepeating;
//                RepeatingToNonRepeatingAreMixed = repeatingToNonRepeatingAreMixed;
//            }

//            public bool IsFromNonRepeatingToRepeating { get; }
//            public bool IsFromRepeatingToNonRepeating { get; }
//            public bool RepeatingToNonRepeatingAreMixed { get; }
//        }

//        // Patch to CustomOperator (e.g. for Converters and Validators)

//        public static IList<InletTuple> MatchSourceAndDestInlets(Operator destOperator)
//        {
//            IList<Inlet> sourceInlets = GetSourceInlets(destOperator);
//            IList<Inlet> destInlets = destOperator.Inlets.ToArray();

//            IList<InletOrOutletTuple> inletOrOutletTuples = MatchSourceAndDestInletsOrOutlets(sourceInlets, destInlets);

//            IList<InletTuple> inletTuples = inletOrOutletTuples.Select(x => new InletTuple((Inlet)x.SourceInletOrOutlet, (Inlet)x.DestInletOrOutlet))
//                                                               .ToArray();

//            return inletTuples;
//        }

//        public static IList<InletTuple> MatchSourceAndDestInlets(IEnumerable<Inlet> sourceInlets, IEnumerable<Inlet> destInlets)
//        {
//            IList<InletOrOutletTuple> inletOrOutletTuples = MatchSourceAndDestInletsOrOutlets(sourceInlets, destInlets);

//            IList<InletTuple> inletTuples = inletOrOutletTuples.Select(x => new InletTuple((Inlet)x.SourceInletOrOutlet, (Inlet)x.DestInletOrOutlet))
//                                                               .ToArray();

//            return inletTuples;
//        }

//        public static IList<OutletTuple> MatchSourceAndDestOutlets(Operator destOperator)
//        {
//            IList<Outlet> sourceOutlets = GetSourceOutlets(destOperator);
//            IList<Outlet> destOutlets = destOperator.Outlets.ToList();

//            IList<InletOrOutletTuple> inletOrOutletTuples = MatchSourceAndDestInletsOrOutlets(sourceOutlets, destOutlets);

//            IList<OutletTuple> outletTuples = inletOrOutletTuples.Select(x => new OutletTuple((Outlet)x.SourceInletOrOutlet, (Outlet)x.DestInletOrOutlet))
//                                                                 .ToArray();

//            return outletTuples;
//        }

//        public static IList<OutletTuple> MatchSourceAndDestOutlets(IEnumerable<Outlet> sourceOutlets, IEnumerable<Outlet> destOutlets)
//        {
//            IList<InletOrOutletTuple> inletOrOutletTuples = MatchSourceAndDestInletsOrOutlets(sourceOutlets, destOutlets);

//            IList<OutletTuple> outletTuples = inletOrOutletTuples.Select(x => new OutletTuple((Outlet)x.SourceInletOrOutlet, (Outlet)x.DestInletOrOutlet))
//                                                                 .ToArray();

//            return outletTuples;
//        }

//        /// <summary> Returned tuples can contain null-dest objects. </summary>
//        public static IList<InletOrOutletTuple> MatchSourceAndDestInletsOrOutlets(
//            IEnumerable<IInletOrOutlet> sourceInletsOrOutlets, 
//            IEnumerable<IInletOrOutlet> destInletsOrOutlets)
//        {
//            // Do a different mapping if going from single repeating inlet to no repeating inlets or outlets.

//            RepetitionInfo repetitionInfo = GetRepetitionInfo(sourceInletsOrOutlets, destInletsOrOutlets);

//            if (repetitionInfo.IsFromRepeatingToNonRepeating)
//            {
//                return MatchSourceAndDestInletsOrOutlets_FromRepeatingToNonRepeatingOrBack(sourceInletsOrOutlets, destInletsOrOutlets);
//            }
//            else if (repetitionInfo.IsFromNonRepeatingToRepeating)
//            {
//                return MatchSourceAndDestInletsOrOutlets_FromRepeatingToNonRepeatingOrBack(sourceInletsOrOutlets, destInletsOrOutlets);
//            }
//            else if (repetitionInfo.RepeatingToNonRepeatingAreMixed)
//            {
//                return MatchSourceAndDestInletsOrOutlets_WithMixedRepeatingAndNonRepeating(sourceInletsOrOutlets, destInletsOrOutlets);
//            }
//            else
//            {
//                throw new Exception(
//                    $"Unsupported {nameof(RepetitionInfo)} "+
//                    $"{new { repetitionInfo.IsFromRepeatingToNonRepeating, repetitionInfo.IsFromNonRepeatingToRepeating, repetitionInfo.RepeatingToNonRepeatingAreMixed }}");
//            }
//        }

//        /// <summary>
//        /// Determines whether you go from sole repeating, to only non-repeating inlets (or outlets),
//        /// or the other way around, or 'mixed mode'.
//        /// </summary>
//        private static RepetitionInfo GetRepetitionInfo(IEnumerable<IInletOrOutlet> sourceInletsOrOutlets, IEnumerable<IInletOrOutlet> destInletsOrOutlets)
//        {
//            // This is not very readable, but avoids traversing the list a many items.

//            IList<bool> sourceIsRepeatingBooleans = sourceInletsOrOutlets.Select(x => x.IsRepeating).Distinct().ToArray();
//            IList<bool> destIsRepeatingBooleans = destInletsOrOutlets.Select(x => x.IsRepeating).Distinct().ToArray();

//            bool sourceHasNonRepetitions = sourceIsRepeatingBooleans.Contains(false);
//            bool sourceHasRepetitions = sourceIsRepeatingBooleans.Contains(true);
//            bool destHasNonRepetitions = destIsRepeatingBooleans.Contains(false);
//            bool destHasRepetitions = destIsRepeatingBooleans.Contains(true);

//            bool sourceIsRepeating = sourceHasRepetitions && !sourceHasNonRepetitions;
//            bool sourceIsNonRepeating = !sourceHasRepetitions && sourceHasNonRepetitions;
//            bool destIsRepeating = destHasRepetitions && !destHasNonRepetitions;
//            bool destIsNonRepeating = !destHasRepetitions && destHasNonRepetitions;

//            bool isFromNonRepeatingToRepeating = sourceIsNonRepeating && destIsRepeating;
//            bool isFromRepeatingToNonRepeating = sourceIsRepeating && destIsNonRepeating;
//            bool repeatingAndNonRepeatingAreMixed = !isFromNonRepeatingToRepeating && !isFromRepeatingToNonRepeating;

//            return new RepetitionInfo(isFromNonRepeatingToRepeating, isFromRepeatingToNonRepeating, repeatingAndNonRepeatingAreMixed);
//        }

//        private static IList<InletOrOutletTuple> MatchSourceAndDestInletsOrOutlets_FromRepeatingToNonRepeatingOrBack(
//            IEnumerable<IInletOrOutlet> sourceInletsOrOutlets, 
//            IEnumerable<IInletOrOutlet> destInletsOrOutlets)
//        {
//            IList<IInletOrOutlet> sourceSortedInletsOrOutlets = sourceInletsOrOutlets.Sort().ToArray();
//            IList<IInletOrOutlet> destSortedInletsOrOutlets = destInletsOrOutlets.Sort().ToArray();

//            var tuples = new List<InletOrOutletTuple>();

//            int maxCount = Math.Max(sourceSortedInletsOrOutlets.Count, destSortedInletsOrOutlets.Count);
//            for (int i = 0; i < maxCount; i++)
//            {
//                IInletOrOutlet sourceInletOrOutlet = sourceSortedInletsOrOutlets[i];
//                IInletOrOutlet destInletOrOutlet = destSortedInletsOrOutlets[i];
//                tuples.Add(new InletOrOutletTuple(sourceInletOrOutlet, destInletOrOutlet));
//            }

//            return tuples;
//        }

//        private static IList<InletOrOutletTuple> MatchSourceAndDestInletsOrOutlets_WithMixedRepeatingAndNonRepeating(
//            IEnumerable<IInletOrOutlet> sourceInletsOrOutlets,
//            IEnumerable<IInletOrOutlet> destInletsOrOutlets)
//        {
//            IList<IInletOrOutlet> sourceSortedInletsOrOutlets = sourceInletsOrOutlets.Sort().ToArray();
//            IList<IInletOrOutlet> destSortedCandidateInletsOrOutlets = destInletsOrOutlets.Sort().ToArray();

//            var tuples = new List<InletOrOutletTuple>();

//            // Match Non-Repeating Ones
//            {
//                IList<IInletOrOutlet> sourceNonRepeatingInletsOrOutlets = sourceSortedInletsOrOutlets.Where(x => !x.IsRepeating).ToArray();
//                IList<IInletOrOutlet> destCandicateNonRepeatingInletsOrOutlets = destSortedCandidateInletsOrOutlets.Where(x => !x.IsRepeating).ToList();
//                foreach (IInletOrOutlet sourceNonRepeatingInletOrOutlet in sourceNonRepeatingInletsOrOutlets)
//                {
//                    IInletOrOutlet destNonRepeatingInletOrOutlet =
//                        TryGetDestInletOrOutlet(sourceNonRepeatingInletOrOutlet, destCandicateNonRepeatingInletsOrOutlets);

//                    tuples.Add(new InletOrOutletTuple(sourceNonRepeatingInletOrOutlet, destNonRepeatingInletOrOutlet));
//                    destCandicateNonRepeatingInletsOrOutlets.Remove(destNonRepeatingInletOrOutlet);
//                }
//            }

//            // Match Repeating Ones
//            {
//                IInletOrOutlet sourceRepeatingInletOrOutlet =
//                    sourceSortedInletsOrOutlets.Reverse().Where(x => x.IsRepeating).FirstOrDefault(); // Optimized for Repeating Inlet at the end.
//                IList<IInletOrOutlet> destCandicateRepeatingInletsOrOutlets = destSortedCandidateInletsOrOutlets.Where(x => x.IsRepeating).ToList();
//                // ReSharper disable once InvertIf
//                if (sourceRepeatingInletOrOutlet != null)
//                {
//                    IInletOrOutlet destRepeatingInletOrOutlet = TryGetDestInletOrOutlet(sourceRepeatingInletOrOutlet, destCandicateRepeatingInletsOrOutlets);

//                    if (destRepeatingInletOrOutlet == null)
//                    {
//                        tuples.Add(new InletOrOutletTuple(sourceRepeatingInletOrOutlet, null));
//                    }
//                    else
//                    {
//                        while (destRepeatingInletOrOutlet != null)
//                        {
//                            tuples.Add(new InletOrOutletTuple(sourceRepeatingInletOrOutlet, destRepeatingInletOrOutlet));
//                            destCandicateRepeatingInletsOrOutlets.Remove(destRepeatingInletOrOutlet);

//                            destRepeatingInletOrOutlet = TryGetDestInletOrOutlet(sourceRepeatingInletOrOutlet, destCandicateRepeatingInletsOrOutlets);
//                        }
//                    }
//                }
//            }

//            return tuples;
//        }

//        private static IInletOrOutlet TryGetDestInletOrOutlet(IInletOrOutlet sourceInletOrOutlet, IList<IInletOrOutlet> candicateDestInletsOrOutlets)
//        {
//            if (candicateDestInletsOrOutlets == null) throw new NullException(() => candicateDestInletsOrOutlets);

//            bool nameIsFilledIn = NameHelper.IsFilledIn(sourceInletOrOutlet.Name);
//            if (nameIsFilledIn)
//            {
//                // Try match by Name and Position (and RepetitionPosition)
//                {
//                    IInletOrOutlet destInletOrOutlet = candicateDestInletsOrOutlets.FirstOrDefault(
//                        x => x.Position == sourceInletOrOutlet.Position &&
//                             NameHelper.AreEqual(x.Name, sourceInletOrOutlet.Name));

//                    if (destInletOrOutlet != null)
//                    {
//                        return destInletOrOutlet;
//                    }
//                }

//                // Try match by Name (and RepetitionPosition)
//                {
//                    IInletOrOutlet destInletOrOutlet = candicateDestInletsOrOutlets.FirstOrDefault(
//                        x => NameHelper.AreEqual(x.Name, sourceInletOrOutlet.Name));

//                    if (destInletOrOutlet != null)
//                    {
//                        return destInletOrOutlet;
//                    }
//                }
//            }

//            DimensionEnum sourceDimensionEnum = sourceInletOrOutlet.GetDimensionEnum();
//            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;

//            if (dimensionIsFilledIn)
//            {
//                // Try match by Dimension and Position (and RepetitionPosition)
//                {
//                    IInletOrOutlet destInletOrOutlet = candicateDestInletsOrOutlets.FirstOrDefault(
//                        x => x.Position == sourceInletOrOutlet.Position &&
//                             x.GetDimensionEnum() == sourceDimensionEnum);

//                    if (destInletOrOutlet != null)
//                    {
//                        return destInletOrOutlet;
//                    }
//                }

//                // Try match by Dimension (and RepetitionPosition)
//                {
//                    IInletOrOutlet destInletOrOutlet = candicateDestInletsOrOutlets.FirstOrDefault(
//                        x => x.GetDimensionEnum() == sourceDimensionEnum);

//                    if (destInletOrOutlet != null)
//                    {
//                        return destInletOrOutlet;
//                    }
//                }
//            }

//            // Try match by Position (and RepetitionPosition)
//            {
//                IInletOrOutlet destInletOrOutlet = candicateDestInletsOrOutlets.FirstOrDefault(
//                    x => x.Position == sourceInletOrOutlet.Position &&
//                         x.RepetitionPosition == sourceInletOrOutlet.RepetitionPosition);

//                return destInletOrOutlet;
//            }
//        }

//        private static IList<Inlet> GetSourceInlets(Operator destOperator)
//        {
//            Patch sourcePatch = destOperator.UnderlyingPatch;

//            if (sourcePatch == null)
//            {
//                return new List<Inlet>();
//            }

//            IList<Inlet> sourceInlets = sourcePatch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchInlet)
//                                                   .Select(x => new PatchInlet_OperatorWrapper(x))
//                                                   .Select(x => x.Inlet)
//                                                   .ToArray();
//            return sourceInlets;
//        }

//        private static IList<Outlet> GetSourceOutlets(Operator destOperator)
//        {
//            Patch sourcePatch = destOperator.UnderlyingPatch;

//            if (sourcePatch == null)
//            {
//                return new List<Outlet>();
//            }

//            IList<Outlet> sourceOutlets = sourcePatch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
//                                                     .Select(x => new PatchOutlet_OperatorWrapper(x))
//                                                     .Select(x => x.Outlet)
//                                                     .ToArray();
//            return sourceOutlets;
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
//        public static Outlet ApplyCustomOperatorToUnderlyingPatch(Outlet sourceOperatorOutlet)
//        {
//            Outlet destUnderlyingOutlet = TryApplyCustomOperatorToUnderlyingPatch(sourceOperatorOutlet);
//            if (destUnderlyingOutlet == null)
//            {
//                // TODO: Low priority: This is a vague error message. Can it be made more specific?
//                throw new Exception($"{nameof(destUnderlyingOutlet)} was null after {nameof(TryApplyCustomOperatorToUnderlyingPatch)}.");
//            }

//            return destUnderlyingOutlet;
//        }

//        /// <summary>
//        /// Used as a helper in producing the output calculation structure.
//        /// 
//        /// Returns the corresponding Outlet (of the PatchOutlet) in the Underlying Patch 
//        /// after having assigned the Underlying Patch's (PatchInlets') Inlets.
//        /// The returned outlet is then ready to be used like any other operator.
//        /// 
//        /// Note that even though a CustomOperator can have multiple outlets, 
//        /// you will only be using one at a time in your calculations.
//        /// </summary>
//        // ReSharper disable once SuggestBaseTypeForParameter
//        private static Outlet TryApplyCustomOperatorToUnderlyingPatch(Outlet operatorOutlet)
//        {
//            if (operatorOutlet == null) throw new NullException(() => operatorOutlet);
            
//            Operator op = operatorOutlet.Operator;

//            IList<InletTuple> inletTuples = MatchSourceAndDestInlets(op);

//            foreach (InletTuple tuple in inletTuples)
//            {
//                Inlet operatorInlet = tuple.DestInlet;
//                Inlet underlyingInlet = tuple.SourceInlet;

//                underlyingInlet?.LinkTo(operatorInlet.InputOutlet);
//            }

//            IList<OutletTuple> outletTuples = MatchSourceAndDestOutlets(op);

//            Outlet underlyingOutlet = outletTuples.Where(x => x.DestOutlet == operatorOutlet)
//                                                  .Single()
//                                                  .SourceOutlet;
//            return underlyingOutlet;
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

//            // Try match by Dimension
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

//            // Do not match by Position, because that would result in something arbitrary.

//            return false;
//        }
//    }
//}
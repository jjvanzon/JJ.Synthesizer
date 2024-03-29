﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.LinkTo;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer
//{
//    internal static class InletOutletMatcher
//    {
//        // Patch to CustomOperator (e.g. for Converters and validators)

//        public static IList<InletTuple> MatchSourceAndDestInlets(Operator destOperator)
//        {
//            IList<Inlet> destInlets = destOperator.Inlets.ToList();
//            IList<Inlet> sourceInlets = GetSourceInlets(destOperator);

//            return MatchSourceAndDestInlets(sourceInlets, destInlets);
//        }

//        /// <summary> Returned tuples can contain null-DestInlets. </summary>
//        public static IList<InletTuple> MatchSourceAndDestInlets(IList<Inlet> sourceInlets, IList<Inlet> destInlets)
//        {
//            IList<Inlet> sourceSortedInlets = sourceInlets.Sort().ToArray();
//            IList<Inlet> destSortedCandidateInlets = destInlets.Sort().ToArray();

//            var tuples = new List<InletTuple>();

//            // Match Non-Repeating Inlets
//            {
//                IList<Inlet> sourceNonRepeatingInlets = sourceSortedInlets.Where(x => !x.IsRepeating).ToArray();
//                IList<Inlet> destCandicateNonRepeatingInlets = destSortedCandidateInlets.Where(x => !x.IsRepeating).ToList();
//                foreach (Inlet sourceNonRepeatingInlet in sourceNonRepeatingInlets)
//                {
//                    Inlet destNonRepeatingInlet = TryGetDestInlet(sourceNonRepeatingInlet, destCandicateNonRepeatingInlets);

//                    tuples.Add(new InletTuple(sourceNonRepeatingInlet, destNonRepeatingInlet));
//                    destCandicateNonRepeatingInlets.Remove(destNonRepeatingInlet);
//                }
//            }

//            // Match Repeating Inlet
//            {
//                Inlet sourceRepeatingInlet = sourceSortedInlets.Reverse().Where(x => x.IsRepeating).FirstOrDefault(); // Optimized for Repeating Inlet at the end.
//                IList<Inlet> destCandicateRepeatingInlets = destSortedCandidateInlets.Where(x => x.IsRepeating).ToList();
//                if (sourceRepeatingInlet != null)
//                {
//                    Inlet destRepeatingInlet;
//                    while ((destRepeatingInlet = TryGetDestInlet(sourceRepeatingInlet, destCandicateRepeatingInlets)) != null)
//                    {
//                        tuples.Add(new InletTuple(sourceRepeatingInlet, destRepeatingInlet));
//                        destCandicateRepeatingInlets.Remove(destRepeatingInlet);
//                    }
//                }
//            }

//            return tuples;
//        }

//        private static Inlet TryGetDestInlet(Inlet sourceInlet, IList<Inlet> candicateDestInlets)
//        {
//            if (candicateDestInlets == null) throw new NullException(() => candicateDestInlets);

//            bool nameIsFilledIn = NameHelper.IsFilledIn(sourceInlet.Name);
//            if (nameIsFilledIn)
//            {
//                // Try match by Name and Position
//                {
//                    Inlet destInlet = candicateDestInlets.FirstOrDefault(
//                        x => x.Position == sourceInlet.Position &&
//                             NameHelper.AreEqual(x.Name, sourceInlet.Name));

//                    if (destInlet != null)
//                    {
//                        return destInlet;
//                    }
//                }

//                // Try match by Name
//                {
//                    Inlet destInlet = candicateDestInlets.FirstOrDefault(x => NameHelper.AreEqual(x.Name, sourceInlet.Name));
//                    if (destInlet != null)
//                    {
//                        return destInlet;
//                    }
//                }
//            }

//            DimensionEnum sourceDimensionEnum = sourceInlet.GetDimensionEnum();
//            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;

//            if (dimensionIsFilledIn)
//            {
//                // Try match by Dimension and Position
//                {
//                    Inlet destInlet = candicateDestInlets.FirstOrDefault(
//                        x => x.Position == sourceInlet.Position &&
//                             x.GetDimensionEnum() == sourceDimensionEnum);

//                    if (destInlet != null)
//                    {
//                        return destInlet;
//                    }
//                }

//                // Try match by Dimension
//                {
//                    Inlet destInlet = candicateDestInlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);
//                    if (destInlet != null)
//                    {
//                        return destInlet;
//                    }
//                }
//            }

//            // Try match by list index
//            {
//                Inlet destInlet = candicateDestInlets.FirstOrDefault(x => x.Position == sourceInlet.Position);
//                return destInlet;
//            }
//        }

//        public static IList<OutletTuple> MatchSourceAndDestOutlets(Operator destOperator)
//        {
//            IList<Outlet> destOutlets = destOperator.Outlets.ToList();
//            IList<Outlet> sourceOutlets = GetSourceOutlets(destOperator);

//            return MatchSourceAndDestOutlets(sourceOutlets, destOutlets);
//        }

//        /// <summary> Returned tuples can contain null-elements. </summary>
//        public static IList<OutletTuple> MatchSourceAndDestOutlets(IList<Outlet> sourceOutlets, IList<Outlet> destOutlets)
//        {
//            IList<Outlet> sourceSortedOutlets = sourceOutlets.Sort().ToList();
//            IList<Outlet> destCandidateOutlets = destOutlets.Sort().ToList();

//            var tuples = new List<OutletTuple>();

//            foreach (Outlet sourceOutlet in sourceSortedOutlets)
//            {
//                Outlet destOutlet = TryGetDestOutlet(sourceOutlet, destCandidateOutlets);

//                tuples.Add(new OutletTuple(sourceOutlet, destOutlet));

//                destCandidateOutlets.Remove(destOutlet);
//            }

//            return tuples;
//        }

//        private static Outlet TryGetDestOutlet(Outlet sourceOutlet, IList<Outlet> destCandicateOutlets)
//        {
//            if (destCandicateOutlets == null) throw new NullException(() => destCandicateOutlets);

//            bool nameIsFilledIn = NameHelper.IsFilledIn(sourceOutlet.Name);
//            if (nameIsFilledIn)
//            {
//                // Try match by Name and Position
//                {
//                    Outlet destOutlet = destCandicateOutlets.FirstOrDefault(
//                        x => x.Position == sourceOutlet.Position &&
//                             NameHelper.AreEqual(x.Name, sourceOutlet.Name));

//                    if (destOutlet != null)
//                    {
//                        return destOutlet;
//                    }
//                }

//                // Try match by Name
//                {
//                    Outlet destOutlet = destCandicateOutlets.FirstOrDefault(x => NameHelper.AreEqual(x.Name, sourceOutlet.Name));
//                    if (destOutlet != null)
//                    {
//                        return destOutlet;
//                    }
//                }
//            }

//            DimensionEnum sourceDimensionEnum = sourceOutlet.GetDimensionEnum();
//            bool dimensionIsFilledIn = sourceDimensionEnum != DimensionEnum.Undefined;
//            if (dimensionIsFilledIn)
//            {
//                // Try match by Dimension and Position
//                {
//                    Outlet destOutlet = destCandicateOutlets.FirstOrDefault(
//                        x => x.Position == sourceOutlet.Position &&
//                             x.GetDimensionEnum() == sourceDimensionEnum);

//                    if (destOutlet != null)
//                    {
//                        return destOutlet;
//                    }
//                }

//                // Try match by Dimension
//                {
//                    Outlet destOutlet = destCandicateOutlets.FirstOrDefault(x => x.GetDimensionEnum() == sourceDimensionEnum);
//                    if (destOutlet != null)
//                    {
//                        return destOutlet;
//                    }
//                }
//            }

//            // Try match by Position
//            {
//                Outlet destOutlet = destCandicateOutlets.FirstOrDefault(x => x.Position == sourceOutlet.Position);
//                return destOutlet;
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

//            // Do not match by list index, because that would result in something arbitrary.

//            return false;
//        }
//    }
//}
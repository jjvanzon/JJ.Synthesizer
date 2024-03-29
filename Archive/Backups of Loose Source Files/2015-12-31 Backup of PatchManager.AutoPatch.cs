﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Data.Synthesizer;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.LinkTo;

//namespace JJ.Business.Synthesizer.Managers
//{
//    public partial class PatchManager
//    {
//        private const int DEFAULT_LIST_INDEX = 0; 

//        /// <summary>
//        /// Use the Patch property after calling this method.
//        /// Do a rollback after calling this method to prevent saving the new patch.
//        /// Tries to produce a new patch by tying together existing patches,
//        /// trying to match PatchInlet and PatchOutlet operators by:
//        /// 1) InletType.Name and OutletType.Name
//        /// 2) PatchInlet Operator.Name and PatchOutlet Operator.Name.
//        /// The non-matched inlets and outlets will become inlets and outlets of the new patch.
//        /// If there is overlap in type or name, they will merge to a single inlet or outlet.
//        /// This causes ambiguity in DefaultValue, ListIndex or Name, 
//        /// which is 'resolved' by taking the properties of the first one in the group.
//        /// </summary>
//        public void AutoPatch(IList<Patch> underlyingPatches)
//        {
//            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

//            CreatePatch();
//            Patch.Name = "Auto-generated Patch";
            
//            var customOperators = new List<Operator>(underlyingPatches.Count);

//            foreach (Patch underlyingPatch in underlyingPatches)
//            {
//                CustomOperator_OperatorWrapper customOperatorWrapper = CustomOperator(underlyingPatch);
//                customOperatorWrapper.Name = String.Format("Auto-generated CustomOperator '{0}'", underlyingPatch.Name);

//                customOperators.Add(customOperatorWrapper);
//            }

//            var matchedOutlets = new List<Outlet>();
//            var matchedInlets = new List<Inlet>();

//            for (int i = 0; i < customOperators.Count; i++)
//            {
//                for (int j = i + 1; j < customOperators.Count; j++)
//                {
//                    Operator customOperator1 = customOperators[i];
//                    Operator customOperator2 = customOperators[j];

//                    foreach (Outlet outlet in customOperator1.Outlets)
//                    {
//                        foreach (Inlet inlet in customOperator2.Inlets)
//                        {
//                            if (AreMatch(outlet, inlet))
//                            {
//                                inlet.LinkTo(outlet);

//                                matchedOutlets.Add(outlet);
//                                matchedInlets.Add(inlet);
//                            }
//                        }
//                    }
//                }
//            }

//            // Unmatched inlets of the custom operators become inlets of the new patch.
//            IList<Inlet> unmatchedInlets = customOperators.SelectMany(x => x.Inlets)
//                                                          .Except(matchedInlets)
//                                                          .ToArray();

//            // If there is overlap in InletType, they will merge to a single inlet.
//            var unmatchedInlets_GroupedByInletType = unmatchedInlets.Where(x => x.InletType != null)
//                                                                    .GroupBy(x => x.GetInletTypeEnum());
//            foreach (var unmatchedInletGroup in unmatchedInlets_GroupedByInletType)
//            {
//                PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(unmatchedInletGroup.ToArray());
//            }

//            // If there is overlap in name, they will merge to a single inlet.
//            var unmatchedInlets_WithoutInletType_GroupedByName = unmatchedInlets.Where(x => x.InletType == null && !String.IsNullOrEmpty(x.Name))
//                                                                                .GroupBy(x => x.Name);
//            foreach (var unmatchedInletGroup in unmatchedInlets_WithoutInletType_GroupedByName)
//            {
//                PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(unmatchedInletGroup.ToArray());
//            }

//            // If there is no InletType or name, unmatched Inlets will convert to individual PatchInlets.
//            var unmatchedInlets_WithoutInletTypeOrName = unmatchedInlets.Where(x => x.InletType == null && String.IsNullOrEmpty(x.Name);
//            foreach (Inlet unmatchedInlet in unmatchedInlets_WithoutInletTypeOrName)
//            {
//                PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(unmatchedInlet);
//            }

//            // Unmatched outlets of the custom operators become outlets of the new patch.
//            IList<Outlet> unmatchedOutlets = customOperators.SelectMany(x => x.Outlets)
//                                                            .Except(matchedOutlets)
//                                                            .ToArray();

//            // If there is overlap in OutletType, they will merge to a single outlet.
//            var unmatchedOutlets_GroupedByOutletType = unmatchedOutlets.Where(x => x.OutletType != null)
//                                                                    .GroupBy(x => x.GetOutletTypeEnum());
//            foreach (var unmatchedOutletGroup in unmatchedOutlets_GroupedByOutletType)
//            {
//                PatchOutlet_OperatorWrapper patchOutletWrapper = ConvertToPatchOutlet(unmatchedOutletGroup.ToArray());
//            }

//            // If there is overlap in name, they will merge to a single outlet.
//            var unmatchedOutlets_WithoutOutletType_GroupedByName = unmatchedOutlets.Where(x => x.OutletType == null && !String.IsNullOrEmpty(x.Name))
//                                                                                   .GroupBy(x => x.Name);
//            foreach (var unmatchedOutletGroup in unmatchedOutlets_WithoutOutletType_GroupedByName)
//            {
//                PatchOutlet_OperatorWrapper patchOutletWrapper = ConvertToPatchOutlet(unmatchedOutletGroup.ToArray());
//            }

//            // If there is no OutletType or name, unmatched Outlets will convert to individual PatchOutlets.
//            foreach (Outlet unmatchedOutlet in unmatchedOutlets)
//            {
//                PatchOutlet_OperatorWrapper patchOutletWrapper = ConvertToPatchOutlet(unmatchedOutlet);
//            }
//        }

//        private PatchInlet_OperatorWrapper ConvertToPatchInlet(IList<Inlet> unmatchedInlets)
//        {
//            Inlet firstUnmatchedInlet = unmatchedInlets.First();

//            PatchInlet_OperatorWrapper patchInletWrapper = Inlet();
//            patchInletWrapper.InletTypeEnum = firstUnmatchedInlet.GetInletTypeEnum();
//            patchInletWrapper.Name = firstUnmatchedInlet.Name;
//            patchInletWrapper.ListIndex = firstUnmatchedInlet.ListIndex;
//            patchInletWrapper.DefaultValue = firstUnmatchedInlet.DefaultValue;

//            foreach (Inlet unmatchedInlet in unmatchedInlets)
//            {
//                unmatchedInlet.LinkTo((Outlet)patchInletWrapper);
//            }

//            return patchInletWrapper;
//        }

//        private PatchInlet_OperatorWrapper ConvertToPatchInlet(Inlet unmatchedInlet)
//        {
//            PatchInlet_OperatorWrapper patchInletWrapper = Inlet();
//            patchInletWrapper.InletTypeEnum = unmatchedInlet.GetInletTypeEnum();
//            patchInletWrapper.Name = unmatchedInlet.Name;
//            patchInletWrapper.ListIndex = unmatchedInlet.ListIndex;
//            patchInletWrapper.DefaultValue = unmatchedInlet.DefaultValue;

//            unmatchedInlet.LinkTo((Outlet)patchInletWrapper);

//            return patchInletWrapper;
//        }

//        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(Outlet unmatchedOutlet)
//        {
//            PatchOutlet_OperatorWrapper patchOutletWrapper = Outlet();
//            patchOutletWrapper.Name = unmatchedOutlet.Name;
//            patchOutletWrapper.ListIndex = unmatchedOutlet.ListIndex;
//            patchOutletWrapper.OutletTypeEnum = unmatchedOutlet.GetOutletTypeEnum();

//            patchOutletWrapper.Input = unmatchedOutlet;

//            return patchOutletWrapper;
//        }

//        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(IList<Outlet> unmatchedOutlets)
//        {
//            Adder_OperatorWrapper adderWrapper = Adder(unmatchedOutlets);

//            Outlet firstUnmatchedOutlet = unmatchedOutlets.First();

//            PatchOutlet_OperatorWrapper patchOutletWrapper = Outlet();
//            patchOutletWrapper.OutletTypeEnum = firstUnmatchedOutlet.GetOutletTypeEnum();
//            patchOutletWrapper.Name = firstUnmatchedOutlet.Name;
//            patchOutletWrapper.ListIndex = firstUnmatchedOutlet.ListIndex;

//            patchOutletWrapper.Input = adderWrapper;

//            return patchOutletWrapper;
//        }

//        private bool AreMatch(Outlet outlet, Inlet inlet)
//        {
//            if (outlet == null)
//            {
//                return false;
//            }

//            if (inlet == null)
//            {
//                return false;
//            }

//            // First match by OutletType / InletType.
//            OutletTypeEnum outletTypeEnum = outlet.GetOutletTypeEnum();
//            if (outletTypeEnum != OutletTypeEnum.Undefined)
//            {
//                InletTypeEnum inletTypeEnum = inlet.GetInletTypeEnum();
//                if (inletTypeEnum != InletTypeEnum.Undefined)
//                {
//                    string outletTypeString = outletTypeEnum.ToString();
//                    string inletTypeString = inletTypeEnum.ToString();

//                    if (String.Equals(outletTypeString, inletTypeString))
//                    {
//                        return true;
//                    }
//                }
//            }

//            // Then match by name
//            if (String.Equals(outlet.Name, inlet.Name))
//            {
//                return true;
//            }

//            // Do not match by list index, because that would result in something arbitrary.

//            return false;
//        }

//        /// <summary> Will return null if no Frequency inlet or Signal outlet is found. </summary>
//        public Outlet TryAutoPatch_WithTone(Tone tone, IList<Patch> underlyingPatches)
//        {
//            if (tone == null) throw new NullException(() => tone);
//            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

//            // Create a new patch out of the other patches.
//            AutoPatch(underlyingPatches);

//            double frequency = tone.GetFrequency();
//            Number_OperatorWrapper frequencyNumberOperatorWrapper = Number(frequency);

//            IEnumerable<Inlet> frequencyInlets = Patch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
//                                                      .Where(x => x.InletTypeEnum == InletTypeEnum.Frequency)
//                                                      .Select(x => x.Inlet);

//            foreach (Inlet frequencyInlet in frequencyInlets)
//            {
//                frequencyInlet.InputOutlet = frequencyNumberOperatorWrapper;
//            }

//            IEnumerable<Outlet> signalOutlets = Patch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
//                                                     .Where(x => x.OutletTypeEnum == OutletTypeEnum.Signal)
//                                                     .Select(x => x.Result);

//            // TODO: Add up the signals instead of taking the first one.
//            Outlet outlet = signalOutlets.First();
//            return outlet;
//        }
//    }
//}

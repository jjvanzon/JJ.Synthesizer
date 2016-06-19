using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using JJ.Business.Canonical;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer
{
    public partial class PatchManager
    {
        /// <summary>
        /// Auto-patches the provided patches and makes a custom operator from it.
        /// Then creates a wrapper patch around it, that enables polyphony.
        /// For more information: see method summary of AutoPatch.
        /// </summary>
        public Outlet AutoPatchPolyphonic(IList<Patch> underlyingPatches, int maxConcurrentNotes)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);
            if (maxConcurrentNotes < 1) throw new LessThanException(() => maxConcurrentNotes, 1);

            AutoPatch(underlyingPatches);
            Patch monophonicAutoPatch = Patch;

            CreatePatch();
            Patch.Name = "Auto-Generated Polyphonic Patch";
            Patch polyphonicAutoPatch = Patch;

            var monophonicOutlets = new List<Outlet>(maxConcurrentNotes);

            for (int i = 0; i < maxConcurrentNotes; i++)
            {
                CustomOperator_OperatorWrapper customOperatorWrapper = CustomOperator(monophonicAutoPatch);

                foreach (Inlet inlet in customOperatorWrapper.Inlets)
                {
                    DimensionEnum inletDimensionEnum = inlet.GetDimensionEnum();
                    if (inletDimensionEnum != DimensionEnum.Undefined)
                    {
                        PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(inlet);
                        patchInletWrapper.Name = String.Format("{0} {1}", inletDimensionEnum, i);

                        inlet.LinkTo((Outlet)patchInletWrapper);
                    }
                }

                Outlet signalOutlet = customOperatorWrapper.Outlets.Where(x => x.GetDimensionEnum() == DimensionEnum.Signal).SingleOrDefault();
                if (signalOutlet != null)
                {
                    monophonicOutlets.Add(signalOutlet);
                }
            }

            // Add Reset operators in between.
            // TODO: Low priority: I have the feeling that this code belongs in the previous loop.
            for (int i = 0; i < monophonicOutlets.Count; i++)
            {
                Outlet monophonicOutlet = monophonicOutlets[i];

                Reset_OperatorWrapper resetWrapper = Reset(monophonicOutlet);
                resetWrapper.ListIndex = i;

                monophonicOutlets[i] = resetWrapper;
            }

            Add_OperatorWrapper addWrapper = Add(monophonicOutlets);
            Outlet polyphonicOutlet = addWrapper.Result;

            // This makes side-effects go off.
            VoidResult savePatchResult = SavePatch();

            // This is sensitive, error prone code, so assert its result 
            ResultHelper.Assert(savePatchResult);

            return polyphonicOutlet;
        }

        /// <summary> Will return null if no Frequency inlet or Signal outlet is found. </summary>
        public Outlet TryAutoPatch_WithTone(Tone tone, IList<Patch> underlyingPatches)
        {
            if (tone == null) throw new NullException(() => tone);
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            // Create a new patch out of the other patches.
            AutoPatch(underlyingPatches);

            double frequency = tone.GetFrequency();
            Number_OperatorWrapper frequencyNumberOperatorWrapper = Number(frequency);

            IEnumerable<Inlet> frequencyInlets = Patch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                      .Where(x => x.Inlet.GetDimensionEnum() == DimensionEnum.Frequency)
                                                      .Select(x => x.Inlet);

            foreach (Inlet frequencyInlet in frequencyInlets)
            {
                frequencyInlet.InputOutlet = frequencyNumberOperatorWrapper;
            }

            IEnumerable<Outlet> signalOutlets = Patch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                                     .Where(x => x.Result.GetDimensionEnum() == DimensionEnum.Signal)
                                                     .Select(x => x.Result);

            // TODO: Add up the signals instead of taking the first one.
            Outlet outlet = signalOutlets.First();
            return outlet;
        }

        /// <summary>
        /// Use the Patch property after calling this method.
        /// Do a rollback after calling this method to prevent saving the new patch.
        /// Tries to produce a new patch by tying together existing patches,
        /// trying to match PatchInlet and PatchOutlet operators by:
        /// 1) Inlet.Dimension.Name and Dimension.Name
        /// 2) PatchInlet Operator.Name and PatchOutlet Operator.Name.
        /// The non-matched inlets and outlets will become inlets and outlets of the new patch.
        /// If there is overlap in type or name, they will merge to a single inlet or outlet.
        /// This causes ambiguity in DefaultValue, ListIndex or Name, 
        /// which is 'resolved' by taking the properties of the first one in the group.
        /// </summary>
        public void AutoPatch(IList<Patch> underlyingPatches)
        {
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            CreatePatch();
            Patch.Name = "Auto-Generated Patch";
            
            var customOperators = new List<Operator>(underlyingPatches.Count);

            foreach (Patch underlyingPatch in underlyingPatches)
            {
                CustomOperator_OperatorWrapper customOperatorWrapper = CustomOperator(underlyingPatch);
                customOperatorWrapper.Name = String.Format("{0}", underlyingPatch.Name);

                customOperators.Add(customOperatorWrapper);
            }

            var matchedOutlets = new List<Outlet>();
            var matchedInlets = new List<Inlet>();

            for (int i = 0; i < customOperators.Count; i++)
            {
                for (int j = i + 1; j < customOperators.Count; j++)
                {
                    Operator customOperator1 = customOperators[i];
                    Operator customOperator2 = customOperators[j];

                    foreach (Outlet outlet in customOperator1.Outlets)
                    {
                        foreach (Inlet inlet in customOperator2.Inlets)
                        {
                            if (InletOutletMatcher.AreMatch(outlet, inlet))
                            {
                                inlet.LinkTo(outlet);

                                matchedOutlets.Add(outlet);
                                matchedInlets.Add(inlet);
                            }
                        }
                    }
                }
            }

            // Unmatched inlets of the custom operators become inlets of the new patch.
            IList<Inlet> unmatchedInlets = customOperators.SelectMany(x => x.Inlets)
                                                          .Except(matchedInlets)
                                                          .ToArray();

            // If there is overlap in Inlet Dimension, they will merge to a single PatchInlet.
            var unmatchedInlets_GroupedByDimension = unmatchedInlets.Where(x => x.Dimension != null)
                                                                    .GroupBy(x => x.GetDimensionEnum());
            foreach (var unmatchedInletGroup in unmatchedInlets_GroupedByDimension)
            {
                PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(unmatchedInletGroup.ToArray());
            }

            // If there is overlap in name, they will merge to a single PatchInlet.
            var unmatchedInlets_WithoutDimension_GroupedByName = unmatchedInlets.Where(x => x.Dimension == null && !String.IsNullOrEmpty(x.Name))
                                                                                .GroupBy(x => x.Name);
            foreach (var unmatchedInletGroup in unmatchedInlets_WithoutDimension_GroupedByName)
            {
                PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(unmatchedInletGroup.ToArray());
            }

            // If there is no Inlet Dimension or name, unmatched Inlets will convert to individual PatchInlets.
            var unmatchedInlets_WithoutDimensionOrName = unmatchedInlets.Where(x => x.Dimension == null && String.IsNullOrEmpty(x.Name));
            foreach (Inlet unmatchedInlet in unmatchedInlets_WithoutDimensionOrName)
            {
                PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(unmatchedInlet);
            }

            // Unmatched outlets of the custom operators become outlets of the new patch.
            IList<Outlet> unmatchedOutlets = customOperators.SelectMany(x => x.Outlets)
                                                            .Except(matchedOutlets)
                                                            .ToArray();

            // If there is overlap in Dimension, they will merge to a single PatchOutlet.
            var unmatchedOutlets_GroupedByDimension = unmatchedOutlets.Where(x => x.Dimension != null)
                                                                       .GroupBy(x => x.GetDimensionEnum());
            foreach (var unmatchedOutletGroup in unmatchedOutlets_GroupedByDimension)
            {
                PatchOutlet_OperatorWrapper patchOutletWrapper = ConvertToPatchOutlet(unmatchedOutletGroup.ToArray());
            }

            // If there is overlap in name, they will merge to a single PatchOutlet.
            var unmatchedOutlets_WithoutDimension_GroupedByName = unmatchedOutlets.Where(x => x.Dimension == null && !String.IsNullOrEmpty(x.Name))
                                                                                   .GroupBy(x => x.Name);
            foreach (var unmatchedOutletGroup in unmatchedOutlets_WithoutDimension_GroupedByName)
            {
                PatchOutlet_OperatorWrapper patchOutletWrapper = ConvertToPatchOutlet(unmatchedOutletGroup.ToArray());
            }

            // If there is no Dimension or name, unmatched Outlets will convert to individual PatchOutlets.
            var unmatchedOutlets_WithoutDimensionOrName = unmatchedOutlets.Where(x => x.Dimension == null && String.IsNullOrEmpty(x.Name));
            foreach (Outlet unmatchedOutlet in unmatchedOutlets_WithoutDimensionOrName)
            {
                PatchOutlet_OperatorWrapper patchOutletWrapper = ConvertToPatchOutlet(unmatchedOutlet);
            }

            // Renumber the patch inlets and outlets
            // TODO: This numbering seems arbitrary.
            IList<PatchInlet_OperatorWrapper> patchInletOperatorWrappers = Patch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                                                .OrderBy(x => x.ListIndex)
                                                                                .ToArray();
            for (int i = 0; i < patchInletOperatorWrappers.Count; i++)
            {
                PatchInlet_OperatorWrapper patchInletOperatorWrapper = patchInletOperatorWrappers[i];
                patchInletOperatorWrapper.ListIndex = i;
            }

            IList<PatchOutlet_OperatorWrapper> patchOutletOperatorWrappers = Patch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                                                    .OrderBy(x => x.ListIndex)
                                                                    .ToArray();

            for (int i = 0; i < patchOutletOperatorWrappers.Count; i++)
            {
                PatchOutlet_OperatorWrapper patchOutletOperatorWrapper = patchOutletOperatorWrappers[i];
                patchOutletOperatorWrapper.ListIndex = i;
            }

            // This is sensitive, error prone code, so verify its result with the validators. 
            VoidResult result = ValidatePatchRecursive();
            ResultHelper.Assert(result);
        }

        private PatchInlet_OperatorWrapper ConvertToPatchInlet(Inlet sourceInlet)
        {
            PatchInlet_OperatorWrapper destPatchInletWrapper = PatchInlet();
            destPatchInletWrapper.Name = sourceInlet.Name;

            // TODO: You might want to do this by calling shared business logic instead of reprogramming it here.
            Inlet destPatchInletInlet = destPatchInletWrapper.Inlet;
            destPatchInletInlet.Dimension = sourceInlet.Dimension;
            destPatchInletInlet.DefaultValue = sourceInlet.DefaultValue;

            sourceInlet.LinkTo(destPatchInletWrapper.Result);

            return destPatchInletWrapper;
        }

        private PatchInlet_OperatorWrapper ConvertToPatchInlet(IList<Inlet> unmatchedInlets)
        {
            Inlet firstUnmatchedInlet = unmatchedInlets.First();

            PatchInlet_OperatorWrapper patchInletWrapper = ConvertToPatchInlet(firstUnmatchedInlet);

            foreach (Inlet unmatchedInlet in unmatchedInlets)
            {
                unmatchedInlet.LinkTo((Outlet)patchInletWrapper);
            }

            return patchInletWrapper;
        }

        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(IList<Outlet> unmatchedOutlets)
        {
            Outlet firstUnmatchedOutlet = unmatchedOutlets.First();
            PatchOutlet_OperatorWrapper patchOutletWrapper = ConvertToPatchOutlet(firstUnmatchedOutlet);

            Outlet patchOutletInput = firstUnmatchedOutlet;
            if (unmatchedOutlets.Count > 1)
            {
                // You only need an adder, when there is more than 1 unmatchedOutlet.
                patchOutletInput = Add(unmatchedOutlets);
            }

            patchOutletWrapper.Input = patchOutletInput;

            return patchOutletWrapper;
        }

        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(Outlet sourceOutlet)
        {
            PatchOutlet_OperatorWrapper destPatchOutletWrapper = PatchOutlet();
            destPatchOutletWrapper.Name = sourceOutlet.Name;
            destPatchOutletWrapper.ListIndex = sourceOutlet.ListIndex;
            destPatchOutletWrapper.Result.Dimension = sourceOutlet.Dimension;

            destPatchOutletWrapper.Input = sourceOutlet;

            return destPatchOutletWrapper;
        }
    }
}

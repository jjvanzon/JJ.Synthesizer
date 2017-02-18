using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using JJ.Business.Canonical;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer
{
    public partial class PatchManager
    {
        /// <summary> Will return null if no Frequency inlet or Signal outlet is found. </summary>
        public Outlet TryAutoPatch_WithTone(Tone tone, IList<Patch> underlyingPatches)
        {
            if (tone == null) throw new NullException(() => tone);
            if (underlyingPatches == null) throw new NullException(() => underlyingPatches);

            // Create a new patch out of the other patches.
            AutoPatch(underlyingPatches);

            double frequency = tone.GetFrequency();
            Number_OperatorWrapper frequencyNumberOperatorWrapper = Number(frequency);

            IList<Inlet> frequencyInlets = Patch.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                .Where(x => x.DimensionEnum == DimensionEnum.Frequency)
                                                .Select(x => x.Inlet)
                                                .ToArray();
            if (frequencyInlets.Count == 0)
            {
                return null;
            }

            foreach (Inlet frequencyInlet in frequencyInlets)
            {
                frequencyInlet.LinkTo(frequencyNumberOperatorWrapper.Result);
            }

            IList<Outlet> signalOutlets = Patch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
                                               .Where(x => x.DimensionEnum == DimensionEnum.Signal)
                                               .Select(x => x.Result)
                                               .ToArray();
            switch (signalOutlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return signalOutlets[0];

                default:
                    var add = Add(signalOutlets);
                    return add;
            }
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
        public void AutoPatch(IList<Patch> sourceUnderlyingPatches)
        {
            if (sourceUnderlyingPatches == null) throw new NullException(() => sourceUnderlyingPatches);

            CreatePatch();
            Patch.Name = "Auto-Generated Patch";

            var intermediateCustomOperators = new List<Operator>(sourceUnderlyingPatches.Count);

            foreach (Patch sourceUnderlyingPatch in sourceUnderlyingPatches)
            {
                CustomOperator_OperatorWrapper intermediateCustomOperatorWrapper = CustomOperator(sourceUnderlyingPatch);
                intermediateCustomOperatorWrapper.Name = $"{sourceUnderlyingPatch.Name}";

                intermediateCustomOperators.Add(intermediateCustomOperatorWrapper);
            }

            var intermediateMatchedOutlets = new List<Outlet>();
            var intermediateMatchedInlets = new List<Inlet>();

            for (int i = 0; i < intermediateCustomOperators.Count; i++)
            {
                for (int j = i + 1; j < intermediateCustomOperators.Count; j++)
                {
                    Operator intermediateCustomOperator1 = intermediateCustomOperators[i];
                    Operator intermediateCustomOperator2 = intermediateCustomOperators[j];

                    foreach (Outlet intermediateOutlet in intermediateCustomOperator1.Outlets)
                    {
                        foreach (Inlet intermediateInlet in intermediateCustomOperator2.Inlets)
                        {
                            // ReSharper disable once InvertIf
                            if (InletOutletMatcher.AreMatch(intermediateOutlet, intermediateInlet))
                            {
                                intermediateInlet.LinkTo(intermediateOutlet);

                                intermediateMatchedOutlets.Add(intermediateOutlet);
                                intermediateMatchedInlets.Add(intermediateInlet);
                            }
                        }
                    }
                }
            }

            // Renumber dest patch inlets and outlets as you create them.
            // The numbering is arbitrary, but ListIndexes have to be unique.
            int listIndex = 0;

            // Unmatched inlets of the custom operators become inlets of the new patch.
            IList<Inlet> intermediateUnmatchedInlets = intermediateCustomOperators.SelectMany(x => x.Inlets)
                                                                                  .Except(intermediateMatchedInlets)
                                                                                  .ToArray();

            // If there is overlap in Inlet Dimension, they will merge to a single PatchInlet.
            var intermediateUnmatchedInlets_GroupedByDimension = intermediateUnmatchedInlets.Where(x => x.Dimension != null)
                                                                                            .GroupBy(x => x.GetDimensionEnum());
            foreach (var intermediateUnmatchedInletGroup in intermediateUnmatchedInlets_GroupedByDimension)
            {
                PatchInlet_OperatorWrapper patchInletOperatorWrapper = ConvertToPatchInlet(intermediateUnmatchedInletGroup.ToArray());
                patchInletOperatorWrapper.ListIndex = listIndex++;
            }

            // If there is overlap in name, they will merge to a single PatchInlet.
            var intermediateUnmatchedInlets_WithoutDimension_GroupedByName = intermediateUnmatchedInlets.Where(x => x.Dimension == null && !string.IsNullOrEmpty(x.Name))
                                                                                                        .GroupBy(x => x.Name);
            foreach (var intermediateUnmatchedInletGroup in intermediateUnmatchedInlets_WithoutDimension_GroupedByName)
            {
                PatchInlet_OperatorWrapper patchInletOperatorWrapper = ConvertToPatchInlet(intermediateUnmatchedInletGroup.ToArray());
                patchInletOperatorWrapper.ListIndex = listIndex++;
            }

            // If there is no Inlet Dimension or name, unmatched Inlets will convert to individual PatchInlets.
            var intermediateUnmatchedInlets_WithoutDimensionOrName = intermediateUnmatchedInlets.Where(x => x.Dimension == null && string.IsNullOrEmpty(x.Name));
            foreach (Inlet unmatchedInlet in intermediateUnmatchedInlets_WithoutDimensionOrName)
            {
                PatchInlet_OperatorWrapper patchInletOperatorWrapper = ConvertToPatchInlet(unmatchedInlet);
                patchInletOperatorWrapper.ListIndex = listIndex++;
            }

            // Unmatched outlets of the custom operators become outlets of the new patch.
            IList<Outlet> intermediateUnmatchedOutlets = intermediateCustomOperators.SelectMany(x => x.Outlets)
                                                                                    .Except(intermediateMatchedOutlets)
                                                                                    .ToArray();

            // If there is overlap in Dimension, they will merge to a single PatchOutlet.
            var intermediateUnmatchedOutlets_GroupedByDimension = intermediateUnmatchedOutlets.Where(x => x.Dimension != null)
                                                                                              .GroupBy(x => x.GetDimensionEnum());
            foreach (var intermediateUnmatchedOutletGroup in intermediateUnmatchedOutlets_GroupedByDimension)
            {
                PatchOutlet_OperatorWrapper patchOutlet_OperatorWrapper = ConvertToPatchOutlet(intermediateUnmatchedOutletGroup.ToArray());
                patchOutlet_OperatorWrapper.ListIndex = listIndex++;
            }

            // If there is overlap in name, they will merge to a single PatchOutlet.
            var intermediateUnmatchedOutlets_WithoutDimension_GroupedByName = intermediateUnmatchedOutlets.Where(x => x.Dimension == null && !string.IsNullOrEmpty(x.Name))
                                                                                                          .GroupBy(x => x.Name);
            foreach (var intermediateUnmatchedOutletGroup in intermediateUnmatchedOutlets_WithoutDimension_GroupedByName)
            {
                PatchOutlet_OperatorWrapper patchOutlet_OperatorWrapper = ConvertToPatchOutlet(intermediateUnmatchedOutletGroup.ToArray());
                patchOutlet_OperatorWrapper.ListIndex = listIndex++;
            }

            // If there is no Dimension or name, unmatched Outlets will convert to individual PatchOutlets.
            var intermediateUnmatchedOutlets_WithoutDimensionOrName = intermediateUnmatchedOutlets.Where(x => x.Dimension == null && string.IsNullOrEmpty(x.Name));
            foreach (Outlet intermediateUnmatchedOutlet in intermediateUnmatchedOutlets_WithoutDimensionOrName)
            {
                PatchOutlet_OperatorWrapper destPatchOutletOperatorWrapper = ConvertToPatchOutlet(intermediateUnmatchedOutlet);
                destPatchOutletOperatorWrapper.ListIndex = listIndex++;
            }

            // This is sensitive, error prone code, so verify its result with the validators.
            VoidResult result = ValidatePatchWithRelatedEntities();
            ResultHelper.Assert(result);
        }

        private PatchInlet_OperatorWrapper ConvertToPatchInlet(Inlet intermediateInlet)
        {
            PatchInlet_OperatorWrapper destPatchInletWrapper = PatchInlet();
            destPatchInletWrapper.Name = intermediateInlet.Name;
            destPatchInletWrapper.ListIndex = intermediateInlet.ListIndex;
            destPatchInletWrapper.Dimension = intermediateInlet.Dimension;
            destPatchInletWrapper.DefaultValue = intermediateInlet.DefaultValue;

            intermediateInlet.LinkTo(destPatchInletWrapper.Result);

            return destPatchInletWrapper;
        }

        private PatchInlet_OperatorWrapper ConvertToPatchInlet(IList<Inlet> intermediateUnmatchedInlets)
        {
            Inlet intermediateFirstUnmatchedInlet = intermediateUnmatchedInlets.First();

            PatchInlet_OperatorWrapper destPatchInletWrapper = ConvertToPatchInlet(intermediateFirstUnmatchedInlet);

            foreach (Inlet intermediateUnmatchedInlet in intermediateUnmatchedInlets)
            {
                intermediateUnmatchedInlet.LinkTo((Outlet)destPatchInletWrapper);
            }

            return destPatchInletWrapper;
        }

        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(IList<Outlet> intermediateUnmatchedOutlets)
        {
            Outlet intermediateFirstUnmatchedOutlet = intermediateUnmatchedOutlets.First();
            PatchOutlet_OperatorWrapper destPatchOutletWrapper = ConvertToPatchOutlet(intermediateFirstUnmatchedOutlet);

            Outlet intermediatePatchOutletInput = intermediateFirstUnmatchedOutlet;
            if (intermediateUnmatchedOutlets.Count > 1)
            {
                // You only need an adder, when there is more than 1 unmatchedOutlet.
                intermediatePatchOutletInput = Add(intermediateUnmatchedOutlets);
            }

            destPatchOutletWrapper.Input = intermediatePatchOutletInput;

            return destPatchOutletWrapper;
        }

        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(Outlet intermediateUnmatchedOutlet)
        {
            PatchOutlet_OperatorWrapper destPatchOutletWrapper = PatchOutlet();
            destPatchOutletWrapper.Name = intermediateUnmatchedOutlet.Name;
            destPatchOutletWrapper.ListIndex = intermediateUnmatchedOutlet.ListIndex;
            destPatchOutletWrapper.Input = intermediateUnmatchedOutlet;
            destPatchOutletWrapper.Dimension = intermediateUnmatchedOutlet.Dimension;

            return destPatchOutletWrapper;
        }
    }
}

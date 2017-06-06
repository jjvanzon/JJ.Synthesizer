using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Business;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Mathematics;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer
{
    public partial class PatchManager
    {
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
            VoidResultDto result = ValidatePatchWithRelatedEntities();
            ResultHelper.Assert(result);
        }

        private PatchInlet_OperatorWrapper ConvertToPatchInlet(Inlet intermediateInlet)
        {
            PatchInlet_OperatorWrapper destPatchInletWrapper = PatchInlet();
            destPatchInletWrapper.Name = intermediateInlet.Name;
            destPatchInletWrapper.ListIndex = intermediateInlet.ListIndex;
            destPatchInletWrapper.Dimension = intermediateInlet.Dimension;
            destPatchInletWrapper.DefaultValue = intermediateInlet.DefaultValue;

            intermediateInlet.LinkTo(destPatchInletWrapper.Outlet);

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

        /// <summary> Will return null if no Frequency inlet or Sound outlet is found. </summary>
        public Outlet TryAutoPatchWithTone(Tone tone, IList<Patch> sourceUnderlyingPatches)
        {
            if (tone == null) throw new NullException(() => tone);
            if (sourceUnderlyingPatches == null) throw new NullException(() => sourceUnderlyingPatches);

            // Create a new patch out of the other patches.
            AutoPatch(sourceUnderlyingPatches);
            Patch autoPatch = Patch;

            CreatePatch();
            var customOperator = CustomOperator(autoPatch);
            var frequency = Number(tone.GetFrequency());

            IList<Inlet> customOperatorFrequencyInlets = customOperator.Inlets.GetMany(DimensionEnum.Frequency);
            if (customOperatorFrequencyInlets.Count == 0)
            {
                return null;
            }

            foreach (Inlet customOperatorFrequencyInlet in customOperatorFrequencyInlets)
            {
                customOperatorFrequencyInlet.LinkTo(frequency.NumberOutlet);
            }

            IList<Outlet> soundOutlets = customOperator.Outlets.GetMany(DimensionEnum.Sound);
            switch (soundOutlets.Count)
            {
                case 0:
                    return null;

                case 1:
                    return soundOutlets[0];

                default:
                    var add = Add(soundOutlets);
                    return add;
            }
        }

        /// <summary>
        /// Will replace PatchManager.Patch with a new patch.
        /// 
        /// Creatively tries to make the best of getting sound out of the source Patch.
        /// Tries to find outlets to combine into sound.
        /// If selectedOperatorID is provided, only outlets of the selected Operator are considered.
        /// If selectedOperatorID is not provided, all outlets of the patch are considered.
        /// Outlets of type sound are preferred,
        /// but if none are found, all outlets are considered.
        /// 
        /// If no suitable outlets were found, a result with Successful = false is returned.
        /// If outlets to combine were found, PatchManager's Patch property will reference the a patch.
        /// Also the outlet that returns the sound is returned through the result.
        /// </summary>
        public Result<Outlet> AutoPatch_TryCombineSounds(Patch sourcePatch, int? selectedOperatorID = null)
        {
            if (selectedOperatorID.HasValue)
            {
                // If an (internal) operator is selected, generate some patch outlets in the patch, before turning it into a custom operator.
                Patch = sourcePatch;

                foreach (Outlet soundOutlet in GetSoundOutletsFromOperatorCreatively(selectedOperatorID.Value))
                {
                    PatchOutlet(DimensionEnum.Sound, soundOutlet);
                }
            }

            CreatePatch();
            Patch.Name = "Auto-Generated Patch";

            CustomOperator_OperatorWrapper customOperator = CustomOperator(sourcePatch);
            //IList<Outlet> soundOutlets = customOperator.Outlets.Where(x => x.GetDimensionEnum() == DimensionEnum.Sound).ToArray();
            IList<Outlet> soundOutlets = GetSoundOutletsFromOperatorCreatively(customOperator);

            if (soundOutlets.Count == 0)
            {
                return new Result<Outlet>
                {
                    Successful = false,
                    Messages = new Messages { new Message(nameof(soundOutlets), ResourceFormatter.PatchHasNoOutlets) }
                };
            }
            else
            {
                Outlet add = Add(soundOutlets);
                Outlet patchOutlet = PatchOutlet(DimensionEnum.Sound, add);

                return new Result<Outlet>
                {
                    Successful = true,
                    Data = patchOutlet
                };
            }
        }

        /// <summary> In case no sound outlets are present, all outlets are returned. </summary>
        private IList<Outlet> GetSoundOutletsFromOperatorCreatively(int operatorID)
        {
            Operator selectedOperator = _repositories.OperatorRepository.Get(operatorID);

            return GetSoundOutletsFromOperatorCreatively(selectedOperator);
        }

        /// <summary> In case no sound outlets are present, all outlets are returned. </summary>
        private static IList<Outlet> GetSoundOutletsFromOperatorCreatively(Operator selectedOperator)
        {
            switch (selectedOperator.Outlets.Count)
            {
                case 0:
                    // Selected Operator has no Outlets
                    return new Outlet[0];

                case 1:
                    // Selected Operator has single Outlet
                    // ReSharper disable once RedundantExplicitArrayCreation
                    return new Outlet[] { selectedOperator.Outlets[0] };

                default:
                    IList<Outlet> soundOutlets = selectedOperator.Outlets
                                                                 .Where(x => x.GetDimensionEnum() == DimensionEnum.Sound)
                                                                 .ToArray();

                    // ReSharper disable once ConvertIfStatementToReturnStatement
                    if (soundOutlets.Count != 0)
                    {
                        // Selected Operator has sound outlets.
                        return soundOutlets;
                    }
                    else
                    {
                        // Selected Operator has no sound outlets.
                        return selectedOperator.Outlets;
                    }
            }
        }

        // TODO: Remove outcommented code.
        ///// <summary> In case no sound outlets are presents, all patch outlets are returned. </summary>
        //private IList<Outlet> GetSoundOutletsFromPatch(Patch sourcePatch)
        //{
        //    IList<Outlet> soundPatchOutlets = sourcePatch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
        //                                                 .Where(x => x.DimensionEnum == DimensionEnum.Sound)
        //                                                 .Select(x => x.Outlet)
        //                                                 .ToArray();
        //    if (soundPatchOutlets.Count != 0)
        //    {
        //        // Patch has Sound PatchOutlets.
        //        return soundPatchOutlets;
        //    }

        //    // Patch has no Sound PatchOutlets:
        //    // Return all PatchOutlets.
        //    IList<Outlet> patchOutlets = sourcePatch.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>()
        //                                            .Select(x => x.Outlet)
        //                                            .ToArray();

        //    return patchOutlets;
        //}

        public Result<Outlet> TryAutoPatchFromDocumentsRandomly([NotNull] IList<Document> documents, bool mustIncludeHidden)
        {
            if (documents == null) throw new NullException(() => documents);

            IList<Patch> patches = documents.SelectMany(x => x.Patches).ToArray();
            IList<Outlet> soundOutlets = GetSoundOutletsFromPatchesWithoutSoundInlets(patches, mustIncludeHidden);

            Outlet soundOutlet = Randomizer.TryGetRandomItem(soundOutlets);

            if (soundOutlet == null)
            {
                return new Result<Outlet>
                {
                    Successful = false,
                    Messages = new Messages { new Message(nameof(DocumentReference), ResourceFormatter.NoSoundFound) }
                };
            }
            else
            {
                return new Result<Outlet>
                {
                    Successful = true,
                    Data = soundOutlet
                };
            }
        }

        /// <summary> Can be used to for instance quickly generate an example sound from a document used as library. </summary>
        public Result<Outlet> TryAutoPatchFromDocumentRandomly(Document document, bool mustIncludeHidden)
        {
            IList<Outlet> soundOutlets = GetSoundOutletsFromPatchesWithoutSoundInlets(document.Patches, mustIncludeHidden);

            // TODO: Select the first patch with a sound inlet and use autopatch those two together.

            Outlet soundOutlet = Randomizer.TryGetRandomItem(soundOutlets);

            if (soundOutlet == null)
            {
                return new Result<Outlet>
                {
                    Successful = false,
                    Messages = new Messages { new Message(nameof(DocumentReference), ResourceFormatter.NoSoundFound) }
                };
            }
            else
            {
                return new Result<Outlet>
                {
                    Successful = true,
                    Data = soundOutlet
                };
            }
        }

        /// <summary> Can be used to for instance quickly generate an example sound from a patch group. </summary>
        public Result<Outlet> TryAutoPatchFromPatchGroupRandomly(Document document, string groupName, bool mustIncludeHidden)
        {
            IList<Patch> patchesInGroup = GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(document.Patches, groupName, mustIncludeHidden);
            IList<Outlet> soundOutlets = GetSoundOutletsFromPatchesWithoutSoundInlets(patchesInGroup, mustIncludeHidden);

            Outlet soundOutlet = Randomizer.TryGetRandomItem(soundOutlets);

            if (soundOutlet == null)
            {
                return new Result<Outlet>
                {
                    Successful = false,
                    Messages = new Messages { new Message(nameof(Document), ResourceFormatter.NoSoundFound) }
                };
            }
            else
            {
                return new Result<Outlet>
                {
                    Successful = true,
                    Data = soundOutlet
                };
            }
        }

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private IList<Outlet> GetSoundOutletsFromPatchesWithoutSoundInlets(IList<Patch> patches, bool mustIncludeHidden)
        {
            IList<Outlet> patches2 = patches.Where(x => !x.Hidden || mustIncludeHidden)
                                            .Where(
                                                x => !x.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                       .Where(y => y.DimensionEnum == DimensionEnum.Sound)
                                                       .Any())
                                            .OrderBy(x => x.Name)
                                            .SelectMany(x => x.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>())
                                            .Where(x => x.DimensionEnum == DimensionEnum.Sound)
                                            .Select(x => x.Outlet)
                                            .ToArray();
            return patches2;
        }
    }
}

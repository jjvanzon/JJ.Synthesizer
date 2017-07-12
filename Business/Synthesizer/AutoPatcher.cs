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
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Mathematics;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer
{
    public class AutoPatcher
    {
        private readonly RepositoryWrapper _repositories;
        private readonly PatchManager _patchManager;

        public AutoPatcher(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _patchManager = new PatchManager(_repositories);
        }

        /// <summary>
        /// Do a rollback after calling this method to prevent saving the new patch.
        /// Tries to produce a new patch by tying together existing patches,
        /// trying to match PatchInlet and PatchOutlet operators by:
        /// 1) Inlet.Dimension.Name and Dimension.Name
        /// 2) PatchInlet Operator.Name and PatchOutlet Operator.Name.
        /// The non-matched inlets and outlets will become inlets and outlets of the new patch.
        /// If there is overlap in type or name, they will merge to a single inlet or outlet.
        /// This causes ambiguity in DefaultValue, Position or Name, 
        /// which is 'resolved' by taking the properties of the first one in the group.
        /// </summary>
        public Patch AutoPatch(IList<Patch> sourceUnderlyingPatches)
        {
            if (sourceUnderlyingPatches == null) throw new NullException(() => sourceUnderlyingPatches);

            Patch patch = _patchManager.CreatePatch();

            var operatorFactory = new OperatorFactory(patch, _repositories);

            patch.Name = "Auto-Generated Patch";

            var intermediateCustomOperators = new List<Operator>(sourceUnderlyingPatches.Count);

            foreach (Patch sourceUnderlyingPatch in sourceUnderlyingPatches)
            {
                OperatorWrapper_WithUnderlyingPatch intermediateCustomOperatorWrapper = operatorFactory.CustomOperator(sourceUnderlyingPatch);
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

            // Unmatched inlets of the custom operators become inlets of the new patch.
            IList<Inlet> intermediateUnmatchedInlets = intermediateCustomOperators.SelectMany(x => x.Inlets)
                                                                                  .Except(intermediateMatchedInlets)
                                                                                  .ToArray();

            // If there is overlap in Inlet Dimension, they will merge to a single PatchInlet.
            var intermediateUnmatchedInlets_GroupedByDimension = intermediateUnmatchedInlets.Where(x => x.Dimension != null)
                                                                                            .GroupBy(x => x.GetDimensionEnum());
            foreach (var intermediateUnmatchedInletGroup in intermediateUnmatchedInlets_GroupedByDimension)
            {
                ConvertToPatchInlet(intermediateUnmatchedInletGroup.ToArray());
            }

            // If there is overlap in Inlet Name, they will merge to a single PatchInlet.
            var intermediateUnmatchedInlets_WithoutDimension_GroupedByName = intermediateUnmatchedInlets.Where(x => x.Dimension == null && !string.IsNullOrEmpty(x.Name))
                                                                                                        .GroupBy(x => x.Name);
            foreach (var intermediateUnmatchedInletGroup in intermediateUnmatchedInlets_WithoutDimension_GroupedByName)
            {
                ConvertToPatchInlet(intermediateUnmatchedInletGroup.ToArray());
            }

            // If there is no Inlet Dimension or name, unmatched Inlets will convert to individual PatchInlets.
            var intermediateUnmatchedInlets_WithoutDimensionOrName = intermediateUnmatchedInlets.Where(x => x.Dimension == null && string.IsNullOrEmpty(x.Name));
            foreach (Inlet unmatchedInlet in intermediateUnmatchedInlets_WithoutDimensionOrName)
            {
                ConvertToPatchInlet(unmatchedInlet);
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
                ConvertToPatchOutlet(intermediateUnmatchedOutletGroup.ToArray());
            }

            // If there is overlap in name, they will merge to a single PatchOutlet.
            var intermediateUnmatchedOutlets_WithoutDimension_GroupedByName = intermediateUnmatchedOutlets.Where(x => x.Dimension == null && !string.IsNullOrEmpty(x.Name))
                                                                                                          .GroupBy(x => x.Name);
            foreach (var intermediateUnmatchedOutletGroup in intermediateUnmatchedOutlets_WithoutDimension_GroupedByName)
            {
                ConvertToPatchOutlet(intermediateUnmatchedOutletGroup.ToArray());
            }

            // If there is no Dimension or name, unmatched Outlets will convert to individual PatchOutlets.
            var intermediateUnmatchedOutlets_WithoutDimensionOrName = intermediateUnmatchedOutlets.Where(x => x.Dimension == null && string.IsNullOrEmpty(x.Name));
            foreach (Outlet intermediateUnmatchedOutlet in intermediateUnmatchedOutlets_WithoutDimensionOrName)
            {
                ConvertToPatchOutlet(intermediateUnmatchedOutlet);
            }

            // This is sensitive, error prone code, so verify its result with the validators.
            IResult result = _patchManager.SavePatch(patch);
            result.Assert();

            return patch;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
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

        private PatchInlet_OperatorWrapper ConvertToPatchInlet(Inlet intermediateInlet)
        {
            var operatorFactory = new OperatorFactory(intermediateInlet.Operator.Patch, _repositories);

            PatchInlet_OperatorWrapper destPatchInletWrapper = operatorFactory.PatchInlet();
            intermediateInlet.LinkTo(destPatchInletWrapper.Outlet);

            Inlet destInlet = destPatchInletWrapper.Inlet;
            destInlet.Name = intermediateInlet.Name;
            destInlet.Dimension = intermediateInlet.Dimension;
            destInlet.Position = intermediateInlet.Position;
            destInlet.DefaultValue = intermediateInlet.DefaultValue;
            destInlet.IsRepeating = intermediateInlet.IsRepeating;
            destInlet.RepetitionPosition = intermediateInlet.RepetitionPosition;

            return destPatchInletWrapper;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(IList<Outlet> intermediateUnmatchedOutlets)
        {
            Outlet intermediateFirstUnmatchedOutlet = intermediateUnmatchedOutlets.First();
            PatchOutlet_OperatorWrapper destPatchOutletWrapper = ConvertToPatchOutlet(intermediateFirstUnmatchedOutlet);

            Outlet intermediatePatchOutletInput = intermediateFirstUnmatchedOutlet;
            if (intermediateUnmatchedOutlets.Count > 1)
            {
                var operatorFactory = new OperatorFactory(intermediateFirstUnmatchedOutlet.Operator.Patch, _repositories);

                // You only need an adder, when there is more than 1 unmatchedOutlet.
                intermediatePatchOutletInput = operatorFactory.Add(intermediateUnmatchedOutlets);
            }

            destPatchOutletWrapper.Input = intermediatePatchOutletInput;

            return destPatchOutletWrapper;
        }

        private PatchOutlet_OperatorWrapper ConvertToPatchOutlet(Outlet intermediateOutlet)
        {
            var operatorFactory = new OperatorFactory(intermediateOutlet.Operator.Patch, _repositories);

            PatchOutlet_OperatorWrapper destPatchOutletWrapper = operatorFactory.PatchOutlet();
            destPatchOutletWrapper.Input = intermediateOutlet;

            Outlet destOutlet = destPatchOutletWrapper.Outlet;
            destOutlet.Name = intermediateOutlet.Name;
            destOutlet.Position = intermediateOutlet.Position;
            destOutlet.Dimension = intermediateOutlet.Dimension;
            destOutlet.IsRepeating = intermediateOutlet.IsRepeating;
            destOutlet.RepetitionPosition = intermediateOutlet.RepetitionPosition;

            return destPatchOutletWrapper;
        }

        /// <summary> Will return null if no Frequency inlet or Sound outlet is found. </summary>
        public Outlet TryAutoPatchWithTone(Tone tone, IList<Patch> sourceUnderlyingPatches)
        {
            if (tone == null) throw new NullException(() => tone);
            if (sourceUnderlyingPatches == null) throw new NullException(() => sourceUnderlyingPatches);

            // Create a new patch out of the other patches.
            Patch autoPatch = AutoPatch(sourceUnderlyingPatches);

            Patch patch = _patchManager.CreatePatch();

            var operatorFactory = new OperatorFactory(patch, _repositories);

            var customOperator = operatorFactory.CustomOperator(autoPatch);
            var frequency = operatorFactory.Number(tone.GetFrequency());

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
                    var add = operatorFactory.Add(soundOutlets);
                    return add;
            }
        }

        /// <summary>
        /// Creatively tries to make the best of getting sound out of the source Patch.
        /// Tries to find outlets to combine into sound.
        /// If selectedOperatorID is provided, only outlets of the selected Operator are considered.
        /// If selectedOperatorID is not provided, all outlets of the patch are considered.
        /// Outlets of type sound are preferred,
        /// but if none are found, all outlets are considered.
        /// 
        /// If no suitable outlets were found, a result with Successful = false is returned.
        /// Also the outlet that returns the sound is returned through the result.
        /// </summary>
        public Result<Outlet> AutoPatch_TryCombineSounds(Patch sourcePatch, int? selectedOperatorID = null)
        {
            if (selectedOperatorID.HasValue)
            {
                // If an (internal) operator is selected, generate some patch outlets in the patch, before turning it into a custom operator.
                var sourceOperatorFactory = new OperatorFactory(sourcePatch, _repositories);

                foreach (Outlet soundOutlet in GetSoundOutletsFromOperatorCreatively(selectedOperatorID.Value))
                {
                    sourceOperatorFactory.PatchOutlet(DimensionEnum.Sound, soundOutlet);
                }
            }

            Patch destPatch = _patchManager.CreatePatch();
            destPatch.Name = "Auto-Generated Patch";

            var operatorFactory = new OperatorFactory(destPatch, _repositories);

            OperatorWrapper_WithUnderlyingPatch customOperator = operatorFactory.CustomOperator(sourcePatch);
            IList<Outlet> soundOutlets = GetSoundOutletsFromOperatorCreatively(customOperator);

            if (soundOutlets.Count == 0)
            {
                return new Result<Outlet>
                {
                    Successful = false,
                    Messages = new[] { ResourceFormatter.PatchHasNoOutlets }
                };
            }
            else
            {
                Outlet add = operatorFactory.Add(soundOutlets);
                Outlet patchOutlet = operatorFactory.PatchOutlet(DimensionEnum.Sound, add);

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
                    Messages = new[] { ResourceFormatter.NoSoundFound }
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
                    Messages = new[] { ResourceFormatter.NoSoundFound }
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
            IList<Patch> patchesInGroup = PatchGrouper.GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(document.Patches, groupName, mustIncludeHidden);
            IList<Outlet> soundOutlets = GetSoundOutletsFromPatchesWithoutSoundInlets(patchesInGroup, mustIncludeHidden);

            Outlet soundOutlet = Randomizer.TryGetRandomItem(soundOutlets);

            if (soundOutlet == null)
            {
                return new Result<Outlet>
                {
                    Successful = false,
                    Messages = new[] { ResourceFormatter.NoSoundFound }
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
                                                x => !x.EnumerateOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                                       .Select(y => new PatchInlet_OperatorWrapper(y))
                                                       .Where(y => y.Inlet.GetDimensionEnum() == DimensionEnum.Sound)
                                                       .Any())
                                            .OrderBy(x => x.Name)
                                            .SelectMany(
                                                x => x.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                                      .Select(y => new PatchOutlet_OperatorWrapper(y)))
                                            .Select(x => x.Outlet)
                                            .Where(x => x.GetDimensionEnum() == DimensionEnum.Sound)
                                            .ToArray();
            return patches2;
        }

        public void CreateNumbersForEmptyInletsWithDefaultValues(
            Operator op,
            float estimatedOperatorWidth,
            float operatorHeight,
            EntityPositionManager entityPositionManager)
        {
            if (op == null) throw new NullException(() => op);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            OperatorFactory operatorFactory = new OperatorFactory(op.Patch, _repositories);

            EntityPosition entityPosition = entityPositionManager.GetOrCreateOperatorPosition(op.ID);

            int inletCount = op.Inlets.Count;
            float spacingX = operatorHeight / 2f;
            float spacingY = operatorHeight;
            float fullWidth = estimatedOperatorWidth * inletCount + spacingX * (inletCount - 1);
            float left = entityPosition.X - fullWidth / 2f;
            float x = left + estimatedOperatorWidth / 2f; // Coordinates are the centers.
            float y = entityPosition.Y - operatorHeight - spacingY;
            float stepX = estimatedOperatorWidth + spacingX;

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet == null)
                {
                    if (inlet.DefaultValue.HasValue)
                    {
                        var number = operatorFactory.Number(inlet.DefaultValue.Value);

                        inlet.LinkTo(number.NumberOutlet);

                        EntityPosition numberEntityPosition = entityPositionManager.GetOrCreateOperatorPosition(number.WrappedOperator.ID);
                        numberEntityPosition.X = x;
                        numberEntityPosition.Y = y;
                    }
                }

                x += stepX;
            }
        }

        public void SubstituteSineForUnfilledInSoundPatchInlets([NotNull] Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            OperatorFactory operatorFactory = new OperatorFactory(patch, _repositories);

            IList<PatchInlet_OperatorWrapper> patchInletWrappers =
                patch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                     .Select(x => new PatchInlet_OperatorWrapper(x))
                     .Where(
                         x => x.Inlet.GetDimensionEnum() == DimensionEnum.Sound &&
                              !x.Inlet.DefaultValue.HasValue &&
                              x.Input == null)
                     .ToArray();

            // ReSharper disable once InvertIf
            if (patchInletWrappers.Count != 0)
            {
                Outlet sineOutlet = operatorFactory.Sine(operatorFactory.Number(440));

                foreach (PatchInlet_OperatorWrapper patchInletWrapper in patchInletWrappers)
                {
                    patchInletWrapper.Input = sineOutlet;
                }
            }
        }

    }
}

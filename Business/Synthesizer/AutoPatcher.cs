using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Mathematics;
// ReSharper disable SuggestVarOrType_Elsewhere

namespace JJ.Business.Synthesizer
{
	public class AutoPatcher
	{
		private readonly RepositoryWrapper _repositories;
		private readonly PatchFacade _patchFacade;

		public AutoPatcher(RepositoryWrapper repositories)
		{
			_repositories = repositories ?? throw new NullException(() => repositories);
			_patchFacade = new PatchFacade(_repositories);
		}

		public Patch AutoPatch(params Patch[] sourceUnderlyingPatches) => AutoPatch((IList<Patch>)sourceUnderlyingPatches);

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

			Patch patch = _patchFacade.CreatePatch();

			var operatorFactory = new OperatorFactory(patch, _repositories);

			patch.Name = "Auto-Generated Patch";

			var intermediateDerivedOperators = new List<Operator>(sourceUnderlyingPatches.Count);

			foreach (Patch sourceUnderlyingPatch in sourceUnderlyingPatches)
			{
				OperatorWrapper intermediateDerivedOperatorWrapper = operatorFactory.New(sourceUnderlyingPatch);
				intermediateDerivedOperatorWrapper.WrappedOperator.Name = $"{sourceUnderlyingPatch.Name}";

				intermediateDerivedOperators.Add(intermediateDerivedOperatorWrapper);
			}

			var intermediateMatchedOutlets = new List<Outlet>();
			var intermediateMatchedInlets = new List<Inlet>();

			for (int i = 0; i < intermediateDerivedOperators.Count; i++)
			{
				for (int j = i + 1; j < intermediateDerivedOperators.Count; j++)
				{
					Operator intermediateDerivedOperator1 = intermediateDerivedOperators[i];
					Operator intermediateDerivedOperator2 = intermediateDerivedOperators[j];

					foreach (Outlet intermediateOutlet in intermediateDerivedOperator1.Outlets)
					{
						foreach (Inlet intermediateInlet in intermediateDerivedOperator2.Inlets)
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
			IList<Inlet> intermediateUnmatchedInlets = intermediateDerivedOperators.SelectMany(x => x.Inlets)
																				   .Except(intermediateMatchedInlets)
																				   .ToArray();

			// If there is overlap in Inlet Dimension, they will merge to a single PatchInlet.
			var intermediateUnmatchedInlets_GroupedByDimension =
				intermediateUnmatchedInlets.Where(x => x.GetDimensionWithFallback() != null)
										   .GroupBy(x => x.GetDimensionEnumWithFallback());

			foreach (var intermediateUnmatchedInletGroup in intermediateUnmatchedInlets_GroupedByDimension)
			{
				ConvertToPatchInlet(intermediateUnmatchedInletGroup.ToArray());
			}

			// If there is overlap in Inlet Name, they will merge to a single PatchInlet.
			var intermediateUnmatchedInlets_WithoutDimension_GroupedByName =
				intermediateUnmatchedInlets.Where(x => x.GetDimensionWithFallback() == null && NameHelper.IsFilledIn(x.GetNameWithFallback()))
										   .GroupBy(x => x.GetNameWithFallback());

			foreach (var intermediateUnmatchedInletGroup in intermediateUnmatchedInlets_WithoutDimension_GroupedByName)
			{
				ConvertToPatchInlet(intermediateUnmatchedInletGroup.ToArray());
			}

			// If there is no Inlet Dimension or name, unmatched Inlets will convert to individual PatchInlets.
			var intermediateUnmatchedInlets_WithoutDimensionOrName =
				intermediateUnmatchedInlets.Where(x => x.GetDimensionWithFallback() == null && !NameHelper.IsFilledIn(x.GetNameWithFallback()));

			foreach (Inlet unmatchedInlet in intermediateUnmatchedInlets_WithoutDimensionOrName)
			{
				ConvertToPatchInlet(unmatchedInlet);
			}

			// Unmatched outlets of the custom operators become outlets of the new patch.
			IList<Outlet> intermediateUnmatchedOutlets = intermediateDerivedOperators.SelectMany(x => x.Outlets)
																					 .Except(intermediateMatchedOutlets)
																					 .ToArray();

			// If there is overlap in Dimension, they will merge to a single PatchOutlet.
			var intermediateUnmatchedOutlets_GroupedByDimension =
				intermediateUnmatchedOutlets.Where(x => x.GetDimensionWithFallback() != null)
											.GroupBy(x => x.GetDimensionEnumWithFallback());

			foreach (var intermediateUnmatchedOutletGroup in intermediateUnmatchedOutlets_GroupedByDimension)
			{
				ConvertToPatchOutlet(intermediateUnmatchedOutletGroup.ToArray());
			}

			// If there is overlap in name, they will merge to a single PatchOutlet.
			var intermediateUnmatchedOutlets_WithoutDimension_GroupedByName =
				intermediateUnmatchedOutlets.Where(x => x.GetDimensionWithFallback() == null && NameHelper.IsFilledIn(x.GetNameWithFallback()))
											.GroupBy(x => x.GetNameWithFallback());

			foreach (var intermediateUnmatchedOutletGroup in intermediateUnmatchedOutlets_WithoutDimension_GroupedByName)
			{
				ConvertToPatchOutlet(intermediateUnmatchedOutletGroup.ToArray());
			}

			// If there is no Dimension or name, unmatched Outlets will convert to individual PatchOutlets.
			var intermediateUnmatchedOutlets_WithoutDimensionOrName =
				intermediateUnmatchedOutlets.Where(x => x.GetDimensionWithFallback() == null && !NameHelper.IsFilledIn(x.GetNameWithFallback()));

			foreach (Outlet intermediateUnmatchedOutlet in intermediateUnmatchedOutlets_WithoutDimensionOrName)
			{
				ConvertToPatchOutlet(intermediateUnmatchedOutlet);
			}

			// This is sensitive, error prone code, so verify its result with the validators.
			IResult result = _patchFacade.SavePatch(patch);
			result.Assert();

			return patch;
		}

		// ReSharper disable once UnusedMethodReturnValue.Local
		private PatchInletOrOutlet_OperatorWrapper ConvertToPatchInlet(IList<Inlet> intermediateUnmatchedInlets)
		{
			Inlet intermediateFirstUnmatchedInlet = intermediateUnmatchedInlets.First();

			PatchInletOrOutlet_OperatorWrapper destPatchInletWrapper = ConvertToPatchInlet(intermediateFirstUnmatchedInlet);

			foreach (Inlet intermediateUnmatchedInlet in intermediateUnmatchedInlets)
			{
				intermediateUnmatchedInlet.LinkTo((Outlet)destPatchInletWrapper);
			}

			return destPatchInletWrapper;
		}

		private PatchInletOrOutlet_OperatorWrapper ConvertToPatchInlet(Inlet intermediateInlet)
		{
			var operatorFactory = new OperatorFactory(intermediateInlet.Operator.Patch, _repositories);

			PatchInletOrOutlet_OperatorWrapper destPatchInletWrapper = operatorFactory.PatchInlet();
			intermediateInlet.LinkTo(destPatchInletWrapper.Outlet);

			Inlet destInlet = destPatchInletWrapper.Inlet;

			InletOutletCloner.Clone(intermediateInlet, destInlet);

			destInlet.RepetitionPosition = null;

			return destPatchInletWrapper;
		}

		// ReSharper disable once UnusedMethodReturnValue.Local
		private PatchInletOrOutlet_OperatorWrapper ConvertToPatchOutlet(IList<Outlet> intermediateUnmatchedOutlets)
		{
			Outlet intermediateFirstUnmatchedOutlet = intermediateUnmatchedOutlets.First();
			PatchInletOrOutlet_OperatorWrapper destPatchOutletWrapper = ConvertToPatchOutlet(intermediateFirstUnmatchedOutlet);

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

		private PatchInletOrOutlet_OperatorWrapper ConvertToPatchOutlet(Outlet intermediateOutlet)
		{
			var operatorFactory = new OperatorFactory(intermediateOutlet.Operator.Patch, _repositories);

			PatchInletOrOutlet_OperatorWrapper destPatchOutletWrapper = operatorFactory.PatchOutlet();
			destPatchOutletWrapper.Input = intermediateOutlet;

			Outlet destOutlet = destPatchOutletWrapper.Outlet;

			InletOutletCloner.Clone(intermediateOutlet, destOutlet);

			destOutlet.RepetitionPosition = null;

			return destPatchOutletWrapper;
		}

		/// <summary> Will return null if no Frequency inlet or Sound outlet is found. </summary>
		public Outlet TryAutoPatchWithTone(Tone tone, IList<Patch> sourceUnderlyingPatches)
		{
			if (tone == null) throw new NullException(() => tone);
			if (sourceUnderlyingPatches == null) throw new NullException(() => sourceUnderlyingPatches);

			// Create a new patch out of the other patches.
			Patch autoPatch = AutoPatch(sourceUnderlyingPatches);

			Patch patch = _patchFacade.CreatePatch();

			var operatorFactory = new OperatorFactory(patch, _repositories);

			OperatorWrapper derivedOperator = operatorFactory.New(autoPatch);
			Outlet frequencyOutlet = operatorFactory.Number(tone.GetCalculatedFrequency());

			IList<Inlet> derivedOperatorFrequencyInlets = derivedOperator.Inlets.GetMany(DimensionEnum.Frequency);
			if (derivedOperatorFrequencyInlets.Count == 0)
			{
				return null;
			}

			foreach (Inlet derivedOperatorFrequencyInlet in derivedOperatorFrequencyInlets)
			{
				derivedOperatorFrequencyInlet.LinkTo(frequencyOutlet);
			}

			IList<Outlet> soundOutlets = derivedOperator.Outlets.GetMany(DimensionEnum.Sound);
			switch (soundOutlets.Count)
			{
				case 0:
					return null;

				case 1:
					return soundOutlets[0];

				default:
					OperatorWrapper add = operatorFactory.Add(soundOutlets);
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

			Patch destPatch = _patchFacade.CreatePatch();
			destPatch.Name = "Auto-Generated Patch";

			var operatorFactory = new OperatorFactory(destPatch, _repositories);

			OperatorWrapper derivedOperator = operatorFactory.New(sourcePatch);
			IList<Outlet> soundOutlets = GetSoundOutletsFromOperatorCreatively(derivedOperator);

			if (soundOutlets.Count == 0)
			{
				return new Result<Outlet>(ResourceFormatter.PatchHasNoOutlets);
			}

			Outlet add = operatorFactory.Add(soundOutlets);
			Outlet patchOutlet = operatorFactory.PatchOutlet(DimensionEnum.Sound, add);

			return new Result<Outlet>
			{
				Successful = true,
				Data = patchOutlet
			};
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
																 .Where(x => x.GetDimensionEnumWithFallback() == DimensionEnum.Sound)
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

		public Result<Outlet> TryAutoPatchFromDocumentsRandomly(IList<Document> documents, bool mustIncludeHidden)
		{
			if (documents == null) throw new NullException(() => documents);

			IList<Patch> patches = documents.SelectMany(x => x.Patches).ToArray();
			IList<Outlet> soundOutlets = GetSoundOutletsFromPatchesWithoutSoundInlets(patches, mustIncludeHidden);

			Outlet soundOutlet = Randomizer.TryGetRandomItem(soundOutlets);

			if (soundOutlet == null)
			{
				return new Result<Outlet>(ResourceFormatter.NoSoundFound);
			}

			return new Result<Outlet>
			{
				Successful = true,
				Data = soundOutlet
			};
		}

		/// <summary> Can be used to for instance quickly generate an example sound from a document used as library. </summary>
		public Result<Outlet> TryAutoPatchFromDocumentRandomly(Document document, bool mustIncludeHidden)
		{
			IList<Outlet> soundOutlets = GetSoundOutletsFromPatchesWithoutSoundInlets(document.Patches, mustIncludeHidden);

			// TODO: Select the first patch with a sound inlet and use autopatch those two together.

			Outlet soundOutlet = Randomizer.TryGetRandomItem(soundOutlets);

			if (soundOutlet == null)
			{
				return new Result<Outlet>(ResourceFormatter.NoSoundFound);
			}

			return new Result<Outlet>
			{
				Successful = true,
				Data = soundOutlet
			};
		}

		/// <summary> Can be used to for instance quickly generate an example sound from a patch group. </summary>
		public Result<Outlet> TryAutoPatchFromPatchGroupRandomly(Document document, string groupName, bool mustIncludeHidden)
		{
			IList<Patch> patchesInGroup = PatchGrouper.GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(document.Patches, groupName, mustIncludeHidden);
			IList<Outlet> soundOutlets = GetSoundOutletsFromPatchesWithoutSoundInlets(patchesInGroup, mustIncludeHidden);

			Outlet soundOutlet = Randomizer.TryGetRandomItem(soundOutlets);

			if (soundOutlet == null)
			{
				return new Result<Outlet>(ResourceFormatter.NoSoundFound);
			}

			return new Result<Outlet>
			{
				Successful = true,
				Data = soundOutlet
			};
		}

		// ReSharper disable once ReturnTypeCanBeEnumerable.Local
		private IList<Outlet> GetSoundOutletsFromPatchesWithoutSoundInlets(IList<Patch> patches, bool mustIncludeHidden)
		{
			IList<Outlet> patches2 = patches.Where(x => !x.Hidden || mustIncludeHidden)
											.Where(
				                                // ReSharper disable once SimplifyLinqExpression
				                                x => !x.EnumerateOperatorsOfType(OperatorTypeEnum.PatchInlet)
													   .Select(y => new PatchInletOrOutlet_OperatorWrapper(y))
													   .Any(y => y.Inlet.GetDimensionEnumWithFallback() == DimensionEnum.Sound))
											.OrderBy(x => x.Name)
											.SelectMany(
												x => x.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
													  .Select(y => new PatchInletOrOutlet_OperatorWrapper(y)))
											.Select(x => x.Outlet)
											.Where(x => x.GetDimensionEnumWithFallback() == DimensionEnum.Sound)
											.ToArray();
			return patches2;
		}

		public IList<Operator> CreateNumbersForEmptyInletsWithDefaultValues(
			Operator op,
			float estimatedOperatorWidth,
			float operatorHeight)
		{
			if (op == null) throw new NullException(() => op);

			var list = new List<Operator>();

			var operatorFactory = new OperatorFactory(op.Patch, _repositories);

			EntityPosition entityPosition = op.EntityPosition;

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
						Number_OperatorWrapper number = operatorFactory.Number(inlet.DefaultValue.Value);
						Outlet numberOutlet = number.Outlets[DimensionEnum.Number];
						inlet.LinkTo(numberOutlet);

						EntityPosition numberEntityPosition = number.WrappedOperator.EntityPosition;
						numberEntityPosition.X = x;
						numberEntityPosition.Y = y;

						list.Add(numberOutlet.Operator);
					}
				}

				x += stepX;
			}

			return list;
		}

		public void SubstituteSineForUnfilledInSoundPatchInlets(Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);

			var operatorFactory = new OperatorFactory(patch, _repositories);

			IList<PatchInletOrOutlet_OperatorWrapper> patchInletWrappers =
				patch.EnumerateOperatorsOfType(OperatorTypeEnum.PatchOutlet)
					 .Select(x => new PatchInletOrOutlet_OperatorWrapper(x))
					 .Where(
						 x => x.Inlet.GetDimensionEnumWithFallback() == DimensionEnum.Sound &&
							  !x.Inlet.DefaultValue.HasValue &&
							  x.Input == null)
					 .ToArray();

			// ReSharper disable once InvertIf
			if (patchInletWrappers.Count != 0)
			{
				Outlet sineOutlet = operatorFactory.Sine(operatorFactory.Number(440));

				foreach (PatchInletOrOutlet_OperatorWrapper patchInletWrapper in patchInletWrappers)
				{
					patchInletWrapper.Input = sineOutlet;
				}
			}
		}

	}
}

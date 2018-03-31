using System;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class MidiMappingGroupDetailsPresenter : EntityPresenterWithSaveBase<MidiMappingGroup, MidiMappingGroupDetailsViewModel>
	{
		private readonly MidiMappingElementRepositories _repositories;
		private readonly MidiMappingElementFacade _midiMappingFacade;

		public MidiMappingGroupDetailsPresenter(MidiMappingElementRepositories repositories, MidiMappingElementFacade midiMappingFacade)
		{
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
			_midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
		}

		protected override MidiMappingGroup GetEntity(MidiMappingGroupDetailsViewModel userInput)
		{
			return _repositories.MidiMappingGroupRepository.Get(userInput.MidiMappingGroup.ID);
		}

		protected override MidiMappingGroupDetailsViewModel ToViewModel(MidiMappingGroup entity)
		{
			return entity.ToDetailsViewModel();
		}

		protected override IResult Save(MidiMappingGroup entity, MidiMappingGroupDetailsViewModel userInput)
		{
			return _midiMappingFacade.SaveMidiMappingGroup(entity);
		}

		public MidiMappingGroupDetailsViewModel CreateElement(MidiMappingGroupDetailsViewModel userInput)
		{
			MidiMappingElement newMidiMappingElement = null;

			return ExecuteAction(
				userInput,
				entity => { newMidiMappingElement = _midiMappingFacade.CreateMidiMappingElementWithDefaults(entity); },
				viewModel => viewModel.CreatedElementID = newMidiMappingElement.ID);
		}

		/// <summary>
		/// NOTE: Has view model validation, which the base class's template method does not support.
		/// Deletes the selected element.
		/// Produces a validation message if no element is selected.
		/// </summary>
		public MidiMappingGroupDetailsViewModel DeleteSelectedElement(MidiMappingGroupDetailsViewModel userInput)
		{
			if (userInput == null) throw new ArgumentNullException(nameof(userInput));

			// RefreshCounter
			userInput.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			userInput.Successful = false;

			// ViewModel Validation
			bool selectedElementIsEmpty = (userInput.SelectedElement?.ID ?? 0) == 0;
			if (selectedElementIsEmpty)
			{
				// Non-Persisted
				userInput.ValidationMessages.Add(ResourceFormatter.SelectAnElementFirst);

				return userInput;
			}

			// GetEntities
			MidiMappingGroup midiMapping = _repositories.MidiMappingGroupRepository.Get(userInput.MidiMappingGroup.ID);

			// Business
			// ReSharper disable once PossibleNullReferenceException
			_midiMappingFacade.DeleteMidiMappingElement(userInput.SelectedElement.ID);

			// ToViewModel
			MidiMappingGroupDetailsViewModel viewModel = ToViewModel(midiMapping);

			// Non-Persisted
			CopyNonPersistedProperties(userInput, viewModel);
			viewModel.SelectedElement = null;

			// Successful
			viewModel.Successful = true;

			return viewModel;
		}

		public MidiMappingGroupDetailsViewModel MoveElement(
			MidiMappingGroupDetailsViewModel userInput,
			int midiMappingElementID,
			float centerX,
			float centerY)
		{
			return ExecuteAction(
				userInput,
				x =>
				{
					// GetEntity
					MidiMappingElement midiMappingElement = _repositories.MidiMappingElementRepository.Get(midiMappingElementID);

					// Business
					midiMappingElement.EntityPosition.X = centerX;
					midiMappingElement.EntityPosition.Y = centerY;
				});
		}

		public void SelectElement(MidiMappingGroupDetailsViewModel viewModel, int operatorID)
		{
			ExecuteNonPersistedAction(viewModel, () => SetSelectedElement(viewModel, operatorID));
		}

		// Helpers

		private void SetSelectedElement(MidiMappingGroupDetailsViewModel viewModel, int operatorID)
		{
			MidiMappingElementItemViewModel previousSelectedElementViewModel = viewModel.SelectedElement;
			if (previousSelectedElementViewModel != null)
			{
				previousSelectedElementViewModel.IsSelected = false;
			}

			if (viewModel.Elements.TryGetValue(operatorID, out MidiMappingElementItemViewModel selectedElementViewModel))
			{
				selectedElementViewModel.IsSelected = true;
			}

			viewModel.SelectedElement = selectedElementViewModel;
		}

		public override void CopyNonPersistedProperties(MidiMappingGroupDetailsViewModel sourceViewModel, MidiMappingGroupDetailsViewModel destViewModel)
		{
			base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

			SetSelectedElement(destViewModel, sourceViewModel.SelectedElement?.ID ?? 0);
		}
	}
}
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
	internal class MidiMappingDetailsPresenter : EntityPresenterWithSaveBase<MidiMapping, MidiMappingDetailsViewModel>
	{
		private readonly MidiMappingRepositories _repositories;
		private readonly MidiMappingFacade _midiMappingFacade;

		public MidiMappingDetailsPresenter(MidiMappingRepositories repositories, MidiMappingFacade midiMappingFacade)
		{
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
			_midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
		}

		protected override MidiMapping GetEntity(MidiMappingDetailsViewModel userInput)
		{
			return _repositories.MidiMappingRepository.Get(userInput.MidiMapping.ID);
		}

		protected override MidiMappingDetailsViewModel ToViewModel(MidiMapping entity)
		{
			return entity.ToDetailsViewModel();
		}

		protected override IResult Save(MidiMapping entity, MidiMappingDetailsViewModel userInput)
		{
			return _midiMappingFacade.SaveMidiMapping(entity);
		}

		public MidiMappingDetailsViewModel CreateElement(MidiMappingDetailsViewModel userInput)
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
		public MidiMappingDetailsViewModel DeleteSelectedElement(MidiMappingDetailsViewModel userInput)
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
			MidiMapping midiMapping = _repositories.MidiMappingRepository.Get(userInput.MidiMapping.ID);

			// Business
			// ReSharper disable once PossibleNullReferenceException
			_midiMappingFacade.DeleteMidiMappingElement(userInput.SelectedElement.ID);

			// ToViewModel
			MidiMappingDetailsViewModel viewModel = ToViewModel(midiMapping);

			// Non-Persisted
			CopyNonPersistedProperties(userInput, viewModel);
			viewModel.SelectedElement = null;

			// Successful
			viewModel.Successful = true;

			return viewModel;
		}

		public MidiMappingDetailsViewModel MoveElement(
			MidiMappingDetailsViewModel userInput,
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

		public void SelectElement(MidiMappingDetailsViewModel viewModel, int operatorID)
		{
			ExecuteNonPersistedAction(viewModel, () => SetSelectedElement(viewModel, operatorID));
		}

		// Helpers

		private void SetSelectedElement(MidiMappingDetailsViewModel viewModel, int operatorID)
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

		public override void CopyNonPersistedProperties(MidiMappingDetailsViewModel sourceViewModel, MidiMappingDetailsViewModel destViewModel)
		{
			base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

			SetSelectedElement(destViewModel, sourceViewModel.SelectedElement?.ID ?? 0);
		}
	}
}
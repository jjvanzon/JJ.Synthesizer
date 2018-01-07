using System;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
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
		private readonly IMidiMappingRepository _midiMappingRepository;
		private readonly EntityPositionFacade _entityPositionFacade;
		private readonly MidiMappingFacade _midiMappingFacade;

		public MidiMappingDetailsPresenter(IMidiMappingRepository midiMappingRepository, EntityPositionFacade entityPositionFacade, MidiMappingFacade midiMappingFacade)
		{
			_midiMappingRepository = midiMappingRepository ?? throw new ArgumentNullException(nameof(midiMappingRepository));
			_entityPositionFacade = entityPositionFacade ?? throw new ArgumentNullException(nameof(entityPositionFacade));
			_midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
		}

		protected override MidiMapping GetEntity(MidiMappingDetailsViewModel userInput)
		{
			return _midiMappingRepository.Get(userInput.MidiMapping.ID);
		}

		protected override MidiMappingDetailsViewModel ToViewModel(MidiMapping entity)
		{
			return entity.ToDetailsViewModel(_entityPositionFacade);
		}

		protected override IResult Save(MidiMapping entity, MidiMappingDetailsViewModel userInput)
		{
			return _midiMappingFacade.SaveMidiMapping(entity);
		}

		public void SelectElement(MidiMappingDetailsViewModel viewModel, int operatorID)
		{
			ExecuteNonPersistedAction(viewModel, () => SetSelectedElement(viewModel, operatorID));
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
			else
			{
				// GetEntities
				MidiMapping midiMapping = _midiMappingRepository.Get(userInput.MidiMapping.ID);

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
		}

		public MidiMappingDetailsViewModel CreateElement(MidiMappingDetailsViewModel userInput)
		{
			MidiMappingElement newMidiMappingElement = null;

			return ExecuteAction(
				userInput,
				entity =>
				{
					newMidiMappingElement = _midiMappingFacade.CreateMidiMappingElementWithDefaults(entity);
				},
				viewModel => viewModel.CreatedElementID = newMidiMappingElement.ID);
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

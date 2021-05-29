using System;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.StringResources;
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
        private readonly MidiMappingRepositories _repositories;
        private readonly MidiMappingFacade _midiMappingFacade;

        public MidiMappingGroupDetailsPresenter(MidiMappingRepositories repositories, MidiMappingFacade midiMappingFacade)
        {
            _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
            _midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
        }

        protected override MidiMappingGroup GetEntity(MidiMappingGroupDetailsViewModel userInput) => _repositories.MidiMappingGroupRepository.Get(userInput.MidiMappingGroup.ID);

        protected override MidiMappingGroupDetailsViewModel ToViewModel(MidiMappingGroup entity) => entity.ToDetailsViewModel();

        protected override IResult Save(MidiMappingGroup entity, MidiMappingGroupDetailsViewModel userInput) => _midiMappingFacade.SaveMidiMappingGroup(entity);

        public MidiMappingGroupDetailsViewModel CreateMidiMapping(MidiMappingGroupDetailsViewModel userInput)
        {
            MidiMapping newMidiMapping = null;

            return ExecuteAction(
                userInput,
                entity => newMidiMapping = _midiMappingFacade.CreateMidiMappingWithDefaults(entity),
                viewModel => viewModel.CreatedMidiMappingID = newMidiMapping.ID);
        }

        /// <summary>
        /// NOTE: Has view model validation, which the base class's template method does not support.
        /// Deletes the selected MidiMapping.
        /// Produces a validation message if no MidiMapping is selected.
        /// </summary>
        public MidiMappingGroupDetailsViewModel DeleteSelectedMidiMapping(MidiMappingGroupDetailsViewModel userInput)
        {
            if (userInput == null) throw new ArgumentNullException(nameof(userInput));

            // RefreshCounter
            userInput.RefreshID = RefreshIDProvider.GetRefreshID();

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validation
            bool selectedMidiMappingIsEmpty = (userInput.SelectedMidiMapping?.ID ?? 0) == 0;
            if (selectedMidiMappingIsEmpty)
            {
                // Non-Persisted
                userInput.ValidationMessages.Add(ResourceFormatter.SelectAnElementFirst);

                return userInput;
            }

            // GetEntities
            MidiMappingGroup midiMapping = _repositories.MidiMappingGroupRepository.Get(userInput.MidiMappingGroup.ID);

            // Business
            // ReSharper disable once PossibleNullReferenceException
            _midiMappingFacade.DeleteMidiMapping(userInput.SelectedMidiMapping.ID);

            // ToViewModel
            MidiMappingGroupDetailsViewModel viewModel = ToViewModel(midiMapping);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.SelectedMidiMapping = null;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public MidiMappingGroupDetailsViewModel MoveMidiMapping(
            MidiMappingGroupDetailsViewModel userInput,
            int midiMappingID,
            float centerX,
            float centerY)
            => ExecuteAction(
                userInput,
                x =>
                {
                    // GetEntity
                    MidiMapping midiMapping = _repositories.MidiMappingRepository.Get(midiMappingID);

                    // Business
                    midiMapping.EntityPosition.X = centerX;
                    midiMapping.EntityPosition.Y = centerY;
                });

        public void SelectMidiMapping(MidiMappingGroupDetailsViewModel viewModel, int operatorID) => ExecuteNonPersistedAction(viewModel, () => SetSelectedMidiMapping(viewModel, operatorID));

        // Helpers

        private void SetSelectedMidiMapping(MidiMappingGroupDetailsViewModel viewModel, int operatorID)
        {
            MidiMappingItemViewModel previousSelectedMidiMappingViewModel = viewModel.SelectedMidiMapping;
            if (previousSelectedMidiMappingViewModel != null)
            {
                previousSelectedMidiMappingViewModel.IsSelected = false;
            }

            if (viewModel.MidiMappings.TryGetValue(operatorID, out MidiMappingItemViewModel selectedElementViewModel))
            {
                selectedElementViewModel.IsSelected = true;
            }

            viewModel.SelectedMidiMapping = selectedElementViewModel;
        }

        protected override void CopyNonPersistedProperties(MidiMappingGroupDetailsViewModel sourceViewModel, MidiMappingGroupDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            SetSelectedMidiMapping(destViewModel, sourceViewModel.SelectedMidiMapping?.ID ?? 0);
        }
    }
}
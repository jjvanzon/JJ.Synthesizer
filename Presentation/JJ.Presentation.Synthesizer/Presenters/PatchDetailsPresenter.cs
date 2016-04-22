using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Calculation.Patches;
using System;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Configuration;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchDetailsPresenter : PresenterBase<PatchDetailsViewModel>
    {
        private static double _patchPlayDuration = GetPatchPlayDuration();
        private static string _patchPlayOutputFilePath = GetPatchPlayOutputFilePath();

        private PatchRepositories _repositories;
        private EntityPositionManager _entityPositionManager;

        public PatchDetailsPresenter(PatchRepositories repositories, EntityPositionManager entityPositionManager)
        {
            if (repositories == null) throw new NullException(() => repositories);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            _repositories = repositories;
            _entityPositionManager = entityPositionManager;
        }

        public PatchDetailsViewModel Show(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.PatchID);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchDetailsViewModel Refresh(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.PatchID);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchDetailsViewModel Close(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            PatchDetailsViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public PatchDetailsViewModel LoseFocus(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            PatchDetailsViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private PatchDetailsViewModel Update(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.PatchID);

            // Business
            var patchManager = new PatchManager(entity, _repositories);
            VoidResult result = patchManager.SavePatch();

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        public PatchDetailsViewModel MoveOperator(PatchDetailsViewModel userInput, int operatorID, float centerX, float centerY)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.Entity.PatchID);
            Operator op = _repositories.OperatorRepository.Get(operatorID);

            // Business
            _entityPositionManager.MoveOperator(op, centerX, centerY);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(patch);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchDetailsViewModel ChangeInputOutlet(PatchDetailsViewModel userInput, int inletID, int inputOutletID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            Patch patch = _repositories.PatchRepository.Get(userInput.Entity.PatchID);
            Inlet inlet = _repositories.InletRepository.Get(inletID);
            Outlet inputOutlet = _repositories.OutletRepository.Get(inputOutletID);

            // Business
            inlet.LinkTo(inputOutlet);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(patch);

            // Non-Persited
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchDetailsViewModel SelectOperator(PatchDetailsViewModel userInput, int operatorID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.PatchID);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            SetSelectedOperator(viewModel, operatorID);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        /// <summary>
        /// Deletes the selected operator.
        /// Produces a validation message if no operator is selected.
        /// </summary>
        public PatchDetailsViewModel DeleteOperator(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.PatchID);

            // ViewModel Validation
            if (userInput.SelectedOperator == null)
            {
                // Non-Persisted
                userInput.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectAnOperatorFirst
                });

                return userInput;
            }
            else
            {
                // Business
                var patchManager = new PatchManager(entity, _repositories);
                patchManager.DeleteOperator(userInput.SelectedOperator.ID);

                // ToViewModel
                PatchDetailsViewModel viewModel = CreateViewModel(entity);

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.SelectedOperator = null;

                // Successful
                viewModel.Successful = true;

                return viewModel;
            }
        }

        public PatchDetailsViewModel CreateOperator(PatchDetailsViewModel userInput, int operatorTypeID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.PatchID);

            // Business
            var patchManager = new PatchManager(entity, _repositories);
            Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        /// <summary>
        /// Writes the output of the currently selected operator to an audio file with a configurable duration.
        /// Returns the output file path if ViewModel.Successful.
        /// TODO: This action is too dependent on infrastructure, because the AudioFileOutput business logic is.
        /// Instead of writing to a file it had better write to a stream.
        /// </summary>
        public string Play(PatchDetailsViewModel userInput, RepositoryWrapper repositories)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (repositories == null) throw new NullException(() => repositories);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validation
            if (userInput.SelectedOperator == null)
            {
                // Non-Persisted
                userInput.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectAnOperatorFirst
                });

                return null;
            }

            Operator selectedOperator = _repositories.OperatorRepository.Get(userInput.SelectedOperator.ID);
            if (selectedOperator.Outlets.Count != 1)
            {
                // Non-Persisted
                userInput.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = PresentationMessages.SelectAnOperatorWithASingleOutlet
                });

                return null;
            }

            // GetEntity
            Outlet outlet = selectedOperator.Outlets.Single();

            // Business
            IPatchCalculator patchCalculator = CreatePatchCalculator(new PatchRepositories(repositories), outlet);

            AudioFileOutputManager audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
            AudioFileOutput audioFileOutput = audioFileOutputManager.CreateWithRelatedEntities();
            audioFileOutput.FilePath = _patchPlayOutputFilePath;
            audioFileOutput.Duration = _patchPlayDuration;
            audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;

            // Infrastructure
            audioFileOutputManager.WriteFile(audioFileOutput, patchCalculator);

            // Successful
            userInput.Successful = true;

            return _patchPlayOutputFilePath;
        }

        // Helpers

        /// <summary>
        /// The SelectedOperator is non-persisted data.
        /// This method sets the selected operator in the view model.
        /// It uses the Operator's ID for this.
        /// It goes through all the operators in the view model,
        /// setting IsSelected to false unless it is the selected operator,
        /// and sets the details view model's SelectedOperator property.
        /// </summary>
        private void SetSelectedOperator(PatchDetailsViewModel viewModel, int operatorID)
        {
            viewModel.SelectedOperator = null;

            foreach (OperatorViewModel operatorViewModel in viewModel.Entity.Operators)
            {
                if (operatorViewModel.ID == operatorID)
                {
                    operatorViewModel.IsSelected = true;
                    viewModel.SelectedOperator = operatorViewModel;
                }
                else
                {
                    operatorViewModel.IsSelected = false;
                }
            }
        }

        private IPatchCalculator CreatePatchCalculator(PatchRepositories repositories, Outlet outlet)
        {
            var patchManager = new PatchManager(outlet.Operator.Patch, repositories);
            return patchManager.CreateCalculator(outlet, new CalculatorCache());
        }

        private static double GetPatchPlayDuration()
        {
            return CustomConfigurationManager.GetSection<ConfigurationSection>().PatchPlayDurationInSeconds;
        }

        private static string GetPatchPlayOutputFilePath()
        {
            return CustomConfigurationManager.GetSection<ConfigurationSection>().PatchPlayHackedAudioFileOutputFilePath;
        }

        private PatchDetailsViewModel CreateViewModel(Patch patch)
        {
            return patch.ToDetailsViewModel(
                _repositories.OperatorTypeRepository,
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.PatchRepository,
                _entityPositionManager);
        }

        protected override void CopyNonPersistedProperties(PatchDetailsViewModel sourceViewModel, PatchDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            if (sourceViewModel.SelectedOperator != null)
            {
                SetSelectedOperator(destViewModel, sourceViewModel.SelectedOperator.ID);
            }
        }
    }
}

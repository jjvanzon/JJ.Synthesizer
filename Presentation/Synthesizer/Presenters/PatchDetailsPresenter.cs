using System.Linq;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Configuration;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchDetailsPresenter : PresenterBase<PatchDetailsViewModel>
    {
        private const int DEFAULT_ADD_INLET_COUNT = 2;
        private const int DEFAULT_AGGREGATE_OVER_INLETS_INLET_COUNT = 3;
        private const int DEFAULT_CLOSEST_OVER_INLETS_ITEM_COUNT = 3;
        private const int DEFAULT_RANGE_OVER_OUTLETS_OUTLET_COUNT = 16;
        private const int DEFAULT_MULTIPLY_INLET_COUNT = 2;
        private const int DEFAULT_SORT_OVER_INLETS_INLET_COUNT = 8;
        private const int DEFAULT_VARIABLE_INLET_OR_OUTLET_COUNT = 16;

        // TODO: These two constants do not belong here, because they should be determined by the vector graphics.
        private const float ESTIMATED_OPERATOR_WIDTH = 50f;
        private const float OPERATOR_HEIGHT = 30f;
        private static readonly double _patchPlayDuration = GetPatchPlayDuration();
        private static readonly string _patchPlayOutputFilePath = GetPatchPlayOutputFilePath();

        private readonly PatchRepositories _repositories;
        private readonly EntityPositionManager _entityPositionManager;

        public PatchDetailsPresenter(PatchRepositories repositories, EntityPositionManager entityPositionManager)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _entityPositionManager = entityPositionManager ?? throw new NullException(() => entityPositionManager);
        }

        public PatchDetailsViewModel Show(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.ID);

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
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.ID);

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
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.ID);

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
            Patch patch = _repositories.PatchRepository.Get(userInput.Entity.ID);
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
            Patch patch = _repositories.PatchRepository.Get(userInput.Entity.ID);
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
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.ID);

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
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.ID);

            // ViewModel Validation
            if (userInput.SelectedOperator == null)
            {
                // Non-Persisted
                userInput.ValidationMessages.Add(new Message
                {
                    PropertyKey = PresentationPropertyNames.SelectedOperator,
                    Text = ResourceFormatter.SelectAnOperatorFirst
                });

                return userInput;
            }
            else
            {
                // Business
                var patchManager = new PatchManager(entity, _repositories);
                patchManager.DeleteOwnedNumberOperators(userInput.SelectedOperator.ID);
                patchManager.DeleteOperatorWithRelatedEntities(userInput.SelectedOperator.ID);

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
            Patch entity = _repositories.PatchRepository.Get(userInput.Entity.ID);

            // Business
            var patchManager = new PatchManager(entity, _repositories);
            var operatorTypeEnum = (OperatorTypeEnum)operatorTypeID;
            int variableInletOrOutletCount = GetVariableInletOrOutletCount(operatorTypeEnum);
            Operator op = patchManager.CreateOperator(operatorTypeEnum, variableInletOrOutletCount);
            patchManager.CreateNumbersForEmptyInletsWithDefaultValues(op, ESTIMATED_OPERATOR_WIDTH, OPERATOR_HEIGHT, _entityPositionManager);

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
                    Text = ResourceFormatter.SelectAnOperatorFirst
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
                    Text = ResourceFormatter.SelectAnOperatorWithASingleOutlet
                });

                return null;
            }

            // GetEntities
            Outlet outlet = selectedOperator.Outlets.Single();
            AudioOutput audioOutput = outlet.Operator.Patch.Document.AudioOutput;

            // Business
            var patchManager = new PatchManager(outlet.Operator.Patch, new PatchRepositories(repositories));
            var calculatorCache = new CalculatorCache();
            int channelCount = audioOutput.GetChannelCount();
            var patchCalculators = new IPatchCalculator[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                patchCalculators[i] = patchManager.CreateCalculator(
                    outlet, 
                    audioOutput.SamplingRate, 
                    channelCount, 
                    i, 
                    calculatorCache);
            }

            var audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(repositories));
            AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
            audioFileOutput.LinkTo(audioOutput.SpeakerSetup);
            audioFileOutput.SamplingRate = audioOutput.SamplingRate;
            audioFileOutput.FilePath = _patchPlayOutputFilePath;
            audioFileOutput.Duration = _patchPlayDuration;
            audioFileOutput.LinkTo(outlet);

            // Infrastructure
            audioFileOutputManager.WriteFile(audioFileOutput, patchCalculators);

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

            foreach (OperatorViewModel operatorViewModel in viewModel.Entity.OperatorDictionary.Values)
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

        private int GetVariableInletOrOutletCount(OperatorTypeEnum operatorTypeEnum)
        {
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Add:
                    return DEFAULT_ADD_INLET_COUNT;

                case OperatorTypeEnum.ClosestOverInlets:
                case OperatorTypeEnum.ClosestOverInletsExp:
                    return DEFAULT_CLOSEST_OVER_INLETS_ITEM_COUNT;

                case OperatorTypeEnum.RangeOverOutlets:
                    return DEFAULT_RANGE_OVER_OUTLETS_OUTLET_COUNT;

                case OperatorTypeEnum.Multiply:
                    return DEFAULT_MULTIPLY_INLET_COUNT;

                case OperatorTypeEnum.MaxOverInlets:
                case OperatorTypeEnum.MinOverInlets:
                    return DEFAULT_AGGREGATE_OVER_INLETS_INLET_COUNT;

                case OperatorTypeEnum.SortOverInlets:
                    return DEFAULT_SORT_OVER_INLETS_INLET_COUNT;

                default:
                    return DEFAULT_VARIABLE_INLET_OR_OUTLET_COUNT;
            }
        }
    }
}

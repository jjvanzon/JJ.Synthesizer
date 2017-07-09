using JetBrains.Annotations;
using JJ.Business.Canonical;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchDetailsPresenter : PresenterBase<PatchDetailsViewModel>
    {
        // TODO: These two constants do not belong here, because they should be determined by the vector graphics.
        private const float ESTIMATED_OPERATOR_WIDTH = 50f;
        private const float OPERATOR_HEIGHT = 30f;

        private readonly RepositoryWrapper _repositories;
        private readonly EntityPositionManager _entityPositionManager;
        private readonly PatchManager _patchManager;
        private readonly AutoPatcher _autoPatcher;

        public PatchDetailsPresenter([NotNull] RepositoryWrapper repositories, EntityPositionManager entityPositionManager)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _entityPositionManager = entityPositionManager ?? throw new NullException(() => entityPositionManager);
            _patchManager = new PatchManager(repositories);
            _autoPatcher = new AutoPatcher(_repositories);
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
            var operatorFactory = new OperatorFactory(entity, _repositories);
            var operatorTypeEnum = (OperatorTypeEnum)operatorTypeID;
            Operator op = operatorFactory.New(operatorTypeEnum);
            _autoPatcher.CreateNumbersForEmptyInletsWithDefaultValues(op, ESTIMATED_OPERATOR_WIDTH, OPERATOR_HEIGHT, _entityPositionManager);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public PatchDetailsViewModel Delete(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.Entity.ID);

            // Businesss
            IResult result = _patchManager.DeletePatchWithRelatedEntities(patch);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(patch);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

            // Successful?
            viewModel.Successful = result.Successful;

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
                userInput.ValidationMessages.Add(
                    new Message(
                        nameof(userInput.SelectedOperator),
                        ResourceFormatter.SelectAnOperatorFirst).ToCanonical());

                return userInput;
            }
            else
            {
                // Business
                _patchManager.DeleteOwnedNumberOperators(userInput.SelectedOperator.ID);
                _patchManager.DeleteOperatorWithRelatedEntities(userInput.SelectedOperator.ID);

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

        public PatchDetailsViewModel LoseFocus(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            PatchDetailsViewModel viewModel = Update(userInput);

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

        public PatchDetailsViewModel Play(PatchDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Patch patch = _repositories.PatchRepository.Get(userInput.Entity.ID);

            // Business
            Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(patch, userInput.SelectedOperator?.ID);
            Outlet outlet = result.Data;
            if (outlet != null)
            {
                _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
            }

            // ToViewModel

            // HACK: AutoPatch can generate PatchOutlets and adders on the fly, that we do not even want to see.
            // That is not nice of it, but I mitigate the problem here a little.

            //PatchDetailsViewModel viewModel = CreateViewModel(patch);
            PatchDetailsViewModel viewModel = userInput;

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.OutletIDToPlay = outlet?.ID;
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

            // Successful?
            viewModel.Successful = result.Successful;

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

        public PatchDetailsViewModel SelectOperator(PatchDetailsViewModel userInput, int operatorID)
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
            SetSelectedOperator(viewModel, operatorID);

            // Successful
            viewModel.Successful = true;

            return viewModel;
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
            VoidResult result = _patchManager.SavePatch(entity);

            // ToViewModel
            PatchDetailsViewModel viewModel = CreateViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
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

        private PatchDetailsViewModel CreateViewModel(Patch patch)
        {
            return patch.ToDetailsViewModel(
                _repositories.DimensionRepository,
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _entityPositionManager);
        }

        protected override void CopyNonPersistedProperties(PatchDetailsViewModel sourceViewModel, PatchDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.CanSave = sourceViewModel.CanSave;

            if (sourceViewModel.SelectedOperator != null)
            {
                SetSelectedOperator(destViewModel, sourceViewModel.SelectedOperator.ID);
            }
        }
    }
}
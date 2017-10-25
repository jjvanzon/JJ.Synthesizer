using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PatchDetailsPresenter : EntityWritePresenterBase<Patch, PatchDetailsViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly EntityPositionManager _entityPositionManager;
        private readonly PatchManager _patchManager;
        private readonly AutoPatcher _autoPatcher;

        public PatchDetailsPresenter(RepositoryWrapper repositories, EntityPositionManager entityPositionManager)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _entityPositionManager = entityPositionManager ?? throw new NullException(() => entityPositionManager);
            _patchManager = new PatchManager(repositories);
            _autoPatcher = new AutoPatcher(_repositories);
        }

        protected override Patch GetEntity(PatchDetailsViewModel userInput) => _repositories.PatchRepository.Get(userInput.Entity.ID);

        protected override PatchDetailsViewModel ToViewModel(Patch patch)
        {
            return patch.ToDetailsViewModel(_repositories.CurveRepository, _entityPositionManager);
        }

        protected override IResult Save(Patch entity) => _patchManager.SavePatch(entity);

        public PatchDetailsViewModel ChangeInputOutlet(PatchDetailsViewModel userInput, int inletID, int inputOutletID)
        {
            return ExecuteAction(userInput, x =>
            {
                // GetEntities
                Inlet inlet = _repositories.InletRepository.Get(inletID);
                Outlet inputOutlet = _repositories.OutletRepository.Get(inputOutletID);

                // Business
                inlet.LinkTo(inputOutlet);

                return ResultHelper.Successful;
            });
        }

        /// <summary>
        /// NOTE: Has view model validation, which the base class's template method does not support.
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
                userInput.ValidationMessages.Add(ResourceFormatter.SelectAnOperatorFirst);

                return userInput;
            }
            else
            {
                // Business
                _patchManager.DeleteOwnedNumberOperators(userInput.SelectedOperator.ID);
                _patchManager.DeleteOperatorWithRelatedEntities(userInput.SelectedOperator.ID);

                // ToViewModel
                PatchDetailsViewModel viewModel = ToViewModel(entity);

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.SelectedOperator = null;

                // Successful
                viewModel.Successful = true;

                return viewModel;
            }
        }

        public PatchDetailsViewModel MoveOperator(PatchDetailsViewModel userInput, int operatorID, float centerX, float centerY)
        {
            return ExecuteAction(userInput, x =>
            {
                // GetEntity
                Operator op = _repositories.OperatorRepository.Get(operatorID);

                // Business
                _entityPositionManager.MoveOperator(op, centerX, centerY);

                return ResultHelper.Successful;
            });
        }

        /// <summary>
        /// NOTE: Cannot use base class's ExecuteAction method,
        /// because there is a hack. See the implementation.
        /// </summary>
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

            //PatchDetailsViewModel viewModel = ToViewModel(patch);
            PatchDetailsViewModel viewModel = userInput;

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.OutletIDToPlay = outlet?.ID;
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        public void SelectOperator(PatchDetailsViewModel viewModel, int operatorID)
        {
            ExecuteNonPersistedAction(viewModel, () => SetSelectedOperator(viewModel, operatorID));
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
            OperatorViewModel previousSelectedOperatorViewModel = viewModel.SelectedOperator;
            if (previousSelectedOperatorViewModel != null)
            {
                previousSelectedOperatorViewModel.IsSelected = false;
            }

            if (viewModel.Entity.OperatorDictionary.TryGetValue(operatorID, out OperatorViewModel selectedOperatorViewModel))
            {
                selectedOperatorViewModel.IsSelected = true;
            }

            viewModel.SelectedOperator = selectedOperatorViewModel;
        }

        public override void CopyNonPersistedProperties(PatchDetailsViewModel sourceViewModel, PatchDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.CanSave = sourceViewModel.CanSave;

            SetSelectedOperator(destViewModel, sourceViewModel.SelectedOperator?.ID ?? 0);
        }
    }
}
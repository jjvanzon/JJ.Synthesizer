using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveDetailsPresenter : PresenterBase<CurveDetailsViewModel>
    {
        private readonly CurveRepositories _repositories;
        private readonly CurveManager _curveManager;

        public CurveDetailsPresenter(CurveRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _curveManager = new CurveManager(_repositories);
        }

        public CurveDetailsViewModel Show(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.Curve.ID);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveDetailsViewModel Refresh(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            
            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.Curve.ID);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurveDetailsViewModel Close(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            CurveDetailsViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public CurveDetailsViewModel LoseFocus(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            CurveDetailsViewModel viewModel = Update(userInput);

            return viewModel;
        }

        private CurveDetailsViewModel Update(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.Curve.ID);

            // Business
            VoidResultDto result = _curveManager.SaveCurveWithRelatedEntities(entity);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        public CurveDetailsViewModel SelectNode(CurveDetailsViewModel userInput, int nodeID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Curve entity = _repositories.CurveRepository.Get(userInput.Curve.ID);

            // ToViewModel
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.SelectedNodeID = nodeID;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        // Helpers

        protected override void CopyNonPersistedProperties(CurveDetailsViewModel sourceViewModel, CurveDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.SelectedNodeID = sourceViewModel.SelectedNodeID;
        }
    }
}
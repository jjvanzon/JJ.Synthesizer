using System;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
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
            return TemplateMethod(userInput, viewModel => viewModel.Visible = true);
        }

        public CurveDetailsViewModel Refresh(CurveDetailsViewModel userInput)
        {
            return TemplateMethod(userInput, x => { });
        }

        public CurveDetailsViewModel Close(CurveDetailsViewModel userInput)
        {
            CurveDetailsViewModel viewModel = Validate(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public CurveDetailsViewModel LoseFocus(CurveDetailsViewModel userInput)
        {
            return Validate(userInput);
        }

        private CurveDetailsViewModel Validate(CurveDetailsViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Curve entity = _repositories.CurveRepository.Get(userInput.Curve.ID);

                    // Business
                    VoidResult result = _curveManager.SaveCurveWithRelatedEntities(entity);

                    // Non-Persisted
                    viewModel.ValidationMessages.AddRange(result.Messages);

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }

        public CurveDetailsViewModel SelectNode(CurveDetailsViewModel userInput, int nodeID)
        {
            return TemplateMethod(userInput, viewModel => viewModel.SelectedNodeID = nodeID);
        }

        public CurveDetailsViewModel NodeMoving(CurveDetailsViewModel userInput, int nodeID, double x, double y)
        {
            return NodeMovedOrMoving(userInput, nodeID, x, y);
        }

        public CurveDetailsViewModel NodeMoved(CurveDetailsViewModel userInput, int nodeID, double x, double y)
        {
            return NodeMovedOrMoving(userInput, nodeID, x, y);
        }

        private CurveDetailsViewModel NodeMovedOrMoving(CurveDetailsViewModel userInput, int nodeID, double x, double y)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // ToEntity
                    Node node = _repositories.NodeRepository.Get(nodeID);

                    // Business
                    node.X = x;
                    node.Y = y;
                });
        }

        // Helpers

        private CurveDetailsViewModel TemplateMethod(CurveDetailsViewModel userInput, Action<CurveDetailsViewModel> action)
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

            // Action
            action(viewModel);

            return viewModel;
        }

        protected override void CopyNonPersistedProperties(CurveDetailsViewModel sourceViewModel, CurveDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.SelectedNodeID = sourceViewModel.SelectedNodeID;
        }
    }
}
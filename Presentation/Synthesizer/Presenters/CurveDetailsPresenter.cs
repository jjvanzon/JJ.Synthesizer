using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveDetailsPresenter : EntityPresenterBaseWithSave<Curve, CurveDetailsViewModel>
    {
        private readonly CurveRepositories _repositories;
        private readonly CurveManager _curveManager;

        public CurveDetailsPresenter(CurveRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _curveManager = new CurveManager(_repositories);
        }

        protected override Curve GetEntity(CurveDetailsViewModel userInput)
        {
            return _repositories.CurveRepository.Get(userInput.Curve.ID);
        }

        protected override IResult Save(Curve entity)
        {
            return _curveManager.SaveCurveWithRelatedEntities(entity);
        }

        protected override CurveDetailsViewModel ToViewModel(Curve entity)
        {
            return entity.ToDetailsViewModel();
        }

        public void SelectNode(CurveDetailsViewModel viewModel, int nodeID)
        {
            ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeID = nodeID);
        }

        public CurveDetailsViewModel CreateNode(CurveDetailsViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                curve =>
                {
                    Node afterNode;
                    if (userInput.SelectedNodeID.HasValue)
                    {
                        afterNode = _repositories.NodeRepository.Get(userInput.SelectedNodeID.Value);
                    }
                    else
                    {
                        // Insert after last node if none selected.
                        afterNode = curve.Nodes.OrderBy(x => x.X).Last();
                    }

                    _curveManager.CreateNode(curve, afterNode);
                });
        }

        public CurveDetailsViewModel ChangeSelectedNodeType(CurveDetailsViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                _ =>
                {
                    // ViewModel Validation
                    if (!userInput.SelectedNodeID.HasValue)
                    {
                        return;
                    }

                    // GetEntity
                    int nodeID = userInput.SelectedNodeID.Value;
                    Node node = _repositories.NodeRepository.Get(nodeID);

                    // Business
                    _curveManager.RotateNodeType(node);
                });
        }

        public CurveDetailsViewModel DeleteSelectedNode(CurveDetailsViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                entity =>
                {
                    // ViewModel Validation
                    if (!userInput.SelectedNodeID.HasValue)
                    {
                        return new VoidResult
                        {
                            Messages = new List<string> { ResourceFormatter.SelectANodeFirst }
                        };
                    }

                    // GetEntity
                    int nodeID = userInput.SelectedNodeID.Value;
                    Node node = _repositories.NodeRepository.Get(nodeID);

                    // Business
                    IResult result = _curveManager.DeleteNode(node);
                    return result;
                },
                viewModel =>
                {
                    viewModel.SelectedNodeID = null;
                });
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
            return ExecuteAction(
                userInput,
                entity =>
                {
                    // ToEntity
                    Node node = _repositories.NodeRepository.Get(nodeID);

                    // Business
                    node.X = x;
                    node.Y = y;
                });
        }

        public override void CopyNonPersistedProperties(CurveDetailsViewModel sourceViewModel, CurveDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.SelectedNodeID = sourceViewModel.SelectedNodeID;
        }
    }
}
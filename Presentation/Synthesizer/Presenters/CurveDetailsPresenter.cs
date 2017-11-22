using System;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveDetailsPresenter : EntityPresenterWithSaveBase<Curve, CurveDetailsViewModel>
    {
        private readonly ICurveRepository _curveRepository;
        private readonly INodeRepository _nodeRepository;
        private readonly CurveManager _curveManager;

        public CurveDetailsPresenter(ICurveRepository curveRepository, INodeRepository nodeRepository, CurveManager curveManager)
        {
            _curveRepository = curveRepository ?? throw new ArgumentNullException(nameof(curveRepository));
            _nodeRepository = nodeRepository ?? throw new ArgumentNullException(nameof(nodeRepository));
            _curveManager = curveManager ?? throw new ArgumentNullException(nameof(curveManager));
        }

        protected override Curve GetEntity(CurveDetailsViewModel userInput)
        {
            return _curveRepository.Get(userInput.Curve.ID);
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
                        afterNode = _nodeRepository.Get(userInput.SelectedNodeID.Value);
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
                    Node node = _nodeRepository.Get(nodeID);

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
                        return new VoidResult(ResourceFormatter.SelectANodeFirst);
                    }

                    // GetEntity
                    int nodeID = userInput.SelectedNodeID.Value;
                    Node node = _nodeRepository.Get(nodeID);

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
                    Node node = _nodeRepository.Get(nodeID);

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
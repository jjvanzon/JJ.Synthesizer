using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

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

                    return ResultHelper.Successful;
                });
        }

        public override void CopyNonPersistedProperties(CurveDetailsViewModel sourceViewModel, CurveDetailsViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.SelectedNodeID = sourceViewModel.SelectedNodeID;
        }
    }
}
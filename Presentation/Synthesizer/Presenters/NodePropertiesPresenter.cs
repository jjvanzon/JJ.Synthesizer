using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class NodePropertiesPresenter : PresenterBaseWithSave<Node, NodePropertiesViewModel>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly CurveManager _curveManager;

        public NodePropertiesPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _nodeRepository = repositories.NodeRepository;
            _curveManager = new CurveManager(repositories);
        }

        protected override Node GetEntity(NodePropertiesViewModel userInput)
        {
            return _nodeRepository.Get(userInput.Entity.ID);
        }

        protected override NodePropertiesViewModel ToViewModel(Node entity)
        {
            return entity.ToPropertiesViewModel();
        }

        protected override IResult Save(Node entity)
        {
            return _curveManager.SaveNode(entity);
        }

        public NodePropertiesViewModel Delete(NodePropertiesViewModel userInput)
        {
            return ExecuteAction(userInput, entity => _curveManager.DeleteNode(entity));
        }
    }
}

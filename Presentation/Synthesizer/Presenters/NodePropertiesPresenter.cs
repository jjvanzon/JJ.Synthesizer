using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class NodePropertiesPresenter : PropertiesPresenterBase<NodePropertiesViewModel>
    {
        private readonly INodeRepository _nodeRepository;
        private readonly CurveManager _curveManager;

        public NodePropertiesPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _nodeRepository = repositories.NodeRepository;
            _curveManager = new CurveManager(repositories);
        }

        protected override NodePropertiesViewModel CreateViewModel(NodePropertiesViewModel userInput)
        {
            // GetEntity
            Node entity = _nodeRepository.Get(userInput.Entity.ID);

            // ToViewModel
            NodePropertiesViewModel viewModel = entity.ToPropertiesViewModel();
            return viewModel;
        }

        protected override void UpdateEntity(NodePropertiesViewModel viewModel)
        {
            // GetEntity
            Node entity = _nodeRepository.Get(viewModel.Entity.ID);

            // Business
            VoidResultDto result = _curveManager.SaveNode(entity);

            // Non-Persisted
            result.Messages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;
        }

        public NodePropertiesViewModel Delete(NodePropertiesViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // Business
                    _curveManager.DeleteNode(userInput.Entity.ID);
                });
        }
    }
}

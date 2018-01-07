using System;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class NodePropertiesPresenter : EntityPresenterWithSaveBase<Node, NodePropertiesViewModel>
	{
		private readonly INodeRepository _nodeRepository;
		private readonly CurveFacade _curveFacade;

		public NodePropertiesPresenter(INodeRepository nodeRepository, CurveFacade curveFacade)
		{
			_nodeRepository = nodeRepository ?? throw new ArgumentNullException(nameof(nodeRepository));
			_curveFacade = curveFacade ?? throw new ArgumentNullException(nameof(curveFacade));
		}

		protected override Node GetEntity(NodePropertiesViewModel userInput)
		{
			return _nodeRepository.Get(userInput.Entity.ID);
		}

		protected override NodePropertiesViewModel ToViewModel(Node entity)
		{
			return entity.ToPropertiesViewModel();
		}

		protected override IResult Save(Node entity, NodePropertiesViewModel userInput)
		{
			return _curveFacade.SaveNode(entity);
		}

		public NodePropertiesViewModel Delete(NodePropertiesViewModel userInput)
		{
			return ExecuteAction(userInput, entity => _curveFacade.DeleteNode(entity));
		}
	}
}

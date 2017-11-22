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
		private readonly CurveManager _curveManager;

		public NodePropertiesPresenter(INodeRepository nodeRepository, CurveManager curveManager)
		{
			_nodeRepository = nodeRepository ?? throw new ArgumentNullException(nameof(nodeRepository));
			_curveManager = curveManager ?? throw new ArgumentNullException(nameof(curveManager));
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

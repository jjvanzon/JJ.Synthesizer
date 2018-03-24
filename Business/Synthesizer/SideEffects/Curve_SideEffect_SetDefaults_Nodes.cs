using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Curve_SideEffect_SetDefaults_Nodes : ISideEffect
	{
		private readonly Curve _curve;
		private readonly INodeRepository _nodeRepository;
		private readonly INodeTypeRepository _nodeTypeRepository;
		private readonly IIDRepository _idRepository;

		public Curve_SideEffect_SetDefaults_Nodes(
			Curve curve, 
			INodeRepository nodeRepository, 
			INodeTypeRepository nodeTypeRepository, 
			IIDRepository idRepository)
		{
			_curve = curve ?? throw new NullException(() => curve);
			_nodeRepository = nodeRepository ?? throw new NullException(() => nodeRepository);
			_nodeTypeRepository = nodeTypeRepository ?? throw new NullException(() => nodeTypeRepository);
			_idRepository = idRepository ?? throw new NullException(() => idRepository);
		}

		public void Execute()
		{
			{
				var node = new Node
				{
					ID = _idRepository.GetID(),
					X = 0,
					Y = 1
				};
				node.SetNodeTypeEnum(NodeTypeEnum.Curve, _nodeTypeRepository);
				node.LinkTo(_curve);
				_nodeRepository.Insert(node);
			}

			{
				var node = new Node
				{
					ID = _idRepository.GetID(),
					X = 1,
					Y = 0
				};
				node.LinkTo(_curve);
				node.SetNodeTypeEnum(NodeTypeEnum.Curve, _nodeTypeRepository);
				_nodeRepository.Insert(node);
			}
		}
	}
}

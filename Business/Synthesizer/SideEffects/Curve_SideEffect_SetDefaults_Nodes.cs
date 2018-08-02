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
		private readonly IInterpolationTypeRepository _interpolationTypeRepository;
		private readonly IIDRepository _idRepository;

		public Curve_SideEffect_SetDefaults_Nodes(
			Curve curve, 
			INodeRepository nodeRepository,
			IInterpolationTypeRepository interpolationTypeRepository, 
			IIDRepository idRepository)
		{
			_curve = curve ?? throw new NullException(() => curve);
			_nodeRepository = nodeRepository ?? throw new NullException(() => nodeRepository);
			_interpolationTypeRepository = interpolationTypeRepository ?? throw new NullException(() => interpolationTypeRepository);
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
				node.SetInterpolationTypeEnum(InterpolationTypeEnum.Cubic, _interpolationTypeRepository);
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
				node.SetInterpolationTypeEnum(InterpolationTypeEnum.Cubic, _interpolationTypeRepository);
				_nodeRepository.Insert(node);
			}
		}
	}
}

using System;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Operator_SideEffect_AutoCreateEntityPosition : ISideEffect
	{
		private readonly Operator _entity;
		private readonly IEntityPositionRepository _entityPositionRepository;
		private readonly IIDRepository _idRepository;

		public Operator_SideEffect_AutoCreateEntityPosition(
			Operator entity,
			IEntityPositionRepository entityPositionRepository,
			IIDRepository idRepository)
		{
			_entity = entity ?? throw new ArgumentNullException(nameof(entity));
			_entityPositionRepository = entityPositionRepository ?? throw new ArgumentNullException(nameof(entityPositionRepository));
			_idRepository = idRepository ?? throw new ArgumentNullException(nameof(idRepository));
		}

		public void Execute()
		{
			if (_entity.EntityPosition == null)
			{
				EntityPosition entityPosition = SideEffectHelper.CreateEntityPosition(_entityPositionRepository, _idRepository);
				_entity.LinkTo(entityPosition);
			}
		}
	}
}
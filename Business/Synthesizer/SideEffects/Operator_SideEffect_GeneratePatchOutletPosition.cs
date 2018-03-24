using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class Operator_SideEffect_GeneratePatchOutletPosition : ISideEffect
	{
		private readonly Operator _entity;

		public Operator_SideEffect_GeneratePatchOutletPosition(Operator entity)
		{
			_entity = entity ?? throw new NullException(() => entity);
		}

		public void Execute()
		{
			if (_entity.Patch == null) throw new NullException(() => _entity.Patch);

			if (_entity.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
			{
				return;
			}

			HashSet<int> positions = _entity.Patch
											.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
											.Where(x => x.ID != _entity.ID) // Not itself
											.SelectMany(x => x.Outlets)
											.Select(x => x.Position)
											.ToHashSet();
			int suggestedPosition = 0;
			bool positionExists = positions.Contains(suggestedPosition);

			while (positionExists)
			{
				suggestedPosition++;
				positionExists = positions.Contains(suggestedPosition);
			}

			var wrapper = new PatchInletOrOutlet_OperatorWrapper(_entity);
			wrapper.Outlet.Position = suggestedPosition;
		}
	}
}

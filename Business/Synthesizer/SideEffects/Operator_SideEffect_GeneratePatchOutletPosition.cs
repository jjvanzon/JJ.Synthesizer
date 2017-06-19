using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;

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
            if (_entity.OperatorType == null) throw new NullException(() => _entity.OperatorType);

            if (_entity.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
            {
                return;
            }

            IList<int> positions = _entity.Patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                          .Where(x => x.ID != _entity.ID) // Not itself
                                          .Select(x => new PatchOutlet_OperatorWrapper(x).Outlet.Position)
                                          .ToArray();
            int suggestedPosition = 0;
            bool positionExists = positions.Contains(suggestedPosition);

            while (positionExists)
            {
                suggestedPosition++;
                positionExists = positions.Contains(suggestedPosition);
            }

            var wrapper = new PatchOutlet_OperatorWrapper(_entity);
            wrapper.Outlet.Position = suggestedPosition;
        }
    }
}

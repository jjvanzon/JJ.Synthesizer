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
    internal class Operator_SideEffect_GeneratePatchInletPosition : ISideEffect
    {
        private readonly Operator _entity;

        public Operator_SideEffect_GeneratePatchInletPosition(Operator entity)
        {
            _entity = entity ?? throw new NullException(() => entity);
        }

        public void Execute()
        {
            if (_entity.Patch == null) throw new NullException(() => _entity.Patch);

            if (_entity.GetOperatorTypeEnum() != OperatorTypeEnum.PatchInlet)
            {
                return;
            }

            IList<int> positions = _entity.Patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                          .Where(x => x.ID != _entity.ID) // Not itself
                                          .Select(x => new PatchInlet_OperatorWrapper(x).Inlet.Position)
                                          .ToArray();
            int suggestedPosition = 0;
            bool positionExists = positions.Contains(suggestedPosition);

            while (positionExists)
            {
                suggestedPosition++;
                positionExists = positions.Contains(suggestedPosition);
            }

            var wrapper = new PatchInlet_OperatorWrapper(_entity);
            wrapper.Inlet.Position = suggestedPosition;
        }
    }
}

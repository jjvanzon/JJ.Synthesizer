using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Operator_SideEffect_GeneratePatchInletListIndex : ISideEffect
    {
        private Operator _entity;

        public Operator_SideEffect_GeneratePatchInletListIndex(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Patch == null) throw new NullException(() => _entity.Patch);
            if (_entity.OperatorType == null) throw new NullException(() => _entity.OperatorType);

            if (_entity.GetOperatorTypeEnum() != OperatorTypeEnum.PatchInlet)
            {
                return;
            }

            IList<int> listIndexes = _entity.Patch.GetOperatorsOfType(OperatorTypeEnum.PatchInlet)
                                                  .Where(x => x.ID != _entity.ID) // Not itself
                                                  .Select(x => new PatchInlet_OperatorWrapper(x).ListIndex)
                                                  .Where(x => x.HasValue)
                                                  .Select(x => x.Value)
                                                  .ToArray();
            int suggestedListIndex = 0;
            bool listIndexExists = listIndexes.Contains(suggestedListIndex);

            while (listIndexExists)
            {
                suggestedListIndex++;
                listIndexExists = listIndexes.Contains(suggestedListIndex);
            }

            var wrapper = new PatchInlet_OperatorWrapper(_entity);
            wrapper.ListIndex = suggestedListIndex;
        }
    }
}

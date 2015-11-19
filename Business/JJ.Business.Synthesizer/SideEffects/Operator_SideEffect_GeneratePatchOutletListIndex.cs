using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Operator_SideEffect_GeneratePatchOutletListIndex : ISideEffect
    {
        private Operator _entity;

        public Operator_SideEffect_GeneratePatchOutletListIndex(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Patch == null) throw new NullException(() => _entity.Patch);
            if (_entity.OperatorType == null) throw new NullException(() => _entity.OperatorType);

            if (_entity.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
            {
                return;
            }

            IList<int> listIndexes = _entity.Patch.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                                  .Select(x => new OperatorWrapper_PatchOutlet(x).ListIndex)
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

            var wrapper = new OperatorWrapper_PatchOutlet(_entity);
            wrapper.ListIndex = suggestedListIndex;
        }
    }
}

using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.SideEffects
{
    // NOTE: This class is not used (yet). It might be used in PatchManager in the future.
    internal class Operator_SideEffect_GenerateName : ISideEffect
    {
        private Operator _entity;

        public Operator_SideEffect_GenerateName(Operator entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Patch == null) throw new NullException(() => _entity.Patch);
            if (_entity.OperatorType == null) throw new NullException(() => _entity.OperatorType);

            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                string operatorTypeDisplayName = ResourceHelper.GetOperatorTypeDisplayName(_entity);
                suggestedName = String.Format("{0} {1}", operatorTypeDisplayName, number++);
                nameExists = _entity.Patch.Operators.Where(x => String.Equals(x.Name, suggestedName)).Any();
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}

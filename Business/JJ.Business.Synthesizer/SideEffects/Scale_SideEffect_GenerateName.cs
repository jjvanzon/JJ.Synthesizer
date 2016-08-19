using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using System.Collections.Generic;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Scale_SideEffect_GenerateName : ISideEffect
    {
        private Scale _entity;

        public Scale_SideEffect_GenerateName(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Document == null) throw new NullException(() => _entity.Document);

            HashSet<string> existingNamesLowerCase = _entity.Document.EnumerateSelfAndParentAndTheirChildren()
                                                                     .SelectMany(x => x.Scales)
                                                                     .Select(x => x.Name?.ToLower())
                                                                     .ToHashSet();
            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Scale, number++);
                nameExists = existingNamesLowerCase.Contains(suggestedName.ToLower());
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}

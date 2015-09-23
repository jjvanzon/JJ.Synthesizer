using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;

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

            // TODO: When it proves to work that a name becomes unique within the whole parent-child tree of documents,
            // apply this to other GenerateName SideEffects too.
            string[] existingNames = _entity.Document.EnumerateSelfAndParentAndChildren()
                                                     .SelectMany(x => x.Scales)
                                                     .Select(x => x.Name)
                                                     .ToArray();
            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Scale, number++);
                nameExists = existingNames.Where(x => String.Equals(x, suggestedName)).Any();
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}

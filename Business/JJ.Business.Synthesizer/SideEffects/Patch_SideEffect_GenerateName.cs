using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Patch_SideEffect_GenerateName : ISideEffect
    {
        private Patch _entity;

        public Patch_SideEffect_GenerateName(Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);
            _entity = entity;
        }

        public void Execute()
        {
            if (_entity.Document == null) throw new NullException(() => _entity.Document);

            int number = 1;
            string suggestedName;
            bool nameExists;

            do
            {
                suggestedName = String.Format("{0} {1}", PropertyDisplayNames.Patch, number++);
                nameExists = _entity.Document.Patches.Where(x => String.Equals(x.Name, suggestedName)).Any();
            }
            while (nameExists);

            _entity.Name = suggestedName;
        }
    }
}

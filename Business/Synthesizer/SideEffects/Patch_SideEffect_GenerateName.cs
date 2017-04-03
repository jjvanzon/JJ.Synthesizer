using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using System.Linq;
using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Patch_SideEffect_GenerateName : ISideEffect
    {
        private readonly Patch _entity;

        public Patch_SideEffect_GenerateName(Patch entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            _entity = entity;
        }

        public void Execute()
        {
            IEnumerable<string> existingNames = _entity.Document.Patches.Select(x => x.Name);

            _entity.Name = SideEffectHelper.GenerateName<Patch>(existingNames);
        }
    }
}
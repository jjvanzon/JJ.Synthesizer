using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Scale_SideEffect_GenerateName : ISideEffect
    {
        private readonly Scale _entity;

        public Scale_SideEffect_GenerateName(Scale entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            _entity = entity;
        }

        public void Execute()
        {
            IEnumerable<string> existingNames = _entity.Document.Scales.Select(x => x.Name);

            _entity.Name = SideEffectHelper.GenerateName<Scale>(existingNames);
        }
    }
}

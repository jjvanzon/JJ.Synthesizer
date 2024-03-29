﻿using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Scale_SideEffect_GenerateName : ISideEffect
    {
        private readonly Scale _entity;

        public Scale_SideEffect_GenerateName(Scale entity) => _entity = entity ?? throw new NullException(() => entity);

        public void Execute()
        {
            bool mustExecute = _entity.Document != null;
            // ReSharper disable once InvertIf
            if (mustExecute)
            {
                IEnumerable<string> existingNames = _entity.Document.Scales.Select(x => x.Name);

                _entity.Name = SideEffectHelper.GenerateName<Scale>(existingNames);
            }
        }
    }
}
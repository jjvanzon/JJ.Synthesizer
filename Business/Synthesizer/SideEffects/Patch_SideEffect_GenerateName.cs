﻿using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using System.Collections.Generic;

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
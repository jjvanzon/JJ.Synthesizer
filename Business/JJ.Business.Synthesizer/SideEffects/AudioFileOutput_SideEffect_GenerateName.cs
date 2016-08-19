using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioFileOutput_SideEffect_GenerateName : ISideEffect
    {
        private readonly AudioFileOutput _entity;

        public AudioFileOutput_SideEffect_GenerateName(AudioFileOutput entity)
        {
            if (entity == null) throw new NullException(() => entity);
            if (entity.Document == null) throw new NullException(() => entity.Document);

            _entity = entity;
        }

        public void Execute()
        {
            IEnumerable<string> existingNames = _entity.Document.EnumerateSelfAndParentAndTheirChildren()
                                                                .SelectMany(x => x.AudioFileOutputs)
                                                                .Select(x => x.Name);

            _entity.Name = SideEffectHelper.GenerateName<AudioFileOutput>(existingNames);
        }
    }
}

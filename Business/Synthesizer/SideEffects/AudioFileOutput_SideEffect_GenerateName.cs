using JJ.Data.Synthesizer;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using System.Linq;
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
            IEnumerable<string> existingNames = _entity.Document.AudioFileOutputs.Select(x => x.Name);

            _entity.Name = SideEffectHelper.GenerateName<AudioFileOutput>(existingNames);
        }
    }
}

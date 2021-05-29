using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioFileOutput_SideEffect_GenerateName : ISideEffect
    {
        private readonly AudioFileOutput _entity;

        public AudioFileOutput_SideEffect_GenerateName(AudioFileOutput entity) => _entity = entity ?? throw new NullException(() => entity);

        public void Execute()
        {
            bool mustExecute = _entity.Document != null;
            // ReSharper disable once InvertIf
            if (mustExecute)
            {
                IEnumerable<string> existingNames = _entity.Document.AudioFileOutputs.Select(x => x.Name);

                _entity.Name = SideEffectHelper.GenerateName<AudioFileOutput>(existingNames);
            }
        }
    }
}
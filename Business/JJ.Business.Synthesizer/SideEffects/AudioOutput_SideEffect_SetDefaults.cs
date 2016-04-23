using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioOutput_SideEffect_SetDefaults : ISideEffect
    {
        private readonly AudioOutput _entity;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;

        public AudioOutput_SideEffect_SetDefaults(AudioOutput entity, ISpeakerSetupRepository speakerSetupRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);

            _entity = entity;
            _speakerSetupRepository = speakerSetupRepository;
        }

        public void Execute()
        {
            _entity.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _speakerSetupRepository);
            _entity.SamplingRate = 44100;
            _entity.MaxConcurrentNotes = 16;
            _entity.BufferDuration = 0.1;
        }
    }
}

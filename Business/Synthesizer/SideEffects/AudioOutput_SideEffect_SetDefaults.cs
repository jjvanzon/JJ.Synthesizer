using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioOutput_SideEffect_SetDefaults : ISideEffect
    {
        private static readonly SpeakerSetupEnum _defaultSpeakerSetupEnum = ConfigurationHelper.GetSection<ConfigurationSection>().DefaultSpeakerSetup;
        private static readonly int _defaultSamplingRate = ConfigurationHelper.GetSection<ConfigurationSection>().DefaultSamplingRate;
        private static readonly int _defaultMaxConcurrentNotes = ConfigurationHelper.GetSection<ConfigurationSection>().DefaultMaxConcurrentNotes;

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
            _entity.SetSpeakerSetupEnum(_defaultSpeakerSetupEnum, _speakerSetupRepository);
            _entity.SamplingRate = _defaultSamplingRate;
            _entity.MaxConcurrentNotes = _defaultMaxConcurrentNotes;
            _entity.DesiredBufferDuration = 0.1;
        }
    }
}

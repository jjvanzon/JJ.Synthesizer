using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Configuration;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioFileOutput_SideEffect_SetDefaults : ISideEffect
    {
        private static readonly SpeakerSetupEnum _defaultSpeakerSetupEnum = ConfigurationHelper.GetSection<ConfigurationSection>().DefaultSpeakerSetup;
        private static readonly int _defaultSamplingRate = ConfigurationHelper.GetSection<ConfigurationSection>().DefaultSamplingRate;

        private readonly AudioFileOutput _entity;
        private readonly ISampleDataTypeRepository _sampleDataTypeRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;
        private readonly IAudioFileFormatRepository _audioFileFormatRepository;

        public AudioFileOutput_SideEffect_SetDefaults(
            AudioFileOutput entity,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IAudioFileFormatRepository audioFileFormatRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);

            _entity = entity;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
        }

        public void Execute()
        {
            _entity.Amplifier = 0.25;
            _entity.TimeMultiplier = 1;
            _entity.Duration = 1;
            _entity.SamplingRate = _defaultSamplingRate;

            _entity.SetAudioFileFormatEnum(AudioFileFormatEnum.Wav, _audioFileFormatRepository);
            _entity.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _sampleDataTypeRepository);
            _entity.SetSpeakerSetupEnum(_defaultSpeakerSetupEnum, _speakerSetupRepository);
        }
    }
}

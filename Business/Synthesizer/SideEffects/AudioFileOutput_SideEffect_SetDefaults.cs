using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioFileOutput_SideEffect_SetDefaults : ISideEffect
    {
        private static readonly SpeakerSetupEnum _defaultSpeakerSetupEnum = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultSpeakerSetup;
        private static readonly int _defaultSamplingRate = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultSamplingRate;

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
            _entity = entity ?? throw new NullException(() => entity);
            _sampleDataTypeRepository = sampleDataTypeRepository ?? throw new NullException(() => sampleDataTypeRepository);
            _speakerSetupRepository = speakerSetupRepository ?? throw new NullException(() => speakerSetupRepository);
            _audioFileFormatRepository = audioFileFormatRepository ?? throw new NullException(() => audioFileFormatRepository);
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

using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class AudioFileOutput_SideEffect_SetDefaults : ISideEffect
    {
        private AudioFileOutput _entity;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;

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
            _entity.Amplifier = 1;
            _entity.TimeMultiplier = 1;
            _entity.Duration = 1;
            _entity.SamplingRate = 44100;

            _entity.SetAudioFileFormatEnum(AudioFileFormatEnum.Wav, _audioFileFormatRepository);
            _entity.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _sampleDataTypeRepository);
            _entity.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _speakerSetupRepository);
        }
    }
}

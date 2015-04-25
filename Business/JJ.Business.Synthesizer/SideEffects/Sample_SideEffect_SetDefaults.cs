using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Business;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Sample_SideEffect_SetDefaults : ISideEffect
    {
        private Sample _entity;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IInterpolationTypeRepository _interpolationTypeRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;

        public Sample_SideEffect_SetDefaults(
            Sample entity,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            IAudioFileFormatRepository audioFileFormatRepository)
        {
            if (entity == null) throw new NullException(() => entity);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);

            _entity = entity;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _interpolationTypeRepository = interpolationTypeRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
        }

        public void Execute()
        {
            _entity.Amplifier = 1;
            _entity.TimeMultiplier = 1;
            _entity.IsActive = true;
            _entity.SamplingRate = 44100;
            _entity.SetAudioFileFormatEnum(AudioFileFormatEnum.Raw, _audioFileFormatRepository);
            _entity.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _sampleDataTypeRepository);
            _entity.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _speakerSetupRepository);
            _entity.SetInterpolationTypeEnum(InterpolationTypeEnum.Line, _interpolationTypeRepository);
        }
    }
}
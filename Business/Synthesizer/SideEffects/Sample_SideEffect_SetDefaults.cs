using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Sample_SideEffect_SetDefaults : ISideEffect
    {
        private Sample _entity;
        private SampleRepositories _repositories;

        public Sample_SideEffect_SetDefaults(Sample entity, SampleRepositories repositories)
        {
            if (entity == null) throw new NullException(() => entity);
            if (repositories == null) throw new NullException(() => repositories);

            _entity = entity;
            _repositories = repositories;
        }

        public void Execute()
        {
            _entity.Amplifier = 1;
            _entity.TimeMultiplier = 1;
            _entity.IsActive = true;
            _entity.SamplingRate = 44100;
            _entity.SetAudioFileFormatEnum(AudioFileFormatEnum.Raw, _repositories.AudioFileFormatRepository);
            _entity.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _repositories.SampleDataTypeRepository);
            _entity.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _repositories.SpeakerSetupRepository);
            _entity.SetInterpolationTypeEnum(InterpolationTypeEnum.Line, _repositories.InterpolationTypeRepository);
        }
    }
}
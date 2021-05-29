using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
    internal class Sample_SideEffect_SetDefaults : ISideEffect
    {
        private readonly Sample _entity;
        private readonly SampleRepositories _repositories;

        public Sample_SideEffect_SetDefaults(Sample entity, SampleRepositories repositories)
        {
            _entity = entity ?? throw new NullException(() => entity);
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        public void Execute()
        {
            _entity.Amplifier = 1;
            _entity.TimeMultiplier = 1;
            _entity.SamplingRate = 44100;
            _entity.SetAudioFileFormatEnum(AudioFileFormatEnum.Raw, _repositories.AudioFileFormatRepository);
            _entity.SetSampleDataTypeEnum(SampleDataTypeEnum.Int16, _repositories.SampleDataTypeRepository);
            _entity.SetSpeakerSetupEnum(SpeakerSetupEnum.Mono, _repositories.SpeakerSetupRepository);
            _entity.SetInterpolationTypeEnum(InterpolationTypeEnum.Line, _repositories.InterpolationTypeRepository);
        }
    }
}
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.SideEffects
{
	internal class AudioOutput_SideEffect_SetDefaults : ISideEffect
	{
		private static readonly SpeakerSetupEnum _defaultSpeakerSetupEnum = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultSpeakerSetup;
		private static readonly int _defaultSamplingRate = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultSamplingRate;
		private static readonly int _defaultMaxConcurrentNotes = CustomConfigurationManager.GetSection<ConfigurationSection>().DefaultMaxConcurrentNotes;

		private readonly AudioOutput _entity;
		private readonly ISpeakerSetupRepository _speakerSetupRepository;

		public AudioOutput_SideEffect_SetDefaults(AudioOutput entity, ISpeakerSetupRepository speakerSetupRepository)
		{
			_entity = entity ?? throw new NullException(() => entity);
			_speakerSetupRepository = speakerSetupRepository ?? throw new NullException(() => speakerSetupRepository);
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

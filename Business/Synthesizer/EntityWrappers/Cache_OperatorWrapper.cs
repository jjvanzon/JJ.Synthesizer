using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.EntityWrappers
{
	public class Cache_OperatorWrapper : OperatorWrapper
	{
		public Cache_OperatorWrapper(Operator op)
			: base(op)
		{ }

		public InterpolationTypeEnum InterpolationType
		{
			get => DataPropertyParser.GetEnum<InterpolationTypeEnum>(WrappedOperator, nameof(InterpolationType));
			set => DataPropertyParser.SetValue(WrappedOperator, nameof(InterpolationType), value);
		}

		public SpeakerSetupEnum SpeakerSetup
		{
			get => DataPropertyParser.GetEnum<SpeakerSetupEnum>(WrappedOperator, nameof(SpeakerSetup));
			set => DataPropertyParser.SetValue(WrappedOperator, nameof(SpeakerSetup), value);
		}

		public int GetChannelCount(ISpeakerSetupRepository speakerSetupRepository)
		{
			if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
			SpeakerSetupEnum speakerSetupEnum = SpeakerSetup;
			SpeakerSetup speakerSetup = speakerSetupRepository.Get((int)speakerSetupEnum);
			return speakerSetup.SpeakerSetupChannels.Count;
		}
	}
}

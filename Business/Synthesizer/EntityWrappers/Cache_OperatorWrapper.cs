using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Cache_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
    {
        public Cache_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get => SignalInlet.InputOutlet;
            set => SignalInlet.LinkTo(value);
        }

        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

        public Outlet Start
        {
            get => StartInlet.InputOutlet;
            set => StartInlet.LinkTo(value);
        }

        public Inlet StartInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Start);

        public Outlet End
        {
            get => EndInlet.InputOutlet;
            set => EndInlet.LinkTo(value);
        }

        public Inlet EndInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.End);

        public Outlet SamplingRate
        {
            get => SamplingRateInlet.InputOutlet;
            set => SamplingRateInlet.LinkTo(value);
        }

        public Inlet SamplingRateInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.SamplingRate);

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

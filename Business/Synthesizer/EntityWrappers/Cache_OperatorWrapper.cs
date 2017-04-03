using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Cache_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        public Cache_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_SIGNAL_INDEX);

        public Outlet Start
        {
            get { return StartInlet.InputOutlet; }
            set { StartInlet.LinkTo(value); }
        }

        public Inlet StartInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_START_INDEX);

        public Outlet End
        {
            get { return EndInlet.InputOutlet; }
            set { EndInlet.LinkTo(value); }
        }

        public Inlet EndInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_END_INDEX);

        public Outlet SamplingRate
        {
            get { return SamplingRateInlet.InputOutlet; }
            set { SamplingRateInlet.LinkTo(value); }
        }

        public Inlet SamplingRateInlet => OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_SAMPLING_RATE_INDEX);

        public InterpolationTypeEnum InterpolationType
        {
            get { return DataPropertyParser.GetEnum<InterpolationTypeEnum>(WrappedOperator, PropertyNames.InterpolationType); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public SpeakerSetupEnum SpeakerSetup
        {
            get { return DataPropertyParser.GetEnum<SpeakerSetupEnum>(WrappedOperator, PropertyNames.SpeakerSetup); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.SpeakerSetup, value); }
        }

        public int GetChannelCount(ISpeakerSetupRepository speakerSetupRepository)
        {
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            SpeakerSetupEnum speakerSetupEnum = SpeakerSetup;
            SpeakerSetup speakerSetup = speakerSetupRepository.Get((int)speakerSetupEnum);
            return speakerSetup.SpeakerSetupChannels.Count;
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.CACHE_SIGNAL_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.CACHE_START_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => Start);
                        return name;
                    }

                case OperatorConstants.CACHE_END_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => End);
                        return name;
                    }

                case OperatorConstants.CACHE_SAMPLING_RATE_INDEX:
                    {
                        string name = ResourceFormatter.GetDisplayName(() => SamplingRate);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }
    }
}

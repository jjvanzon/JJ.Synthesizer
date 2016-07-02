using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Cache_OperatorWrapper : OperatorWrapperBase
    {
        public Cache_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return SignalInlet.InputOutlet; }
            set { SignalInlet.LinkTo(value); }
        }

        public Inlet SignalInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_SIGNAL_INDEX); }
        }

        public Outlet StartTime
        {
            get { return StartTimeInlet.InputOutlet; }
            set { StartTimeInlet.LinkTo(value); }
        }

        public Inlet StartTimeInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_START_TIME_INDEX); }
        }

        public Outlet EndTime
        {
            get { return EndTimeInlet.InputOutlet; }
            set { EndTimeInlet.LinkTo(value); }
        }

        public Inlet EndTimeInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_END_TIME_INDEX); }
        }

        public Outlet SamplingRate
        {
            get { return SamplingRateInlet.InputOutlet; }
            set { SamplingRateInlet.LinkTo(value); }
        }

        public Inlet SamplingRateInlet
        {
            get { return OperatorHelper.GetInlet(WrappedOperator, OperatorConstants.CACHE_SAMPLING_RATE_INDEX); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, OperatorConstants.CACHE_RESULT_INDEX); }
        }

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

        public DimensionEnum Dimension
        {
            get { return DataPropertyParser.GetEnum<DimensionEnum>(WrappedOperator, PropertyNames.Dimension); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.Dimension, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case OperatorConstants.CACHE_SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case OperatorConstants.CACHE_START_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => StartTime);
                        return name;
                    }

                case OperatorConstants.CACHE_END_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => EndTime);
                        return name;
                    }

                case OperatorConstants.CACHE_SAMPLING_RATE_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => SamplingRate);
                        return name;
                    }

                default:
                    throw new InvalidIndexException(() => listIndex, () => WrappedOperator.Inlets.Count);
            }
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Cache_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

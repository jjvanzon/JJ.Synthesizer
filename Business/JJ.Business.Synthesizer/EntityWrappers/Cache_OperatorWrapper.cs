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
        private const int SIGNAL_INDEX = 0;
        private const int START_TIME_INDEX = 1;
        private const int END_TIME_INDEX = 2;
        private const int SAMPLING_RATE_INDEX = 3;
        private const int RESULT_INDEX = 0;

        public Cache_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet StartTime
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, START_TIME_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, START_TIME_INDEX).LinkTo(value); }
        }

        public Outlet EndTime
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, END_TIME_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, END_TIME_INDEX).LinkTo(value); }
        }

        public Outlet SamplingRate
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SAMPLING_RATE_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SAMPLING_RATE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public InterpolationTypeEnum InterpolationTypeEnum
        {
            get { return DataPropertyParser.GetEnum<InterpolationTypeEnum>(WrappedOperator, PropertyNames.InterpolationType); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public SpeakerSetupEnum SpeakerSetupEnum
        {
            get { return DataPropertyParser.GetEnum<SpeakerSetupEnum>(WrappedOperator, PropertyNames.SpeakerSetup); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.SpeakerSetup, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            switch (listIndex)
            {
                case SIGNAL_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
                        return name;
                    }

                case START_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => StartTime);
                        return name;
                    }

                case END_TIME_INDEX:
                    {
                        string name = ResourceHelper.GetPropertyDisplayName(() => EndTime);
                        return name;
                    }

                case SAMPLING_RATE_INDEX:
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

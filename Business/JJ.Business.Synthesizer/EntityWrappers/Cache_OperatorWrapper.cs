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
        private const int RESULT_INDEX = 0;

        public Cache_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(WrappedOperator, SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(WrappedOperator, SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX); }
        }

        public double StartTime
        {
            get { return OperatorDataParser.GetDouble(WrappedOperator, PropertyNames.StartTime); }
            set { OperatorDataParser.SetValue(WrappedOperator, PropertyNames.StartTime, value); }
        }

        public double EndTime
        {
            get { return OperatorDataParser.GetDouble(WrappedOperator, PropertyNames.EndTime); }
            set { OperatorDataParser.SetValue(WrappedOperator, PropertyNames.EndTime, value); }
        }

        public int SamplingRate
        {
            get { return OperatorDataParser.GetInt32(WrappedOperator, PropertyNames.SamplingRate); }
            set { OperatorDataParser.SetValue(WrappedOperator, PropertyNames.SamplingRate, value); }
        }

        public InterpolationTypeEnum InterpolationTypeEnum
        {
            get { return OperatorDataParser.GetEnum<InterpolationTypeEnum>(WrappedOperator, PropertyNames.InterpolationType); }
            set { OperatorDataParser.SetValue(WrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public SpeakerSetupEnum SpeakerSetupEnum
        {
            get { return OperatorDataParser.GetEnum<SpeakerSetupEnum>(WrappedOperator, PropertyNames.SpeakerSetup); }
            set { OperatorDataParser.SetValue(WrappedOperator, PropertyNames.SpeakerSetup, value); }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Signal);
            return name;
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

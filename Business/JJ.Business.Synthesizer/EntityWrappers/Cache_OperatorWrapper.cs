using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Cache_OperatorWrapper : OperatorWrapperBase
    {
        public Cache_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.MAXIMUM_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.MAXIMUM_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.MAXIMUM_RESULT_INDEX); }
        }

        public double StartTime
        {
            get { return OperatorDataParser.GetDouble(_wrappedOperator, PropertyNames.StartTime); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.StartTime, value); }
        }

        public double EndTime
        {
            get { return OperatorDataParser.GetDouble(_wrappedOperator, PropertyNames.EndTime); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.EndTime, value); }
        }

        public int SamplingRate
        {
            get { return OperatorDataParser.GetInt32(_wrappedOperator, PropertyNames.SamplingRate); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.SamplingRate, value); }
        }

        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum
        {
            get { return OperatorDataParser.GetEnum<ResampleInterpolationTypeEnum>(_wrappedOperator, PropertyNames.InterpolationType); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public SpeakerSetupEnum SpeakerSetupEnum
        {
            get { return OperatorDataParser.GetEnum<SpeakerSetupEnum>(_wrappedOperator, PropertyNames.SpeakerSetup); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.SpeakerSetup, value); }
        }

        public static implicit operator Outlet(Cache_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

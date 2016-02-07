using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Resample_OperatorWrapper : OperatorWrapperBase
    {
        public Resample_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.RESAMPLE_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.RESAMPLE_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet SamplingRate
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.RESAMPLE_RESULT_INDEX); }
        }

        public ResampleInterpolationTypeEnum ResampleInterpolationTypeEnum
        {
            get { return OperatorDataParser.GetEnum<ResampleInterpolationTypeEnum>(_wrappedOperator, PropertyNames.InterpolationType); }
            set { OperatorDataParser.SetValue(_wrappedOperator, PropertyNames.InterpolationType, value); }
        }

        public static implicit operator Outlet(Resample_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

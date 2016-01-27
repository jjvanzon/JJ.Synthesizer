using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class HighPassFilter_OperatorWrapper : OperatorWrapperBase
    {
        public HighPassFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.HIGH_PASS_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.HIGH_PASS_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet MinFrequency
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.HIGH_PASS_MIN_FREQUENCY_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.HIGH_PASS_MIN_FREQUENCY_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.HIGH_PASS_RESULT_INDEX); }
        }

        public static implicit operator Outlet(HighPassFilter_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

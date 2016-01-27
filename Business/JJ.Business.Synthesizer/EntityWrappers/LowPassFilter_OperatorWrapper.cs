using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class LowPassFilter_OperatorWrapper : OperatorWrapperBase
    {
        public LowPassFilter_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOW_PASS_SIGNAL_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOW_PASS_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet MaxFrequency
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.LOW_PASS_MAX_FREQUENCY_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.LOW_PASS_MAX_FREQUENCY_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.LOW_PASS_RESULT_INDEX); }
        }

        public static implicit operator Outlet(LowPassFilter_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Power_OperatorWrapper : OperatorWrapperBase
    {
        public Power_OperatorWrapper(Operator op)
            :base(op)
        { }

        public Outlet Base
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.POWER_BASE_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.POWER_BASE_INDEX).LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.POWER_EXPONENT_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.POWER_EXPONENT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.POWER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Power_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

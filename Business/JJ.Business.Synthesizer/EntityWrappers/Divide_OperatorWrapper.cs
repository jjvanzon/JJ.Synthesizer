using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Divide_OperatorWrapper : OperatorWrapperBase
    {
        public Divide_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Numerator
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.DIVIDE_NUMERATOR_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.DIVIDE_NUMERATOR_INDEX).LinkTo(value); }
        }

        public Outlet Denominator
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.DIVIDE_DENOMINATOR_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.DIVIDE_DENOMINATOR_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, OperatorConstants.DIVIDE_ORIGIN_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, OperatorConstants.DIVIDE_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, OperatorConstants.DIVIDE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Divide_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Divide_OperatorWrapper : OperatorWrapperBase
    {
        public Divide_OperatorWrapper(Operator op)
            :base(op)
        { }

        public Outlet Numerator
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.DIVIDE_NUMERATOR_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.DIVIDE_NUMERATOR_INDEX).LinkTo(value); }
        }

        public Outlet Denominator
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.DIVIDE_DENOMINATOR_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.DIVIDE_DENOMINATOR_INDEX).LinkTo(value); }
        }

        public Outlet Origin
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.DIVIDE_ORIGIN_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.DIVIDE_ORIGIN_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_operator, OperatorConstants.DIVIDE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Divide_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

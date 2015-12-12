using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Exponent_OperatorWrapper : OperatorWrapperBase
    {
        public Exponent_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Low
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.EXPONENT_LOW_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.EXPONENT_LOW_INDEX).LinkTo(value); }
        }

        public Outlet High
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.EXPONENT_HIGH_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.EXPONENT_HIGH_INDEX).LinkTo(value); }
        }

        public Outlet Ratio
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.EXPONENT_RATIO_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.EXPONENT_RATIO_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_operator, OperatorConstants.EXPONENT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Exponent_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

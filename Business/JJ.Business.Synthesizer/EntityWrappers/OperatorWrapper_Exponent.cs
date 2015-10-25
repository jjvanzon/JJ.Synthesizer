using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Exponent : OperatorWrapperBase
    {
        public OperatorWrapper_Exponent(Operator op)
            : base(op)
        { }

        public Outlet Low
        {
            get { return GetInlet(OperatorConstants.EXPONENT_LOW_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.EXPONENT_LOW_INDEX).LinkTo(value); }
        }

        public Outlet High
        {
            get { return GetInlet(OperatorConstants.EXPONENT_HIGH_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.EXPONENT_HIGH_INDEX).LinkTo(value); }
        }

        public Outlet Ratio
        {
            get { return GetInlet(OperatorConstants.EXPONENT_RATIO_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.EXPONENT_RATIO_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.EXPONENT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Exponent wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

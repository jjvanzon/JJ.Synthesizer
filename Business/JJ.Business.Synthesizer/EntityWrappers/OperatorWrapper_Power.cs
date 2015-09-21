using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Power : OperatorWrapperBase
    {
        public OperatorWrapper_Power(Operator op)
            :base(op)
        { }

        public Outlet Base
        {
            get { return GetInlet(OperatorConstants.POWER_BASE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.POWER_BASE_INDEX).LinkTo(value); }
        }

        public Outlet Exponent
        {
            get { return GetInlet(OperatorConstants.POWER_EXPONENT_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.POWER_EXPONENT_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.POWER_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Power wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

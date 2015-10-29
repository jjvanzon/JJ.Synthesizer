using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_Select : OperatorWrapperBase
    {
        public OperatorWrapper_Select(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.SELECT_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SELECT_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Time
        {
            get { return GetInlet(OperatorConstants.SELECT_TIME_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SELECT_TIME_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SELECT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(OperatorWrapper_Select wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
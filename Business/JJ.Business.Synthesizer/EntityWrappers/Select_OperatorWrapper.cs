using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Select_OperatorWrapper : OperatorWrapperBase
    {
        public Select_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.SELECT_SIGNAL_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.SELECT_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet Time
        {
            get { return OperatorHelper.GetInlet(_operator, OperatorConstants.SELECT_TIME_INDEX).InputOutlet; }
            set { OperatorHelper.GetInlet(_operator, OperatorConstants.SELECT_TIME_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_operator, OperatorConstants.SELECT_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Select_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}
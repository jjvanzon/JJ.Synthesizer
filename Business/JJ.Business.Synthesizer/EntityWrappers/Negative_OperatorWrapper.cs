using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Negative_OperatorWrapper : OperatorWrapperBase
    {
        private const int X_INDEX = 0;
        private const int RESULT_INDEX = 0;

        public Negative_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet X
        {
            get { return OperatorHelper.GetInputOutlet(_wrappedOperator, X_INDEX); }
            set { OperatorHelper.GetInlet(_wrappedOperator, X_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return OperatorHelper.GetOutlet(_wrappedOperator, RESULT_INDEX); }
        }

        public static implicit operator Outlet(Negative_OperatorWrapper wrapper)
        {
            if (wrapper == null) return null;

            return wrapper.Result;
        }
    }
}

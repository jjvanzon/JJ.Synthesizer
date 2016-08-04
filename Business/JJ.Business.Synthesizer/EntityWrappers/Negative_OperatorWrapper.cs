using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Resources;

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
            get { return XInlet.InputOutlet; }
            set { XInlet.LinkTo(value); }
        }

        public Inlet XInlet => OperatorHelper.GetInlet(WrappedOperator, X_INDEX);

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => X);
            return name;
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }

        public static implicit operator Outlet(Negative_OperatorWrapper wrapper) => wrapper?.Result;
    }
}

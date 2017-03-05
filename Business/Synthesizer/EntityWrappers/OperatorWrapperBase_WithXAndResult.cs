using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithXAndResult : OperatorWrapperBase_WithResult
    {
        private const int X_INDEX = 0;

        public OperatorWrapperBase_WithXAndResult(Operator op)
            : base(op)
        { }

        public Outlet X
        {
            get { return XInlet.InputOutlet; }
            set { XInlet.LinkTo(value); }
        }

        public Inlet XInlet => OperatorHelper.GetInlet(WrappedOperator, X_INDEX);

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceFormatter.GetDisplayName(() => X);
            return name;
        }
    }
}

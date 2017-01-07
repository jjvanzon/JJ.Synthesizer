using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithResult : OperatorWrapperBase_WithSingleOutlet
    {
        private const int RESULT_INDEX = 0;

        public OperatorWrapperBase_WithResult(Operator op)
            : base(op)
        { }

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, RESULT_INDEX);

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex != 0) throw new NotEqualException(() => listIndex, 0);

            string name = ResourceHelper.GetPropertyDisplayName(() => Result);
            return name;
        }
    }
}

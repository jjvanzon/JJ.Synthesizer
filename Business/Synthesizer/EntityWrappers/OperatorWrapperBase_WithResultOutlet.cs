using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithResultOutlet : OperatorWrapperBase_WithOneOutlet
    {
        public OperatorWrapperBase_WithResultOutlet(Operator op)
            : base(op)
        { }

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, DimensionEnum.Result);
    }
}

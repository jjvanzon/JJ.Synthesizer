using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithNumberOutlet : OperatorWrapperBase_WithOneOutlet
    {
        public OperatorWrapperBase_WithNumberOutlet(Operator op)
            : base(op)
        { }

        public Outlet NumberOutlet => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.Number);
    }
}

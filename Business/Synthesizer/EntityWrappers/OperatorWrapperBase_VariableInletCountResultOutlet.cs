using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_VariableInletCountResultOutlet : OperatorWrapperBase_VariableInletCountOneOutlet
    {
        public OperatorWrapperBase_VariableInletCountResultOutlet(Operator op)
            : base(op)
        { }

        public Outlet Result => OperatorHelper.GetOutlet(WrappedOperator, DimensionEnum.Number);
    }
}
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithNumberInletAndResultOutlet : OperatorWrapperBase_WithResultOutlet
    {
        public OperatorWrapperBase_WithNumberInletAndResultOutlet(Operator op)
            : base(op)
        { }

        public Outlet Number
        {
            get => NumberInlet.InputOutlet;
            set => NumberInlet.LinkTo(value);
        }

        public Inlet NumberInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Number);
    }
}

using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithNumberInletAndNumberOutlet : OperatorWrapperBase_WithNumberOutlet
    {
        public OperatorWrapperBase_WithNumberInletAndNumberOutlet(Operator op)
            : base(op)
        { }

        public Outlet NumberInput
        {
            get => NumberInlet.InputOutlet;
            set => NumberInlet.LinkTo(value);
        }

        public Inlet NumberInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Number);
    }
}

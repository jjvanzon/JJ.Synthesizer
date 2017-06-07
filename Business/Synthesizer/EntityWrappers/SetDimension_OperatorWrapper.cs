using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SetDimension_OperatorWrapper : OperatorWrapperBase_WithOneOutlet
    {
        public SetDimension_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet PassThroughInput
        {
            get => PassThroughInlet.InputOutlet;
            set => PassThroughInlet.LinkTo(value);
        }

        public Inlet PassThroughInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.PassThrough);

        public Outlet Number
        {
            get => NumberInlet.InputOutlet;
            set => NumberInlet.LinkTo(value);
        }

        public Inlet NumberInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Number);

        public Outlet PassThroughOutlet => InletOutletSelector.GetOutlet(WrappedOperator, DimensionEnum.PassThrough);
    }
}
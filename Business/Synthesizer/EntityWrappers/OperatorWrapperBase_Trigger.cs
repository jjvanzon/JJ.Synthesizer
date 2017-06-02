using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_Trigger : OperatorWrapperBase_WithOneOutlet
    {
        public OperatorWrapperBase_Trigger(Operator op)
            : base(op)
        { }

        public Outlet PassThroughInput
        {
            get => PassThroughInlet.InputOutlet;
            set => PassThroughInlet.LinkTo(value);
        }

        public Inlet PassThroughInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.PassThrough);

        public Outlet Reset
        {
            get => ResetInlet.InputOutlet;
            set => ResetInlet.LinkTo(value);
        }

        public Inlet ResetInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Reset);

        public Outlet PassThroughOutlet => OperatorHelper.GetOutlet(WrappedOperator, DimensionEnum.PassThrough);
    }
}
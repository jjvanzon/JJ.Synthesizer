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

        public Inlet PassThroughInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.PassThrough);

        public Outlet X
        {
            get => XInlet.InputOutlet;
            set => XInlet.LinkTo(value);
        }

        public Inlet XInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.X);

        public Outlet PassThroughOutlet => OperatorHelper.GetOutlet(WrappedOperator, DimensionEnum.PassThrough);
    }
}
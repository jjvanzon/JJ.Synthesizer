using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public abstract class OperatorWrapperBase_WithXAndResult : OperatorWrapperBase_WithResultOutlet
    {
        public OperatorWrapperBase_WithXAndResult(Operator op)
            : base(op)
        { }

        public Outlet X
        {
            get => XInlet.InputOutlet;
            set => XInlet.LinkTo(value);
        }

        public Inlet XInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.X);
    }
}

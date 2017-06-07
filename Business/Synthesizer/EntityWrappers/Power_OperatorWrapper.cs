using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Power_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
    {
        public Power_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Base
        {
            get => BaseInlet.InputOutlet;
            set => BaseInlet.LinkTo(value);
        }

        public Inlet BaseInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Base);

        public Outlet Exponent
        {
            get => ExponentInlet.InputOutlet;
            set => ExponentInlet.LinkTo(value);
        }

        public Inlet ExponentInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Exponent);
    }
}
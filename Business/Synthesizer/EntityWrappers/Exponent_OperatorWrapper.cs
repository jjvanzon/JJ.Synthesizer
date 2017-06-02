using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Exponent_OperatorWrapper : OperatorWrapperBase_WithResultOutlet
    {
        public Exponent_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Low
        {
            get => LowInlet.InputOutlet;
            set => LowInlet.LinkTo(value);
        }

        public Inlet LowInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Low);

        public Outlet High
        {
            get => HighInlet.InputOutlet;
            set => HighInlet.LinkTo(value);
        }

        public Inlet HighInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.High);

        public Outlet Ratio
        {
            get => RatioInlet.InputOutlet;
            set => RatioInlet.LinkTo(value);
        }

        public Inlet RatioInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Ratio);
    }
}

using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Random_OperatorWrapper : OperatorWrapperBase_WithSignalOutlet
    {
        public Random_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Rate
        {
            get => RateInlet.InputOutlet;
            set => RateInlet.LinkTo(value);
        }

        public Inlet RateInlet => OperatorHelper.GetInlet(WrappedOperator, DimensionEnum.Rate);

        public ResampleInterpolationTypeEnum InterpolationType
        {
            get => DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, nameof(InterpolationType));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(InterpolationType), value);
        }
    }
}

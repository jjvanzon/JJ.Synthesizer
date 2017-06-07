using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Interpolate_OperatorWrapper : OperatorWrapperBase_WithNumberOutlet
    {
        public Interpolate_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get => SignalInlet.InputOutlet;
            set => SignalInlet.LinkTo(value);
        }

        public Inlet SignalInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.Signal);

        public Outlet SamplingRate
        {
            get => SamplingRateInlet.InputOutlet;
            set => SamplingRateInlet.LinkTo(value);
        }

        public Inlet SamplingRateInlet => InletOutletSelector.GetInlet(WrappedOperator, DimensionEnum.SamplingRate);

        public ResampleInterpolationTypeEnum InterpolationType
        {
            get => DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, nameof(InterpolationType));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(InterpolationType), value);
        }
    }
}

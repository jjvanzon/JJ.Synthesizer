using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Interpolate_OperatorWrapper : OperatorWrapper
    {
        public Interpolate_OperatorWrapper(Operator op)
            : base(op)
        { }

        public ResampleInterpolationTypeEnum InterpolationType
        {
            get => DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, nameof(InterpolationType));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(InterpolationType), value);
        }
    }
}

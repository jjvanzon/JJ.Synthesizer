using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_WithInterpolation : OperatorWrapper
    {
        public OperatorWrapper_WithInterpolation(Operator op)
            : base(op)
        { }

        public InterpolationTypeEnum InterpolationType
        {
            get => DataPropertyParser.GetEnum<InterpolationTypeEnum>(WrappedOperator, nameof(InterpolationType));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(InterpolationType), value);
        }
    }
}

using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapperBase_WithDimensionAndResampleInterpolationType : OperatorWrapperBase_WithDimension
    {
        public OperatorWrapperBase_WithDimensionAndResampleInterpolationType(Operator op)
            : base(op)
        { }

        public ResampleInterpolationTypeEnum InterpolationType
        {
            get { return DataPropertyParser.GetEnum<ResampleInterpolationTypeEnum>(WrappedOperator, PropertyNames.InterpolationType); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.InterpolationType, value); }
        }
    }
}
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Triangle_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Triangle_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Triangle,
                new DimensionEnum[] { DimensionEnum.Frequency, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }
    }
}
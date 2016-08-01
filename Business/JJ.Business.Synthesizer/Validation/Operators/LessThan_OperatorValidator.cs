using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class LessThan_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public LessThan_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.LessThan,
                  new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new DimensionEnum[] { DimensionEnum.Undefined })
        { }

        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();

            base.Execute();
        }
    }
}

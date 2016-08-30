using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class And_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public And_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.And,
                  new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new DimensionEnum[] { DimensionEnum.Undefined })
        { }

        protected override void Execute()
        {
            ExecuteValidator(new OperatorValidator_NoDimension(Object));

            base.Execute();
        }
    }
}

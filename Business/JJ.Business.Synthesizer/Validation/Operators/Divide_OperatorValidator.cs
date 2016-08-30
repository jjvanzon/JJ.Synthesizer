using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Divide_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Divide_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Divide,
                  new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                  new DimensionEnum[] { DimensionEnum.Undefined })
        { }

        protected override void Execute()
        {
            ExecuteValidator(new OperatorValidator_NoDimension(Object));

            base.Execute();
        }
    }
}

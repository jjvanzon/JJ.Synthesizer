using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Or_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public Or_OperatorValidator(Operator obj)
            : base(
                obj, 
                OperatorTypeEnum.Or,
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

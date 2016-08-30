using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class HighShelfFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public HighShelfFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.HighShelfFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal })
        { }

        protected override void Execute()
        {
            ExecuteValidator(new OperatorValidator_NoDimension(Object));

            base.Execute();
        }
    }
}
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PeakingEQFilter_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public PeakingEQFilter_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.PeakingEQFilter,
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
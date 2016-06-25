using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverDimension_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public ClosestOverDimension_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.ClosestOverDimension,
                  expectedInletCount: 5,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.Dimension, PropertyNames.Recalculation })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string recalculationString = DataPropertyParser.TryGetString(op, PropertyNames.Recalculation);
                For(() => recalculationString, PropertyNames.Recalculation)
                    .NotNullOrEmpty()
                    .IsEnum<AggregateRecalculationEnum>()
                    .IsNot(AggregateRecalculationEnum.Undefined);
            }
        }
    }
}

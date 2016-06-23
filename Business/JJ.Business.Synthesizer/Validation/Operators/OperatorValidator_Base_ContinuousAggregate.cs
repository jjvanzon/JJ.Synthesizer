using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Base_ContinuousAggregate : OperatorValidator_Base
    {
        public OperatorValidator_Base_ContinuousAggregate(Operator obj, OperatorTypeEnum operatorTypeEnum)
            : base(
                  obj,
                  operatorTypeEnum, 
                  expectedInletCount: 4,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] 
                  {
                      PropertyNames.Dimension,
                      PropertyNames.Recalculation
                  })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                // Dimension can be Undefined, but key must exist.
                string dimensionString = DataPropertyParser.TryGetString(op, PropertyNames.Dimension);
                For(() => dimensionString, PropertyNames.Dimension)
                    .NotNullOrEmpty()
                    .IsEnum<DimensionEnum>();

                string recalculationString = DataPropertyParser.TryGetString(op, PropertyNames.Recalculation);
                For(() => recalculationString, PropertyNames.Recalculation)
                    .NotNullOrEmpty()
                    .IsEnum<AggregateRecalculationEnum>()
                    .IsNot(AggregateRecalculationEnum.Undefined);
            }
        }
    }
}

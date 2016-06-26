using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Base_AggregateOverDimension : OperatorValidator_Base_WithDimension
    {
        public OperatorValidator_Base_AggregateOverDimension(Operator obj, OperatorTypeEnum operatorTypeEnum)
            : base(
                  obj,
                  operatorTypeEnum,
                  expectedInletCount: 4,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.Dimension, PropertyNames.CollectionRecalculation })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string recalculationRecalculationString = DataPropertyParser.TryGetString(op, PropertyNames.CollectionRecalculation);
                For(() => recalculationRecalculationString, PropertyNames.CollectionRecalculation)
                    .NotNullOrEmpty()
                    .IsEnum<CollectionRecalculationEnum>()
                    .IsNot(CollectionRecalculationEnum.Undefined);
            }
        }
    }
}
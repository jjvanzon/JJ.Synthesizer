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
                  allowedDataKeys: new string[] { PropertyNames.Dimension, PropertyNames.CollectionRecalculation })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string collectionRecalculationString = DataPropertyParser.TryGetString(op, PropertyNames.CollectionRecalculation);
                For(() => collectionRecalculationString, PropertyNames.CollectionRecalculation)
                    .NotNullOrEmpty()
                    .IsEnum<CollectionRecalculationEnum>()
                    .IsNot(CollectionRecalculationEnum.Undefined);
            }
        }
    }
}

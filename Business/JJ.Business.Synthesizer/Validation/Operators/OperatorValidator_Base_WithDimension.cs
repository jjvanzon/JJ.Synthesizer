using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Base_WithDimension : OperatorValidator_Base
    {
        public OperatorValidator_Base_WithDimension(
            Operator obj,
            OperatorTypeEnum expectedOperatorTypeEnum,
            int expectedInletCount,
            int expectedOutletCount)
            : base(
                  obj,
                  expectedOperatorTypeEnum,
                  expectedInletCount,
                  expectedOutletCount,
                  allowedDataKeys: new string[] { PropertyNames.Dimension })
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            if (DataPropertyParser.DataIsWellFormed(op))
            {
                string dimensionString = DataPropertyParser.TryGetString(op, PropertyNames.Dimension);
                For(() => dimensionString, PropertyNames.Dimension)
                    .NotNullOrEmpty()
                    .IsEnum<DimensionEnum>()
                    .IsNot(DimensionEnum.Undefined);
            }
        }
    }
}

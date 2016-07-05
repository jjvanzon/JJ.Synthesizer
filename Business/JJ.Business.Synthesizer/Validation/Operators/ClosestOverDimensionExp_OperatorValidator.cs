using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DataProperty;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverDimensionExp_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public ClosestOverDimensionExp_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.ClosestOverDimensionExp,
                  expectedInletCount: 5,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.Dimension, PropertyNames.CollectionRecalculation })
        { }

        protected override void Execute()
        {
            base.Execute();

            Execute(new CollectionRecalculation_DataProperty_Validator(Object.Data));
        }
    }
}

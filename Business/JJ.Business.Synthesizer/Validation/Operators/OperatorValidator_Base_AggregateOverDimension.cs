using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DataProperty;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Base_AggregateOverDimension : OperatorValidator_Base
    {
        public OperatorValidator_Base_AggregateOverDimension(Operator obj, OperatorTypeEnum operatorTypeEnum)
            : base(
                obj,
                operatorTypeEnum,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined },
                expectedDataKeys: new string[] { PropertyNames.CollectionRecalculation })
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new CollectionRecalculation_DataProperty_Validator(Object.Data));
        }
    }
}
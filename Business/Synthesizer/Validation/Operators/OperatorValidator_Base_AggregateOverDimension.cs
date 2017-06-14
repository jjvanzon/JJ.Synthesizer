using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OperatorValidator_Base_AggregateOverDimension : OperatorValidator_Base_WithOperatorType
    {
        public OperatorValidator_Base_AggregateOverDimension(Operator obj, OperatorTypeEnum operatorTypeEnum)
            : base(
                obj,
                operatorTypeEnum,
                new[] { DimensionEnum.Signal, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Signal },
                expectedDataKeys: new[] { nameof(OperatorWrapperBase_AggregateOverDimension.CollectionRecalculation) })
        { 
            ExecuteValidator(new CollectionRecalculation_DataProperty_Validator(obj.Data));
        }
    }
}
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverDimensionExp_OperatorValidator : OperatorValidator_Base
    {
        public ClosestOverDimensionExp_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.ClosestOverDimensionExp,
                new[] { DimensionEnum.Input, DimensionEnum.Collection, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Number },
                expectedDataKeys: new[] { nameof(ClosestOverDimensionExp_OperatorWrapper.CollectionRecalculation) })
        { 
            ExecuteValidator(new CollectionRecalculation_DataProperty_Validator(obj.Data));
        }
    }
}

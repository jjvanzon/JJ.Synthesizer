using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverDimension_OperatorValidator : OperatorValidator_Base
    {
        public ClosestOverDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.ClosestOverDimension,
                new[] { DimensionEnum.Input, DimensionEnum.Collection, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Result },
                expectedDataKeys: new[] { nameof(ClosestOverDimension_OperatorWrapper.CollectionRecalculation) })
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new CollectionRecalculation_DataProperty_Validator(Obj.Data));
        }
    }
}

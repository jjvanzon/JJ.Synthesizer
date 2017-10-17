using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AggregateOverDimension_OperatorValidator : OperatorValidator_Basic
    {
        public AggregateOverDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                expectedDataKeys: new[] { nameof(OperatorWrapper_WithCollectionRecalculation.CollectionRecalculation) })
        {
            ExecuteValidator(new CollectionRecalculation_DataProperty_Validator(obj.Data));
        }
    }
}
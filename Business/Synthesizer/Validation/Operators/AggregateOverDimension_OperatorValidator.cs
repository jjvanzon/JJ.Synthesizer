using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class AggregateOverDimension_OperatorValidator : OperatorValidator_Basic
    {
        public AggregateOverDimension_OperatorValidator(Operator op)
            : base(
                op,
                expectedDataKeys: new[] { nameof(OperatorWrapper_WithCollectionRecalculation.CollectionRecalculation) })
        {
            ExecuteValidator(new CollectionRecalculation_DataProperty_Validator(op.Data));
            ExecuteValidator(new OperatorValidator_CurveAndSampleAreNull(op));
        }
    }
}
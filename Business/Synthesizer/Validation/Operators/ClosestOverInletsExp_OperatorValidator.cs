using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class ClosestOverInletsExp_OperatorValidator : OperatorValidator_Base_ClosestOverInlets
    {
        public ClosestOverInletsExp_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.ClosestOverInletsExp)
        { }
    }
}
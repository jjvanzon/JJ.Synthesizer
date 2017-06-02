using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Multiply_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Multiply_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Multiply, DimensionEnum.Result, expectedDataKeys: new string[0])
        { }
    }
}
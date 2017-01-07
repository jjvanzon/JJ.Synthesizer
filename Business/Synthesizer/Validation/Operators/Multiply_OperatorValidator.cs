using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Multiply_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Multiply_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Multiply, expectedDataKeys: new string[0])
        { }
    }
}
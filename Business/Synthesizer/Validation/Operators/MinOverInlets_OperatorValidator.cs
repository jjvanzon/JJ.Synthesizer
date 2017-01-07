using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MinOverInlets_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public MinOverInlets_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.MinOverInlets, expectedDataKeys: new string[0])
        { }
    }
}
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Add_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public Add_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.Add, DimensionEnum.Number, expectedDataKeys: new string[0])
        { }
    }
}
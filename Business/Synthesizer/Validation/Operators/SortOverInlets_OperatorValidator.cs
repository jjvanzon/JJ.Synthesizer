using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SortOverInlets_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public SortOverInlets_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.SortOverInlets, expectedDataKeys: new string[0])
        { }
    }
}
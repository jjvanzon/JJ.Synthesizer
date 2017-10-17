using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Reset_OperatorValidator : OperatorValidator_Basic
    {
        public Reset_OperatorValidator(Operator obj)
            : base(obj, expectedDataKeys: new[] { nameof(Reset_OperatorWrapper.Position) })
        {
            ExecuteValidator(new Position_DataProperty_Validator(obj.Data));
        }
    }
}
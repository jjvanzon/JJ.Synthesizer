using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Interpolate_OperatorValidator : OperatorValidator
    {
        public Interpolate_OperatorValidator(Operator obj)
            : base(
                obj,
                expectedDataKeys: new[] { nameof(Interpolate_OperatorWrapper.InterpolationType) })
        { 
            ExecuteValidator(new ResampleInterpolationType_DataProperty_Validator(obj.Data));
        }
    }
}
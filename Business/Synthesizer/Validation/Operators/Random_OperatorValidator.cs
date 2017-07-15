using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Random_OperatorValidator : OperatorValidator_WithUnderlyingPatch
    {
        public Random_OperatorValidator(Operator obj)
            : base(
                obj,
                expectedDataKeys: new[] { nameof(Random_OperatorWrapper.InterpolationType) })
        { 
            ExecuteValidator(new ResampleInterpolationType_DataProperty_Validator(obj.Data));
        }
    }
}
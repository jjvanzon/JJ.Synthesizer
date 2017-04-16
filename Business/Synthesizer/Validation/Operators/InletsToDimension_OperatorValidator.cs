using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class InletsToDimension_OperatorValidator : OperatorValidator_Base_VariableInletCountOneOutlet
    {
        public InletsToDimension_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.InletsToDimension,
                expectedDataKeys: new[] { PropertyNames.InterpolationType })
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new ResampleInterpolationType_DataProperty_Validator(Obj.Data));
        }
    }
}
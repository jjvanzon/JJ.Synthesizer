using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Interpolate_OperatorValidator : OperatorValidator_Base
    {
        public Interpolate_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.Interpolate,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal },
                expectedDataKeys: new[] { PropertyNames.InterpolationType })
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new ResampleInterpolationType_DataProperty_Validator(Obj.Data));
        }
    }
}
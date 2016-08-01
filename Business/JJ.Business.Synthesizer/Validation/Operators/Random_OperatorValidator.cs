using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation.DataProperty;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Random_OperatorValidator : OperatorValidator_Base
    {
        public Random_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.Random,
                expectedDataKeys: new string[] { PropertyNames.InterpolationType },
                expectedInletCount: 2,
                expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            ExecuteValidator(new ResampleInterpolationType_DataProperty_Validator(Object.Data));
        }
    }
}
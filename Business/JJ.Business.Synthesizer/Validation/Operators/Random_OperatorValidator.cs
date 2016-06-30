using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation.OperatorData;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Random_OperatorValidator : OperatorValidator_Base_WithDimension
    {
        public Random_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.Random,
                  expectedInletCount: 2,
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.InterpolationType, PropertyNames.Dimension })
        { }

        protected override void Execute()
        {
            base.Execute();

            Execute(new ResampleInterpolationType_OperatorData_Validator(Object.Data));
        }
    }
}
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class OneOverX_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public OneOverX_OperatorValidator(Operator obj)
            : base(obj, OperatorTypeEnum.OneOverX, expectedInletCount: 1, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();

            base.Execute();
        }
    }
}

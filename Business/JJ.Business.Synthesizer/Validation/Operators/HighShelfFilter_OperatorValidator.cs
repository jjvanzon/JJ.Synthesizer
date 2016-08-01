using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class HighShelfFilter_OperatorValidator : OperatorValidator_Base
    {
        public HighShelfFilter_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.HighShelfFilter,
                expectedDataKeys: new string[0],
                expectedInletCount: 4,
                expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();

            base.Execute();
        }
    }
}
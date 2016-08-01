using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Reset_OperatorValidator : OperatorValidator_Base
    {
        public Reset_OperatorValidator(Operator obj)
            : base(
                  obj,
                OperatorTypeEnum.Reset,
                expectedDataKeys: new string[] { PropertyNames.ListIndex },
                expectedInletCount: 1,
                expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            For(() => Object.Dimension, PropertyDisplayNames.Dimension).IsNull();

            base.Execute();
        }
    }
}
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Number : OperatorValidator_Base
    {
        public OperatorValidator_Number(Operator obj)
            : base(obj, OperatorTypeEnum.Number, expectedInletCount: 0, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Data, PropertyDisplayNames.Number)
                .NotNullOrEmpty()
                .IsDouble();
        }
    }
}

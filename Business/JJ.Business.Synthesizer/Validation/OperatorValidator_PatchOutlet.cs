using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_PatchOutlet : OperatorValidator_Base
    {
        public OperatorValidator_PatchOutlet(Operator obj)
            : base(obj, OperatorTypeEnum.PatchOutlet, expectedInletCount: 1, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Name, CommonTitles.Name).NotNullOrEmpty();

            For(() => Object.Data, PropertyDisplayNames.ListIndex)
                .NotNullOrEmpty()
                .IsInteger();
        }
    }
}

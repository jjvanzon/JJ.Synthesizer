using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_PatchInlet : OperatorValidator_Base
    {
        public OperatorValidator_PatchInlet(Operator obj)
            : base(obj, OperatorTypeEnum.PatchInlet, 1, PropertyNames.Input, PropertyNames.Result)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Name, CommonTitles.Name).NotNullOrEmpty();

            For(() => Object.Data, PropertyDisplayNames.SortOrder)
                .NotNullOrEmpty()
                .IsInteger();
        }
    }
}

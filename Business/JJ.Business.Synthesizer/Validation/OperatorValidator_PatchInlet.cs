using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_PatchInlet : OperatorValidator_Base
    {
        public OperatorValidator_PatchInlet(Operator obj)
            : base(obj, OperatorTypeEnum.PatchInlet, expectedInletCount: 1, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            var patchInletWrapper = new PatchInlet_OperatorWrapper(Object);
            Inlet patchInletInlet = patchInletWrapper.Inlet;

            For(() => Object.Name, CommonTitles.Name).NotNullOrEmpty();
            For(() => patchInletInlet.ListIndex, PropertyDisplayNames.ListIndex).NotNull().GreaterThanOrEqual(0);
            For(() => patchInletInlet.GetInletTypeEnum(), PropertyDisplayNames.InletType).IsEnum<InletTypeEnum>();
        }
    }
}

using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_PatchOutlet : OperatorValidator_Base
    {
        public OperatorValidator_PatchOutlet(Operator obj)
            : base(obj, OperatorTypeEnum.PatchOutlet, expectedInletCount: 1, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            var patchOutletWrapper = new PatchOutlet_OperatorWrapper(Object);

            For(() => patchOutletWrapper.ListIndex, PropertyDisplayNames.ListIndex).NotNull().GreaterThanOrEqual(0);
            For(() => patchOutletWrapper.Result.GetOutletTypeEnum(), PropertyDisplayNames.OutletType).IsEnum<OutletTypeEnum>();

            // PatchOutlet.Result.OutletType is optional.
            // patchOutlet.Name is optional.
        }
    }
}

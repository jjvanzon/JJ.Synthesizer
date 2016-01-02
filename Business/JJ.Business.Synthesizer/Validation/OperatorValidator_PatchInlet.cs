using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;

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

            var wrapper = new PatchInlet_OperatorWrapper(Object);

            For(() => Object.Name, CommonTitles.Name).NotNullOrEmpty();
            For(() => wrapper.ListIndex, PropertyDisplayNames.ListIndex).NotNull().GreaterThanOrEqual(0);
            For(() => wrapper.InletTypeEnum, PropertyDisplayNames.InletType).IsEnum<InletTypeEnum>();

            Inlet inlet = wrapper.Inlet;
            Execute(new OperatorValidator_PatchInlet_Inlet(inlet), ValidationHelper.GetMessagePrefix(inlet));
        }
    }
}

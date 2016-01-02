using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_PatchInlet_Inlet : FluentValidator<Inlet>
    {
        public OperatorValidator_PatchInlet_Inlet(Inlet obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            var wrapper = new PatchInlet_OperatorWrapper(Object.Operator);

            Inlet inlet = wrapper.Inlet;

            // TODO: Use clearer validation messages texts.
            For(() => inlet.DefaultValue, PropertyDisplayNames.DefaultValue).Is(wrapper.DefaultValue);
            For(() => inlet.GetInletTypeEnum(), PropertyDisplayNames.InletType).Is(wrapper.InletTypeEnum);
        }
    }
}

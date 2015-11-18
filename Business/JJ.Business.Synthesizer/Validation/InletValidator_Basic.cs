using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class InletValidator_Basic : FluentValidator<Inlet>
    {
        private readonly int _expectedListIndex;

        public InletValidator_Basic(Inlet obj, int expectedListIndex)
            : base(obj, postponeExecute: true)
        {
            _expectedListIndex = expectedListIndex;

            Execute();
        }

        protected override void Execute()
        {
            // TODO: Enable this code line again, after making sure that the UI does not keep on generating false validation messages.
            //For(() => Object.ListIndex, PropertyDisplayNames.ListIndex).Is(_expectedListIndex);

            For(() => Object.GetInletTypeEnum(), PropertyDisplayNames.InletType).IsEnum<InletTypeEnum>();
        }
    }
}

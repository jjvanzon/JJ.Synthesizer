using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Basic_ScaleValidator : VersatileValidator<Scale>
    {
        public Basic_ScaleValidator(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object.BaseFrequency.HasValue)
            {
                For(() => Object.BaseFrequency, PropertyDisplayNames.BaseFrequency).NotNaN().NotInfinity().GreaterThan(0);
            }

            For(() => Object.ScaleType, PropertyDisplayNames.ScaleType).NotNull();
        }
    }
}

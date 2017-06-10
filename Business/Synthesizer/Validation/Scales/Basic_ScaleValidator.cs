using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Basic_ScaleValidator : VersatileValidator<Scale>
    {
        public Basic_ScaleValidator(Scale obj)
            : base(obj)
        { 
            if (obj.BaseFrequency.HasValue)
            {
                For(() => obj.BaseFrequency, ResourceFormatter.BaseFrequency).NotNaN().NotInfinity().GreaterThan(0);
            }

            For(() => obj.ScaleType, ResourceFormatter.ScaleType).NotNull();
        }
    }
}

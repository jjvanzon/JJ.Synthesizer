using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ToneValidator : VersatileValidator<Tone>
    {
        public ToneValidator(Tone obj)
            : base(obj)
        { 
            For(() => obj.Scale, ResourceFormatter.Scale).NotNull();
            For(() => obj.Number, ResourceFormatter.Number).NotNaN().NotInfinity();
        }
    }
}

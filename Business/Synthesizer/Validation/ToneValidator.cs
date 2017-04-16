using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ToneValidator : VersatileValidator<Tone>
    {
        public ToneValidator(Tone obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.Scale, ResourceFormatter.Scale).NotNull();
            For(() => Obj.Number, ResourceFormatter.Number).NotNaN().NotInfinity();
        }
    }
}

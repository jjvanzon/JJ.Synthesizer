using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
    internal class ScaleWarningValidator : VersatileValidator<Scale>
    {
        public ScaleWarningValidator(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.BaseFrequency, PropertyDisplayNames.BaseFrequency).IsNull();
            For(() => Obj.Tones.Count, PropertyDisplayNames.ToneCount).GreaterThan(0);
        }
    }
}
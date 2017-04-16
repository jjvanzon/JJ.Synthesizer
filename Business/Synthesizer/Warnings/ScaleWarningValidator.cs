using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
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
            For(() => Obj.BaseFrequency, ResourceFormatter.BaseFrequency).IsNull();
            For(() => Obj.Tones.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Tone)).GreaterThan(0);
        }
    }
}
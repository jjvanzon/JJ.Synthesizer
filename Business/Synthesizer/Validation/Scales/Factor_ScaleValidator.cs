using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Factor_ScaleValidator : VersatileValidator<Scale>
    {
        public Factor_ScaleValidator(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.BaseFrequency, PropertyDisplayNames.BaseFrequency).NotNull();
            For(() => Obj.GetScaleTypeEnum(), PropertyDisplayNames.ScaleType).Is(ScaleTypeEnum.Factor);
        }
    }
}

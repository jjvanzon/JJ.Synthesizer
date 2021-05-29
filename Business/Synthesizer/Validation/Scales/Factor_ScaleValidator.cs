using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Factor_ScaleValidator : VersatileValidator
    {
        public Factor_ScaleValidator(Scale obj)
        {
            if (obj == null) throw new NullException(() => obj);

            For(obj.BaseFrequency, ResourceFormatter.BaseFrequency).NotNull();
            For(obj.GetScaleTypeEnum(), ResourceFormatter.ScaleType).Is(ScaleTypeEnum.Factor);
        }
    }
}

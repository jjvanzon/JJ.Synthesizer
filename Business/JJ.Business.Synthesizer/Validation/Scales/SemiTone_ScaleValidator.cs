using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class SemiTone_ScaleValidator : FluentValidator<Scale>
    {
        public SemiTone_ScaleValidator(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.BaseFrequency, PropertyDisplayNames.BaseFrequency).NotNull();
            For(() => Object.GetScaleTypeEnum(), PropertyDisplayNames.ScaleType).Is(ScaleTypeEnum.SemiTone);
        }
    }
}

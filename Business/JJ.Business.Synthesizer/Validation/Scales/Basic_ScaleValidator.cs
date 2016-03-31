using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Basic_ScaleValidator : FluentValidator<Scale>
    {
        public Basic_ScaleValidator(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object.BaseFrequency.HasValue)
            {
                For(() => Object.BaseFrequency, PropertyDisplayNames.BaseFrequency).GreaterThan(0);
            }

            For(() => Object.GetScaleTypeEnum(), PropertyDisplayNames.ScaleType)
                .IsEnum<ScaleTypeEnum>()
                .IsNot(ScaleTypeEnum.Undefined);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ScaleValidator_Basic : FluentValidator<Scale>
    {
        public ScaleValidator_Basic(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Object.BaseFrequency.HasValue)
            {
                For(() => Object.BaseFrequency, PropertyDisplayNames.BaseFrequency).Above(0);
            }

            For(() => Object.GetScaleTypeEnum(), PropertyDisplayNames.ScaleType)
                .IsEnumValue<ScaleTypeEnum>()
                .IsNot(ScaleTypeEnum.Undefined);

            foreach (Tone tone in Object.Tones)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(tone);
                Execute(new ToneValidator(tone), messagePrefix);
            }
        }
    }
}

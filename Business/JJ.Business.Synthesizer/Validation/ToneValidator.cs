using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ToneValidator : FluentValidator<Tone>
    {
        public ToneValidator(Tone obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            // TODO: Adapt other business logic so the poor choice of bounds on the octave and tone numbers is undone.

            For(() => Object.Scale, PropertyDisplayNames.Scale).NotNull();
            //For(() => Object.Octave, PropertyDisplayNames.Octave).MinValue(1);

            bool isSemiTone = Object.Scale != null &&
                              Object.Scale.GetScaleTypeEnum() == ScaleTypeEnum.SemiTones;
            if (isSemiTone)
            {
                //For(() => Object.Number, PropertyDisplayNames.Octave).MinValue(1);
            }
            else
            {
                //For(() => Object.Number, PropertyDisplayNames.Octave).MinValue(0);
            }
        }
    }
}

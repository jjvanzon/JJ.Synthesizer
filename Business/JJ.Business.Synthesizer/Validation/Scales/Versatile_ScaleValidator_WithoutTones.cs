using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Versatile_ScaleValidator_WithoutTones : FluentValidator<Scale>
    {
        public Versatile_ScaleValidator_WithoutTones(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            ScaleTypeEnum scaleTypeEnum = Object.GetScaleTypeEnum();

            Execute(new Basic_ScaleValidator(Object));

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    Execute(new LiteralFrequency_ScaleValidator(Object));
                    break;

                case ScaleTypeEnum.Factor:
                    Execute(new Factor_ScaleValidator(Object));
                    break;

                case ScaleTypeEnum.Exponent:
                    Execute(new Exponent_ScaleValidator(Object));
                    break;

                case ScaleTypeEnum.SemiTone:
                    Execute(new SemiTone_ScaleValidator(Object));
                    break;
            }
        }
    }
}

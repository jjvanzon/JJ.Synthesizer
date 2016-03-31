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

            Execute<Basic_ScaleValidator>();

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    Execute<LiteralFrequency_ScaleValidator>();
                    break;

                case ScaleTypeEnum.Factor:
                    Execute<Factor_ScaleValidator>();
                    break;

                case ScaleTypeEnum.Exponent:
                    Execute<Exponent_ScaleValidator>();
                    break;

                case ScaleTypeEnum.SemiTone:
                    Execute<SemiTone_ScaleValidator>();
                    break;
            }
        }
    }
}

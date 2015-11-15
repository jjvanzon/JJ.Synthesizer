using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ScaleValidator_Versatile_WithoutTones : FluentValidator<Scale>
    {
        public ScaleValidator_Versatile_WithoutTones(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            ScaleTypeEnum scaleTypeEnum = Object.GetScaleTypeEnum();

            Execute<ScaleValidator_Basic>();

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    Execute<ScaleValidator_LiteralFrequency>();
                    break;

                case ScaleTypeEnum.Factor:
                    Execute<ScaleValidator_Factor>();
                    break;

                case ScaleTypeEnum.Exponent:
                    Execute<ScaleValidator_Exponent>();
                    break;

                case ScaleTypeEnum.SemiTone:
                    Execute<ScaleValidator_SemiTone>();
                    break;
            }
        }
    }
}

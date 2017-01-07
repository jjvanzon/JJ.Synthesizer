using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class Versatile_ScaleValidator_WithoutTones : VersatileValidator<Scale>
    {
        public Versatile_ScaleValidator_WithoutTones(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            ScaleTypeEnum scaleTypeEnum = Object.GetScaleTypeEnum();

            ExecuteValidator(new Basic_ScaleValidator(Object));

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    ExecuteValidator(new LiteralFrequency_ScaleValidator(Object));
                    break;

                case ScaleTypeEnum.Factor:
                    ExecuteValidator(new Factor_ScaleValidator(Object));
                    break;

                case ScaleTypeEnum.Exponent:
                    ExecuteValidator(new Exponent_ScaleValidator(Object));
                    break;

                case ScaleTypeEnum.SemiTone:
                    ExecuteValidator(new SemiTone_ScaleValidator(Object));
                    break;
            }
        }
    }
}

using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
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
            ScaleTypeEnum scaleTypeEnum = Obj.GetScaleTypeEnum();

            ExecuteValidator(new Basic_ScaleValidator(Obj));

            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    ExecuteValidator(new LiteralFrequency_ScaleValidator(Obj));
                    break;

                case ScaleTypeEnum.Factor:
                    ExecuteValidator(new Factor_ScaleValidator(Obj));
                    break;

                case ScaleTypeEnum.Exponent:
                    ExecuteValidator(new Exponent_ScaleValidator(Obj));
                    break;

                case ScaleTypeEnum.SemiTone:
                    ExecuteValidator(new SemiTone_ScaleValidator(Obj));
                    break;
            }
        }
    }
}

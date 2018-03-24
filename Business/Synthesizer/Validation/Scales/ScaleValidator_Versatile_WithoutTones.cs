using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
	internal class ScaleValidator_Versatile_WithoutTones : VersatileValidator
	{
		public ScaleValidator_Versatile_WithoutTones(Scale obj)
		{
			if (obj == null) throw new NullException(() => obj);

			ScaleTypeEnum scaleTypeEnum = obj.GetScaleTypeEnum();

			ExecuteValidator(new Basic_ScaleValidator(obj));

			switch (scaleTypeEnum)
			{
				case ScaleTypeEnum.LiteralFrequency:
					ExecuteValidator(new LiteralFrequency_ScaleValidator(obj));
					break;

				case ScaleTypeEnum.Factor:
					ExecuteValidator(new Factor_ScaleValidator(obj));
					break;

				case ScaleTypeEnum.Exponent:
					ExecuteValidator(new Exponent_ScaleValidator(obj));
					break;

				case ScaleTypeEnum.SemiTone:
					ExecuteValidator(new SemiTone_ScaleValidator(obj));
					break;
			}
		}
	}
}

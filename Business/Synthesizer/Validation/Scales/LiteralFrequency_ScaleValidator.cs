using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
	internal class LiteralFrequency_ScaleValidator : VersatileValidator
	{
		public LiteralFrequency_ScaleValidator(Scale obj)
		{
			if (obj == null) throw new NullException(() => obj);

			For(obj.GetScaleTypeEnum(), ResourceFormatter.ScaleType).Is(ScaleTypeEnum.LiteralFrequency);
		}
	}
}

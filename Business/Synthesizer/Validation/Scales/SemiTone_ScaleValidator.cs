using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
	internal class SemiTone_ScaleValidator : VersatileValidator
	{
		public SemiTone_ScaleValidator(Scale obj)
		{
			if (obj == null) throw new NullException(() => obj);

			For(obj.BaseFrequency, ResourceFormatter.BaseFrequency).NotNull();
			For(obj.GetScaleTypeEnum(), ResourceFormatter.ScaleType).Is(ScaleTypeEnum.SemiTone);
		}
	}
}

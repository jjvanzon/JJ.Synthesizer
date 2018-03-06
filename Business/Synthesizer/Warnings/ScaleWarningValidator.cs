using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class ScaleWarningValidator : VersatileValidator
	{
		public ScaleWarningValidator(Scale obj)
		{
			if (obj == null) throw new NullException(() => obj);

			For(obj.BaseFrequency, ResourceFormatter.BaseFrequency).IsNull();
			For(obj.Tones.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Tone)).GreaterThan(0);
		}
	}
}
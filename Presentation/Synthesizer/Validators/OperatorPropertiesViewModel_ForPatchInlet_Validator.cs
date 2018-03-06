using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Validators
{
	internal class OperatorPropertiesViewModel_ForPatchInlet_Validator : VersatileValidator
	{
		public OperatorPropertiesViewModel_ForPatchInlet_Validator(OperatorPropertiesViewModel_ForPatchInlet obj)
		{
			if (obj == null) throw new NullException(() => obj);

			For(obj.DefaultValue, ResourceFormatter.DefaultValue).IsDouble();
		}
	}
}

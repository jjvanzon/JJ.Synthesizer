using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
	internal class PatchValidator_Name : VersatileValidator
	{
		public PatchValidator_Name(Patch obj)
		{
			if (obj == null) throw new NullException(() => obj);

			bool mustValidate = obj.Document != null;
			if (mustValidate)
			{
				ExecuteValidator(new NameValidator(obj.Name));
			}
		}
	}
}

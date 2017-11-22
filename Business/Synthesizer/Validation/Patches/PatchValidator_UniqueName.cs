using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
	internal class PatchValidator_UniqueName : VersatileValidator
	{
		/// <summary>
		/// NOTE:
		/// Do not always execute this validator everywhere,
		/// because then validating a document becomes inefficient.
		/// Extensive document validation will include validating that the Patch names are unique already
		/// and it will do so in a more efficient way.
		/// </summary>
		public PatchValidator_UniqueName(Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);

			if (patch.Document == null)
			{
				return;
			}

			bool isUnique = ValidationHelper.PatchNameIsUnique(patch);
			// ReSharper disable once InvertIf
			if (!isUnique)
			{
				Messages.AddNotUniqueMessageSingular(CommonResourceFormatter.Name, patch.Name);
			}
		}
	}
}

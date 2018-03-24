using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class AudioFileOutputValidator_InDocument : VersatileValidator
	{
		public AudioFileOutputValidator_InDocument(AudioFileOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			For(entity.Document, ResourceFormatter.Document).NotNull();
			For(entity.FilePath, ResourceFormatter.FilePath).MaxLength(255);

			ExecuteValidator(new NameValidator(entity.Name));
		}
	}
}
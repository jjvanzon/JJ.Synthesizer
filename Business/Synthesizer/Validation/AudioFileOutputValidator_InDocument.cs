using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator_InDocument : VersatileValidator<AudioFileOutput>
    {
        public AudioFileOutputValidator_InDocument(AudioFileOutput entity)
            : base(entity)
        { 
            For(() => entity.Document, ResourceFormatter.Document).NotNull();
            For(() => entity.FilePath, ResourceFormatter.FilePath).MaxLength(255);

            ExecuteValidator(new NameValidator(entity.Name));
        }
    }
}
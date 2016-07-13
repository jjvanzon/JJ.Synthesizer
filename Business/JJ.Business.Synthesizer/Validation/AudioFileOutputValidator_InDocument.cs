using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator_InDocument : FluentValidator<AudioFileOutput>
    {
        public AudioFileOutputValidator_InDocument(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Document, PropertyDisplayNames.Document).NotNull();
            For(() => Object.FilePath, PropertyDisplayNames.FilePath).MaxLength(255);

            ExecuteValidator(new NameValidator(Object.Name));
        }
    }
}
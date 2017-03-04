using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator_InDocument : VersatileValidator<AudioFileOutput>
    {
        public AudioFileOutputValidator_InDocument(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.Document, ResourceFormatter.Document).NotNull();
            For(() => Obj.FilePath, ResourceFormatter.FilePath).MaxLength(255);

            ExecuteValidator(new NameValidator(Obj.Name));
        }
    }
}
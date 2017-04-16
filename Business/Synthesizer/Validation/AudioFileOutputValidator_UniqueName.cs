using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator_UniqueName : VersatileValidator<AudioFileOutput>
    {
        /// <summary>
        /// NOTE:
        /// Do not always execute this validator everywhere,
        /// because then validating a document becomes inefficient.
        /// Extensive document validation will include validating that the AudioFileOutput names are unique already
        /// and it will do so in a more efficient way.
        /// </summary>
        public AudioFileOutputValidator_UniqueName(AudioFileOutput obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Obj.Document == null)
            {
                return;
            }

            bool isUnique = ValidationHelper.AudioFileOutputNameIsUnique(Obj);
            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                ValidationMessages.AddNotUniqueMessageSingular(PropertyNames.Name, CommonResourceFormatter.Name, Obj.Name);
            }
        }
    }
}

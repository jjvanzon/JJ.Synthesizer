using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.ResourceStrings;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class AudioFileOutputValidator_UniqueName : VersatileValidator
    {
        /// <summary>
        /// NOTE:
        /// Do not always execute this validator everywhere,
        /// because then validating a document becomes inefficient.
        /// Extensive document validation will include validating that the AudioFileOutput names are unique already
        /// and it will do so in a more efficient way.
        /// </summary>
        public AudioFileOutputValidator_UniqueName(AudioFileOutput audioFileOutput)
        {
            if (audioFileOutput == null) throw new NullException(() => audioFileOutput);

            if (audioFileOutput.Document == null)
            {
                return;
            }

            bool isUnique = ValidationHelper.AudioFileOutputNameIsUnique(audioFileOutput);
            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                Messages.AddNotUniqueMessageSingular(CommonResourceFormatter.Name, audioFileOutput.Name);
            }
        }
    }
}

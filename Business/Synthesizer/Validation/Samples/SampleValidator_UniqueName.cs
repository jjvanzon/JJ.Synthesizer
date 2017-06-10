using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Samples
{
    internal class SampleValidator_UniqueName : VersatileValidator<Sample>
    {
        /// <summary>
        /// NOTE:
        /// Do not always execute this validator everywhere,
        /// because then validating a document becomes inefficient.
        /// Extensive document validation will include validating that the Sample names are unique already
        /// and it will do so in a more efficient way.
        /// </summary>
        public SampleValidator_UniqueName(Sample obj)
            : base(obj)
        {
            if (obj.Document == null)
            {
                return;
            }

            bool isUnique = ValidationHelper.SampleNameIsUnique(obj);
            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                ValidationMessages.AddNotUniqueMessageSingular(nameof(obj.Name), CommonResourceFormatter.Name, obj.Name);
            }
        }
    }
}

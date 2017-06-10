using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class ScaleValidator_UniqueName : VersatileValidator<Scale>
    {
        /// <summary>
        /// NOTE:
        /// Do not always execute this validator everywhere,
        /// because then validating a document becomes inefficient.
        /// Extensive document validation will include validating that the Scale names are unique already
        /// and it will do so in a more efficient way.
        /// </summary>
        public ScaleValidator_UniqueName(Scale obj)
            : base(obj)
        { 
            if (obj.Document == null)
            {
                return;
            }

            bool isUnique = ValidationHelper.ScaleNameIsUnique(obj);
            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                ValidationMessages.AddNotUniqueMessageSingular(nameof(obj.Name), CommonResourceFormatter.Name, obj.Name);
            }
        }
    }
}

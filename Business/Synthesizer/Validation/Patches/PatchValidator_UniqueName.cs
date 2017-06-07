using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_UniqueName : VersatileValidator<Patch>
    {
        /// <summary>
        /// NOTE:
        /// Do not always execute this validator everywhere,
        /// because then validating a document becomes inefficient.
        /// Extensive document validation will include validating that the Patch names are unique already
        /// and it will do so in a more efficient way.
        /// </summary>
        public PatchValidator_UniqueName(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            if (Obj.Document == null)
            {
                return;
            }

            bool isUnique = ValidationHelper.PatchNameIsUnique(Obj);
            // ReSharper disable once InvertIf
            if (!isUnique)
            {
                ValidationMessages.AddNotUniqueMessageSingular(nameof(Obj.Name), CommonResourceFormatter.Name, Obj.Name);
            }
        }
    }
}

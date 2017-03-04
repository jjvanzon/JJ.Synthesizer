using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
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
                string message = MessageFormatter.NotUnique_WithPropertyName_AndValue(CommonTitlesFormatter.Name, Obj.Name);
                ValidationMessages.Add(PropertyNames.Name, message);
            }
        }
    }
}

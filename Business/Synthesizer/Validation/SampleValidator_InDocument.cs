using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class SampleValidator_InDocument : VersatileValidator<Sample>
    {
        public SampleValidator_InDocument(Sample obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Document, PropertyDisplayNames.Document).NotNull();

            ExecuteValidator(new NameValidator(Object.Name));
            
            // TODO: Consider if more additional constraints need to be enforced in a document e.g. reference constraints.
        }
    }
}
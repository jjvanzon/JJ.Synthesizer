using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Samples
{
    internal class SampleValidator_InDocument : VersatileValidator<Sample>
    {
        public SampleValidator_InDocument(Sample obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Obj.Document, ResourceFormatter.Document).NotNull();

            ExecuteValidator(new NameValidator(Obj.Name));
            
            // TODO: Consider if more additional constraints need to be enforced in a document e.g. reference constraints.
        }
    }
}
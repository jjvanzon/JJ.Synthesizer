using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class SampleValidator_InDocument : FluentValidator<Sample>
    {
        public SampleValidator_InDocument(Sample obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Execute(new SampleValidator(Object));

            Execute(new NameValidator(Object.Name));
            
            // TODO: Consider if more additional constraints need to be enforced in a document e.g. reference constraints.
        }
    }
}
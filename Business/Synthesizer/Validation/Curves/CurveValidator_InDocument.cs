using JetBrains.Annotations;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Curves
{
    internal class CurveValidator_InDocument : VersatileValidator<Curve>
    {
        public CurveValidator_InDocument([NotNull] Curve obj)
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
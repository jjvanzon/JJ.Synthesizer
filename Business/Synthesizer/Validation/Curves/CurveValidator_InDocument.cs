using JetBrains.Annotations;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Curves
{
    internal class CurveValidator_InDocument : VersatileValidator<Curve>
    {
        public CurveValidator_InDocument([NotNull] Curve obj)
            : base(obj)
        { 
            For(() => obj.Document, ResourceFormatter.Document).NotNull();

            ExecuteValidator(new NameValidator(obj.Name));

            // TODO: Consider if more additional constraints need to be enforced in a document e.g. reference constraints. 
        }
    }
}
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class ScaleValidator_InDocument : VersatileValidator<Scale>
    {
        public ScaleValidator_InDocument(Scale obj)
            : base(obj)
        { 
            For(() => obj.Document, ResourceFormatter.Document).NotNull();

            ExecuteValidator(new NameValidator(obj.Name));
        }
    }
}

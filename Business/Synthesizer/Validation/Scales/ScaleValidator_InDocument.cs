using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Scales
{
    internal class ScaleValidator_InDocument : VersatileValidator
    {
        public ScaleValidator_InDocument(Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            For(scale.Document, ResourceFormatter.Document).NotNull();

            ExecuteValidator(new NameValidator(scale.Name));
        }
    }
}

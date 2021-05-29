using JJ.Framework.ResourceStrings;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class IDValidator : VersatileValidator
    {
        public IDValidator(int id) => For(id, CommonResourceFormatter.ID).GreaterThan(0);
    }
}
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal class ToneViewModelValidator : VersatileValidator
    {
        /// <param name="numberPropertyDisplayName">Varies with the ScaleType</param>
        public ToneViewModelValidator(ToneViewModel obj, string numberPropertyDisplayName)
        {
            if (obj == null) throw new NullException(() => obj);

            For(obj.Octave, ResourceFormatter.Octave).IsInteger();
            For(obj.Value, numberPropertyDisplayName).IsDouble();
        }
    }
}

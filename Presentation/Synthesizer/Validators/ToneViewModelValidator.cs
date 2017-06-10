using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal class ToneViewModelValidator : VersatileValidator<ToneViewModel>
    {
        /// <param name="numberPropertyDisplayName">Varies with the ScalType</param>
        public ToneViewModelValidator(ToneViewModel obj, string numberPropertyDisplayName)
            : base(obj)
        {
            For(() => obj.Octave, ResourceFormatter.Octave).IsInteger();
            For(() => obj.Number, numberPropertyDisplayName).IsDouble();
        }
    }
}

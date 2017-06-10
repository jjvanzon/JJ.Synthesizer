using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal class ToneGridEditViewModelValidator : VersatileValidator<ToneGridEditViewModel>
    {
        public ToneGridEditViewModelValidator(ToneGridEditViewModel obj)
            : base(obj)
        { 
            for (int i = 0; i < obj.Tones.Count; i++)
            {
                ToneViewModel toneViewModel = obj.Tones[i];
                string messagePrefix = ValidationHelper.GetMessagePrefix(toneViewModel, i + 1);

                ExecuteValidator(new ToneViewModelValidator(toneViewModel, obj.NumberTitle), messagePrefix);
            }
        }
    }
}

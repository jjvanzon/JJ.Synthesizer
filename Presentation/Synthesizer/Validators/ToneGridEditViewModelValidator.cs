using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal class ToneGridEditViewModelValidator : VersatileValidator<ToneGridEditViewModel>
    {
        public ToneGridEditViewModelValidator(ToneGridEditViewModel obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            for (int i = 0; i < Obj.Tones.Count; i++)
            {
                ToneViewModel toneViewModel = Obj.Tones[i];
                string messagePrefix = ValidationHelper.GetMessagePrefix(toneViewModel, i + 1);

                ExecuteValidator(new ToneViewModelValidator(toneViewModel, Obj.NumberTitle), messagePrefix);
            }
        }
    }
}

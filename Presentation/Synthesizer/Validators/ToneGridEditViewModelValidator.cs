using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
	internal class ToneGridEditViewModelValidator : VersatileValidator
	{
		public ToneGridEditViewModelValidator(ToneGridEditViewModel obj)
		{
			if (obj == null) throw new NullException(() => obj);

			for (var i = 0; i < obj.Tones.Count; i++)
			{
				ToneViewModel toneViewModel = obj.Tones[i];
				string messagePrefix = ValidationHelper.GetMessagePrefix(toneViewModel, i + 1);

				ExecuteValidator(new ToneViewModelValidator(toneViewModel, obj.ValueTitle), messagePrefix);
			}
		}
	}
}

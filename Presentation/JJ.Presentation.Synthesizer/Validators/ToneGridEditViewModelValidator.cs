using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal class ToneGridEditViewModelValidator : FluentValidator<ToneGridEditViewModel>
    {
        public ToneGridEditViewModelValidator(ToneGridEditViewModel obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            for (int i = 0; i < Object.Tones.Count; i++)
            {
                ToneViewModel toneViewModel = Object.Tones[i];
                string messagePrefix = ValidationHelper.GetMessagePrefix(toneViewModel, i + 1);

                Execute(new ToneViewModelValidator(toneViewModel, Object.NumberTitle));
            }
        }
    }
}

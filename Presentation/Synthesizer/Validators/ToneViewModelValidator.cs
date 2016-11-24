using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal class ToneViewModelValidator : VersatileValidator<ToneViewModel>
    {
        private readonly string _numberPropertyDisplayName;

        /// <param name="numberPropertyDisplayName">Varies with the ScalType</param>
        public ToneViewModelValidator(ToneViewModel obj, string numberPropertyDisplayName)
            : base(obj, postponeExecute: true)
        {
            _numberPropertyDisplayName = numberPropertyDisplayName;

            Execute();
        }

        protected override void Execute()
        {
            For(() => Object.Octave, PropertyDisplayNames.Octave).IsInteger();
            For(() => Object.Number, _numberPropertyDisplayName).IsDouble();
        }
    }
}

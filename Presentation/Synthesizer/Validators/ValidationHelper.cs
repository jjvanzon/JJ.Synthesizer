using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal static class ValidationHelper
    {
        /// <param name="i">1-based</param>
        public static string GetMessagePrefix(ToneViewModel viewModel, int i)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            string messagePrefix = String.Format("{0} {1}: ", PropertyDisplayNames.Tone, i);
            return messagePrefix;
        }
    }
}

using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal static class ValidationHelper
    {
        /// <param name="i">1-based</param>
        public static string GetMessagePrefix(ToneViewModel viewModel, int i)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            string messagePrefix = $"{ResourceFormatter.Tone} {i}: ";
            return messagePrefix;
        }
    }
}

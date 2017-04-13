using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected virtual void CopyNonPersistedProperties(TViewModel sourceViewModel, TViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages.AddRange(sourceViewModel.ValidationMessages);
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
            destViewModel.RefreshCounter = sourceViewModel.RefreshCounter;
        }
    }
}
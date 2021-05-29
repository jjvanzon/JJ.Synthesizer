using System;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
    internal class PresenterBase<TViewModel>
        where TViewModel : ScreenViewModelBase
    {
        /// <summary>
        /// NOTE: If data read is never edited in this context, we can pretend it is a NonPersistedAction too.
        /// </summary>
        protected virtual void ExecuteNonPersistedAction(TViewModel viewModel, Action action)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            if (action == null) throw new ArgumentNullException(nameof(action));

            action();

            viewModel.RefreshID = RefreshIDProvider.GetRefreshID();
            viewModel.Successful = true;
        }

        protected virtual void CopyNonPersistedProperties(TViewModel sourceViewModel, TViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages.AddRange(sourceViewModel.ValidationMessages);
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
            destViewModel.RefreshID = sourceViewModel.RefreshID;
        }
    }
}
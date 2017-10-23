using System;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
    internal abstract class GridPresenterBase<TViewModel> : PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        public void Show(TViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = true);

        public TViewModel Refresh(TViewModel userInput) => ExecuteAction(userInput, x => { });

        public void Close(TViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);

        protected TViewModel ExecuteAction(TViewModel userInput, Action<TViewModel> action)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            TViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            // Action
            action(viewModel);

            return viewModel;
        }

        protected abstract TViewModel CreateViewModel(TViewModel userInput);
    }
}

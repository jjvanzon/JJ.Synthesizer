using System;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal abstract class GridPresenterBase<TViewModel> : PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        public TViewModel Show(TViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel => viewModel.Visible = true);
        }

        public TViewModel Refresh(TViewModel userInput)
        {
            return TemplateMethod(userInput, x => { });
        }

        public TViewModel Close(TViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel => viewModel.Visible = false);
        }

        private TViewModel TemplateMethod(TViewModel userInput, Action<TViewModel> action)
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

            // Action
            action(viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;

        }

        protected abstract TViewModel CreateViewModel(TViewModel userInput);
    }
}

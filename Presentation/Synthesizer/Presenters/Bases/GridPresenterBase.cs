using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using System;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
    internal abstract class GridPresenterBase<TEntity, TViewModel> : PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected abstract TEntity GetEntity(TViewModel userInput);

        protected abstract TViewModel ToViewModel(TEntity entity);

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

            // ToEntity
            TEntity entity = GetEntity(userInput);

            // ToViewModel
            TViewModel viewModel = ToViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            // Action
            action(viewModel);

            return viewModel;
        }
    }
}

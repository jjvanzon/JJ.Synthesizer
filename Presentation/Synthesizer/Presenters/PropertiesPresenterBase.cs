using System;
using JetBrains.Annotations;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal abstract class PropertiesPresenterBase<TViewModel> : PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected abstract TViewModel CreateViewModel(TViewModel userInput);
        protected abstract TViewModel UpdateEntity(TViewModel userInput);

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
            if (userInput == null) throw new NullException(() => userInput);

            TViewModel viewModel = UpdateEntity(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public TViewModel LoseFocus(TViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            TViewModel viewModel = UpdateEntity(userInput);

            return viewModel;
        }

        /// <summary>
        /// Manages the RefreshCounter, basics around the Successful flag,
        /// Creating a new view model and copying basic non-persisted properties to it.
        /// You can extend this with more logic.
        /// </summary>
        protected TViewModel TemplateMethod([NotNull] TViewModel userInput, [NotNull] Action<TViewModel> action)
        {
            if (userInput == null) throw new NullException(() => userInput);
            if (action == null) throw new NullException(() => action);

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
    }
}

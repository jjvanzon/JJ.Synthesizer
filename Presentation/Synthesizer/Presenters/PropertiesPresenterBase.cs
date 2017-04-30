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

        /// <summary> Not mandatory for read-only views. </summary>
        protected virtual void UpdateEntity(TViewModel viewModel)
        { }

        public TViewModel Show(TViewModel userInput) => TemplateMethod(userInput, viewModel => viewModel.Visible = true);

        public TViewModel Refresh(TViewModel userInput) => TemplateMethod(userInput, x => { });

        public TViewModel LoseFocus(TViewModel userInput) => TemplateMethod(userInput, viewModel => UpdateEntity(viewModel));

        public TViewModel Close(TViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    UpdateEntity(userInput);

                    if (viewModel.Successful)
                    {
                        viewModel.Visible = false;
                    }
                });
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

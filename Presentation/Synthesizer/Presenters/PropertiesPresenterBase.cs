using System;
using JJ.Business.Canonical;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal abstract class PropertiesPresenterBase<TEntity, TViewModel> : PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected abstract TEntity GetEntity(TViewModel userInput);
        protected abstract TViewModel ToViewModel(TEntity entity);

        /// <summary> Base does nothing. Not mandatory to override for read-only views. </summary>
        protected virtual IResult Save(TEntity entity) => null;
        
        /// <summary> Base does nothing. Not mandatory to override for read-only views. </summary>
        protected virtual IResult SaveWithUserInput(TEntity entity, TViewModel userInput)
        {
            return Save(entity);
        }

        public TViewModel Show(TViewModel userInput)
        {
            return TemplateAction(userInput, viewModel => viewModel.Visible = true);
        }

        public TViewModel Refresh(TViewModel userInput)
        {
            return TemplateAction(userInput);
        }

        public TViewModel LoseFocus(TViewModel userInput)
        {
            return TemplateAction(userInput, entity => SaveWithUserInput(entity, userInput));
        }

        public TViewModel Close(TViewModel userInput)
        {
            return TemplateAction(
                userInput,
                entity => SaveWithUserInput(entity, userInput),
                viewModel =>
                {
                    if (viewModel.Successful) viewModel.Visible = false;
                }
            );
        }

        /// <summary>
        /// Manages the RefreshCounter, basics around the Successful flag,
        /// Creating a new view model and copying basic non-persisted properties to it.
        /// You can extend this with more logic.
        /// </summary>
        protected TViewModel TemplateAction(TViewModel userInput, Action<TViewModel> nonPersisted)
        {
            return TemplateAction(userInput, null, nonPersisted);
        }

        /// <summary>
        /// Manages the RefreshCounter, basics around the Successful flag,
        /// Creating a new view model and copying basic non-persisted properties to it.
        /// You can extend this with more logic.
        /// </summary>
        /// <param name="businessDelegate">Can return null.</param>
        protected TViewModel TemplateAction(
            TViewModel userInput,
            Func<TEntity, IResult> businessDelegate = null,
            Action<TViewModel> nonPersistedDelegate = null)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            TEntity entity = GetEntity(userInput);

            // Business
            IResult result = businessDelegate?.Invoke(entity);

            // ToViewModel
            TViewModel viewModel = ToViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            if (result != null)
            {
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
            }

            // Successful?
            viewModel.Successful = result?.Successful ?? true;

            // Non-Persisted
            // (might use viewModel.Successful)
            nonPersistedDelegate?.Invoke(viewModel);

            return viewModel;
        }
    }
}
using JJ.Business.Canonical;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using System;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
    internal abstract class EntityPresenterBase<TEntity, TViewModel> : PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected abstract TEntity GetEntity(TViewModel userInput);

        protected abstract TViewModel ToViewModel(TEntity entity);

        public virtual void Show(TViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = true);

        public virtual TViewModel Refresh(TViewModel userInput) => ExecuteAction(userInput, _ => { });

        /// <see cref="ExecuteAction(TViewModel,Func{TEntity, IResult},Action{TViewModel})"/>
        protected TViewModel ExecuteAction(
            TViewModel userInput, 
            Action<TEntity> businessDelegate = null,
            Action<TViewModel> nonPersistedDelegate = null)
        {
            return ExecuteAction(userInput, x =>
            {
                businessDelegate?.Invoke(x);
                return ResultHelper.Successful;
            }, nonPersistedDelegate);
        }

        /// <summary>
        /// Manages the RefreshCounter, basics around the Successful flag,
        /// Creating a new view model and copying basic non-persisted properties to it.
        /// You can extend this with more logic using the delegates over overrides.
        /// </summary>
        /// <param name="businessDelegate">Can return null.</param>
        protected TViewModel ExecuteAction(
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
                viewModel.ValidationMessages.AddRange(result.Messages);
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
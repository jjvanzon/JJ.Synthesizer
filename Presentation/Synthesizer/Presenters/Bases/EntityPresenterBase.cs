using System;
using JJ.Business.Canonical;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
	internal abstract class EntityPresenterBase<TEntity, TViewModel> : PresenterBase<TViewModel>
		where TViewModel : ScreenViewModelBase
	{
		private static readonly Action<TViewModel> _defaultNonPersistedDelegate = _ => { };
		private static readonly Action<TEntity> _defaultBusinessAction = _ => { };
		private static readonly Func<TEntity, IResult> _defaultBusinessFunc = _ => ResultHelper.Successful;

		protected abstract TEntity GetEntity(TViewModel userInput);

		protected abstract TViewModel ToViewModel(TEntity entity);

		public virtual void Show(TViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = true);

		public virtual TViewModel Refresh(TViewModel userInput) => ExecuteAction(userInput, _defaultBusinessAction);

		/// <see cref="ExecuteAction(TViewModel,Func{TEntity, IResult},Action{TViewModel})"/>
		protected TViewModel ExecuteAction(
			TViewModel userInput,
			Action<TEntity> businessDelegate = null,
			Action<TViewModel> nonPersistedDelegate = null)
		{
			return ExecuteAction(
				userInput,
				x =>
				{
					businessDelegate?.Invoke(x);
					return ResultHelper.Successful;
				},
				nonPersistedDelegate ?? _defaultNonPersistedDelegate);
		}

		protected TViewModel ExecuteAction(
			TViewModel userInput,
			Func<TEntity, IResult> businessDelegate = null,
			Action<TViewModel> nonPersistedDelegate = null)
		{
			
			return ExecuteAction(
				userInput,
				() => GetEntity(userInput),
				x =>
				{
					businessDelegate?.Invoke(x);
					return ResultHelper.Successful;
				},
				nonPersistedDelegate ?? _defaultNonPersistedDelegate);
		}

		protected TViewModel ExecuteAction(
			TViewModel userInput,
			Func<TEntity> getEntityDelegate)
		{
			return ExecuteAction(userInput, getEntityDelegate, _defaultBusinessFunc, _defaultNonPersistedDelegate);
		}

		protected TViewModel ExecuteAction(
			TViewModel userInput,
			Func<TEntity> getEntityDelegate,
			Func<TEntity, IResult> businessDelegate)
		{
			return ExecuteAction(userInput, getEntityDelegate, businessDelegate, _defaultNonPersistedDelegate);
		}

		/// <summary>
		/// Manages the RefreshCounter, basics around the Successful flag,
		/// Creating a new view model and copying basic non-persisted properties to it.
		/// You can extend this with more logic using the delegates over overrides.
		/// </summary>
		/// <param name="businessDelegate">Can return null.</param>
		protected TViewModel ExecuteAction(
			TViewModel userInput,
			Func<TEntity> getEntityDelegate,
			Func<TEntity, IResult> businessDelegate,
			Action<TViewModel> nonPersistedDelegate)
		{
			if (businessDelegate == null) throw new ArgumentNullException(nameof(businessDelegate));
			if (userInput == null) throw new NullException(() => userInput);
			if (getEntityDelegate == null) throw new NullException(() => getEntityDelegate);
			if (nonPersistedDelegate == null) throw new ArgumentNullException(nameof(nonPersistedDelegate));

			// RefreshCounter
			userInput.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			userInput.Successful = false;

			// ToEntity
			TEntity entity = getEntityDelegate();

			// Business
			IResult result = businessDelegate.Invoke(entity);

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
			nonPersistedDelegate.Invoke(viewModel);

			return viewModel;
		}
	}
}
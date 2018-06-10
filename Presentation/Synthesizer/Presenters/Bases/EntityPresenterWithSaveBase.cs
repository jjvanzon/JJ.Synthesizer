using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
	internal abstract class EntityPresenterWithSaveBase<TEntity, TViewModel> 
		: EntityPresenterBase<TEntity, TViewModel>
		where TViewModel : ScreenViewModelBase
	{
		/// <param name="userInput">
		/// Only used sometimes, typically in case validations must be executed on the view model.
		/// Typically, validations are executed on the entity instead.
		/// </param>
		protected abstract IResult Save(TEntity entity, TViewModel userInput);

		public TViewModel LoseFocus(TViewModel userInput) => ExecuteAction(userInput, entity => Save(entity, userInput));

	    public TViewModel Close(TViewModel userInput) => ExecuteAction(
	        userInput,
	        entity => Save(entity, userInput),
	        viewModel =>
	        {
	            if (viewModel.Successful) viewModel.Visible = false;
	        }
	    );
	}
}
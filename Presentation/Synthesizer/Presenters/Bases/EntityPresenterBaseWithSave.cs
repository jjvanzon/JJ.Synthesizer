using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
    internal abstract class EntityPresenterBaseWithSave<TEntity, TViewModel> 
        : EntityPresenterBase<TEntity, TViewModel>
        where TViewModel : ViewModelBase
    {
        /// <summary> Base does nothing. Not mandatory to override for read-only views. </summary>
        protected virtual IResult Save(TEntity entity) => null;
        
        /// <summary> Base delegates to Save, whose base method does nothing. </summary>
        protected virtual IResult SaveWithUserInput(TEntity entity, TViewModel userInput) => Save(entity);

        public TViewModel LoseFocus(TViewModel userInput)
        {
            return ExecuteAction(userInput, entity => SaveWithUserInput(entity, userInput));
        }

        public TViewModel Close(TViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                entity => SaveWithUserInput(entity, userInput),
                viewModel =>
                {
                    if (viewModel.Successful) viewModel.Visible = false;
                }
            );
        }
    }
}
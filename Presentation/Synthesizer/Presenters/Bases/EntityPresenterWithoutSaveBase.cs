using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
	internal abstract class EntityPresenterWithoutSaveBase<TEntity, TViewModel> : EntityPresenterBase<TEntity, TViewModel>
		where TViewModel : ScreenViewModelBase
	{
		public virtual void Close(TViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);
	}
}

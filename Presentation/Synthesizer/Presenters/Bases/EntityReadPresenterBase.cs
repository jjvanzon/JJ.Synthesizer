using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
    internal abstract class EntityReadPresenterBase<TEntity, TViewModel> : EntityPresenterBase<TEntity, TViewModel>
        where TViewModel : ViewModelBase
    {
        public void Close(TViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);
    }
}

using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class TopButtonBarPresenter : PresenterBase<TopButtonBarViewModel>
    {
        public void Refresh(TopButtonBarViewModel viewModel, DocumentTreeNodeTypeEnum selectedNodeType, bool patchDetailsVisible)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.CanAddToInstrument = ToViewModelHelper.GetCanAddToInstrument(selectedNodeType);
                    viewModel.CanCreate = ToViewModelHelper.GetCanCreate(selectedNodeType, patchDetailsVisible);
                    viewModel.CanDelete = ToViewModelHelper.GetCanDelete(selectedNodeType);
                    viewModel.CanOpenExternally = ToViewModelHelper.GetCanOpenExternally(selectedNodeType);
                    viewModel.CanPlay = ToViewModelHelper.GetCanPlay(selectedNodeType);
                });

        protected override void CopyNonPersistedProperties(TopButtonBarViewModel sourceViewModel, TopButtonBarViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.CanAddToInstrument = sourceViewModel.CanAddToInstrument;
            destViewModel.CanCreate = sourceViewModel.CanCreate;
            destViewModel.CanDelete = sourceViewModel.CanDelete;
            destViewModel.CanOpenExternally = sourceViewModel.CanOpenExternally;
            destViewModel.CanPlay = sourceViewModel.CanPlay;
        }
    }
}
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class TopButtonBarPresenter : PresenterBase<TopButtonBarViewModel>
    {
        public void Refresh(TopButtonBarViewModel viewModel, DocumentTreeNodeTypeEnum selectedNodeType, bool documentTreeVisible, bool patchDetailsVisible)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.CanAddToInstrument = ToViewModelHelper.GetCanAddToInstrument(selectedNodeType, documentTreeVisible);
                    viewModel.CanCreate = ToViewModelHelper.GetCanCreate(selectedNodeType, documentTreeVisible, patchDetailsVisible);
                    viewModel.CanClone = ToViewModelHelper.GetCanClone(selectedNodeType, documentTreeVisible);
                    viewModel.CanDelete = ToViewModelHelper.GetCanDelete(selectedNodeType, documentTreeVisible);
                    viewModel.CanOpenExternally = ToViewModelHelper.GetCanOpenExternally(selectedNodeType, documentTreeVisible);
                    viewModel.CanPlay = ToViewModelHelper.GetCanPlay(selectedNodeType, documentTreeVisible);
                });

        protected override void CopyNonPersistedProperties(TopButtonBarViewModel sourceViewModel, TopButtonBarViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.CanAddToInstrument = sourceViewModel.CanAddToInstrument;
            destViewModel.CanCreate = sourceViewModel.CanCreate;
            destViewModel.CanClone = sourceViewModel.CanClone;
            destViewModel.CanDelete = sourceViewModel.CanDelete;
            destViewModel.CanOpenExternally = sourceViewModel.CanOpenExternally;
            destViewModel.CanPlay = sourceViewModel.CanPlay;
        }
    }
}
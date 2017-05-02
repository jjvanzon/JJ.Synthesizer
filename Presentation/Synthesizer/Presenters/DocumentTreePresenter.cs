using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentTreePresenter : PresenterBase<DocumentTreeViewModel>
    {
        private readonly PatchRepositories _repositories;

        public DocumentTreePresenter(PatchRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        public DocumentTreeViewModel Close(DocumentTreeViewModel userInput) => TemplateMethod(userInput, viewModel => viewModel.Visible = false);

        public DocumentTreeViewModel Refresh(DocumentTreeViewModel userInput) => TemplateMethod(userInput, x => { });

        public DocumentTreeViewModel Show(DocumentTreeViewModel userInput) => TemplateMethod(userInput, viewModel => viewModel.Visible = true);

        public DocumentTreeViewModel SelectAudioFileOutputs(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = null;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioFileOutputList;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectAudioOutput(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = null;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioOutput;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectCurves(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = null;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Curves;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectLibraries(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = null;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Libraries;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectLibrary(DocumentTreeViewModel userInput, int id)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Library;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectLibraryPatch(DocumentTreeViewModel userInput, int id)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryPatch;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectSamples(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = null;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Samples;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectScales(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = null;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Scales;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectPatch(DocumentTreeViewModel userInput, int id)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Patch;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        public DocumentTreeViewModel SelectPatchGroup(DocumentTreeViewModel userInput, string group)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedItemID = null;
                    viewModel.SelectedPatchGroup = group;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.PatchGroup;
                    viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
                });
        }

        // Helpers

        private DocumentTreeViewModel TemplateMethod(DocumentTreeViewModel userInput, Action<DocumentTreeViewModel> action)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            DocumentTreeViewModel viewModel = CreateViewModel(userInput);

            // Action
            action(viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        private DocumentTreeViewModel CreateViewModel(DocumentTreeViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.ID);

            // ToViewModel
            var converter = new RecursiveToDocumentTreeViewModelFactory();
            DocumentTreeViewModel viewModel = converter.ToTreeViewModel(document, _repositories);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            viewModel.CanPlay = ViewModelHelper.GetCanPlay(userInput.SelectedNodeType);

            return viewModel;
        }

        protected override void CopyNonPersistedProperties(DocumentTreeViewModel sourceViewModel, DocumentTreeViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.SelectedNodeType = sourceViewModel.SelectedNodeType;
            destViewModel.SelectedItemID = sourceViewModel.SelectedItemID;
            destViewModel.OutletIDToPlay = sourceViewModel.OutletIDToPlay;
            destViewModel.SelectedPatchGroup = sourceViewModel.SelectedPatchGroup;
            destViewModel.CanPlay = sourceViewModel.CanPlay;
        }
    }
}
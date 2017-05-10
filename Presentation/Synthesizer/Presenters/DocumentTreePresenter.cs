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

        public DocumentTreeViewModel Open(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    if (userInput.SelectedNodeType != DocumentTreeNodeTypeEnum.Library)
                    {
                        throw new NotEqualException(() => userInput.SelectedNodeType, DocumentTreeNodeTypeEnum.Library);
                    }

                    if (!userInput.SelectedItemID.HasValue)
                    {
                        throw new NullException(() => userInput.SelectedItemID);
                    }

                    viewModel.LowerDocumentReferenceIDToOpen = userInput.SelectedItemID;
                });
        }

        public DocumentTreeViewModel Refresh(DocumentTreeViewModel userInput) => TemplateMethod(userInput, x => { });

        public DocumentTreeViewModel Show(DocumentTreeViewModel userInput) => TemplateMethod(userInput, viewModel => viewModel.Visible = true);

        public DocumentTreeViewModel SelectAudioFileOutputs(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioFileOutputList;
                });
        }

        public DocumentTreeViewModel SelectAudioOutput(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioOutput;
                });
        }

        public DocumentTreeViewModel SelectCurves(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Curves;
                });
        }

        public DocumentTreeViewModel SelectLibraries(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Libraries;
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
                });
        }

        public DocumentTreeViewModel SelectLibraryPatchGroup(DocumentTreeViewModel userInput, int lowerDocumentReferenceID, string patchGroup)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedPatchGroupLowerDocumentReferenceID = lowerDocumentReferenceID;
                    viewModel.SelectedPatchGroup = patchGroup;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryPatchGroup;
                });
        }

        public DocumentTreeViewModel SelectSamples(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Samples;
                });
        }

        public DocumentTreeViewModel SelectScales(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Scales;
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
                });
        }

        public DocumentTreeViewModel SelectPatchGroup(DocumentTreeViewModel userInput, string group)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    viewModel.SelectedPatchGroup = group;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.PatchGroup;
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

            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.ID);

            // ToViewModel
            var converter = new RecursiveToDocumentTreeViewModelFactory();
            DocumentTreeViewModel viewModel = converter.ToTreeViewModel(document, _repositories);

            // NOTE: Keep split up into two Non-Persisted phases:
            // CopyNonPersisted must be done first, because action will change some of the properties.
            // And the second Non-Persisted phase is dependent on what was don in the action.

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Action
            action(viewModel);

            // Non-Persisted
            viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
            viewModel.CanOpen = ViewModelHelper.GetCanOpen(viewModel.SelectedNodeType);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        protected override void CopyNonPersistedProperties(DocumentTreeViewModel sourceViewModel, DocumentTreeViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.SelectedNodeType = sourceViewModel.SelectedNodeType;
            destViewModel.SelectedItemID = sourceViewModel.SelectedItemID;
            destViewModel.OutletIDToPlay = sourceViewModel.OutletIDToPlay;
            destViewModel.SelectedPatchGroup = sourceViewModel.SelectedPatchGroup;
            destViewModel.SelectedPatchGroupLowerDocumentReferenceID = sourceViewModel.SelectedPatchGroupLowerDocumentReferenceID;
            destViewModel.CanPlay = sourceViewModel.CanPlay;
        }
    }
}
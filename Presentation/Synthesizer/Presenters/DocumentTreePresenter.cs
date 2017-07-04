using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentTreePresenter : PresenterBase<DocumentTreeViewModel>
    {
        private readonly RepositoryWrapper _repositories;

        public DocumentTreePresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
        }

        public DocumentTreeViewModel Close(DocumentTreeViewModel userInput) => TemplateMethod(userInput, viewModel => viewModel.Visible = false);

        public DocumentTreeViewModel OpenItemExternally(DocumentTreeViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    if (!userInput.SelectedItemID.HasValue)
                    {
                        throw new NullException(() => userInput.SelectedItemID);
                    }

                    switch (userInput.SelectedNodeType)
                    {
                        case DocumentTreeNodeTypeEnum.Library:
                            DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(userInput.SelectedItemID.Value);
                            viewModel.DocumentToOpenExternally = documentReference.LowerDocument.ToIDAndName();
                            break;

                        case DocumentTreeNodeTypeEnum.LibraryPatch:
                            Patch patch = _repositories.PatchRepository.Get(userInput.SelectedItemID.Value);
                            viewModel.DocumentToOpenExternally = patch.Document.ToIDAndName();
                            viewModel.PatchToOpenExternally = patch.ToIDAndName();
                            break;

                        default:
                           throw new ValueNotSupportedException(userInput.SelectedNodeType);
                    }
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
                    viewModel.SelectedCanonicalPatchGroup = NameHelper.ToCanonical(patchGroup);
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
                    viewModel.SelectedCanonicalPatchGroup = NameHelper.ToCanonical(group);
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
            var converter = new RecursiveDocumentTreeViewModelFactory();
            DocumentTreeViewModel viewModel = converter.ToTreeViewModel(document);

            // NOTE: Keep split up into two Non-Persisted phases:
            // CopyNonPersisted must be done first, because action will change some of the properties.
            // And the second Non-Persisted phase is dependent on what was done in the action.

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            ClearSelectedItemIfDeleted(viewModel);

            // Action
            action(viewModel);

            // Non-Persisted
            viewModel.CanPlay = ViewModelHelper.GetCanPlay(viewModel.SelectedNodeType);
            viewModel.CanOpenExternally = ViewModelHelper.GetCanOpenExternally(viewModel.SelectedNodeType);

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
            destViewModel.SelectedCanonicalPatchGroup = sourceViewModel.SelectedCanonicalPatchGroup;
            destViewModel.SelectedPatchGroupLowerDocumentReferenceID = sourceViewModel.SelectedPatchGroupLowerDocumentReferenceID;
            destViewModel.CanPlay = sourceViewModel.CanPlay;
        }

        private void ClearSelectedItemIfDeleted(DocumentTreeViewModel viewModel)
        {
            if (!viewModel.SelectedItemID.HasValue)
            {
                return;
            }

            switch (viewModel.SelectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.Library:
                {
                    DocumentReference entity = _repositories.DocumentReferenceRepository.TryGet(viewModel.SelectedItemID.Value);
                    if (entity == null)
                    {
                        ClearSelection(viewModel);
                    }
                    break;
                }

                case DocumentTreeNodeTypeEnum.LibraryPatch:
                {
                    Patch entity = _repositories.PatchRepository.TryGet(viewModel.SelectedItemID.Value);
                    if (entity == null)
                    {
                        ClearSelection(viewModel);
                    }
                    break;
                }

                case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
                {
                    // ReSharper disable once SimplifyLinqExpression
                    bool nodeExists = viewModel.LibrariesNode.List
                                               .SelectMany(x => x.PatchGroupNodes)
                                               .Where(x => string.Equals(x.CanonicalGroupName, viewModel.SelectedCanonicalPatchGroup))
                                               .Any();
                    if (!nodeExists)
                    { 
                
                        
                        ClearSelection(viewModel);
                    }
                    break;
                }

                case DocumentTreeNodeTypeEnum.Patch:
                {
                    Patch entity = _repositories.PatchRepository.TryGet(viewModel.SelectedItemID.Value);
                    if (entity == null)
                    {
                        ClearSelection(viewModel);
                    }
                    break;
                }

                case DocumentTreeNodeTypeEnum.PatchGroup:
                {
                    bool nodeExists1 = viewModel.PatchesNode
                                                .PatchGroupNodes
                                                .Where(x => string.Equals(x.CanonicalGroupName, viewModel.SelectedCanonicalPatchGroup))
                                                .Any();

                    bool nodeExists2 = NameHelper.AreEqual(viewModel.SelectedCanonicalPatchGroup, "");

                    bool nodeExists = nodeExists1 || nodeExists2;

                    if (!nodeExists)
                    {
                        ClearSelection(viewModel);
                    }
                    break;
                }
            }
        }

        private void ClearSelection(DocumentTreeViewModel viewModel)
        {
            viewModel.SelectedItemID = null;
            viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Undefined;
            viewModel.SelectedCanonicalPatchGroup = NameHelper.ToCanonical(null);
            viewModel.SelectedPatchGroupLowerDocumentReferenceID = null;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentTreePresenter : PresenterBase<DocumentTreeViewModel>
    {
        private readonly RepositoryWrapper _repositories;
        private readonly DocumentFacade _documentFacade;
        private readonly MidiMappingFacade _midiMappingFacade;
        private readonly PatchFacade _patchFacade;
        private readonly ScaleFacade _scaleFacade;

        public DocumentTreePresenter(
            DocumentFacade documentFacade,
            MidiMappingFacade midiMappingFacade,
            PatchFacade patchFacade,
            ScaleFacade scaleFacade,
            RepositoryWrapper repositories)
        {
            _documentFacade = documentFacade ?? throw new ArgumentNullException(nameof(documentFacade));
            _patchFacade = patchFacade ?? throw new ArgumentNullException(nameof(patchFacade));
            _repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
            _scaleFacade = scaleFacade ?? throw new ArgumentNullException(nameof(scaleFacade));
            _midiMappingFacade = midiMappingFacade ?? throw new ArgumentNullException(nameof(midiMappingFacade));
        }

        private void Close(DocumentTreeViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);

        public DocumentTreeViewModel Create(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new ArgumentNullException(nameof(userInput));

            switch (userInput.SelectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.Midi:
                    return CreateMidiMappingGroup(userInput);

                case DocumentTreeNodeTypeEnum.PatchGroup:
                    return CreatePatch(userInput);

                case DocumentTreeNodeTypeEnum.Scales:
                    return CreateScale(userInput);

                default:
                    throw new ValueNotSupportedException(userInput.SelectedNodeType);
            }
        }

        private DocumentTreeViewModel CreatePatch(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(userInput.ID);

                    // Business
                    Patch patch = _patchFacade.CreatePatch(document);
                    patch.GroupName = userInput.SelectedFriendlyPatchGroupName;

                    // Non-Persisted
                    viewModel.CreatedEntityID = patch.ID;
                });

        private DocumentTreeViewModel CreateMidiMappingGroup(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(userInput.ID);

                    // Business
                    MidiMappingGroup midiMapping = _midiMappingFacade.CreateMidiMappingGroupWithDefaults(document);

                    // Non-Persisted
                    viewModel.CreatedEntityID = midiMapping.ID;
                });

        private DocumentTreeViewModel CreateScale(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(userInput.ID);

                    // Business
                    Scale scale = _scaleFacade.Create(document);

                    // Non-Persisted
                    viewModel.CreatedEntityID = scale.ID;
                });

        public DocumentTreeViewModel Delete(DocumentTreeViewModel userInput)
        {
            if (userInput == null) throw new ArgumentNullException(nameof(userInput));

            switch (userInput.SelectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.Library:
                    return DeleteLibrary(userInput);

                case DocumentTreeNodeTypeEnum.MidiMappingGroup:
                    return DeleteMidiMappingGroup(userInput);

                case DocumentTreeNodeTypeEnum.Patch:
                    return DeletePatch(userInput);

                case DocumentTreeNodeTypeEnum.Scale:
                    return DeleteScale(userInput);

                default:
                    throw new ValueNotSupportedException(userInput.SelectedNodeType);
            }
        }

        private DocumentTreeViewModel DeleteLibrary(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

                    return _documentFacade.DeleteDocumentReference(userInput.SelectedItemID.Value);
                });

        private DocumentTreeViewModel DeleteMidiMappingGroup(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

                    _midiMappingFacade.DeleteMidiMappingGroup(userInput.SelectedItemID.Value);
                });

        private DocumentTreeViewModel DeletePatch(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

                    return _patchFacade.DeletePatchWithRelatedEntities(userInput.SelectedItemID.Value);
                });

        private DocumentTreeViewModel DeleteScale(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

                    _scaleFacade.DeleteWithRelatedEntities(userInput.SelectedItemID.Value);
                });

        public DocumentTreeViewModel HoverPatch(DocumentTreeViewModel userInput, int id)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Document currentDocument = _repositories.DocumentRepository.Get(userInput.ID);
                    Patch underlyingPatch = _repositories.PatchRepository.Get(id);

                    // Business
                    IList<IDAndName> usedInDtos = _documentFacade.GetUsedIn(currentDocument, underlyingPatch);

                    // ToViewModel
                    viewModel.PatchToolTipText = ToViewModelHelper.GetPatchNodeToolTipText(underlyingPatch, usedInDtos);
                });

        public DocumentTreeViewModel OpenItemExternally(DocumentTreeViewModel userInput)
            => ExecuteAction(
                userInput,
                viewModel =>
                {
                    if (!viewModel.SelectedItemID.HasValue)
                    {
                        throw new NullException(() => viewModel.SelectedItemID);
                    }

                    switch (viewModel.SelectedNodeType)
                    {
                        case DocumentTreeNodeTypeEnum.Library:
                            DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(viewModel.SelectedItemID.Value);
                            viewModel.DocumentToOpenExternally = documentReference.LowerDocument.ToIDAndName();
                            break;

                        case DocumentTreeNodeTypeEnum.LibraryPatch:
                            Patch patch = _repositories.PatchRepository.Get(viewModel.SelectedItemID.Value);
                            viewModel.DocumentToOpenExternally = patch.Document.ToIDAndName();
                            viewModel.PatchToOpenExternally = patch.ToIDAndName();
                            break;

                        default:
                            throw new ValueNotSupportedException(viewModel.SelectedNodeType);
                    }
                });

        public DocumentTreeViewModel Refresh(DocumentTreeViewModel userInput) => ExecuteAction(userInput, x => { });

        private void Show(DocumentTreeViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = true);

        public void ShowOrClose(DocumentTreeViewModel viewModel)
        {
            // Redirect
            if (viewModel.Visible)
            {
                Close(viewModel);
            }
            else
            {
                Show(viewModel);
            }
        }

        public void SelectAudioFileOutputs(DocumentTreeViewModel viewModel)
            => ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioFileOutputList);

        public void SelectAudioOutput(DocumentTreeViewModel viewModel)
            => ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioOutput);

        public void SelectLibraries(DocumentTreeViewModel viewModel)
            => ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Libraries);

        public void SelectLibrary(DocumentTreeViewModel viewModel, int documentReferenceID)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = documentReferenceID;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Library;
                });

        public void SelectLibraryMidi(DocumentTreeViewModel viewModel, int documentReferenceID)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = documentReferenceID;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryMidi;
                });

        public void SelectLibraryMidiMappingGroup(DocumentTreeViewModel viewModel, int id)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryMidiMappingGroup;
                });

        public void SelectLibraryPatch(DocumentTreeViewModel viewModel, int id)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryPatch;
                });

        public void SelectLibraryPatchGroup(DocumentTreeViewModel viewModel, int lowerDocumentReferenceID, string patchGroup)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedPatchGroupLowerDocumentReferenceID = lowerDocumentReferenceID;
                    viewModel.SelectedFriendlyPatchGroupName = patchGroup;
                    viewModel.SelectedCanonicalPatchGroupName = NameHelper.ToCanonical(patchGroup);
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryPatchGroup;
                });

        public void SelectLibraryScales(DocumentTreeViewModel viewModel, int documentReferenceID)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = documentReferenceID;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryScales;
                });

        public void SelectLibraryScale(DocumentTreeViewModel viewModel, int id)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryScale;
                });

        public void SelectMidi(DocumentTreeViewModel viewModel)
            => ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Midi);

        public void SelectMidiMappingGroup(DocumentTreeViewModel viewModel, int id)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.MidiMappingGroup;
                });

        public void SelectPatch(DocumentTreeViewModel viewModel, int id)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Patch;
                });

        public void SelectPatchGroup(DocumentTreeViewModel viewModel, string friendlyPatchGroupName)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedFriendlyPatchGroupName = friendlyPatchGroupName;
                    viewModel.SelectedCanonicalPatchGroupName = NameHelper.ToCanonical(friendlyPatchGroupName);
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.PatchGroup;
                });

        public void SelectScale(DocumentTreeViewModel viewModel, int id)
            => ExecuteNonPersistedAction(
                viewModel,
                () =>
                {
                    viewModel.SelectedItemID = id;
                    viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Scale;
                });

        public void SelectScales(DocumentTreeViewModel viewModel)
            => ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Scales);

        // Helpers

        private DocumentTreeViewModel ExecuteAction(DocumentTreeViewModel userInput, Action<DocumentTreeViewModel> action)
            => ExecuteAction(
                userInput,
                x =>
                {
                    action(x);
                    return ResultHelper.Successful;
                });

        private DocumentTreeViewModel ExecuteAction(DocumentTreeViewModel userInput, Func<DocumentTreeViewModel, IResult> action)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshID = RefreshIDProvider.GetRefreshID();

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.ID);

            // ToViewModel
            var factory = new RecursiveDocumentTreeViewModelFactory();
            DocumentTreeViewModel viewModel = factory.ToTreeViewModel(document);

            // NOTE: Keep split up into two Non-Persisted phases:
            // CopyNonPersisted must be done first, because action will change some of the properties.
            // And the second Non-Persisted phase is dependent on what was done in the action.

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            ClearSelectedItemIfDeleted(viewModel);

            // Business
            IResult result = action(viewModel);

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result.Messages);
            viewModel.SelectedNodeType = viewModel.SelectedNodeType;

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        protected override void ExecuteNonPersistedAction(DocumentTreeViewModel viewModel, Action action)
        {
            base.ExecuteNonPersistedAction(viewModel, action);

            // Non-Persisted
            viewModel.SelectedNodeType = viewModel.SelectedNodeType;
        }

        protected override void CopyNonPersistedProperties(DocumentTreeViewModel sourceViewModel, DocumentTreeViewModel destViewModel)
        {
            base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

            destViewModel.OutletIDToPlay = sourceViewModel.OutletIDToPlay;
            destViewModel.SelectedFriendlyPatchGroupName = sourceViewModel.SelectedFriendlyPatchGroupName;
            destViewModel.SelectedCanonicalPatchGroupName = sourceViewModel.SelectedCanonicalPatchGroupName;
            destViewModel.SelectedItemID = sourceViewModel.SelectedItemID;
            destViewModel.SelectedNodeType = sourceViewModel.SelectedNodeType;
            destViewModel.SelectedPatchGroupLowerDocumentReferenceID = sourceViewModel.SelectedPatchGroupLowerDocumentReferenceID;
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

                case DocumentTreeNodeTypeEnum.LibraryMidi:

                {
                    Document entity = _repositories.DocumentRepository.TryGet(viewModel.SelectedItemID.Value);

                    if (entity.MidiMappingGroups.Count == 0)
                    {
                        ClearSelection(viewModel);
                    }

                    break;
                }

                case DocumentTreeNodeTypeEnum.LibraryMidiMappingGroup:

                {
                    MidiMappingGroup entity = _repositories.MidiMappingGroupRepository.TryGet(viewModel.SelectedItemID.Value);

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
                                               .Any(x => string.Equals(x.CanonicalGroupName, viewModel.SelectedCanonicalPatchGroupName));

                    if (!nodeExists)
                    {
                        ClearSelection(viewModel);
                    }

                    break;
                }

                case DocumentTreeNodeTypeEnum.LibraryScales:

                {
                    Document entity = _repositories.DocumentRepository.TryGet(viewModel.SelectedItemID.Value);

                    if (entity.Scales.Count == 0)
                    {
                        ClearSelection(viewModel);
                    }

                    break;
                }

                case DocumentTreeNodeTypeEnum.LibraryScale:

                {
                    Scale entity = _repositories.ScaleRepository.TryGet(viewModel.SelectedItemID.Value);

                    if (entity == null)
                    {
                        ClearSelection(viewModel);
                    }

                    break;
                }

                case DocumentTreeNodeTypeEnum.MidiMappingGroup:

                {
                    MidiMappingGroup entity = _repositories.MidiMappingGroupRepository.TryGet(viewModel.SelectedItemID.Value);

                    if (entity == null)
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
                                                .Any(x => string.Equals(x.CanonicalGroupName, viewModel.SelectedCanonicalPatchGroupName));

                    bool nodeExists2 = NameHelper.AreEqual(viewModel.SelectedCanonicalPatchGroupName, NameHelper.ToCanonical(null));

                    bool nodeExists = nodeExists1 || nodeExists2;

                    if (!nodeExists)
                    {
                        ClearSelection(viewModel);
                    }

                    break;
                }

                case DocumentTreeNodeTypeEnum.Scale:

                {
                    Scale entity = _repositories.ScaleRepository.TryGet(viewModel.SelectedItemID.Value);

                    if (entity == null)
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
            viewModel.SelectedCanonicalPatchGroupName = NameHelper.ToCanonical(null);
            viewModel.SelectedPatchGroupLowerDocumentReferenceID = null;
        }
    }
}
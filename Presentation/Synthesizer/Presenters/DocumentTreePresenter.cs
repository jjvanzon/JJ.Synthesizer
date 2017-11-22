using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
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
		private readonly DocumentManager _documentManager;
		private readonly PatchManager _patchManager;

		public DocumentTreePresenter(DocumentManager documentManager, PatchManager patchManager, RepositoryWrapper repositories)
		{
			_documentManager = documentManager ?? throw new ArgumentNullException(nameof(documentManager));
			_patchManager = patchManager ?? throw new ArgumentNullException(nameof(patchManager));
			_repositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
		}

		public void Close(DocumentTreeViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = false);

		public DocumentTreeViewModel OpenItemExternally(DocumentTreeViewModel userInput)
		{
			return ExecuteAction(
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
		}

		public DocumentTreeViewModel Refresh(DocumentTreeViewModel userInput) => ExecuteAction(userInput, x => { });

		public DocumentTreeViewModel Remove(DocumentTreeViewModel userInput)
		{
			switch (userInput.SelectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.Library:
					return RemoveLibrary(userInput);

				case DocumentTreeNodeTypeEnum.Patch:
					return DeletePatch(userInput);

				default:
					throw new ValueNotSupportedException(userInput.SelectedNodeType);
			}
		}

		private DocumentTreeViewModel RemoveLibrary(DocumentTreeViewModel userInput)
		{
			return ExecuteAction(
				userInput,
				viewModel =>
				{
					if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

					// Business
					VoidResult result = _documentManager.DeleteDocumentReference(userInput.SelectedItemID.Value);

					// Non-Persisted
					viewModel.ValidationMessages = result.Messages;

					// Successful?
					viewModel.Successful = result.Successful;
				});
		}

		private DocumentTreeViewModel DeletePatch(DocumentTreeViewModel userInput)
		{
			return ExecuteAction(
				userInput,
				viewModel =>
				{
					if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

					// GetEntity
					Patch patch = _repositories.PatchRepository.Get(userInput.SelectedItemID.Value);

					// Businesss
					IResult result = _patchManager.DeletePatchWithRelatedEntities(patch);

					// Non-Persisted
					viewModel.ValidationMessages.AddRange(result.Messages);

					// Successful?
					viewModel.Successful = result.Successful;
				});
		}

		public void Show(DocumentTreeViewModel viewModel) => ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = true);

		public void SelectAudioFileOutputs(DocumentTreeViewModel viewModel)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioFileOutputList);
		}

		public void SelectAudioOutput(DocumentTreeViewModel viewModel)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.AudioOutput);
		}

		public void SelectLibraries(DocumentTreeViewModel viewModel)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Libraries);
		}

		public void SelectLibrary(DocumentTreeViewModel viewModel, int documentReferenceID)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					viewModel.SelectedItemID = documentReferenceID;
					viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Library;
				});
		}

		public void SelectLibraryPatch(DocumentTreeViewModel viewModel, int id)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					viewModel.SelectedItemID = id;
					viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryPatch;
				});
		}

		public void SelectLibraryPatchGroup(DocumentTreeViewModel viewModel, int lowerDocumentReferenceID, string patchGroup)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					viewModel.SelectedPatchGroupLowerDocumentReferenceID = lowerDocumentReferenceID;
					viewModel.SelectedCanonicalPatchGroup = NameHelper.ToCanonical(patchGroup);
					viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.LibraryPatchGroup;
				});
		}

		public void SelectScales(DocumentTreeViewModel viewModel)
		{
			ExecuteNonPersistedAction(viewModel, () => viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Scales);
		}

		public void SelectPatch(DocumentTreeViewModel viewModel, int id)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					viewModel.SelectedItemID = id;
					viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.Patch;
				});
		}

		public void SelectPatchGroup(DocumentTreeViewModel viewModel, string group)
		{
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					viewModel.SelectedCanonicalPatchGroup = NameHelper.ToCanonical(group);
					viewModel.SelectedNodeType = DocumentTreeNodeTypeEnum.PatchGroup;
				});
		}

		public DocumentTreeViewModel HoverPatch(DocumentTreeViewModel userInput, int id)
		{
			return ExecuteAction(
				userInput,
				viewModel =>
				{
					// GetEntity
					Patch patch = _repositories.PatchRepository.Get(id);

					// Business
					IList<IDAndName> usedInDtos = _documentManager.GetUsedIn(patch);

					// ToViewModel
					viewModel.PatchToolTipText = ToViewModelHelper.GetPatchNodeToolTipText(patch, usedInDtos);
				});
		}

		// Helpers

		private DocumentTreeViewModel ExecuteAction(DocumentTreeViewModel userInput, Action<DocumentTreeViewModel> action)
		{
			if (userInput == null) throw new NullException(() => userInput);

			// RefreshCounter
			userInput.RefreshID = RefreshIDProvider.GetRefreshID();

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
			SetSelectedNodeType(viewModel, viewModel.SelectedNodeType);

			// Successful
			viewModel.Successful = true;

			return viewModel;
		}

		protected override void ExecuteNonPersistedAction(DocumentTreeViewModel viewModel, Action action)
		{
			base.ExecuteNonPersistedAction(viewModel, action);

			// Non-Persisted
			SetSelectedNodeType(viewModel, viewModel.SelectedNodeType);
		}

		private static void SetSelectedNodeType(DocumentTreeViewModel viewModel, DocumentTreeNodeTypeEnum nodeType)
		{
			viewModel.SelectedNodeType = nodeType;

			viewModel.CanAdd = ToViewModelHelper.GetCanAdd(nodeType);
			viewModel.CanAddToInstrument = ToViewModelHelper.GetCanAddToInstrument(nodeType);
			viewModel.CanOpenExternally = ToViewModelHelper.GetCanOpenExternally(nodeType);
			viewModel.CanPlay = ToViewModelHelper.GetCanPlay(nodeType);
			viewModel.CanRemove = ToViewModelHelper.GetCanRemove(nodeType);
		}

		public override void CopyNonPersistedProperties(DocumentTreeViewModel sourceViewModel, DocumentTreeViewModel destViewModel)
		{
			base.CopyNonPersistedProperties(sourceViewModel, destViewModel);

			destViewModel.CanAdd = sourceViewModel.CanAdd;
			destViewModel.CanAddToInstrument = sourceViewModel.CanAddToInstrument;
			destViewModel.CanCreateNew = sourceViewModel.CanCreateNew;
			destViewModel.CanOpenExternally = sourceViewModel.CanOpenExternally;
			destViewModel.CanPlay = sourceViewModel.CanPlay;
			destViewModel.CanRemove = sourceViewModel.CanRemove;
			destViewModel.OutletIDToPlay = sourceViewModel.OutletIDToPlay;
			destViewModel.SelectedCanonicalPatchGroup = sourceViewModel.SelectedCanonicalPatchGroup;
			destViewModel.SelectedItemID = sourceViewModel.SelectedItemID;
			destViewModel.SelectedNodeType = sourceViewModel.SelectedNodeType;
			destViewModel.SelectedPatchGroupLowerDocumentReferenceID = sourceViewModel.SelectedPatchGroupLowerDocumentReferenceID;
			destViewModel.Visible = destViewModel.Visible;
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
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Aggregates;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

// ReSharper disable InvertIf
// ReSharper disable RedundantCaseLabel
// ReSharper disable CoVariantArrayConversion

namespace JJ.Presentation.Synthesizer.Presenters
{
	public partial class MainPresenter
	{
		// General Actions

		public void Close()
		{
			if (MainViewModel.Document.IsOpen)
			{
				Document_Close();

				if (MainViewModel.Document.SaveChangesPopup.Visible)
				{
					return;
				}
			}

			MainViewModel.MustClose = true;
		}

		public void PopupMessages_OK()
		{
			MainViewModel.PopupMessages = new List<string>();
		}

		/// <param name="documentName">nullable</param>
		/// <param name="patchName">nullable</param>
		public void Show(string documentName, string patchName)
		{
			// Redirect
			if (string.IsNullOrEmpty(documentName) && string.IsNullOrEmpty(patchName))
			{
				ShowWithoutDocumentNameOrPatchName();
			}
			else if (!string.IsNullOrEmpty(documentName) && string.IsNullOrEmpty(patchName))
			{
				ShowWithDocumentName(documentName);
			}
			else if (string.IsNullOrEmpty(documentName) && !string.IsNullOrEmpty(patchName))
			{
				throw new Exception($"if {nameof(documentName)} is empty, {nameof(patchName)} cannot be filled in.");
			}
			else if (!string.IsNullOrEmpty(documentName) && !string.IsNullOrEmpty(patchName))
			{
				ShowWithDocumentNameAndPatchName(documentName, patchName);
			}
		}

		private void ShowWithoutDocumentNameOrPatchName()
		{
			// Create ViewModel
			MainViewModel = ToViewModelHelper.CreateEmptyMainViewModel();

			// Partial Actions
			string titleBar = _titleBarPresenter.Show();
			MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
			DocumentGridViewModel documentGridViewModel = MainViewModel.DocumentGrid;
			documentGridViewModel = _documentGridPresenter.Load(documentGridViewModel);
			_monitoringBarPresenter.Load(MainViewModel.MonitoringBar);

			// DispatchViewModel
			MainViewModel.TitleBar = titleBar;
			DispatchViewModel(menuViewModel);
			DispatchViewModel(documentGridViewModel);
			DispatchViewModel(MainViewModel.MonitoringBar);
		}

		private void ShowWithDocumentName(string documentName)
		{
			// Create ViewModel
			MainViewModel = ToViewModelHelper.CreateEmptyMainViewModel();

			// Businesss
			Document document = _repositories.DocumentRepository.TryGetByName(documentName);
			if (document == null)
			{
				// GetUserInput
				DocumentOrPatchNotFoundPopupViewModel userInput = MainViewModel.DocumentOrPatchNotFound;

				// Template Method
				ExecuteReadAction(null, () => _documentOrPatchNotFoundPresenter.Show(userInput, documentName));
			}
			else
			{
				// Redirect
				Document_Open(document);
			}
		}

		private void ShowWithDocumentNameAndPatchName(string documentName, string patchName)
		{
			// Create ViewModel
			MainViewModel = ToViewModelHelper.CreateEmptyMainViewModel();

			// Businesss
			Document document = _repositories.DocumentRepository.TryGetByName(documentName);
			string canonicalPatchName = NameHelper.ToCanonical(patchName);
			Patch patch = document?.Patches
								  .Where(x => string.Equals(NameHelper.ToCanonical(x.Name), canonicalPatchName))
								  .SingleWithClearException(new { canonicalPatchName });

			if (document == null || patch == null)
			{
				// GetUserInput
				DocumentOrPatchNotFoundPopupViewModel userInput = MainViewModel.DocumentOrPatchNotFound;

				// Template Method
				ExecuteReadAction(userInput, () => _documentOrPatchNotFoundPresenter.Show(userInput, documentName, patchName));
			}
			else
			{
				// Redirect
				Document_Open(document);
				PatchDetails_Show(patch.ID);
			}
		}

		// AudioFileOutput

		public void AudioFileOutputGrid_Close()
		{
			// GetViewModel
			AudioFileOutputGridViewModel viewModel = MainViewModel.Document.AudioFileOutputGrid;

			// TemplateMethod
			ExecuteNonPersistedAction(viewModel, () => _audioFileOutputGridPresenter.Close(viewModel));
		}

		public void AudioFileOutputGrid_Create()
		{
			// GetViewModel
			AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

			// Template Method
			AudioFileOutputGridViewModel gridViewModel = ExecuteCreateAction(userInput, () => _audioFileOutputGridPresenter.Create(userInput));

			if (gridViewModel.Successful)
			{
				// GetEntity
				AudioFileOutput audioFileOutput = _repositories.AudioFileOutputRepository.Get(gridViewModel.CreatedAudioFileOutputID);

				// ToViewModel
				AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel();

				// DispatchViewModel
				DispatchViewModel(propertiesViewModel);

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.AudioFileOutput, audioFileOutput.ID).ToViewModel().AsArray(),
					States = GetAudioFileOutputStates(audioFileOutput.ID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);

				// Refresh
				DocumentTree_Refresh();
				AudioFileOutputGrid_Refresh();
			}
		}

		public void AudioFileOutputGrid_Delete(int id)
		{
			// GetViewModel
			AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

			// Undo History
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.AudioFileOutput, id).ToViewModel().AsArray(),
				States = GetAudioFileOutputStates(id)
			};

			// Template Method
			AudioFileOutputGridViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _audioFileOutputGridPresenter.Delete(userInput, id));

			if (viewModel.Successful)
			{
				// ToViewModel
				MainViewModel.Document.AudioFileOutputPropertiesDictionary.Remove(id);

				if (MainViewModel.Document.VisibleAudioFileOutputProperties?.Entity.ID == id)
				{
					MainViewModel.Document.VisibleAudioFileOutputProperties = null;
				}

				// Refresh
				DocumentTree_Refresh();
				AudioFileOutputGrid_Refresh();
			}
		}

		public void AudioFileOutputGrid_Show()
		{
			// GetViewModel
			AudioFileOutputGridViewModel viewModel = MainViewModel.Document.AudioFileOutputGrid;

			// TemplateMethod
			ExecuteNonPersistedAction(viewModel, () => _audioFileOutputGridPresenter.Show(viewModel));
		}

		public void AudioFileOutputProperties_Show(int id)
		{
			AudioFileOutputPropertiesViewModel viewModel = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(viewModel, () => _audioFileOutputPropertiesPresenter.Show(viewModel));
		}

		public void AudioFileOutputProperties_Close(int id)
		{
			// GetViewModel
			AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			AudioFileOutputPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _audioFileOutputPropertiesPresenter.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleAudioFileOutputProperties = null;

				// Refresh
				AudioFileOutputGrid_Refresh();
			}
		}

		public void AudioFileOutputProperties_Delete(int id)
		{
			// GetViewModel
			AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

			// Undo History
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.AudioFileOutput, id).ToViewModel().AsArray(),
				States = GetAudioFileOutputStates(id)
			};

			// Template Method
			AudioFileOutputPropertiesViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _audioFileOutputPropertiesPresenter.Delete(userInput));

			if (viewModel.Successful)
			{
				// ToViewModel
				MainViewModel.Document.AudioFileOutputPropertiesDictionary.Remove(id);

				if (MainViewModel.Document.VisibleAudioFileOutputProperties?.Entity.ID == id)
				{
					MainViewModel.Document.VisibleAudioFileOutputProperties = null;
				}

				// Refresh
				DocumentTree_Refresh();
				AudioFileOutputGrid_Refresh();
			}
		}

		public void AudioFileOutputProperties_LoseFocus(int id)
		{
			// GetViewModel
			AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			AudioFileOutputPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _audioFileOutputPropertiesPresenter.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				AudioFileOutputGrid_Refresh();
			}
		}

		// AudioOutput

		private void AudioOutputProperties_Show()
		{
			AudioOutputPropertiesViewModel viewModel = MainViewModel.Document.AudioOutputProperties;

			ExecuteNonPersistedAction(viewModel, () => _audioOutputPropertiesPresenter.Show(viewModel));
		}

		private void AudioOutputProperties_Switch()
		{
			if (MainViewModel.PropertiesPanelVisible)
			{
				AudioOutputProperties_Show();
			}
		}

		public void AudioOutputProperties_Close()
		{
			AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

			ExecuteUpdateAction(userInput, () => _audioOutputPropertiesPresenter.Close(userInput));
		}

		public void AudioOutputProperties_LoseFocus()
		{
			AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

			ExecuteUpdateAction(userInput, () => _audioOutputPropertiesPresenter.LoseFocus(userInput));
		}

		public void AudioOutputProperties_Play()
		{
			// NOTE:
			// Cannot use partial presenter, because this action uses both
			// AudioOutputProperties and Instrument view model.

			// GetViewModel
			AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

			// TemplateMethod
			ExecuteReadAction(
				userInput,
				() =>
				{
					// GetEntities
					AudioOutput audioOutput = _repositories.AudioOutputRepository.Get(userInput.Entity.ID);
					IList<Patch> entities = MainViewModel.Document.InstrumentBar.Patches.Select(x => _repositories.PatchRepository.Get(x.EntityID)).ToArray();

					// Business
					Patch autoPatch = _autoPatcher.AutoPatch(entities);
					_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(autoPatch);
					Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(autoPatch);
					Outlet outlet = result.Data;

					// ToViewModel
					AudioOutputPropertiesViewModel viewModel = audioOutput.ToPropertiesViewModel();

					// Non-Persisted
					viewModel.Visible = userInput.Visible;
					viewModel.ValidationMessages = result.Messages;
					viewModel.Successful = result.Successful;
					viewModel.OutletIDToPlay = outlet?.ID;

					return viewModel;
				});
		}

		// AutoPatch

		public void AutoPatchPopup_Close()
		{
			AutoPatchPopupViewModel userInput = MainViewModel.Document.AutoPatchPopup;

			ExecuteReadAction(
				userInput,
				() =>
				{
					// RefreshCounter
					userInput.RefreshID = RefreshIDProvider.GetRefreshID();
					userInput.PatchDetails.RefreshID = RefreshIDProvider.GetRefreshID();

					// Action
					AutoPatchPopupViewModel viewModel = ToViewModelHelper.CreateEmptyAutoPatchViewModel();

					// Non-Persisted
					viewModel.RefreshID = userInput.RefreshID;
					viewModel.PatchDetails.RefreshID = userInput.PatchDetails.RefreshID;

					return viewModel;
				});
		}

		public void AutoPatchPopup_Save()
		{
			AutoPatchPopupViewModel userInput = MainViewModel.Document.AutoPatchPopup;

			AutoPatchPopupViewModel viewModel = ExecuteUpdateAction(
				userInput,
				() =>
				{
					// RefreshCounter
					userInput.RefreshID = RefreshIDProvider.GetRefreshID();
					userInput.PatchDetails.RefreshID = RefreshIDProvider.GetRefreshID();

					// Set !Successful
					userInput.Successful = false;

					// Get Entities
					Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

					// ToEntity
					Patch patch = userInput.ToEntityWithRelatedEntities(_repositories);

					// Business
					patch.LinkTo(document);
					IResult result = _patchFacade.SavePatch(patch);

					AutoPatchPopupViewModel viewModel2;
					if (result.Successful)
					{
						// ToViewModel
						viewModel2 = ToViewModelHelper.CreateEmptyAutoPatchViewModel();
					}
					else
					{
						// ToViewModel
						viewModel2 = patch.ToAutoPatchViewModel(
							_repositories.SampleRepository,
							_repositories.CurveRepository,
							_repositories.InterpolationTypeRepository);

						viewModel2.Visible = userInput.Visible;
					}

					// Non-Persisted
					viewModel2.ValidationMessages.AddRange(result.Messages);
					viewModel2.RefreshID = userInput.RefreshID;
					viewModel2.PatchDetails.RefreshID = userInput.PatchDetails.RefreshID;

					// Successful?
					viewModel2.Successful = result.Successful;

					return viewModel2;
				});

			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
				PatchDetails_Show(userInput.PatchDetails.Entity.ID);
				PatchProperties_Show(userInput.PatchDetails.Entity.ID);
			}
		}

		// Curve

		private void Curve_Expand(int id)
		{
			ExecuteReadAction(
				null,
				() =>
				{
					// GetEntity
					int operatorID = GetOperatorIDByCurveID(id);
					Operator op = _repositories.OperatorRepository.Get(operatorID);
					int patchID = op.Patch.ID;

					// Redirect
					OperatorProperties_Show(operatorID);
					CurveDetails_Show(id);
					PatchDetails_Show(patchID);
					Operator_Select(patchID, operatorID);
				});
		}

		private void CurveDetails_Show(int id)
		{
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(userInput, () => _curveDetailsPresenter.Show(userInput));
		}

		public void CurveDetails_Close(int id)
		{
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

			ExecuteUpdateAction(userInput, () => _curveDetailsPresenter.Close(userInput));
		}

		public void CurveDetails_Expand(int curveID)
		{
			// Redirect
			Curve_Expand(curveID);
		}

		public void CurveDetails_ExpandNode(int curveID, int nodeID)
		{
			// Redirect
			Node_Expand(curveID, nodeID);
		}

		public void CurveDetails_LoseFocus(int id)
		{
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

			ExecuteUpdateAction(userInput, () => _curveDetailsPresenter.LoseFocus(userInput));
		}

		private void CurveDetails_SelectNode(int curveID, int nodeID)
		{
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

			ExecuteNonPersistedAction(userInput, () => _curveDetailsPresenter.SelectNode(userInput, nodeID));
		}

		public void Curve_Select(int id)
		{
			ExecuteReadAction(
				null,
				() =>
				{
					// GetEntity
					int operatorID = GetOperatorIDByCurveID(id);
					Operator op = _repositories.OperatorRepository.Get(operatorID);
					int patchID = op.Patch.ID;

					// Redirect
					OperatorProperties_Switch(operatorID);
					PatchDetails_SelectOperator(patchID, operatorID);
				});
		}

		// Document Grid

		public void DocumentCannotDelete_OK()
		{
			// GetViewModel
			DocumentCannotDeleteViewModel userInput = MainViewModel.DocumentCannotDelete;

			// Partial Action
			ExecuteNonPersistedAction(userInput, () => _documentCannotDeletePresenter.OK(userInput));
		}

		public void DocumentDelete_Cancel()
		{
			// GetViewModel
			DocumentDeleteViewModel viewModel = MainViewModel.DocumentDelete;

			// Partial Action
			ExecuteNonPersistedAction(viewModel, () => _documentDeletePresenter.Cancel(viewModel));
		}

		public void DocumentDelete_Confirm()
		{
			// GetViewModel
			DocumentDeleteViewModel viewModel = MainViewModel.DocumentDelete;

			// Partial Action
			ScreenViewModelBase viewModel2 = _documentDeletePresenter.Confirm(viewModel);

			// RefreshCounter
			viewModel.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			viewModel.Successful = false;

			if (viewModel2 is DocumentDeletedViewModel)
			{
				_repositories.Commit();
			}

			// Successful
			viewModel.Successful = true;

			// DispatchViewModel
			DispatchViewModel(viewModel2);
		}

		public void DocumentDeleted_OK()
		{
			// GetViewModel
			DocumentDeletedViewModel viewModel = MainViewModel.DocumentDeleted;

			// Partial Action
			ExecuteNonPersistedAction(viewModel, () => _documentDeletedPresenter.OK(viewModel));
		}

		public void DocumentDelete_Show(int id)
		{
			// GetViewModel
			DocumentGridViewModel gridViewModel = MainViewModel.DocumentGrid;

			// RefreshCounter
			gridViewModel.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			gridViewModel.Successful = false;

			// Partial Action
			ScreenViewModelBase viewModel2 = _documentDeletePresenter.Show(id);

			// Successful
			gridViewModel.Successful = true;

			// DispatchViewModel
			DispatchViewModel(viewModel2);
		}

		public void DocumentDetails_Close()
		{
			// GetViewModel
			DocumentDetailsViewModel viewModel = MainViewModel.DocumentDetails;

			// Partial Action
			_documentDetailsPresenter.Close(viewModel);

			// DispatchViewModel
			DispatchViewModel(viewModel);
		}

		public void Document_Create()
		{
			// Dirty Check
			if (MainViewModel.Document.IsDirty)
			{
				SaveChangesPopup_Show(mustGoToDocumentCreateAfterConfirmation: true);
				return;
			}

			// GetViewModel
			DocumentGridViewModel gridViewModel = MainViewModel.DocumentGrid;

			// RefreshCounter
			gridViewModel.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			gridViewModel.Successful = false;

			// Partial Action
			DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();

			// Successful
			viewModel.Successful = true;

			// DispatchViewModel
			DispatchViewModel(viewModel);
		}

		public void DocumentDetails_Save()
		{
			// GetViewModel
			DocumentDetailsViewModel userInput = MainViewModel.DocumentDetails;

			// Partial Action
			DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Save(userInput);

			// Commit
			// (do it before opening the document, which does a big query, which requires at least a flush.)
			if (viewModel.Successful)
			{
				_repositories.DocumentRepository.Commit();
			}

			// DispatchViewModel
			DispatchViewModel(viewModel);

			if (viewModel.Successful)
			{
				// Refresh
				DocumentGrid_Refresh();

				// Redirect
				Document_Open(viewModel.Document.ID);
			}
		}

		public void DocumentGrid_Close()
		{
			DocumentGridViewModel viewModel = MainViewModel.DocumentGrid;

			ExecuteNonPersistedAction(viewModel, () => _documentGridPresenter.Close(viewModel));
		}

		public void DocumentGrid_Play(int id)
		{
			DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

			ExecuteReadAction(userInput, () => _documentGridPresenter.Play(userInput, id));
		}

		public void DocumentGrid_Show()
		{
			DocumentGridViewModel viewModel = MainViewModel.DocumentGrid;

			ExecuteReadAction(viewModel, () => _documentGridPresenter.Load(viewModel));
		}

		// Document

		public void Document_Activate()
		{
			Document_Refresh();
		}

		public void Document_Close()
		{
			// Dirty Check
			if (MainViewModel.Document.IsDirty)
			{
				SaveChangesPopup_Show();
				return;
			}

			// Redirect
			Document_CloseConfirmed();
		}

		private void Document_CloseConfirmed()
		{
			// Partial Actions
			string titleBar = _titleBarPresenter.Show();
			MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
			DocumentViewModel documentViewModel = ToViewModelHelper.CreateEmptyDocumentViewModel();

			// DispatchViewModel
			MainViewModel.TitleBar = titleBar;
			MainViewModel.Menu = menuViewModel;
			MainViewModel.Document = documentViewModel;

			// Redirect
			DocumentGrid_Show();
		}

		public void Document_Open(string name)
		{
			Document document = _documentFacade.Get(name);

			Document_Open(document);
		}

		public void Document_Open(int id)
		{
			Document document = _documentFacade.Get(id);

			Document_Open(document);
		}

		private void Document_Open(Document document)
		{
			// Dirty Check
			if (MainViewModel.Document.IsDirty)
			{
				SaveChangesPopup_Show(document.ID);
				return;
			}

			// ToViewModel
			DocumentViewModel viewModel = document.ToViewModel(_repositories);

			// Non-Persisted
			viewModel.DocumentTree.Visible = true;
			viewModel.IsOpen = true;

			// Set everything to successful.
			viewModel.AudioFileOutputGrid.Successful = true;
			viewModel.AudioFileOutputPropertiesDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.AudioOutputProperties.Successful = true;
			viewModel.AutoPatchPopup.PatchDetails.Successful = true;
			viewModel.AutoPatchPopup.PatchProperties.Successful = true;
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForCaches.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForCurves.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForInletsToDimension.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForNumbers.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForPatchInlets.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForPatchOutlets.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForSamples.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_WithCollectionRecalculation.Values.ForEach(x => x.Successful = true);
			viewModel.AutoPatchPopup.OperatorPropertiesDictionary_WithInterpolation.Values.ForEach(x => x.Successful = true);
			viewModel.InstrumentBar.Successful = true;
			viewModel.CurveDetailsDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.DocumentProperties.Successful = true;
			viewModel.DocumentTree.Successful = true;
			viewModel.NodePropertiesDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_ForCaches.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_ForCurves.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_ForInletsToDimension.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_ForNumbers.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_ForPatchInlets.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_ForPatchOutlets.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_ForSamples.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_WithCollectionRecalculation.Values.ForEach(x => x.Successful = true);
			viewModel.OperatorPropertiesDictionary_WithInterpolation.Values.ForEach(x => x.Successful = true);
			viewModel.PatchDetailsDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.PatchPropertiesDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.ScalePropertiesDictionary.Values.ForEach(x => x.Successful = true);
			viewModel.ToneGridEditDictionary.Values.ForEach(x => x.Successful = true);

			// Partials
			string titleBar = _titleBarPresenter.Show(document);
			MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: true);
			viewModel.InstrumentBar = _instrumentBarPresenter.OpenDocument(viewModel.InstrumentBar);
			_monitoringBarPresenter.Load(MainViewModel.MonitoringBar);

			// DispatchViewModel
			MainViewModel.Document = viewModel;
			MainViewModel.TitleBar = titleBar;
			DispatchViewModel(menuViewModel);
			DispatchViewModel(MainViewModel.MonitoringBar);

			// Redirect
			MainViewModel.DocumentGrid.Visible = false;
			if (document.Patches.Count == 1)
			{
				PatchDetails_Show(document.Patches[0].ID);
			}
		}

		public void DocumentOrPatchNotFound_OK()
		{
			DocumentOrPatchNotFoundPopupViewModel userInput = MainViewModel.DocumentOrPatchNotFound;

			ExecuteNonPersistedAction(userInput, () => _documentOrPatchNotFoundPresenter.OK(userInput));
		}

		public void DocumentProperties_Show()
		{
			DocumentPropertiesViewModel viewModel = MainViewModel.Document.DocumentProperties;

			ExecuteNonPersistedAction(viewModel, () => _documentPropertiesPresenter.Show(viewModel));
		}

		public void DocumentProperties_Close()
		{
			DocumentProperties_CloseOrLoseFocus(_documentPropertiesPresenter.Close);
		}

		public void DocumentProperties_LoseFocus()
		{
			DocumentProperties_CloseOrLoseFocus(_documentPropertiesPresenter.LoseFocus);
		}

		private void DocumentProperties_CloseOrLoseFocus(Func<DocumentPropertiesViewModel, DocumentPropertiesViewModel> partialAction)
		{
			// GetViewModel
			DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

			// Template Method
			DocumentPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => partialAction(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				TitleBar_Refresh();
				DocumentGrid_Refresh();
				DocumentTree_Refresh();
			}
		}

		public void DocumentProperties_Play()
		{
			DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

			ExecuteReadAction(userInput, () => _documentPropertiesPresenter.Play(userInput));
		}

		/// <summary>
		/// Will do a a ViewModel to Entity conversion.
		/// (The private Refresh methods do not.)
		/// Will also apply (external) UnderlyingPatches.
		/// </summary>
		public void Document_Refresh()
		{
			if (MainViewModel.Document.IsOpen)
			{
				// ToEntity
				MainViewModel.ToEntityWithRelatedEntities(_repositories);

				// Business
				_documentFacade.Refresh(MainViewModel.Document.ID);

				// ToViewModel
				DocumentViewModel_Refresh();
			}
		}

		public void Document_Save()
		{
			// ToEntity
			// Get rid of AutoPatch view model temporarily from the DocumentViewModel.
			// It should not be saved and this is the only action upon which it should not be converted to Entity.
			Document document;
			AutoPatchPopupViewModel originalAutoPatchPopup = null;
			try
			{
				originalAutoPatchPopup = MainViewModel.Document.AutoPatchPopup;
				MainViewModel.Document.AutoPatchPopup = null;

				document = MainViewModel.ToEntityWithRelatedEntities(_repositories);
			}
			finally
			{
				if (originalAutoPatchPopup != null)
				{
					MainViewModel.Document.AutoPatchPopup = originalAutoPatchPopup;
				}
			}

			// Business
			IResult validationResult = _documentFacade.Save(document);
			IResult warningsResult = _documentFacade.GetWarningsRecursive(document);

			// Commit
			if (validationResult.Successful)
			{
				_repositories.Commit();
				_systemFacade.RefreshSystemDocumentIfNeeded(document);

				MainViewModel.Document.IsDirty = false;
			}

			// ToViewModel
			MainViewModel.ValidationMessages = validationResult.Messages;
			MainViewModel.WarningMessages = warningsResult.Messages;
		}

		public void DocumentTree_AddToInstrument()
		{
			// Involves both DocumentTree and Instrument view,
			// so cannot be handled by a single sub-presenter.

			DocumentTreeViewModel documentTreeViewModel = MainViewModel.Document.DocumentTree;

			if (!documentTreeViewModel.SelectedItemID.HasValue)
			{
				throw new NotHasValueException(() => documentTreeViewModel.SelectedItemID);
			}
			int entityID = documentTreeViewModel.SelectedItemID.Value;

			switch (documentTreeViewModel.SelectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.Patch:
				case DocumentTreeNodeTypeEnum.LibraryPatch:
					// Redirect
					InstrumentBar_AddPatch(entityID);
					break;

				case DocumentTreeNodeTypeEnum.MidiMappingGroup:
				case DocumentTreeNodeTypeEnum.LibraryMidiMappingGroup:
					// Redirect
					InstrumentBar_AddMidiMappingGroup(entityID);
					break;

				case DocumentTreeNodeTypeEnum.Scale:
				case DocumentTreeNodeTypeEnum.LibraryScale:
					// Redirect
					InstrumentBar_SetScale(entityID);
					break;

				default:
					throw new ValueNotSupportedException(documentTreeViewModel.SelectedNodeType);
			}
		}

		public void DocumentTree_Create()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			switch (userInput.SelectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.Libraries:
					// Redirect
					DocumentTree_CreateLibrary();
					break;

				case DocumentTreeNodeTypeEnum.Midi:
					// Redirect
					DocumentTree_CreateMidiMappingGroup();
					break;

				case DocumentTreeNodeTypeEnum.PatchGroup:
					// Redirect
					DocumentTree_CreatePatch();
					break;

				case DocumentTreeNodeTypeEnum.Patch:
				case DocumentTreeNodeTypeEnum.LibraryPatch:
					// Redirect
					DocumentTree_CreateOperator();
					break;

				case DocumentTreeNodeTypeEnum.Scales:
					// Redirect
					DocumentTree_CreateScale();
					break;

				default:
					throw new ValueNotSupportedException(userInput.SelectedNodeType);
			}
		}

		private void DocumentTree_CreateLibrary()
		{
			LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

			ExecuteUpdateAction(userInput, () => _librarySelectionPopupPresenter.Load(userInput));
		}

		private void DocumentTree_CreateMidiMappingGroup()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Template Method
			DocumentTreeViewModel viewModel = ExecuteCreateAction(userInput, () => _documentTreePresenter.Create(userInput));

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.MidiMappingGroup, viewModel.CreatedEntityID).ToViewModel().AsArray(),
					States = GetMidiMappingGroupStates(viewModel.CreatedEntityID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);

				// Redirect
				MidiMappingGroupDetails_Show(viewModel.CreatedEntityID);
			}
		}

		private void DocumentTree_CreateOperator()
		{
			if (MainViewModel.Document.VisiblePatchDetails == null)
			{
				throw new NullException(() => MainViewModel.Document.VisiblePatchDetails);
			}

			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			if (!userInput.SelectedItemID.HasValue)
			{
				throw new NotHasValueException(() => MainViewModel.Document.DocumentTree.SelectedItemID);
			}

			// TemplateMethod
			Operator op = null;
			IList<Operator> createdOperators = new List<Operator>();

			DocumentTreeViewModel viewModel = ExecuteCreateAction(
				userInput,
				() =>
				{
					// RefreshCounter
					userInput.RefreshID = RefreshIDProvider.GetRefreshID();

					// Set !Successful
					userInput.Successful = false;

					// GetEntities
					Patch underlyingPatch = _repositories.PatchRepository.Get(userInput.SelectedItemID.Value);
					Patch patch = _repositories.PatchRepository.Get(MainViewModel.Document.VisiblePatchDetails.Entity.ID);

					bool isSamplePatch = underlyingPatch.IsSamplePatch();
					if (isSamplePatch)
					{
						// ToViewModel
						MainViewModel.Document.SampleFileBrowser.Visible = true;
						MainViewModel.Document.SampleFileBrowser.DestPatchID = patch.ID;
					}
					else
					{
						// Business
						var operatorFactory = new OperatorFactory(patch, _repositories);
						op = operatorFactory.New(underlyingPatch, GetVariableInletOrOutletCount(underlyingPatch));

						IList<Operator> autoCreatedNumberOperators =
							_autoPatcher.CreateNumbersForEmptyInletsWithDefaultValues(op, ESTIMATED_OPERATOR_WIDTH, OPERATOR_HEIGHT);

						createdOperators.AddRange(autoCreatedNumberOperators);
						// Put main operator last so it is dispatched last upon redo and put on top.
						createdOperators.Add(op);
					}

					// Successful
					userInput.Successful = true;

					// Do not do bother with ToViewModel. We will do a full Refresh later.
					return userInput;
				});

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = createdOperators.Select(x => x.ToEntityTypeAndIDViewModel()).ToArray(),
					States = createdOperators.SelectMany(x => GetOperatorStates(x.ID)).Distinct().ToArray()
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);

				// Redirect
				if (op != null)
				{
					Operator_Expand(op.ID);
				}
			}
		}

		private void DocumentTree_CreatePatch()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Template Method
			DocumentTreeViewModel viewModel = ExecuteCreateAction(userInput, () => _documentTreePresenter.Create(userInput));

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.Patch, viewModel.CreatedEntityID).ToViewModel().AsArray(),
					States = GetPatchStates(viewModel.CreatedEntityID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);

				// Redirect
				Patch_Expand(viewModel.CreatedEntityID);
			}
		}

		private void DocumentTree_CreateScale()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Template Method
			DocumentTreeViewModel viewModel = ExecuteCreateAction(userInput, () => _documentTreePresenter.Create(userInput));

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.Scale, viewModel.CreatedEntityID).ToViewModel().AsArray(),
					States = GetScaleStates(viewModel.CreatedEntityID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);

				// Redirect
				Scale_Show(viewModel.CreatedEntityID);
			}
		}

		public void DocumentTree_Close()
		{
			ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.Close);
		}

		public void DocumentTree_Delete()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Redirect
			switch (userInput.SelectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.MidiMappingGroup:
					DocumentTree_DeleteMidiMappingGroup();
					break;

				case DocumentTreeNodeTypeEnum.Library:
					DocumentTree_DeleteLibrary();
					break;

				case DocumentTreeNodeTypeEnum.Patch:
					DocumentTree_DeletePatch();
					break;

				case DocumentTreeNodeTypeEnum.Scale:
					DocumentTree_DeleteScale();
					break;

				default:
					throw new ValueNotSupportedException(userInput.SelectedNodeType);
			}
		}

		private void DocumentTree_DeleteMidiMappingGroup()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Undo History
			int id = userInput.SelectedItemID ?? 0;
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.MidiMappingGroup, id).ToViewModel().AsArray(),
				States = GetMidiMappingGroupStates(id)
			};

			// Template Method
			DocumentTreeViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _documentTreePresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		private void DocumentTree_DeleteLibrary()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Undo History
			int id = userInput.SelectedItemID ?? 0;
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.DocumentReference, id).ToViewModel().AsArray(),
				States = GetLibraryStates(id)
			};

			// Template Method
			DocumentTreeViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _documentTreePresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		private void DocumentTree_DeletePatch()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Undo History
			int id = userInput.SelectedItemID ?? 0;
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.Patch, id).ToViewModel().AsArray(),
				States = GetPatchStates(id)
			};

			// Template Method
			DocumentTreeViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _documentTreePresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		private void DocumentTree_DeleteScale()
		{
			// GetViewModel
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			// Undo History
			int id = userInput.SelectedItemID ?? 0;
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.Scale, id).ToViewModel().AsArray(),
				States = GetScaleStates(id)
			};

			// Template Method
			DocumentTreeViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _documentTreePresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void DocumentTree_HoverPatch(int id)
		{
			DocumentTreeViewModel viewModel = MainViewModel.Document.DocumentTree;

			ExecuteReadAction(viewModel, () => _documentTreePresenter.HoverPatch(viewModel, id));
		}

		public void DocumentTree_OpenItemExternally()
		{
			DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

			ExecuteReadAction(userInput, () => _documentTreePresenter.OpenItemExternally(userInput));
		}

		public void DocumentTree_Play()
		{
			// GetViewModel
			DocumentTreeViewModel viewModel = MainViewModel.Document.DocumentTree;

			// TemplateMethod
			ExecuteReadAction(viewModel, func);

			DocumentTreeViewModel func()
			{
				// RefreshCounter
				viewModel.RefreshID = RefreshIDProvider.GetRefreshID();

				// Set !Successful
				viewModel.Successful = false;

				// GetEntities
				Document document = _repositories.DocumentRepository.Get(viewModel.ID);

				Result<Outlet> result;
				switch (viewModel.SelectedNodeType)
				{
					case DocumentTreeNodeTypeEnum.AudioOutput:
					{
						// GetEntities
						IList<Patch> entities = MainViewModel.Document.InstrumentBar.Patches
															 .Select(x => _repositories.PatchRepository.Get(x.EntityID))
															 .ToArray();
						// Business
						Patch autoPatch = _autoPatcher.AutoPatch(entities);
						_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(autoPatch);
						result = _autoPatcher.AutoPatch_TryCombineSounds(autoPatch);

						break;
					}

					case DocumentTreeNodeTypeEnum.Library:
					{
						if (!viewModel.SelectedItemID.HasValue) throw new NullException(() => viewModel.SelectedItemID);

						// GetEntity
						DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(viewModel.SelectedItemID.Value);

						// Business
						result = _autoPatcher.TryAutoPatchFromDocumentRandomly(documentReference.LowerDocument, mustIncludeHidden: false);
						if (result.Data != null)
						{
							_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
						}

						break;
					}

					case DocumentTreeNodeTypeEnum.Patch:
					case DocumentTreeNodeTypeEnum.LibraryPatch:
					{
						if (!viewModel.SelectedItemID.HasValue) throw new NullException(() => viewModel.SelectedItemID);

						// GetEntities
						Patch patch = _repositories.PatchRepository.Get(viewModel.SelectedItemID.Value);

						// Business
						result = _autoPatcher.AutoPatch_TryCombineSounds(patch);
						if (result.Data != null)
						{
							_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
						}

						break;
					}

					case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
					{
						if (!viewModel.SelectedPatchGroupLowerDocumentReferenceID.HasValue) throw new NullException(() => viewModel.SelectedPatchGroupLowerDocumentReferenceID);

						// GetEntities
						DocumentReference lowerDocumentReference = _repositories.DocumentReferenceRepository.Get(viewModel.SelectedPatchGroupLowerDocumentReferenceID.Value);

						// Business
						result = _autoPatcher.TryAutoPatchFromPatchGroupRandomly(
							lowerDocumentReference.LowerDocument,
							viewModel.SelectedCanonicalPatchGroup,
							mustIncludeHidden: false);
						if (result.Data != null)
						{
							_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
						}

						break;
					}

					case DocumentTreeNodeTypeEnum.PatchGroup:
					{
						// Business
						result = _autoPatcher.TryAutoPatchFromPatchGroupRandomly(document, viewModel.SelectedCanonicalPatchGroup, mustIncludeHidden: false);

						break;
					}

					case DocumentTreeNodeTypeEnum.Libraries:
					{
						// Business
						IList<Document> lowerDocuments = document.LowerDocumentReferences.Select(x => x.LowerDocument).ToArray();
						result = _autoPatcher.TryAutoPatchFromDocumentsRandomly(lowerDocuments, mustIncludeHidden: false);
						if (result.Data != null)
						{
							_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
						}

						break;
					}

					case DocumentTreeNodeTypeEnum.AudioFileOutputList:
					case DocumentTreeNodeTypeEnum.Scales:
					default:
					{
						// Successful
						viewModel.Successful = true;

						return viewModel;
					}
				}

				// Business
				Outlet outlet = result.Data;

				// Non-Persisted
				viewModel.OutletIDToPlay = outlet?.ID;
				viewModel.Successful = result.Successful;
				viewModel.ValidationMessages.AddRange(result.Messages);

				return viewModel;
			}
		}

		public void DocumentTree_SelectAudioFileOutputs()
		{
			ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectAudioFileOutputs);
		}

		public void DocumentTree_SelectAudioOutput()
		{
			ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectAudioOutput);

			// Redirect
			AudioOutputProperties_Switch();
		}

		public void DocumentTree_SelectLibraries()
		{
			ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectLibraries);
		}

		public void DocumentTree_SelectLibrary(int documentReferenceID)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibrary(x, documentReferenceID));

			// Redirect
			LibraryProperties_Switch(documentReferenceID);
		}

		public void DocumentTree_SelectLibraryMidi(int documentReferenceID)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryMidi(x, documentReferenceID));
		}

		public void DocumentTree_SelectLibraryMidiMappingGroup(int id)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryMidiMappingGroup(x, id));
		}

		public void DocumentTree_SelectLibraryPatch(int id)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryPatch(x, id));
		}

		public void DocumentTree_SelectLibraryPatchGroup(int lowerDocumentReferenceID, string patchGroup)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryPatchGroup(x, lowerDocumentReferenceID, patchGroup));
		}

		public void DocumentTree_SelectLibraryScale(int id)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryScale(x, id));
		}

		public void DocumentTree_SelectLibraryScales(int documentReferenceID)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryScales(x, documentReferenceID));
		}

		public void DocumentTree_SelectMidi() => ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectMidi);

		public void DocumentTree_SelectMidiMappingGroup(int id)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectMidiMappingGroup(x, id));

			if (MainViewModel.Document.VisibleMidiMappingGroupDetails != null)
			{
				MidiMappingGroupDetails_Switch(id);
			}
		}

		public void DocumentTree_SelectScale(int id)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectScale(x, id));

			// Redirect
			ScaleProperties_Switch(id);

			if (MainViewModel.Document.VisibleToneGridEdit != null)
			{
				ToneGridEdit_Switch(id);
			}
		}

		public void DocumentTree_SelectScales()
		{
			ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectScales);
		}

		public void DocumentTree_SelectPatch(int id)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectPatch(x, id));

			// Redirect
			PatchProperties_Switch(id);
		}

		public void DocumentTree_SelectPatchGroup(string group)
		{
			ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectPatchGroup(x, group));
		}

		public void DocumentTree_Show()
		{
			ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.Show);
		}

		public void DocumentTree_ShowAudioOutput()
		{
			// Redirect
			AudioOutputProperties_Show();
		}

		public void DocumentTree_ShowLibrary(int documentReferenceID)
		{
			// Redirect
			LibraryProperties_Show(documentReferenceID);
		}

		public void DocumentTree_ShowMidiMappingGroup(int id)
		{
			// Redirect
			MidiMappingGroupDetails_Show(id);
		}

		public void DocumentTree_ShowPatch(int id)
		{
			// Redirect
			PatchDetails_Show(id);
		}

		public void DocumentTree_ShowScale(int id)
		{
			// Redirect
			Scale_Show(id);
		}

		/// <summary>
		/// On top of the regular ExecuteNonPersistedAction,
		/// will set CanCreate, which cannot be determined by entities or DocumentTreeViewModel alone.
		/// </summary>
		private void ExecuteNonPersistedDocumentTreeAction(Action<DocumentTreeViewModel> partialAction)
		{
			DocumentTreeViewModel viewModel = MainViewModel.Document.DocumentTree;

			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					partialAction(viewModel);
					SetCanCreate(viewModel);
				});
		}

		private void SetCanCreate(DocumentTreeViewModel viewModel)
		{
			bool patchDetailsVisible = MainViewModel.Document.VisiblePatchDetails != null;
			viewModel.CanCreate = ToViewModelHelper.GetCanCreate(viewModel.SelectedNodeType, patchDetailsVisible);
		}

		// InstrumentBar

		private void InstrumentBar_AddMidiMappingGroup(int midiMappingGroupID)
		{
			InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(userInput, () => _instrumentBarPresenter.AddMidiMappingGroup(userInput, midiMappingGroupID));
		}

		private void InstrumentBar_AddPatch(int patchID)
		{
			InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(userInput, () => _instrumentBarPresenter.AddPatch(userInput, patchID));
		}

		public void InstrumentBar_Expand()
		{
			// GetViewModel
			InstrumentBarViewModel instrumentBarUserInput = MainViewModel.Document.InstrumentBar;
			AutoPatchPopupViewModel autoPatchPopupUserInput = MainViewModel.Document.AutoPatchPopup;

			// RefreshCounter
			instrumentBarUserInput.RefreshID = RefreshIDProvider.GetRefreshID();
			autoPatchPopupUserInput.RefreshID = RefreshIDProvider.GetRefreshID();
			autoPatchPopupUserInput.PatchDetails.RefreshID = RefreshIDProvider.GetRefreshID();

			// Set !Successful
			instrumentBarUserInput.Successful = false;

			// ToEntity
			Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);

			// Get Entities
			IList<Patch> underlyingPatches = instrumentBarUserInput.Patches.Select(x => _repositories.PatchRepository.Get(x.EntityID)).ToArray();

			// Business
			Patch autoPatch = _autoPatcher.AutoPatch(underlyingPatches);

			// Business
			IResult validationResult = _documentFacade.Save(document);
			if (!validationResult.Successful)
			{
				// Non-Persisted
				instrumentBarUserInput.ValidationMessages.AddRange(validationResult.Messages);

				// DispatchViewModel
				DispatchViewModel(instrumentBarUserInput);

				return;
			}

			// ToViewModel
			AutoPatchPopupViewModel autoPatchPopupViewModel = autoPatch.ToAutoPatchViewModel(
				_repositories.SampleRepository,
				_repositories.CurveRepository,
				_repositories.InterpolationTypeRepository);

			// Non-Persisted
			autoPatchPopupViewModel.Visible = true;
			autoPatchPopupViewModel.RefreshID = autoPatchPopupUserInput.RefreshID;
			autoPatchPopupViewModel.PatchDetails.RefreshID = autoPatchPopupUserInput.PatchDetails.RefreshID;

			// Successful
			instrumentBarUserInput.Successful = true;
			autoPatchPopupViewModel.Successful = true;

			// DispatchViewModel
			DispatchViewModel(autoPatchPopupViewModel);
		}

		public void InstrumentBar_ExpandMidiMappingGroup(int midiMappingGroupID)
		{
			// Redirect
			MidiMappingGroup_Expand(midiMappingGroupID);
		}

		public void InstrumentBar_ExpandPatch(int patchID)
		{
			// Redirect
			Patch_Expand(patchID);
		}

		public void InstrumentBar_MoveMidiMappingGroup(int midiMappingGroupID, int newPosition)
		{
			InstrumentBarViewModel viewModel = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(viewModel, () => _instrumentBarPresenter.MoveMidiMappingGroup(viewModel, midiMappingGroupID, newPosition));
		}

		public void InstrumentBar_MoveMidiMappingGroupBackward(int midiMappingGroupID)
		{
			InstrumentBarViewModel viewModel = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(viewModel, () => _instrumentBarPresenter.MoveMidiMappingGroupBackward(viewModel, midiMappingGroupID));
		}

		public void InstrumentBar_MoveMidiMappingGroupForward(int midiMappingGroupID)
		{
			InstrumentBarViewModel viewModel = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(viewModel, () => _instrumentBarPresenter.MoveMidiMappingGroupForward(viewModel, midiMappingGroupID));
		}

		public void InstrumentBar_MovePatch(int patchID, int newPosition)
		{
			InstrumentBarViewModel viewModel = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(viewModel, () => _instrumentBarPresenter.MovePatch(viewModel, patchID, newPosition));
		}

		public void InstrumentBar_MovePatchBackward(int patchID)
		{
			InstrumentBarViewModel viewModel = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(viewModel, () => _instrumentBarPresenter.MovePatchBackward(viewModel, patchID));
		}

		public void InstrumentBar_MovePatchForward(int patchID)
		{
			InstrumentBarViewModel viewModel = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(viewModel, () => _instrumentBarPresenter.MovePatchForward(viewModel, patchID));
		}

		public void InstrumentBar_Play()
		{
			InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(userInput, () => _instrumentBarPresenter.Play(userInput));
		}

		public void InstrumentBar_PlayPatch(int patchID)
		{
			InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(userInput, () => _instrumentBarPresenter.PlayPatch(userInput, patchID));
		}

		public void InstrumentBar_DeleteMidiMappingGroup(int midiMappingGroupID)
		{
			InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(userInput, () => _instrumentBarPresenter.DeleteMidiMappingGroup(userInput, midiMappingGroupID));
		}

		public void InstrumentBar_RemovePatch(int patchID)
		{
			InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(userInput, () => _instrumentBarPresenter.DeletePatch(userInput, patchID));
		}

		private void InstrumentBar_SetScale(int scaleID)
		{
			InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

			ExecuteReadAction(userInput, () => _instrumentBarPresenter.SetScale(userInput, scaleID));
		}

		// Library

		public void LibraryProperties_Close(int documentReferenceID)
		{
			// GetViewModel
			LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

			// Template Method
			LibraryPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _libraryPropertiesPresenter.Close(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleLibraryProperties = null;

				DocumentViewModel_Refresh();
			}
		}

		public void LibraryProperties_LoseFocus(int documentReferenceID)
		{
			// GetViewModel
			LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

			// Template Method
			LibraryPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _libraryPropertiesPresenter.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void LibraryProperties_OpenExternally(int documentReferenceID)
		{
			LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

			ExecuteReadAction(userInput, () => _libraryPropertiesPresenter.OpenExternally(userInput));
		}

		public void LibraryProperties_Play(int documentReferenceID)
		{
			LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

			ExecuteReadAction(userInput, () => _libraryPropertiesPresenter.Play(userInput));
		}

		public void LibraryProperties_Remove(int documentReferenceID)
		{
			// GetViewModel
			LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

			// Template Method
			LibraryPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _libraryPropertiesPresenter.Remove(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		private void LibraryProperties_Show(int documentReferenceID)
		{
			LibraryPropertiesViewModel viewModel = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

			ExecuteNonPersistedAction(viewModel, () => _libraryPropertiesPresenter.Show(viewModel));
		}

		private void LibraryProperties_Switch(int documentReferenceID)
		{
			if (MainViewModel.PropertiesPanelVisible)
			{
				LibraryProperties_Show(documentReferenceID);
			}
		}

		/// <see cref="LibrarySelectionPopupPresenter.Cancel"/>
		public void LibrarySelectionPopup_Cancel()
		{
			LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

			ExecuteNonPersistedAction(userInput, () => _librarySelectionPopupPresenter.Cancel(userInput));
		}

		/// <see cref="LibrarySelectionPopupPresenter.Close"/>
		public void LibrarySelectionPopup_Close()
		{
			LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

			ExecuteNonPersistedAction(userInput, () => _librarySelectionPopupPresenter.Close(userInput));
		}

		public void LibrarySelectionPopup_OK(int? lowerDocumentID)
		{
			// GetViewModel
			LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

			// Template Method
			LibrarySelectionPopupViewModel viewModel = ExecuteCreateAction(userInput, () => _librarySelectionPopupPresenter.OK(userInput, lowerDocumentID));

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.DocumentReference, viewModel.CreatedDocumentReferenceID).ToViewModel().AsArray(),
					States = GetLibraryStates(viewModel.CreatedDocumentReferenceID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);
			}
		}

		public void LibrarySelectionPopup_OpenItemExternally(int lowerDocumentID)
		{
			LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

			ExecuteReadAction(userInput, () => _librarySelectionPopupPresenter.OpenItemExternally(userInput, lowerDocumentID));
		}

		public void LibrarySelectionPopup_Play(int lowerDocumentID)
		{
			LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

			ExecuteReadAction(userInput, () => _librarySelectionPopupPresenter.Play(userInput, lowerDocumentID));
		}

		// MidiMapping

		public void MidiMappingGroupDetails_AddToInstrument(int id)
		{
			// Redirect
			InstrumentBar_AddMidiMappingGroup(id);
		}

		public void MidiMappingGroupDetails_Close(int id)
		{
			MidiMappingGroupDetailsViewModel userInput = ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(userInput, () => _midiMappingDetailsPresenter.Close(userInput));

			MainViewModel.Document.VisibleMidiMappingGroupDetails = null;
		}

		public void MidiMappingGroupDetails_CreateMidiMapping(int midiMappingGroupID)
		{
			// GetViewModel
			MidiMappingGroupDetailsViewModel userInput = ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document, midiMappingGroupID);

			// Template Method
			MidiMappingGroupDetailsViewModel viewModel = ExecuteCreateAction(userInput, () => _midiMappingDetailsPresenter.CreateMidiMapping(userInput));

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.MidiMapping, viewModel.CreatedMidiMappingID).ToViewModel().AsArray(),
					States = GetMidiMappingStates(viewModel.CreatedMidiMappingID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);

				// Redirect
				MidiMapping_Expand(midiMappingGroupID, viewModel.CreatedMidiMappingID);
			}
		}

		public void MidiMappingGroupDetails_DeleteSelectedMidiMapping(int midiMappingGroupID)
		{
			// GetViewModel
			MidiMappingGroupDetailsViewModel userInput = ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document, midiMappingGroupID);

			// Undo History
			int id = userInput.SelectedMidiMapping?.ID ?? 0;
			UndoDeleteViewModel undoItem = null;
			if (id != 0)
			{
				undoItem = new UndoDeleteViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.MidiMapping, id).ToViewModel().AsArray(),
					States = GetMidiMappingStates(id)
				};
			}

			// Template Method
			MidiMappingGroupDetailsViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _midiMappingDetailsPresenter.DeleteSelectedMidiMapping(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void MidiMappingGroupDetails_ExpandMidiMapping(int midiMappingID)
		{
			// Redirect
			MidiMappingProperties_Show(midiMappingID);
		}

		public void MidiMappingGroupDetails_MoveMidiMapping(int midiMappingGroupID, int midiMappingID, float centerX, float centerY)
		{
			MidiMappingGroupDetailsViewModel userInput = ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document, midiMappingGroupID);

			ExecuteUpdateAction(userInput, () => _midiMappingDetailsPresenter.MoveMidiMapping(userInput, midiMappingID, centerX, centerY));

		}

		/// <summary> Only selecting the element, not e.g. switching properties. </summary>
		private void MidiMappingGroupDetails_SelectMidiMapping(int midiMappingGroupID, int midiMappingID)
		{
			MidiMappingGroupDetailsViewModel userInput = ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document, midiMappingGroupID);

			ExecuteNonPersistedAction(userInput, () => _midiMappingDetailsPresenter.SelectMidiMapping(userInput, midiMappingID));
		}

		private void MidiMappingGroupDetails_Show(int id)
		{
			MidiMappingGroupDetailsViewModel userInput = ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(userInput, () => _midiMappingDetailsPresenter.Show(userInput));
		}

		private void MidiMappingGroupDetails_Switch(int id)
		{
			if (MainViewModel.DetailsOrGridPanelVisible)
			{
				MidiMappingGroupDetails_Show(id);
			}
		}

		/// <summary> Affects multiple partials. </summary>
		private void MidiMapping_Expand(int midiMappingGroupID, int midiMappingID)
		{
			// Redirect
			MidiMappingProperties_Show(midiMappingID);
			MidiMappingGroupDetails_Show(midiMappingGroupID);
			MidiMappingGroupDetails_SelectMidiMapping(midiMappingGroupID, midiMappingID);
		}

		/// <summary> Affects multiple partials. </summary>
		public void MidiMapping_Select(int midiMappingGroupID, int midiMappingID)
		{
			// Redirect
			MidiMappingGroupDetails_SelectMidiMapping(midiMappingGroupID, midiMappingID);
			MidiMappingProperties_Switch(midiMappingID);
		}

		public void MidiMappingProperties_ChangeMidiMappingType(int midiMappingID)
		{
			MidiMappingPropertiesViewModel userInput = ViewModelSelector.GetMidiMappingPropertiesViewModel(MainViewModel.Document, midiMappingID);

			ExecuteUpdateAction(userInput, () => _midiMappingPropertiesPresenter.ChangeMidiMappingType(userInput));
		}

		public void MidiMappingProperties_Close(int id)
		{
			// GetViewModel
			MidiMappingPropertiesViewModel userInput = ViewModelSelector.GetMidiMappingPropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			MidiMappingPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _midiMappingPropertiesPresenter.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleMidiMappingProperties = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void MidiMappingProperties_Delete(int id)
		{
			// GetViewModel
			MidiMappingPropertiesViewModel userInput = ViewModelSelector.GetMidiMappingPropertiesViewModel(MainViewModel.Document, id);

			// Undo History
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.MidiMapping, id).ToViewModel().AsArray(),
				States = GetMidiMappingStates(id)
			};

			// Template Method
			MidiMappingPropertiesViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _midiMappingPropertiesPresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void MidiMappingProperties_Expand(int id)
		{
			MidiMappingPropertiesViewModel viewModel = ViewModelSelector.GetMidiMappingPropertiesViewModel(MainViewModel.Document, id);

			// Redirect
			MidiMapping_Expand(viewModel.MidiMappingGroupID, id);
		}

		public void MidiMappingProperties_LoseFocus(int id)
		{
			// GetViewModel
			MidiMappingPropertiesViewModel userInput = ViewModelSelector.GetMidiMappingPropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			MidiMappingPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _midiMappingPropertiesPresenter.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		private void MidiMappingProperties_Show(int id)
		{
			MidiMappingPropertiesViewModel viewModel = ViewModelSelector.GetMidiMappingPropertiesViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(viewModel, () => _midiMappingPropertiesPresenter.Show(viewModel));
		}

		private void MidiMappingProperties_Switch(int id)
		{
			if (MainViewModel.PropertiesPanelVisible)
			{
				// Redirect
				MidiMappingProperties_Show(id);
			}
		}

		private void MidiMappingGroup_Expand(int id) => MidiMappingGroupDetails_Show(id);

		// Monitoring

		public void Monitoring_DimensionValuesChanged(IList<(DimensionEnum dimensionEnum, string name, double value)> values)
		{
			MonitoringBarViewModel viewModel = MainViewModel.MonitoringBar;

			ExecuteNonPersistedAction(viewModel, () => _monitoringBarPresenter.DimensionValuesChanged(viewModel, values));
		}

		public void Monitoring_MidiNoteOnOccurred(int midiNoteNumber, int midiVelocity, int midiChannel)
		{
			MonitoringBarViewModel viewModel = MainViewModel.MonitoringBar;

			ExecuteNonPersistedAction(viewModel, () => _monitoringBarPresenter.MidiNoteOnOccurred(viewModel, midiNoteNumber, midiVelocity, midiChannel));
		}

		public void Monitoring_MidiControllerValueChanged(int midiControllerCode, int midiControllerValue, int midiChannel)
		{
			MonitoringBarViewModel viewModel = MainViewModel.MonitoringBar;

			ExecuteNonPersistedAction(viewModel, () => _monitoringBarPresenter.MidiControllerValueChanged(viewModel, midiControllerCode, midiControllerValue, midiChannel));
		}

		// Node

		/// <summary>
		/// Rotates between node types for the selected node.
		/// If no node is selected, nothing happens.
		/// </summary>
		public void Node_ChangeSelectedNodeType(int curveID)
		{
			// GetViewModel
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

			// Undo History
			IList<ScreenViewModelBase> oldStates = default;
			if (userInput.SelectedNodeID.HasValue)
			{
				oldStates = GetNodeStates(userInput.SelectedNodeID.Value);
			}

			// Template Method
			CurveDetailsViewModel viewModel = ExecuteSpecialUpdateAction(userInput, () => _curveDetailsPresenter.ChangeSelectedNodeType(userInput));

			if (viewModel.Successful && userInput.SelectedNodeID.HasValue)
			{
				// Refresh
				int nodeID = userInput.SelectedNodeID.Value;
				CurveDetailsNode_Refresh(curveID, nodeID);
				NodeProperties_Refresh(nodeID);

				// Undo History
				IList<ScreenViewModelBase> newStates = GetNodeStates(userInput.SelectedNodeID.Value);
				var undoItem = new UndoUpdateViewModel
				{
					OldStates = oldStates,
					NewStates = newStates
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);
			}
		}

		public void Node_Create(int curveID)
		{
			// GetViewModel
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

			// Template Method
			CurveDetailsViewModel viewModel = ExecuteCreateAction(userInput, () => _curveDetailsPresenter.CreateNode(userInput));

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.Node, viewModel.CreatedNodeID).ToViewModel().AsArray(),
					States = GetNodeStates(viewModel.CreatedNodeID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);
			}
		}

		public void Node_DeleteSelected(int curveID)
		{
			// GetViewModel
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

			// Undo History
			int id = userInput.SelectedNodeID ?? 0;
			UndoDeleteViewModel undoItem = null;
			if (id != 0)
			{
				undoItem = new UndoDeleteViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.Node, id).ToViewModel().AsArray(),
					States = GetNodeStates(id)
				};
			}

			// Template Method
			CurveDetailsViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _curveDetailsPresenter.DeleteSelectedNode(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void Node_Moving(int curveID, int nodeID, double x, double y)
		{
			// Opted to not use the TemplateActionMethod
			// (which would do a complete DocumentViewModel to Entity conversion),
			// because this is faster but less robust.
			// Because it is not nice when moving nodes is slow.
			// When you work in-memory backed with zipped XML,
			// you might use the more robust method again.
			// The overhead is mainly in the database queries.

			// GetViewModel
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

			// Partial ToEntity
			userInput.ToEntityWithNodes(_curveRepositories);

			// Partial Action
			CurveDetailsViewModel viewModel = _curveDetailsPresenter.NodeMoving(userInput, nodeID, x, y);

			// Refresh
			if (viewModel.Successful)
			{
				CurveDetailsNode_Refresh(curveID, nodeID);
				NodeProperties_Refresh(nodeID);
			}
		}

		public void Node_Moved(int curveID, int nodeID, double x, double y)
		{
			// GetViewModel
			CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

			// TemplateMethod
			CurveDetailsViewModel viewModel = ExecuteUpdateAction(userInput, () => _curveDetailsPresenter.NodeMoved(userInput, nodeID, x, y));

			// Refresh
			if (viewModel.Successful)
			{
				CurveDetailsNode_Refresh(curveID, nodeID);
				NodeProperties_Refresh(nodeID);
			}
		}

		public void NodeProperties_Close(int id)
		{
			// GetViewModel
			NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			NodePropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _nodePropertiesPresenter.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleNodeProperties = null;

				// Refresh
				Node node = _repositories.NodeRepository.Get(id);
				CurveDetailsNode_Refresh(node.Curve.ID, id);
			}
		}

		public void NodeProperties_Delete(int id)
		{
			// GetViewModel
			NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

			// Undo History
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.Node, id).ToViewModel().AsArray(),
				States = GetNodeStates(id)
			};

			// Template Method
			NodePropertiesViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _nodePropertiesPresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void NodeProperties_Expand(int id)
		{
			NodePropertiesViewModel viewModel = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

			// Redirect
			Node_Expand(viewModel.CurveID, id);
		}

		public void NodeProperties_LoseFocus(int id)
		{
			// GetViewModel
			NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			NodePropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _nodePropertiesPresenter.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				Node node = _repositories.NodeRepository.Get(id);
				CurveDetailsNode_Refresh(node.Curve.ID, id);
			}
		}

		private void NodeProperties_Show(int id)
		{
			NodePropertiesViewModel viewModel = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(viewModel, () => _nodePropertiesPresenter.Show(viewModel));
		}

		private void NodeProperties_Switch(int id)
		{
			if (MainViewModel.PropertiesPanelVisible)
			{
				NodeProperties_Show(id);
			}
		}

		public void Node_Select(int curveID, int nodeID)
		{
			// Redirect
			CurveDetails_SelectNode(curveID, nodeID);
			NodeProperties_Switch(nodeID);
		}

		private void Node_Expand(int curveID, int nodeID)
		{
			ExecuteReadAction(
				null,
				() =>
				{
					// GetEntity
					int operatorID = GetOperatorIDByCurveID(curveID);
					Operator op = _repositories.OperatorRepository.Get(operatorID);
					int patchID = op.Patch.ID;

					// Redirect
					NodeProperties_Show(nodeID);
					CurveDetails_Show(curveID);
					CurveDetails_SelectNode(curveID, nodeID);
					PatchDetails_Show(patchID);
					PatchDetails_SelectOperator(patchID, operatorID);
				});
		}

		// Operator

		public void Operator_ChangeInputOutlet(int patchID, int inletID, int inputOutletID)
		{
			PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

			ExecuteUpdateAction(userInput, () => _patchDetailsPresenter.ChangeInputOutlet(userInput, inletID, inputOutletID));
		}

		public void Operator_Move(int patchID, int operatorID, float centerX, float centerY)
		{
			PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

			ExecuteUpdateAction(userInput, () => _patchDetailsPresenter.MoveOperator(userInput, operatorID, centerX, centerY));
		}

		public void OperatorProperties_Close(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel userInput = ViewModelSelector.GetOperatorPropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_Close_ForCache(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForCache userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForCache viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForCache.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_ForCache = null;

				// Refresh
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_Close_ForCurve(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve_ByOperatorID(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForCurve viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForCurve.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_ForCurve = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_Close_ForInletsToDimension(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForInletsToDimension userInput =
				ViewModelSelector.GetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForInletsToDimension viewModel = ExecuteUpdateAction(
				userInput,
				() => _operatorPropertiesPresenter_ForInletsToDimension.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension = null;

				// Refresh
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_Close_ForNumber(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForNumber userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForNumber viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForNumber.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_ForNumber = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_Close_ForPatchInlet(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForPatchInlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForPatchInlet viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_Close_ForPatchOutlet(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForPatchOutlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForPatchOutlet viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_Close_ForSample(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForSample userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForSample viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForSample.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_ForSample = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_Close_WithInterpolation(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_WithInterpolation userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_WithInterpolation viewModel =
				ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_WithInterpolation.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = null;

				// Refresh
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_Close_WithCollectionRecalculation(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_WithCollectionRecalculation userInput =
				ViewModelSelector.GetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ExecuteUpdateAction(
				userInput,
				() => _operatorPropertiesPresenter_WithCollectionRecalculation.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = null;

				// Refresh
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_Delete(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModelBase userInput = ViewModelSelector.GetOperatorPropertiesViewModelPolymorphic(MainViewModel.Document, id);

			// Undo History
			IList<int> deletedOperatorIDs = GetOperatorIDsToDelete(userInput.PatchID, id);
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = deletedOperatorIDs.Select(x => (EntityTypeEnum.Operator, x).ToViewModel()).ToArray(),
				States = deletedOperatorIDs.SelectMany(GetOperatorStates).Distinct().ToArray()
			};

			// TemplateMethod
			OperatorPropertiesViewModelBase viewModel = ExecuteDeleteAction(userInput, undoItem, () => GetOperatorPropertiesPresenter(id).Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_Expand(int id)
		{
			// Redirect
			Operator_Expand(id);
		}

		public void OperatorProperties_LoseFocus(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel userInput = ViewModelSelector.GetOperatorPropertiesViewModel(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_LoseFocus_ForCache(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForCache userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForCache viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForCache.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_LoseFocus_ForCurve(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve_ByOperatorID(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForCurve viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForCurve.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_LoseFocus_ForInletsToDimension(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForInletsToDimension userInput =
				ViewModelSelector.GetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForInletsToDimension viewModel = ExecuteUpdateAction(
				userInput,
				() => _operatorPropertiesPresenter_ForInletsToDimension.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_LoseFocus_ForNumber(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForNumber userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForNumber viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForNumber.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_LoseFocus_ForPatchInlet(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForPatchInlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForPatchInlet viewModel =
				ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_LoseFocus_ForPatchOutlet(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForPatchOutlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForPatchOutlet viewModel =
				ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_LoseFocus_ForSample(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_ForSample userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_ForSample viewModel = ExecuteUpdateAction(userInput, () => _operatorPropertiesPresenter_ForSample.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void OperatorProperties_LoseFocus_WithInterpolation(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_WithInterpolation userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_WithInterpolation viewModel = ExecuteUpdateAction(
				userInput,
				() => _operatorPropertiesPresenter_WithInterpolation.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_LoseFocus_WithCollectionRecalculation(int id)
		{
			// GetViewModel
			OperatorPropertiesViewModel_WithCollectionRecalculation userInput =
				ViewModelSelector.GetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, id);

			// TemplateMethod
			OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ExecuteUpdateAction(
				userInput,
				() => _operatorPropertiesPresenter_WithCollectionRecalculation.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				PatchDetails_RefreshOperator(userInput.ID);
			}
		}

		public void OperatorProperties_Play(int id)
		{
			OperatorPropertiesViewModelBase userInput = ViewModelSelector.GetOperatorPropertiesViewModelPolymorphic(MainViewModel.Document, id);

			ExecuteReadAction(userInput, () => GetOperatorPropertiesPresenter(id).Play(userInput));
		}

		private void OperatorProperties_Show(int id)
		{
			{
				OperatorPropertiesViewModel viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_ForCache viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForCache.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_ForCurve viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve_ByOperatorID(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForCurve.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_ForInletsToDimension
					viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForInletsToDimension.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_ForNumber viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForNumber.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_ForPatchInlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForPatchInlet.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_ForPatchOutlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForPatchOutlet.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_ForSample viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForSample.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_WithInterpolation viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_WithInterpolation.Show(viewModel));
					return;
				}
			}
			{
				OperatorPropertiesViewModel_WithCollectionRecalculation viewModel =
					ViewModelSelector.TryGetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, id);
				if (viewModel != null)
				{
					ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_WithCollectionRecalculation.Show(viewModel));
					return;
				}
			}

			throw new NotFoundException<OperatorPropertiesViewModelBase>(new { OperatorID = id });
		}

		private void OperatorProperties_Switch(int id)
		{
			if (MainViewModel.PropertiesPanelVisible)
			{
				OperatorProperties_Show(id);
			}
		}

		public void Operator_Select(int patchID, int operatorID)
		{
			// Redirect
			PatchDetails_SelectOperator(patchID, operatorID);
			OperatorProperties_Switch(operatorID);
		}

		public void Operator_Expand(int operatorID)
		{
			ExecuteReadAction(
				null,
				() =>
				{
					// GetEntities
					Operator op = _repositories.OperatorRepository.Get(operatorID);
					Curve curve = op.Curve;

					// Redirect
					if (curve != null)
					{
						Curve_Expand(curve.ID);
					}
					else
					{
						int patchID = op.Patch.ID;
						OperatorProperties_Show(operatorID);
						PatchDetails_Show(patchID);
					}
				});
		}

		// Patch

		public void PatchDetails_AddToInstrument(int id)
		{
			// Redirect
			InstrumentBar_AddPatch(id);
		}

		public void PatchDetails_Close(int id)
		{
			PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(userInput, () => _patchDetailsPresenter.Close(userInput));

			MainViewModel.Document.VisiblePatchDetails = null;
		}

		public void PatchDetails_DeleteSelectedOperator(int patchID)
		{
			// GetViewModel
			PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

			// Undo History
			IList<int> operatorIDsToDelete = GetOperatorIDsToDelete(patchID, userInput.SelectedOperator?.ID);
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = operatorIDsToDelete.Select(x => (EntityTypeEnum.Operator, x).ToViewModel()).ToArray(),
				States = operatorIDsToDelete.SelectMany(GetOperatorStates).Distinct().ToArray()
			};

			// Template Method
			PatchDetailsViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _patchDetailsPresenter.DeleteOperator(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void PatchDetails_Play(int id)
		{
			PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

			ExecuteReadAction(userInput, () => _patchDetailsPresenter.Play(userInput));
		}

		private void PatchDetails_SelectOperator(int patchID, int operatorID)
		{
			PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

			ExecuteNonPersistedAction(userInput, () => _patchDetailsPresenter.SelectOperator(userInput, operatorID));
		}

		public void PatchDetails_Show(int id)
		{
			PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(userInput, () => _patchDetailsPresenter.Show(userInput));
		}

		public void PatchDetails_Expand(int id)
		{
			// Redirect
			Patch_Expand(id);
		}

		public void PatchDetails_Select(int id)
		{
			// Redirect
			PatchProperties_Switch(id);
			DocumentTree_SelectPatch(id);
		}

		private void Patch_Expand(int id)
		{
			ExecuteReadAction(
				null,
				() =>
				{
					// GetEntities
					Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
					Patch patch = _repositories.PatchRepository.Get(id);

					// Business
					bool isExternal = patch.IsExternal(document);

					if (isExternal)
					{
						// Non-Persisted
						MainViewModel.Document.DocumentToOpenExternally = patch.Document.ToIDAndName();
						MainViewModel.Document.PatchToOpenExternally = patch.ToIDAndName();
					}
					else
					{
						// Redirect
						PatchProperties_Show(id);
						PatchDetails_Show(id);
						DocumentTree_SelectPatch(id);
					}
				});
		}

		public void PatchProperties_AddToInstrument(int id)
		{
			// Redirect
			InstrumentBar_AddPatch(id);
		}

		public void PatchProperties_ChangeHasDimension(int id)
		{
			PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

			ExecuteUpdateAction(userInput, () => _patchPropertiesPresenter.ChangeHasDimension(userInput));
		}

		public void PatchProperties_Close(int id)
		{
			// GetViewModel
			PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

			// Template Method
			PatchPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _patchPropertiesPresenter.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisiblePatchProperties = null;

				// Refresh
				DocumentViewModel_Refresh();
			}
		}

		public void PatchProperties_Delete(int id)
		{
			// GetViewModel
			PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

			// Undo History
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.Patch, id).ToViewModel().AsArray(),
				States = GetPatchStates(id)
			};

			// Template Method
			PatchPropertiesViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _patchPropertiesPresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void PatchProperties_Expand(int id)
		{
			// Redirect
			Patch_Expand(id);
		}

		public void PatchProperties_LoseFocus(int id)
		{
			// GetViewModel
			PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

			// Template Method
			PatchPropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _patchPropertiesPresenter.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void PatchProperties_Play(int id)
		{
			PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

			ExecuteReadAction(userInput, () => _patchPropertiesPresenter.Play(userInput));
		}

		private void PatchProperties_Show(int id)
		{
			PatchPropertiesViewModel viewModel = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(viewModel, () => _patchPropertiesPresenter.Show(viewModel));
		}

		private void PatchProperties_Switch(int id)
		{
			if (MainViewModel.PropertiesPanelVisible)
			{
				PatchProperties_Show(id);
			}
		}

		// Sample

		public void SampleFileBrowser_Cancel()
		{
			SampleFileBrowserViewModel userInput = MainViewModel.Document.SampleFileBrowser;

			ExecuteNonPersistedAction(userInput, () => _sampleFileBrowserPresenter.Cancel(userInput));
		}

		public void SampleFileBrowser_OK()
		{
			// GetViewModel
			SampleFileBrowserViewModel userInput = MainViewModel.Document.SampleFileBrowser;

			// TemplateMethod
			SampleFileBrowserViewModel viewModel = ExecuteCreateAction(userInput, () => _sampleFileBrowserPresenter.OK(userInput));

			if (viewModel.Successful)
			{
				// Refresh
				DocumentViewModel_Refresh();

				// Undo History
				// (Put main operator last so it is dispatched last upon redo and put on top.)
				IList<int> createdOperatorIDs = viewModel.AutoCreatedNumberOperatorIDs.Union(viewModel.CreatedMainOperatorID).ToArray();
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = createdOperatorIDs.Select(x => (EntityTypeEnum.Operator, x).ToViewModel()).ToArray(),
					States = createdOperatorIDs.SelectMany(GetOperatorStates).Distinct().ToArray()
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);
			}
		}

		// SaveChanges

		private void SaveChangesPopup_Show(int? documentIDToOpenAfterConfirmation = null, bool mustGoToDocumentCreateAfterConfirmation = false)
		{
			SaveChangesPopupViewModel viewModel = MainViewModel.Document.SaveChangesPopup;

			ExecuteNonPersistedAction(
				viewModel,
				() => _saveChangesPopupPresenter.Show(viewModel, documentIDToOpenAfterConfirmation, mustGoToDocumentCreateAfterConfirmation));
		}

		public void SaveChangesPopup_Cancel()
		{
			SaveChangesPopupViewModel viewModel = MainViewModel.Document.SaveChangesPopup;

			ExecuteNonPersistedAction(viewModel, () => _saveChangesPopupPresenter.Cancel(viewModel));
		}

		public void SaveChangesPopup_No()
		{
			// GetViewModel
			SaveChangesPopupViewModel viewModel = MainViewModel.Document.SaveChangesPopup;

			// TemplateMethod
			ExecuteNonPersistedAction(
				viewModel,
				() =>
				{
					_saveChangesPopupPresenter.No(viewModel);
					MainViewModel.Document.IsDirty = false;
				});

			// Redirect
			RedirectAfterSaveChangesPopup(viewModel);
		}

		public void SaveChangesPopup_Yes()
		{
			// GetViewModel
			SaveChangesPopupViewModel viewModel = MainViewModel.Document.SaveChangesPopup;

			// TemplateMethod
			ExecuteNonPersistedAction(viewModel, () => _saveChangesPopupPresenter.Yes(viewModel));

			// Redirect
			Document_Save();
			RedirectAfterSaveChangesPopup(viewModel);
		}

		private void RedirectAfterSaveChangesPopup(SaveChangesPopupViewModel viewModel)
		{
			Document_Close();

			if (viewModel.DocumentIDToOpenAfterConfirmation.HasValue)
			{
				Document_Open(viewModel.DocumentIDToOpenAfterConfirmation.Value);
			}
			else if (viewModel.MustGoToDocumentCreateAfterConfirmation)
			{
				Document_Create();
			}

			viewModel.DocumentIDToOpenAfterConfirmation = null;
			viewModel.MustGoToDocumentCreateAfterConfirmation = false;
		}

		// Scale

		private void Scale_Show(int id)
		{
			// Redirect
			ScaleProperties_Show(id);
			ToneGridEdit_Show(id);
		}

		private void ScaleProperties_Show(int id)
		{
			ScalePropertiesViewModel viewModel = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

			ExecuteNonPersistedAction(viewModel, () => _scalePropertiesPresenter.Show(viewModel));
		}

		public void ScaleProperties_Close(int id)
		{
			// Get ViewModel
			ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

			// Template Method
			ScalePropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _scalePropertiesPresenter.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleScaleProperties = null;

				// Refresh
				ToneGridEdit_Refresh(userInput.Entity.ID);
				DocumentTree_Refresh();
				ScaleLookup_Refresh();
			}
		}

		public void ScaleProperties_Delete(int id)
		{
			// GetViewModel
			ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

			// Undo History
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.Scale, id).ToViewModel().AsArray(),
				States = GetScaleStates(id)
			};

			// Template Method
			ScalePropertiesViewModel viewModel = ExecuteDeleteAction(userInput, undoItem, () => _scalePropertiesPresenter.Delete(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				DocumentViewModel_Refresh();
			}
		}

		public void ScaleProperties_LoseFocus(int id)
		{
			// Get ViewModel
			ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

			// Template Method
			ScalePropertiesViewModel viewModel = ExecuteUpdateAction(userInput, () => _scalePropertiesPresenter.LoseFocus(userInput));

			// Refresh
			if (viewModel.Successful)
			{
				ToneGridEdit_Refresh(userInput.Entity.ID);
				DocumentTree_Refresh();
				ScaleLookup_Refresh();
			}
		}

		public void ScaleProperties_SetInstrumentScale(int scaleID)
		{
			// Redirect
			InstrumentBar_SetScale(scaleID);
		}

		private void ScaleProperties_Switch(int id)
		{
			if (MainViewModel.PropertiesPanelVisible)
			{
				ScaleProperties_Show(id);
			}
		}

		// Tone

		public void Tone_Create(int scaleID)
		{
			// GetViewModel
			ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

			// TemplateMethod
			ToneGridEditViewModel viewModel = ExecuteCreateAction(userInput, () => _toneGridEditPresenter.CreateTone(userInput));

			if (viewModel.Successful)
			{
				// Undo History
				var undoItem = new UndoCreateViewModel
				{
					EntityTypesAndIDs = (EntityTypeEnum.Tone, viewModel.CreatedToneID).ToViewModel().AsArray(),
					States = GetToneStates(scaleID)
				};
				MainViewModel.Document.UndoHistory.Push(undoItem);
			}
		}

		public void Tone_Delete(int scaleID, int toneID)
		{
			// GetViewModel
			ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

			// Undo History
			var undoItem = new UndoDeleteViewModel
			{
				EntityTypesAndIDs = (EntityTypeEnum.Tone, toneID).ToViewModel().AsArray(),
				States = GetToneStates(scaleID)
			};

			// Template Method
			ExecuteDeleteAction(userInput, undoItem, () => _toneGridEditPresenter.DeleteTone(userInput, toneID));
		}

		public void ToneGridEdit_Close(int scaleID)
		{
			ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

			ToneGridEditViewModel viewModel = ExecuteUpdateAction(userInput, () => _toneGridEditPresenter.Close(userInput));

			if (viewModel.Successful)
			{
				MainViewModel.Document.VisibleToneGridEdit = null;
			}
		}

		public void ToneGridEdit_Edit(int scaleID)
		{
			ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

			ExecuteUpdateAction(userInput, () => _toneGridEditPresenter.Edit(userInput));
		}

		public void ToneGridEdit_LoseFocus(int scaleID)
		{
			ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

			ExecuteUpdateAction(userInput, () => _toneGridEditPresenter.LoseFocus(userInput));
		}

		public void ToneGridEdit_SetInstrumentScale(int scaleID)
		{
			// Redirect
			InstrumentBar_SetScale(scaleID);
		}

		private void ToneGridEdit_Show(int scaleID)
		{
			ToneGridEditViewModel viewModel = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

			ExecuteNonPersistedAction(viewModel, () => _toneGridEditPresenter.Show(viewModel));
		}

		private void ToneGridEdit_Switch(int scaleID)
		{
			if (MainViewModel.DetailsOrGridPanelVisible)
			{
				ToneGridEdit_Show(scaleID);
			}
		}

		public void Tone_Play(int scaleID, int toneID)
		{
			// NOTE:
			// Cannot use partial presenter, because this action uses both
			// ToneGridEditViewModel and Instrument view model.

			// GetEntity
			ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

			// Template Method
			ExecuteReadAction(
				userInput,
				() =>
				{
					// ViewModel Validator
					IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
					if (!viewModelValidator.IsValid)
					{
						userInput.ValidationMessages = viewModelValidator.Messages;
						DispatchViewModel(userInput);
						return null;
					}

					// GetEntities
					Tone tone = _repositories.ToneRepository.Get(toneID);

					var underlyingPatches = new List<Patch>(MainViewModel.Document.InstrumentBar.Patches.Count);
					foreach (InstrumentItemViewModel itemViewModel in MainViewModel.Document.InstrumentBar.Patches)
					{
						Patch underlyingPatch = _repositories.PatchRepository.Get(itemViewModel.EntityID);
						underlyingPatches.Add(underlyingPatch);
					}

					// Business
					Outlet outlet = null;
					if (underlyingPatches.Count != 0)
					{
						outlet = _autoPatcher.TryAutoPatchWithTone(tone, underlyingPatches);
					}

					if (outlet != null)
					{
						_autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
					}

					if (outlet == null) // Fallback to Sine
					{
						Patch patch = _patchFacade.CreatePatch();

						var operatorFactory = new OperatorFactory(patch, _repositories);
						double frequency = tone.GetFrequency();
						outlet = operatorFactory.Sine(operatorFactory.PatchInlet(DimensionEnum.Frequency, frequency));
					}

					// ToViewModel
					ToneGridEditViewModel viewModel = tone.Scale.ToToneGridEditViewModel();

					// Non-Persisted
					viewModel.OutletIDToPlay = outlet.ID;
					viewModel.Visible = userInput.Visible;
					viewModel.Successful = true;

					return viewModel;
				});
		}

		// Helpers

		/// <summary>
		/// A template method for a MainPresenter action method,
		/// that will not read or write the entity model,
		/// but works with non-entity model data only.
		///
		/// Most steps otherwise needed in for instance write actions are not needed.
		/// 
		/// Executes a sub-presenter's action and surrounds it with:
		/// a) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
		/// 
		/// All you need to do is provide the right sub-viewmodel,
		/// provide a delegate to the sub-presenter's action method.
		/// </summary>
		private void ExecuteNonPersistedAction<TViewModel>(TViewModel viewModelToDispatch, Action partialAction)
			where TViewModel : ScreenViewModelBase
		{
			if (viewModelToDispatch == null) throw new ArgumentNullException(nameof(viewModelToDispatch));

			// Partial Action
			partialAction();

			// DispatchViewModel
			DispatchViewModel(viewModelToDispatch);
		}

		/// <summary>
		/// A template method for a MainPresenter action method,
		/// that will read the document model, but not write to it.
		///
		/// This version omits the full document validation and successful flags.
		/// 
		/// Executes a sub-presenter's action and surrounds it with:
		/// a) Converting the full document view model to entity.
		/// b) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
		/// 
		/// All you need to do is provide the right sub-viewmodel,
		/// provide a delegate to the sub-presenter's action method.
		/// </summary>
		/// <param name="viewModelToDispatch">
		/// Can be null if no view model is relevant. But if you have a relevant view model, please pass it along.
		/// </param>
		private void ExecuteReadAction(ScreenViewModelBase viewModelToDispatch, Action partialAction)
		{
			// ToEntity
			if (MainViewModel.Document.IsOpen)
			{
				MainViewModel.ToEntityWithRelatedEntities(_repositories);
			}

			// Partial Action
			partialAction();

			// DispatchViewModel
			if (viewModelToDispatch != null)
			{
				DispatchViewModel(viewModelToDispatch);
			}
		}

		/// <summary>
		/// A template method for a MainPresenter action method,
		/// that will read the document model, but not write to it.
		///
		/// This version omits the full document validation and successful flags
		/// but allows the partial action to return a new view model.
		/// 
		/// Executes a sub-presenter's action and surrounds it with:
		/// a) Converting the full document view model to entity.
		/// b) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
		/// 
		/// All you need to do is provide the right sub-viewmodel,
		/// provide a delegate to the sub-presenter's action method.
		/// </summary>
		/// <param name="viewModelToDoNothingWith">
		/// You can pass null to it if you want.
		/// Or a specific view model that your action is about.
		/// This parameter is not used in this method.
		/// However, it is there for a very important reason.
		/// If it were not, you could by accident call the other overload
		/// of ExecuteReadAction and not use the return value of your delegate.
		/// By having this parameter, the overload that is called, is always
		/// only dependent on partialAction returning or not returning a view model.
		/// </param>
		// ReSharper disable once UnusedParameter.Local
		private void ExecuteReadAction<TViewModel>(TViewModel viewModelToDoNothingWith, Func<TViewModel> partialAction)
			where TViewModel : ScreenViewModelBase
		{
			// ToEntity
			if (MainViewModel.Document.IsOpen)
			{
				MainViewModel.ToEntityWithRelatedEntities(_repositories);
			}

			// Partial Action
			TViewModel viewModel = partialAction();

			// DispatchViewModel
			DispatchViewModel(viewModel);
		}

		private TViewModel ExecuteCreateAction<TViewModel>(TViewModel userInput, Func<TViewModel> partialAction)
			where TViewModel : ScreenViewModelBase
		{
			return ExecuteWriteAction(userInput, partialAction, undoHistoryDelegate: () => MainViewModel.Document.RedoFuture.Clear());
		}

		/// <summary>
		/// Manages undo, but works only if userInput is the final state of the action, not if other data changes have to be applied,
		/// before having the 'new state' of the undo action.
		/// </summary>
		private TViewModel ExecuteUpdateAction<TViewModel>(TViewModel userInput, Func<TViewModel> partialAction)
			where TViewModel : ScreenViewModelBase
		{
			return ExecuteWriteAction(
				userInput,
				partialAction,
				undoHistoryDelegate: () =>
				{
					var undoItemViewModel = new UndoUpdateViewModel
					{
						OldStates = userInput.OriginalState.AsArray(),
						NewStates = userInput.AsArray()
					};
					MainViewModel.Document.UndoHistory.Push(undoItemViewModel);

					MainViewModel.Document.RedoFuture.Clear();
				});
		}

		/// <summary>
		/// The normal ExecuteUpdateAction will handle undo state for most update actions.
		/// Too bad it is only suitable for when the userInput is the 'new state' for the undo history.
		/// For instance for the ChangeSelectedNodeType action, the user input is not the final state of the action.
		/// </summary>
		private TViewModel ExecuteSpecialUpdateAction<TViewModel>(TViewModel userInput, Func<TViewModel> partialAction)
			where TViewModel : ScreenViewModelBase
		{
			return ExecuteWriteAction(
				userInput,
				partialAction,
				undoHistoryDelegate: () => MainViewModel.Document.RedoFuture.Clear());
		}

		/// <param name="undoItemViewModel">
		/// For delete actions the undo item must be created before executing this template method,
		/// since afterwards the state to remember is already gone.
		/// </param>
		private TViewModel ExecuteDeleteAction<TViewModel>(TViewModel userInput, UndoDeleteViewModel undoItemViewModel, Func<TViewModel> partialAction)
			where TViewModel : ScreenViewModelBase
		{
			return ExecuteWriteAction(
				userInput,
				partialAction,
				undoHistoryDelegate: () =>
				{
					MainViewModel.Document.UndoHistory.Push(undoItemViewModel);
					MainViewModel.Document.RedoFuture.Clear();
				});
		}

		/// <summary>
		/// A template method for a MainPresenter action method,
		/// that will write to the document entity.
		/// 
		/// Works for most write actions. Less suitable for specialized cases:
		/// In particular the ones that are not about the open document.
		///
		/// Executes a sub-presenter's action and surrounds it with:
		/// a) Converting the full document view model to entity.
		/// b) Doing a full document validation.
		/// c) Managing view model transactionality.
		/// d) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
		/// e) Setting dirty flag
		/// f) Undo history handling (calls the undoHistoryDelegate)
		/// 
		/// All you need to do is provide the right sub-viewmodel,
		/// provide a delegate to the sub-presenter's action method
		/// and possibly do some refreshing of other view models afterwards.
		/// </summary>
		private TViewModel ExecuteWriteAction<TViewModel>(TViewModel userInput, Func<TViewModel> partialAction, Action undoHistoryDelegate)
			where TViewModel : ScreenViewModelBase
		{
			if (userInput == null) throw new NullException(() => userInput);

			// Set !Successful
			userInput.Successful = false;

			// ToEntity
			Document document = null;
			if (MainViewModel.Document.IsOpen)
			{
				document = MainViewModel.ToEntityWithRelatedEntities(_repositories);
			}

			// Partial Action
			TViewModel viewModel = partialAction();
			if (!viewModel.Successful)
			{
				// DispatchViewModel
				DispatchViewModel(viewModel);

				return viewModel;
			}

			// Set !Successful
			viewModel.Successful = false;

			if (MainViewModel.Document.IsOpen)
			{
				// Business
				IResult validationResult = _documentFacade.Save(document);
				if (!validationResult.Successful)
				{
					// Non-Persisted
					viewModel.ValidationMessages.AddRange(validationResult.Messages);

					// DispatchViewModel
					DispatchViewModel(viewModel);

					return viewModel;
				}

				// Undo History
				undoHistoryDelegate();

				// Dirty Flag
				MainViewModel.Document.IsDirty = true;
			}

			// Successful
			viewModel.Successful = true;

			// DispatchViewModel
			DispatchViewModel(viewModel);

			return viewModel;
		}
	}
}
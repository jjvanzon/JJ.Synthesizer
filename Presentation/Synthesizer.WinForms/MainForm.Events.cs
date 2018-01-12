using System;
using System.Windows.Forms;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
#pragma warning disable IDE1006 // Naming Styles

namespace JJ.Presentation.Synthesizer.WinForms
{
	internal partial class MainForm
	{
		private void BindEvents()
		{
			FormClosing += MainForm_FormClosing;

			audioFileOutputGridUserControl.CloseRequested += audioFileOutputGridUserControl_CloseRequested;
			audioFileOutputGridUserControl.AddRequested += audioFileOutputGridUserControl_AddRequested;
			audioFileOutputGridUserControl.RemoveRequested += audioFileOutputGridUserControl_RemoveRequested;
			audioFileOutputGridUserControl.ShowItemRequested += audioFileOutputGridUserControl_ShowItemRequested;
			audioFileOutputPropertiesUserControl.CloseRequested += audioFileOutputPropertiesUserControl_CloseRequested;
			audioFileOutputPropertiesUserControl.LoseFocusRequested += audioFileOutputPropertiesUserControl_LoseFocusRequested;
			audioFileOutputPropertiesUserControl.RemoveRequested += audioFileOutputPropertiesUserControl_RemoveRequested;
			audioOutputPropertiesUserControl.CloseRequested += audioOutputPropertiesUserControl_CloseRequested;
			audioOutputPropertiesUserControl.LoseFocusRequested += audioOutputPropertiesUserControl_LoseFocusRequested;
			audioOutputPropertiesUserControl.PlayRequested += audioOutputPropertiesUserControl_PlayRequested;
			currentInstrumentUserControl.ExpandRequested += currentInstrumentUserControl_ExpandRequested;
			currentInstrumentUserControl.ExpandItemRequested += currentInstrumentUserControl_ExpandItemRequested;
			currentInstrumentUserControl.MoveBackwardRequested += currentInstrumentUserControl_MoveBackwardRequested;
			currentInstrumentUserControl.MoveForwardRequested += currentInstrumentUserControl_MoveForwardRequested;
			currentInstrumentUserControl.PlayRequested += currentInstrumentUserControl_PlayRequested;
			currentInstrumentUserControl.PlayItemRequested += CurrentInstrumentUserControl_PlayItemRequested;
			currentInstrumentUserControl.RemoveRequested += currentInstrumentUserControl_RemoveRequested;
			curveDetailsListUserControl.ChangeSelectedNodeTypeRequested += curveDetailsListUserControl_ChangeSelectedNodeTypeRequested;
			curveDetailsListUserControl.CloseRequested += curveDetailsListUserControl_CloseRequested;
			curveDetailsListUserControl.CreateNodeRequested += curveDetailsListUserControl_CreateNodeRequested;
			curveDetailsListUserControl.DeleteSelectedNodeRequested += curveDetailsListUserControl_DeleteSelectedNodeRequested;
			curveDetailsListUserControl.ExpandCurveRequested += curveDetailsListUserControl_ExpandCurveRequested;
			curveDetailsListUserControl.LoseFocusRequested += curveDetailsListUserControl_LoseFocusRequested;
			curveDetailsListUserControl.NodeMoving += curveDetailsListUserControl_NodeMoving;
			curveDetailsListUserControl.NodeMoved += curveDetailsListUserControl_NodeMoved;
			curveDetailsListUserControl.SelectCurveRequested += curveDetailsListUserControl_SelectCurveRequested;
			curveDetailsListUserControl.SelectNodeRequested += curveDetailsListUserControl_SelectNodeRequested;
			curveDetailsListUserControl.ExpandNodeRequested += curveDetailsListUserControl_ExpandNodeRequested;
			documentDetailsUserControl.CloseRequested += documentDetailsUserControl_CloseRequested;
			documentDetailsUserControl.DeleteRequested += documentDetailsUserControl_DeleteRequested;
			documentDetailsUserControl.SaveRequested += documentDetailsUserControl_SaveRequested;
			documentGridUserControl.AddRequested += documentGridUserControl_AddRequested;
			documentGridUserControl.CloseRequested += documentGridUserControl_CloseRequested;
			documentGridUserControl.PlayRequested += documentGridUserControl_PlayRequested;
			documentGridUserControl.RemoveRequested += documentGridUserControl_RemoveRequested;
			documentGridUserControl.ShowItemRequested += documentGridUserControl_ShowItemRequested;
			documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
			documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;
			documentPropertiesUserControl.PlayRequested += documentPropertiesUserControl_PlayRequested;
			documentTreeUserControl.AddToInstrumentRequested += documentTreeUserControl_AddToInstrumentRequested;
			documentTreeUserControl.AudioFileOutputsNodeSelected += documentTreeUserControl_AudioFileOutputsNodeSelected;
			documentTreeUserControl.AudioOutputNodeSelected += documentTreeUserControl_AudioOutputNodeSelected;
			documentTreeUserControl.CloseRequested += documentTreeUserControl_CloseRequested;
			documentTreeUserControl.LibrariesNodeSelected += documentTreeUserControl_LibrariesNodeSelected;
			documentTreeUserControl.LibraryNodeSelected += documentTreeUserControl_LibraryNodeSelected;
			documentTreeUserControl.LibraryPatchNodeSelected += documentTreeUserControl_LibraryPatchNodeSelected;
			documentTreeUserControl.LibraryPatchGroupNodeSelected += documentTreeUserControl_LibraryPatchGroupNodeSelected;
			documentTreeUserControl.MidiNodeSelected += documentTreeUserControl_MidiNodeSelected;
			documentTreeUserControl.MidiMappingNodeSelected += documentTreeUserControl_MidiMappingNodeSelected;
			documentTreeUserControl.NewRequested += documentTreeUserControl_NewRequested;
			documentTreeUserControl.PatchGroupNodeSelected += documentTreeUserControl_PatchGroupNodeSelected;
			documentTreeUserControl.PatchHovered += documentTreeUserControl_PatchHovered;
			documentTreeUserControl.PatchNodeSelected += documentTreeUserControl_PatchNodeSelected;
			documentTreeUserControl.PlayRequested += documentTreeUserControl_PlayRequested;
			documentTreeUserControl.OpenItemExternallyRequested += documentTreeUserControl_OpenItemExternallyRequested;
			documentTreeUserControl.RedoRequested += documentTreeUserControl_RedoRequested;
			documentTreeUserControl.RefreshRequested += documentTreeUserControl_RefreshRequested;
			documentTreeUserControl.RemoveRequested += documentTreeUserControl_RemoveRequested;
			documentTreeUserControl.SaveRequested += documentTreeUserControl_SaveRequested;
			documentTreeUserControl.ScalesNodeSelected += documentTreeUserControl_ScalesNodeSelected;
			documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
			documentTreeUserControl.ShowAudioOutputRequested += documentTreeUserControl_ShowAudioOutputRequested;
			documentTreeUserControl.ShowLibraryRequested += documentTreeUserControl_ShowLibraryRequested;
			documentTreeUserControl.ShowMidiMappingRequested += DocumentTreeUserControl_ShowMidiMappingRequested;
			documentTreeUserControl.ShowPatchRequested += documentTreeUserControl_ShowPatchRequested;
			documentTreeUserControl.ShowScalesRequested += documentTreeUserControl_ShowScalesRequested;
			documentTreeUserControl.UndoRequested += documentTreeUserControl_UndoRequested;
			libraryPropertiesUserControl.CloseRequested += libraryPropertiesUserControl_CloseRequested;
			libraryPropertiesUserControl.LoseFocusRequested += libraryPropertiesUserControl_LoseFocusRequested;
			libraryPropertiesUserControl.PlayRequested += libraryPropertiesUserControl_PlayRequested;
			libraryPropertiesUserControl.ExpandRequested += libraryPropertiesUserControl_ExpandRequested;
			libraryPropertiesUserControl.RemoveRequested += libraryPropertiesUserControl_RemoveRequested;
			midiMappingDetailsUserControl.CloseRequested += midiMappingDetailsUserControl_CloseRequested		;
			midiMappingDetailsUserControl.MoveElementRequested += midiMappingDetailsUserControl_MoveElementRequested			;
			menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
			menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
			menuUserControl.ShowDocumentGridRequested += menuUserControl_ShowDocumentGridRequested;
			menuUserControl.ShowDocumentPropertiesRequested += MenuUserControl_ShowDocumentPropertiesRequested;
			nodePropertiesUserControl.CloseRequested += nodePropertiesUserControl_CloseRequested;
			nodePropertiesUserControl.ExpandRequested += nodePropertiesUserControl_ExpandRequested;
			nodePropertiesUserControl.LoseFocusRequested += nodePropertiesUserControl_LoseFocusRequested;
			nodePropertiesUserControl.RemoveRequested += nodePropertiesUserControl_RemoveRequested;
			operatorPropertiesUserControl.CloseRequested += operatorPropertiesUserControl_CloseRequested;
			operatorPropertiesUserControl.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl.LoseFocusRequested += operatorPropertiesUserControl_LoseFocusRequested;
			operatorPropertiesUserControl.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_ForCache.CloseRequested += operatorPropertiesUserControl_ForCache_CloseRequested;
			operatorPropertiesUserControl_ForCache.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForCache.LoseFocusRequested += operatorPropertiesUserControl_ForCache_LoseFocusRequested;
			operatorPropertiesUserControl_ForCache.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForCache.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_ForCurve.CloseRequested += operatorPropertiesUserControl_ForCurve_CloseRequested;
			operatorPropertiesUserControl_ForCurve.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForCurve.LoseFocusRequested += operatorPropertiesUserControl_ForCurve_LoseFocusRequested;
			operatorPropertiesUserControl_ForCurve.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForCurve.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_ForInletsToDimension.CloseRequested += operatorPropertiesUserControl_ForInletsToDimension_CloseRequested;
			operatorPropertiesUserControl_ForInletsToDimension.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForInletsToDimension.LoseFocusRequested += operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested;
			operatorPropertiesUserControl_ForInletsToDimension.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForInletsToDimension.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_ForNumber.CloseRequested += operatorPropertiesUserControl_ForNumber_CloseRequested;
			operatorPropertiesUserControl_ForNumber.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForNumber.LoseFocusRequested += operatorPropertiesUserControl_ForNumber_LoseFocusRequested;
			operatorPropertiesUserControl_ForNumber.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForNumber.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_ForPatchInlet.CloseRequested += operatorPropertiesUserControl_ForPatchInlet_CloseRequested;
			operatorPropertiesUserControl_ForPatchInlet.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForPatchInlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested;
			operatorPropertiesUserControl_ForPatchInlet.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForPatchInlet.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_ForPatchOutlet.CloseRequested += operatorPropertiesUserControl_ForPatchOutlet_CloseRequested;
			operatorPropertiesUserControl_ForPatchOutlet.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForPatchOutlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested;
			operatorPropertiesUserControl_ForPatchOutlet.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForPatchOutlet.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_ForSample.CloseRequested += operatorPropertiesUserControl_ForSample_CloseRequested;
			operatorPropertiesUserControl_ForSample.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForSample.LoseFocusRequested += operatorPropertiesUserControl_ForSample_LoseFocusRequested;
			operatorPropertiesUserControl_ForSample.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForSample.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_WithInterpolation.CloseRequested += operatorPropertiesUserControl_WithInterpolation_CloseRequested;
			operatorPropertiesUserControl_WithInterpolation.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_WithInterpolation.LoseFocusRequested += operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested;
			operatorPropertiesUserControl_WithInterpolation.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_WithInterpolation.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.CloseRequested += operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.LoseFocusRequested += operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
			patchDetailsUserControl.AddToInstrumentRequested += patchDetailsUserControl_AddToInstrumentRequested;
			patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
			patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
			patchDetailsUserControl.DeleteOperatorRequested += patchDetailsUserControl_DeleteOperatorRequested;
			patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
			patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
			patchDetailsUserControl.RemoveRequested += patchDetailsUserControl_RemoveRequested;
			patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
			patchDetailsUserControl.ExpandOperatorRequested += patchDetailsUserControl_ExpandOperatorRequested;
			patchDetailsUserControl.ExpandPatchRequested += patchDetailsUserControl_ExpandPatchRequested;
			patchDetailsUserControl.ExpandRequested += patchDetailsUserControl_ExpandRequested;
			patchDetailsUserControl.SelectPatchRequested += patchDetailsUserControl_SelectPatchRequested;
			patchPropertiesUserControl.AddToInstrumentRequested += patchPropertiesUserControl_AddToInstrumentRequested;
			patchPropertiesUserControl.CloseRequested += patchPropertiesUserControl_CloseRequested;
			patchPropertiesUserControl.ExpandRequested += patchPropertiesUserControl_ExpandRequested;
			patchPropertiesUserControl.HasDimensionChanged += patchPropertiesUserControl_HasDimensionChanged;
			patchPropertiesUserControl.LoseFocusRequested += patchPropertiesUserControl_LoseFocusRequested;
			patchPropertiesUserControl.PlayRequested += patchPropertiesUserControl_PlayRequested;
			patchPropertiesUserControl.RemoveRequested += patchPropertiesUserControl_RemoveRequested;
			scaleGridUserControl.CloseRequested += scaleGridUserControl_CloseRequested;
			scaleGridUserControl.AddRequested += scaleGridUserControl_AddRequested;
			scaleGridUserControl.RemoveRequested += scaleGridUserControl_RemoveRequested;
			scaleGridUserControl.ShowItemRequested += scaleGridUserControl_ShowItemRequested;
			scalePropertiesUserControl.CloseRequested += scalePropertiesUserControl_CloseRequested;
			scalePropertiesUserControl.LoseFocusRequested += scalePropertiesUserControl_LoseFocusRequested;
			scalePropertiesUserControl.RemoveRequested += scalePropertiesUserControl_RemoveRequested;
			toneGridEditUserControl.CloseRequested += toneGridEditUserControl_CloseRequested;
			toneGridEditUserControl.CreateToneRequested += toneGridEditUserControl_CreateToneRequested;
			toneGridEditUserControl.DeleteToneRequested += toneGridEditUserControl_DeleteToneRequested;
			toneGridEditUserControl.Edited += toneGridEditUserControl_Edited;
			toneGridEditUserControl.LoseFocusRequested += toneGridEditUserControl_LoseFocusRequested;
			toneGridEditUserControl.PlayToneRequested += toneGridEditUserControl_PlayToneRequested;

			_documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;
			_autoPatchPopupForm.CloseRequested += _autoPatchPopupForm_CloseRequested;
			_autoPatchPopupForm.SaveRequested += _autoPatchPopupForm_SaveRequested;
			_autoPatchPopupForm.patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
			_autoPatchPopupForm.patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
			_autoPatchPopupForm.patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
			_librarySelectionPopupForm.CancelRequested += _librarySelectionPopupForm_CancelRequested;
			_librarySelectionPopupForm.CloseRequested += _librarySelectionPopupForm_CloseRequested;
			_librarySelectionPopupForm.OKRequested += _librarySelectionPopupForm_OKRequested;
			_librarySelectionPopupForm.OpenItemExternallyRequested += _librarySelectionPopupForm_OpenItemExternallyRequested;
			_librarySelectionPopupForm.PlayRequested += _librarySelectionPopupForm_PlayRequested;

			ModalPopupHelper.DocumentDeleteConfirmed += ModalPopupHelper_DocumentDeleteConfirmed;
			ModalPopupHelper.DocumentDeleteCanceled += ModalPopupHelper_DocumentDeleteCanceled;
			ModalPopupHelper.DocumentDeletedOKRequested += ModalPopupHelper_DocumentDeletedOKRequested;
			ModalPopupHelper.DocumentOrPatchNotFoundOKRequested += ModalPopupHelper_DocumentOrPatchNotFoundOKRequested;
			ModalPopupHelper.PopupMessagesOKRequested += ModalPopupHelper_PopupMessagesOKRequested;
			ModalPopupHelper.SampleFileBrowserCanceled += ModalPopupHelper_SampleFileBrowserCanceled;
			ModalPopupHelper.SampleFileBrowserOKRequested += ModalPopupHelper_SampleFileBrowserOKRequested;
			ModalPopupHelper.SaveChangesPopupCanceled += ModalPopupHelper_SaveChangesPopupCanceled;
			ModalPopupHelper.SaveChangesPopupNoRequested += ModalPopupHelper_SaveChangesPopupNoRequested;
			ModalPopupHelper.SaveChangesPopupYesRequested += ModalPopupHelper_SaveChangesPopupYesRequested;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Close();

					if (_mainPresenter.MainViewModel.MustClose)
					{
						Program.RemoveMainWindow(this);
					}
					else
					{
						e.Cancel = true;
					}
				});
		}

		/// <summary>
		/// This will among other things reclaim ownership of the Midi device when switching between document windows.
		/// </summary>
		private void MainForm_Activated(object sender, EventArgs e)
		{
			if (!_mustHandleMainFormActivated) return;
			if (_mainPresenter.MainViewModel == null) return;
			if (!_mainPresenter.MainViewModel.Document.IsOpen) return;

			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentActivate();

					if (_mainPresenter.MainViewModel.Successful)
					{
						int audioOutputID = _mainPresenter.MainViewModel.Document.AudioOutputProperties.Entity.ID;
						AudioOutput audioOutput = _repositories.AudioOutputRepository.Get(audioOutputID);
						Patch patch = GetCurrentInstrumentPatch();

						_infrastructureFacade.UpdateInfrastructure(audioOutput, patch);
					}
				});
		}

		// AudioFileOutput
		
		private void audioFileOutputGridUserControl_AddRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.AudioFileOutputGridCreate);
		}

		private void audioFileOutputGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputGridDelete(e.Value));
		}

		private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.AudioFileOutputGridClose);
		}

		private void audioFileOutputGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputPropertiesShow(e.Value));
		}

		private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputPropertiesClose(e.Value));
		}

		private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputPropertiesLoseFocus(e.Value));
		}

		private void audioFileOutputPropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputPropertiesDelete(e.Value));
		}

		// AudioOutput

		private void audioOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.AudioOutputPropertiesClose();

					RecreatePatchCalculatorIfSuccessful();
					SetAudioOutputIfNeeded();
				});
		}

		private void audioOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.AudioOutputPropertiesLoseFocus();

					RecreatePatchCalculatorIfSuccessful();
					SetAudioOutputIfNeeded();
				});
		}

		private void audioOutputPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.AudioOutputPropertiesPlay();

					PlayOutletIfNeeded();
				});
		}

		// CurrentInstrument

		private void patchPropertiesUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchPropertiesAddToInstrument(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void currentInstrumentUserControl_ExpandRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.CurrentInstrumentExpand);
		}

		private void currentInstrumentUserControl_ExpandItemRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurrentInstrumentExpandItem(e.Value));
		}

		private void currentInstrumentUserControl_MoveBackwardRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() =>
			{
				_mainPresenter.CurrentInstrumentMoveBackward(e.Value);
				RecreatePatchCalculatorIfSuccessful();
			});
		}

		private void currentInstrumentUserControl_MoveForwardRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() =>
			{
				_mainPresenter.CurrentInstrumentMoveForward(e.Value);
				RecreatePatchCalculatorIfSuccessful();
			});
		}

		private void currentInstrumentUserControl_PlayRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentPlay();

					PlayOutletIfNeeded();
				});
		}

		private void CurrentInstrumentUserControl_PlayItemRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentPlayItem(e.Value);

					PlayOutletIfNeeded();
				});
		}

		private void currentInstrumentUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.RemoveFromInstrument(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void _autoPatchPopupForm_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.AutoPatchPopupClose);

		private void _autoPatchPopupForm_SaveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(_mainPresenter.AutoPatchPopupSave);

		// Curve

		private void curveDetailsListUserControl_ChangeSelectedNodeTypeRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeChangeSelectedNodeType(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetailsClose(e.Value));
		}

		private void curveDetailsListUserControl_CreateNodeRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeCreate(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_DeleteSelectedNodeRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeDeleteSelected(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_ExpandCurveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetailsExpand(e.Value));
		}

		private void curveDetailsListUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetailsLoseFocus(e.Value));
		}

		private void curveDetailsListUserControl_NodeMoving(object sender, MoveNodeEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeMoving(e.CurveID, e.NodeID, e.X, e.Y);
				});
		}

		private void curveDetailsListUserControl_NodeMoved(object sender, MoveNodeEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeMoved(e.CurveID, e.NodeID, e.X, e.Y);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_SelectCurveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveSelect(e.Value));
		}

		private void curveDetailsListUserControl_SelectNodeRequested(object sender, NodeEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.NodeSelect(e.CurveID, e.NodeID));
		}

		private void curveDetailsListUserControl_ExpandNodeRequested(object sender, NodeEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetailsExpandNode(e.CurveID, e.NodeID));
		}

		// Document Grid

		private void documentGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentCreate);

		private void documentGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentGridClose);

		private void documentGridUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentGridPlay(e.Value);
					PlayOutletIfNeeded();
				});
		}

		private void documentGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentDeleteShow(e.Value));
		}

		private void documentGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					ForceLoseFocus();

					_mainPresenter.DocumentOpen(e.Value);

					SetAudioOutputIfNeeded();
				});
		}

		// Document Details

		private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentDetailsSave);

		private void documentDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentDeleteShow(e.Value));
		}

		private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentDetailsClose);

		// Document Tree

		private void documentTreeUserControl_AddToInstrumentRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentTreeAddToInstrument();
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void documentTreeUserControl_AudioFileOutputsNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTreeSelectAudioFileOutputs);
		}

		private void documentTreeUserControl_AudioOutputNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTreeSelectAudioOutput);

		private void documentTreeUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTreeClose);

		private void documentTreeUserControl_LibrariesNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTreeSelectLibraries);

		private void documentTreeUserControl_LibraryNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeSelectLibrary(e.Value));
		}

		private void documentTreeUserControl_LibraryPatchNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeSelectLibraryPatch(e.Value));
		}

		private void documentTreeUserControl_LibraryPatchGroupNodeSelected(object sender, LibraryPatchGroupEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeSelectLibraryPatchGroup(e.LowerDocumentReferenceID, e.PatchGroup));
		}

		private void documentTreeUserControl_MidiNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeSelectMidi());
		}

		private void documentTreeUserControl_MidiMappingNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeSelectMidiMapping(e.Value));
		}

		private void documentTreeUserControl_NewRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeCreate());
		}

		private void documentTreeUserControl_OpenItemExternallyRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentTreeOpenItemExternally();
					OpenDocumentExternallyAndOptionallyPatchIfNeeded();
				});
		}

		private void documentTreeUserControl_PatchGroupNodeSelected(object sender, EventArgs<string> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeSelectPatchGroup(e.Value));
		}

		private void documentTreeUserControl_PatchHovered(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeHoverPatch(e.Value));
		}

		private void documentTreeUserControl_PatchNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeSelectPatch(e.Value));
		}

		private void documentTreeUserControl_PlayRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentTreePlay();
					PlayOutletIfNeeded();
				});
		}

		private void documentTreeUserControl_RefreshRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentRefresh();
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void documentTreeUserControl_RedoRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.Redo);

		private void documentTreeUserControl_RemoveRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTreeDelete);

		private void documentTreeUserControl_ScalesNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTreeSelectScales);

		private void documentTreeUserControl_SaveRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentSave);

		private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.AudioFileOutputGridShow);

		private void documentTreeUserControl_ShowAudioOutputRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTreeShowAudioOutput);
		}

		private void documentTreeUserControl_ShowLibraryRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeShowLibrary(e.Value));
		}

		private void DocumentTreeUserControl_ShowMidiMappingRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeShowMidiMapping(e.Value));
		}

		private void documentTreeUserControl_ShowPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTreeShowPatch(e.Value));
		}

		private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.ScaleGridShow);

		private void documentTreeUserControl_UndoRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.Undo);
		
		// Document Properties

		private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentPropertiesClose);

		private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentPropertiesLoseFocus);

		private void documentPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentPropertiesPlay();
					PlayOutletIfNeeded();
				});
		}

		// Library

		private void libraryPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryPropertiesClose(e.Value));
		}

		private void libraryPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryPropertiesLoseFocus(e.Value));
		}

		private void libraryPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.LibraryPropertiesPlay(e.Value);
					PlayOutletIfNeeded();
				});
		}

		private void libraryPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.LibraryPropertiesOpenExternally(e.Value);
					OpenDocumentExternallyAndOptionallyPatchIfNeeded();
				});
		}

		private void libraryPropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryPropertiesRemove(e.Value));
		}

		private void midiMappingDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingDetailsClose(e.Value));
		}


		private void midiMappingDetailsUserControl_MoveElementRequested(object sender, EventArgs<(int midiMappingID, int midiMappingElementID, float x, float y)> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingDetailsMoveElement(e.Value.midiMappingID, e.Value.midiMappingElementID, e.Value.x, e.Value.y));
		}

		private void _librarySelectionPopupForm_CancelRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.LibrarySelectionPopupCancel);

		private void _librarySelectionPopupForm_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.LibrarySelectionPopupClose);

		private void _librarySelectionPopupForm_OKRequested(object sender, EventArgs<int?> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibrarySelectionPopupOK(e.Value));
		}

		private void _librarySelectionPopupForm_OpenItemExternallyRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.LibrarySelectionPopupOpenItemExternally(e.Value);
					OpenDocumentExternallyAndOptionallyPatchIfNeeded();
				});
		}

		private void _librarySelectionPopupForm_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.LibrarySelectionPopupPlay(e.Value);
					PlayOutletIfNeeded();
				});
		}

		// Menu

		private void menuUserControl_ShowDocumentGridRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentGridShow);

		private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTreeShow);

		private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					ForceLoseFocus();
					_mainPresenter.DocumentClose();
				});
		}


		private void MenuUserControl_ShowDocumentPropertiesRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentPropertiesShow);
		}

		// Node

		private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodePropertiesClose(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}


		private void nodePropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.NodePropertiesExpand(e.Value));
		}

		private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodePropertiesLoseFocus(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void nodePropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodePropertiesDelete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Operator

		private void operatorPropertiesUserControlBase_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesPlay(e.Value);
					PlayOutletIfNeeded();
				});
		}

		private void operatorPropertiesUserControlBase_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesDelete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControlBase_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.OperatorPropertiesExpand(e.Value));
		}

		private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCache_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_ForCache(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCache_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_ForCache(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_ForCurve(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_ForCurve(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_ForInletsToDimension(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForInletsToDimension_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_ForInletsToDimension(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_ForNumber(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_ForNumber(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_ForPatchInlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_ForPatchInlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_ForPatchOutlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_ForPatchOutlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_ForSample(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_ForSample(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_WithInterpolation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithInterpolation_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_WithInterpolation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesLoseFocus_WithCollectionRecalculation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorPropertiesClose_WithCollectionRecalculation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Patch

		private void patchDetailsUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchDetailsAddToInstrument(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorChangeInputOutlet(e.PatchID, e.InletID, e.InputOutletID);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void patchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _mainPresenter.PatchDetailsClose(e.Value));

		private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchDetailsDeleteSelectedOperator(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.OperatorMove(e.PatchID, e.OperatorID, e.X, e.Y));
		}

		private void patchDetailsUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchDetailsPlay(e.Value);

					PlayOutletIfNeeded();
				});
		}

		private void patchDetailsUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			// PatchDetails Delete action deletes the Selected Operator, not the Patch.
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchDetailsDeleteSelectedOperator(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void patchDetailsUserControl_SelectOperatorRequested(object sender, PatchAndOperatorEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.OperatorSelect(e.PatchID, e.OperatorID));
		}

		private void patchDetailsUserControl_ExpandOperatorRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.OperatorExpand(e.Value));
		}

		/// <summary> This is for the background double click. </summary>
		private void patchDetailsUserControl_ExpandPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetailsExpand(e.Value));
		}

		/// <summary> This is for the tool bar button click. </summary>
		private void patchDetailsUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetailsExpand(e.Value));
		}

		private void patchDetailsUserControl_SelectPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetailsSelect(e.Value));
		}

		private void patchPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchPropertiesClose(e.Value));
		}

		private void patchPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchPropertiesExpand(e.Value));
		}

		private void patchPropertiesUserControl_HasDimensionChanged(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchPropertiesChangeHasDimension(e.Value));
		}

		private void patchPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchPropertiesLoseFocus(e.Value));
		}

		private void patchPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchPropertiesPlay(e.Value);
					PlayOutletIfNeeded();
				});
		}

		private void patchPropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchPropertiesDelete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Scale

		private void scaleGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.ScaleGridCreate);

		private void scaleGridUserControl_RemoveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _mainPresenter.ScaleGridDelete(e.Value));

		private void scaleGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.ScaleGridClose);

		private void scaleGridUserControl_ShowItemRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _mainPresenter.ScaleShow(e.Value));

		private void toneGridEditUserControl_CloseRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _mainPresenter.ToneGridEditClose(e.Value));

		private void toneGridEditUserControl_Edited(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneGridEditEdit(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.ToneGridEditLoseFocus(e.Value));
		}

		private void toneGridEditUserControl_CreateToneRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneCreate(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void toneGridEditUserControl_DeleteToneRequested(object sender, ScaleAndToneEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneDelete(e.ScaleID, e.ToneID);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void toneGridEditUserControl_PlayToneRequested(object sender, ScaleAndToneEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.TonePlay(e.ScaleID, e.ToneID);
					PlayOutletIfNeeded();
				});
		}

		private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ScalePropertiesClose(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ScalePropertiesLoseFocus(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void scalePropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ScalePropertiesDelete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Message Boxes

		private void ModalPopupHelper_DocumentDeleteCanceled(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentDeleteCancel);

		private void ModalPopupHelper_DocumentDeleteConfirmed(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(_mainPresenter.DocumentDeleteConfirm);
		}

		private void ModalPopupHelper_DocumentDeletedOKRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentDeletedOK);
		private void ModalPopupHelper_DocumentOrPatchNotFoundOKRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentOrPatchNotFoundOK);
		private void ModalPopupHelper_PopupMessagesOKRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.PopupMessagesOK);
		private void ModalPopupHelper_SampleFileBrowserCanceled(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.SampleFileBrowserCancel);
		private void ModalPopupHelper_SampleFileBrowserOKRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.SampleFileBrowserOK);
		private void ModalPopupHelper_SaveChangesPopupYesRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.SaveChangesPopupYes);
		private void ModalPopupHelper_SaveChangesPopupNoRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.SaveChangesPopupNo);
		private void ModalPopupHelper_SaveChangesPopupCanceled(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.SaveChangesPopupCancel);

		// DocumentCannotDeleteForm

		private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentCannotDeleteOK);

		// Template Method

		/// <summary> Surrounds a call to a presenter action with rollback and ApplyViewModel. </summary>
		private void TemplateActionHandler(Action action)
		{
			try
			{
				action();
			}
			finally
			{
				_repositories.Rollback();

				// This is done in the finally block,
				// so that upon an exception, focus is set to the original control again.
				ApplyViewModel();
			}
		}
	}
}
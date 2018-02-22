using System;
using System.Windows.Forms;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Common;
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
			audioFileOutputGridUserControl.DeleteRequested += AudioFileOutputGridUserControl_DeleteRequested;
			audioFileOutputGridUserControl.ShowItemRequested += audioFileOutputGridUserControl_ShowItemRequested;
			audioFileOutputPropertiesUserControl.CloseRequested += audioFileOutputPropertiesUserControl_CloseRequested;
			audioFileOutputPropertiesUserControl.LoseFocusRequested += audioFileOutputPropertiesUserControl_LoseFocusRequested;
			audioFileOutputPropertiesUserControl.DeleteRequested += AudioFileOutputPropertiesUserControl_DeleteRequested;
			audioOutputPropertiesUserControl.CloseRequested += audioOutputPropertiesUserControl_CloseRequested;
			audioOutputPropertiesUserControl.LoseFocusRequested += audioOutputPropertiesUserControl_LoseFocusRequested;
			audioOutputPropertiesUserControl.PlayRequested += audioOutputPropertiesUserControl_PlayRequested;
			currentInstrumentBarUserControl.ExpandRequested += CurrentInstrumentBarUserControl_ExpandRequested;
			currentInstrumentBarUserControl.ExpandPatchRequested += CurrentInstrumentBarUserControl_ExpandPatchRequested;
			currentInstrumentBarUserControl.MovePatchBackwardRequested += CurrentInstrumentBarUserControl_MovePatchBackwardRequested;
			currentInstrumentBarUserControl.MovePatchForwardRequested += CurrentInstrumentBarBarUserControl_MovePatchForwardRequested;
			currentInstrumentBarUserControl.PlayRequested += CurrentInstrumentBarUserControl_PlayRequested;
			currentInstrumentBarUserControl.PlayPatchRequested += CurrentInstrumentBarBarUserControl_PlayPatchRequested;
			currentInstrumentBarUserControl.DeletePatchRequested += CurrentInstrumentBarBarUserControl_DeletePatchRequested;
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
			documentGridUserControl.DeleteRequested += DocumentGridUserControl_DeleteRequested;
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
			documentTreeUserControl.DeleteRequested += DocumentTreeUserControl_DeleteRequested;
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
			libraryPropertiesUserControl.DeleteRequested += LibraryPropertiesUserControl_DeleteRequested;
			menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
			menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
			menuUserControl.ShowDocumentGridRequested += menuUserControl_ShowDocumentGridRequested;
			menuUserControl.ShowDocumentPropertiesRequested += MenuUserControl_ShowDocumentPropertiesRequested;
			midiMappingDetailsUserControl.CloseRequested += midiMappingDetailsUserControl_CloseRequested;
			midiMappingDetailsUserControl.DeleteRequested += midiMappingDetailsUserControl_DeleteRequested;
			midiMappingDetailsUserControl.ExpandElementRequested += midiMappingDetailsUserControl_ExpandElementRequested;
			midiMappingDetailsUserControl.MoveElementRequested += midiMappingDetailsUserControl_MoveElementRequested;
			midiMappingDetailsUserControl.NewRequested += midiMappingDetailsUserControl_NewRequested;
			midiMappingDetailsUserControl.SelectElementRequested += midiMappingDetailsUserControl_SelectElementRequested;
			midiMappingElementPropertiesUserControl.CloseRequested += midiMappingElementPropertiesUserControl_CloseRequested;
			midiMappingElementPropertiesUserControl.DeleteRequested += midiMappingElementPropertiesUserControl_DeleteRequested;
			midiMappingElementPropertiesUserControl.ExpandRequested += midiMappingElementPropertiesUserControl_ExpandRequested;
			midiMappingElementPropertiesUserControl.LoseFocusRequested += midiMappingElementPropertiesUserControl_LoseFocusRequested;
			nodePropertiesUserControl.CloseRequested += nodePropertiesUserControl_CloseRequested;
			nodePropertiesUserControl.ExpandRequested += nodePropertiesUserControl_ExpandRequested;
			nodePropertiesUserControl.LoseFocusRequested += nodePropertiesUserControl_LoseFocusRequested;
			nodePropertiesUserControl.DeleteRequested += NodePropertiesUserControl_DeleteRequested;
			operatorPropertiesUserControl.CloseRequested += operatorPropertiesUserControl_CloseRequested;
			operatorPropertiesUserControl.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl.LoseFocusRequested += operatorPropertiesUserControl_LoseFocusRequested;
			operatorPropertiesUserControl.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_ForCache.CloseRequested += operatorPropertiesUserControl_ForCache_CloseRequested;
			operatorPropertiesUserControl_ForCache.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForCache.LoseFocusRequested += operatorPropertiesUserControl_ForCache_LoseFocusRequested;
			operatorPropertiesUserControl_ForCache.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForCache.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_ForCurve.CloseRequested += operatorPropertiesUserControl_ForCurve_CloseRequested;
			operatorPropertiesUserControl_ForCurve.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForCurve.LoseFocusRequested += operatorPropertiesUserControl_ForCurve_LoseFocusRequested;
			operatorPropertiesUserControl_ForCurve.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForCurve.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_ForInletsToDimension.CloseRequested += operatorPropertiesUserControl_ForInletsToDimension_CloseRequested;
			operatorPropertiesUserControl_ForInletsToDimension.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForInletsToDimension.LoseFocusRequested +=
				operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested;
			operatorPropertiesUserControl_ForInletsToDimension.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForInletsToDimension.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_ForNumber.CloseRequested += operatorPropertiesUserControl_ForNumber_CloseRequested;
			operatorPropertiesUserControl_ForNumber.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForNumber.LoseFocusRequested += operatorPropertiesUserControl_ForNumber_LoseFocusRequested;
			operatorPropertiesUserControl_ForNumber.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForNumber.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_ForPatchInlet.CloseRequested += operatorPropertiesUserControl_ForPatchInlet_CloseRequested;
			operatorPropertiesUserControl_ForPatchInlet.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForPatchInlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested;
			operatorPropertiesUserControl_ForPatchInlet.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForPatchInlet.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_ForPatchOutlet.CloseRequested += operatorPropertiesUserControl_ForPatchOutlet_CloseRequested;
			operatorPropertiesUserControl_ForPatchOutlet.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForPatchOutlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested;
			operatorPropertiesUserControl_ForPatchOutlet.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForPatchOutlet.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_ForSample.CloseRequested += operatorPropertiesUserControl_ForSample_CloseRequested;
			operatorPropertiesUserControl_ForSample.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_ForSample.LoseFocusRequested += operatorPropertiesUserControl_ForSample_LoseFocusRequested;
			operatorPropertiesUserControl_ForSample.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_ForSample.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_WithInterpolation.CloseRequested += operatorPropertiesUserControl_WithInterpolation_CloseRequested;
			operatorPropertiesUserControl_WithInterpolation.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_WithInterpolation.LoseFocusRequested += operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested;
			operatorPropertiesUserControl_WithInterpolation.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_WithInterpolation.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.CloseRequested +=
				operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.ExpandRequested += operatorPropertiesUserControlBase_ExpandRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.LoseFocusRequested +=
				operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
			operatorPropertiesUserControl_WithCollectionRecalculation.DeleteRequested += OperatorPropertiesUserControlBase_DeleteRequested;
			patchDetailsUserControl.AddToInstrumentRequested += patchDetailsUserControl_AddToInstrumentRequested;
			patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
			patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
			patchDetailsUserControl.DeleteRequested += PatchDetailsUserControl_DeleteRequested;
			patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
			patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
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
			patchPropertiesUserControl.DeleteRequested += PatchPropertiesUserControl_DeleteRequested;
			scaleGridUserControl.CloseRequested += scaleGridUserControl_CloseRequested;
			scaleGridUserControl.AddRequested += scaleGridUserControl_AddRequested;
			scaleGridUserControl.DeleteRequested += ScaleGridUserControl_DeleteRequested;
			scaleGridUserControl.ShowItemRequested += scaleGridUserControl_ShowItemRequested;
			scalePropertiesUserControl.CloseRequested += scalePropertiesUserControl_CloseRequested;
			scalePropertiesUserControl.LoseFocusRequested += scalePropertiesUserControl_LoseFocusRequested;
			scalePropertiesUserControl.DeleteRequested += ScalePropertiesUserControl_DeleteRequested;
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
					_mainPresenter.Document_Activate();

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
			TemplateActionHandler(_mainPresenter.AudioFileOutputGrid_Create);
		}

		private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.AudioFileOutputGrid_Close);
		}

		private void AudioFileOutputGridUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputGrid_Delete(e.Value));
		}

		private void audioFileOutputGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_Show(e.Value));
		}

		private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_Close(e.Value));
		}

		private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_LoseFocus(e.Value));
		}

		private void AudioFileOutputPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_Delete(e.Value));
		}

		// AudioOutput

		private void audioOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.AudioOutputProperties_Close();

					RecreatePatchCalculatorIfSuccessful();
					SetAudioOutputIfNeeded();
				});
		}

		private void audioOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.AudioOutputProperties_LoseFocus();

					RecreatePatchCalculatorIfSuccessful();
					SetAudioOutputIfNeeded();
				});
		}

		private void audioOutputPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(_mainPresenter.AudioOutputProperties_Play);
		}

		// CurrentInstrument

		private void patchPropertiesUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchProperties_AddToInstrument(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void CurrentInstrumentBarUserControl_ExpandRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.CurrentInstrumentBar_Expand);
		}

		private void CurrentInstrumentBarUserControl_ExpandPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() =>
			{
				_mainPresenter.CurrentInstrumentBar_ExpandPatch(e.Value);
			});
		}

		private void CurrentInstrumentBarUserControl_MovePatchBackwardRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentBar_MovePatchBackward(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void CurrentInstrumentBarBarUserControl_MovePatchForwardRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentBar_MovePatchForward(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void CurrentInstrumentBarUserControl_PlayRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.CurrentInstrumentBar_Play);
		}

		private void CurrentInstrumentBarBarUserControl_PlayPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurrentInstrumentBar_PlayPatch(e.Value));
		}

		private void CurrentInstrumentBarBarUserControl_DeletePatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentBarRemovePatch(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void _autoPatchPopupForm_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.AutoPatchPopup_Close);
		}

		private void _autoPatchPopupForm_SaveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(_mainPresenter.AutoPatchPopup_Save);
		}

		// Curve

		private void curveDetailsListUserControl_ChangeSelectedNodeTypeRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Node_ChangeSelectedNodeType(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetails_Close(e.Value));
		}

		private void curveDetailsListUserControl_CreateNodeRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Node_Create(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_DeleteSelectedNodeRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Node_DeleteSelected(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_ExpandCurveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetails_Expand(e.Value));
		}

		private void curveDetailsListUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetails_LoseFocus(e.Value));
		}

		private void curveDetailsListUserControl_NodeMoving(object sender, MoveNodeEventArgs e)
		{
			TemplateActionHandler(
				() => { _mainPresenter.Node_Moving(e.CurveID, e.NodeID, e.X, e.Y); });
		}

		private void curveDetailsListUserControl_NodeMoved(object sender, MoveNodeEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Node_Moved(e.CurveID, e.NodeID, e.X, e.Y);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void curveDetailsListUserControl_SelectCurveRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.Curve_Select(e.Value));
		}

		private void curveDetailsListUserControl_SelectNodeRequested(object sender, NodeEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.Node_Select(e.CurveID, e.NodeID));
		}

		private void curveDetailsListUserControl_ExpandNodeRequested(object sender, NodeEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.CurveDetails_ExpandNode(e.CurveID, e.NodeID));
		}

		// Document Grid

		private void documentGridUserControl_AddRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.Document_Create);
		}

		private void documentGridUserControl_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentGrid_Close);
		}

		private void documentGridUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentGrid_Play(e.Value));
		}

		private void DocumentGridUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentDelete_Show(e.Value));
		}

		private void documentGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					ForceLoseFocus();

					_mainPresenter.Document_Open(e.Value);

					SetAudioOutputIfNeeded();
				});
		}

		// Document Details

		private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentDetails_Save);
		}

		private void documentDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentDelete_Show(e.Value));
		}

		private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentDetails_Close);
		}

		// Document Tree

		private void documentTreeUserControl_AddToInstrumentRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.DocumentTree_AddToInstrument();
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void documentTreeUserControl_AudioFileOutputsNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_SelectAudioFileOutputs);
		}

		private void documentTreeUserControl_AudioOutputNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_SelectAudioOutput);
		}

		private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_Close);
		}

		private void documentTreeUserControl_LibrariesNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_SelectLibraries);
		}

		private void documentTreeUserControl_LibraryNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibrary(e.Value));
		}

		private void documentTreeUserControl_LibraryPatchNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryPatch(e.Value));
		}

		private void documentTreeUserControl_LibraryPatchGroupNodeSelected(object sender, LibraryPatchGroupEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryPatchGroup(e.LowerDocumentReferenceID, e.PatchGroup));
		}

		private void documentTreeUserControl_MidiNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectMidi());
		}

		private void documentTreeUserControl_MidiMappingNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectMidiMapping(e.Value));
		}

		private void documentTreeUserControl_NewRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_Create);
		}

		private void documentTreeUserControl_OpenItemExternallyRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_OpenItemExternally);
		}

		private void documentTreeUserControl_PatchGroupNodeSelected(object sender, EventArgs<string> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectPatchGroup(e.Value));
		}

		private void documentTreeUserControl_PatchHovered(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_HoverPatch(e.Value));
		}

		private void documentTreeUserControl_PatchNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectPatch(e.Value));
		}

		private void documentTreeUserControl_PlayRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_Play());
		}

		private void documentTreeUserControl_RefreshRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Document_Refresh();
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void documentTreeUserControl_RedoRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.Redo);
		}

		private void DocumentTreeUserControl_DeleteRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_Delete);
		}

		private void documentTreeUserControl_ScalesNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_SelectScales);
		}

		private void documentTreeUserControl_SaveRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.Document_Save);
		}

		private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.AudioFileOutputGrid_Show);
		}

		private void documentTreeUserControl_ShowAudioOutputRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_ShowAudioOutput);
		}

		private void documentTreeUserControl_ShowLibraryRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowLibrary(e.Value));
		}

		private void DocumentTreeUserControl_ShowMidiMappingRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowMidiMapping(e.Value));
		}

		private void documentTreeUserControl_ShowPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowPatch(e.Value));
		}

		private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.ScaleGrid_Show);
		}

		private void documentTreeUserControl_UndoRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.Undo);
		}

		// Document Properties

		private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentProperties_Close);
		}

		private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentProperties_LoseFocus);
		}

		private void documentPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(_mainPresenter.DocumentProperties_Play);
		}

		// Library

		private void libraryPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryProperties_Close(e.Value));
		}

		private void libraryPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryProperties_LoseFocus(e.Value));
		}

		private void libraryPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryProperties_Play(e.Value));
		}

		private void libraryPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryProperties_OpenExternally(e.Value));
		}

		private void LibraryPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibraryProperties_Remove(e.Value));
		}

		private void _librarySelectionPopupForm_CancelRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.LibrarySelectionPopup_Cancel);
		}

		private void _librarySelectionPopupForm_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.LibrarySelectionPopup_Close);
		}

		private void _librarySelectionPopupForm_OKRequested(object sender, EventArgs<int?> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibrarySelectionPopup_OK(e.Value));
		}

		private void _librarySelectionPopupForm_OpenItemExternallyRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibrarySelectionPopup_OpenItemExternally(e.Value));
		}

		private void _librarySelectionPopupForm_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.LibrarySelectionPopup_Play(e.Value));
		}

		// MidiMapping

		private void midiMappingDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingDetails_Close(e.Value));
		}

		private void midiMappingDetailsUserControl_MoveElementRequested(
			object sender,
			EventArgs<(int midiMappingID, int midiMappingElementID, float x, float y)> e)
		{
			TemplateActionHandler(
				() => _mainPresenter.MidiMappingDetails_MoveElement(e.Value.midiMappingID, e.Value.midiMappingElementID, e.Value.x, e.Value.y));
		}

		private void midiMappingDetailsUserControl_NewRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingDetails_CreateElement(e.Value));
		}

		private void midiMappingDetailsUserControl_SelectElementRequested(object sender, EventArgs<(int midiMappingID, int midiMappingElementID)> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingElement_Select(e.Value.midiMappingID, e.Value.midiMappingElementID));
		}

		private void midiMappingDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingDetails_DeleteSelectedElement(e.Value));
		}

		private void midiMappingDetailsUserControl_ExpandElementRequested(object sender, EventArgs<(int midiMappingID, int midiMappingElementID)> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingDetails_ExpandElement(e.Value.midiMappingID, e.Value.midiMappingElementID));
		}

		private void midiMappingElementPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingElementProperties_Close(e.Value));
		}

		private void midiMappingElementPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingElementProperties_Delete(e.Value));
		}

		private void midiMappingElementPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingElementProperties_Expand(e.Value));
		}

		private void midiMappingElementPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingElementProperties_LoseFocus(e.Value));
		}

		// Menu

		private void menuUserControl_ShowDocumentGridRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentGrid_Show);
		}

		private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentTree_Show);
		}

		private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					ForceLoseFocus();
					_mainPresenter.Document_Close();
				});
		}

		private void MenuUserControl_ShowDocumentPropertiesRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentProperties_Show);
		}

		// Node

		private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeProperties_Close(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void nodePropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.NodeProperties_Expand(e.Value));
		}

		private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeProperties_LoseFocus(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void NodePropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.NodeProperties_Delete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Operator

		private void operatorPropertiesUserControlBase_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.OperatorProperties_Play(e.Value));
		}

		private void OperatorPropertiesUserControlBase_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Delete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControlBase_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.OperatorProperties_Expand(e.Value));
		}

		private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCache_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_ForCache(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCache_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_ForCache(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_ForCurve(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_ForCurve(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_ForInletsToDimension(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForInletsToDimension_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_ForInletsToDimension(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_ForNumber(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_ForNumber(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_ForPatchInlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_ForPatchInlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_ForPatchOutlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_ForPatchOutlet(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_ForSample(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_ForSample(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_WithInterpolation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithInterpolation_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_WithInterpolation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_LoseFocus_WithCollectionRecalculation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.OperatorProperties_Close_WithCollectionRecalculation(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Patch

		private void patchDetailsUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchDetails_AddToInstrument(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Operator_ChangeInputOutlet(e.PatchID, e.InletID, e.InputOutletID);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void patchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetails_Close(e.Value));
		}

		private void PatchDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchDetails_DeleteSelectedOperator(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.Operator_Move(e.PatchID, e.OperatorID, e.X, e.Y));
		}

		private void patchDetailsUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetails_Play(e.Value));
		}

		private void patchDetailsUserControl_SelectOperatorRequested(object sender, PatchAndOperatorEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.Operator_Select(e.PatchID, e.OperatorID));
		}

		private void patchDetailsUserControl_ExpandOperatorRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.Operator_Expand(e.Value));
		}

		/// <summary> This is for the background double click. </summary>
		private void patchDetailsUserControl_ExpandPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetails_Expand(e.Value));
		}

		/// <summary> This is for the tool bar button click. </summary>
		private void patchDetailsUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetails_Expand(e.Value));
		}

		private void patchDetailsUserControl_SelectPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchDetails_Select(e.Value));
		}

		private void patchPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchProperties_Close(e.Value));
		}

		private void patchPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchProperties_Expand(e.Value));
		}

		private void patchPropertiesUserControl_HasDimensionChanged(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchProperties_ChangeHasDimension(e.Value));
		}

		private void patchPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchProperties_LoseFocus(e.Value));
		}

		private void patchPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.PatchProperties_Play(e.Value));
		}

		private void PatchPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.PatchProperties_Delete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Scale

		private void scaleGridUserControl_AddRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.ScaleGrid_Create);
		}

		private void ScaleGridUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.ScaleGrid_Delete(e.Value));
		}

		private void scaleGridUserControl_CloseRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.ScaleGrid_Close);
		}

		private void scaleGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.Scale_Show(e.Value));
		}

		private void toneGridEditUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.ToneGridEdit_Close(e.Value));
		}

		private void toneGridEditUserControl_Edited(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneGridEdit_Edit(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.ToneGridEdit_LoseFocus(e.Value));
		}

		private void toneGridEditUserControl_CreateToneRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Tone_Create(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void toneGridEditUserControl_DeleteToneRequested(object sender, ScaleAndToneEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Tone_Delete(e.ScaleID, e.ToneID);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void toneGridEditUserControl_PlayToneRequested(object sender, ScaleAndToneEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.Tone_Play(e.ScaleID, e.ToneID));
		}

		private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ScaleProperties_Close(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ScaleProperties_LoseFocus(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		private void ScalePropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ScaleProperties_Delete(e.Value);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		// Message Boxes

		private void ModalPopupHelper_DocumentDeleteCanceled(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentDelete_Cancel);
		}

		private void ModalPopupHelper_DocumentDeleteConfirmed(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(_mainPresenter.DocumentDelete_Confirm);
		}

		private void ModalPopupHelper_DocumentDeletedOKRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentDeleted_OK);
		}

		private void ModalPopupHelper_DocumentOrPatchNotFoundOKRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentOrPatchNotFound_OK);
		}

		private void ModalPopupHelper_PopupMessagesOKRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.PopupMessages_OK);
		}

		private void ModalPopupHelper_SampleFileBrowserCanceled(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.SampleFileBrowser_Cancel);
		}

		private void ModalPopupHelper_SampleFileBrowserOKRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.SampleFileBrowser_OK);
		}

		private void ModalPopupHelper_SaveChangesPopupYesRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.SaveChangesPopup_Yes);
		}

		private void ModalPopupHelper_SaveChangesPopupNoRequested(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.SaveChangesPopup_No);
		}

		private void ModalPopupHelper_SaveChangesPopupCanceled(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.SaveChangesPopup_Cancel);
		}

		// DocumentCannotDeleteForm

		private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
		{
			TemplateActionHandler(_mainPresenter.DocumentCannotDelete_OK);
		}

		// Template Method

		/// <summary>
		/// Surrounds a call to a presenter action with rollback and ApplyViewModel.
		/// Also executes some side-effects if needed, for instance playing an outlet or opening a document externally.
		/// </summary>
		private void TemplateActionHandler(Action action)
		{
			try
			{
				action();

				PlayOutletIfNeeded();
				OpenDocumentExternallyAndOptionallyPatchIfNeeded();
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
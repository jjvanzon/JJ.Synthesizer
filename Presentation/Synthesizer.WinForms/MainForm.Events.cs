using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.WinForms.Helpers;
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
			currentInstrumentBarUserControl.ExpandMidiMappingGroupRequested += CurrentInstrumentBarUserControl_ExpandMidiMappingGroupRequested;
			currentInstrumentBarUserControl.ExpandPatchRequested += CurrentInstrumentBarUserControl_ExpandPatchRequested;
			currentInstrumentBarUserControl.MoveMidiMappingGroupBackwardRequested +=
				CurrentInstrumentBarUserControl_MoveMidiMappingGroupBackwardRequested;
			currentInstrumentBarUserControl.MoveMidiMappingGroupForwardRequested +=
				CurrentInstrumentBarBarUserControl_MoveMidiMappingGroupForwardRequested;
			currentInstrumentBarUserControl.MovePatchBackwardRequested += CurrentInstrumentBarUserControl_MovePatchBackwardRequested;
			currentInstrumentBarUserControl.MovePatchForwardRequested += CurrentInstrumentBarBarUserControl_MovePatchForwardRequested;
			currentInstrumentBarUserControl.PlayRequested += CurrentInstrumentBarUserControl_PlayRequested;
			currentInstrumentBarUserControl.PlayPatchRequested += CurrentInstrumentBarBarUserControl_PlayPatchRequested;
			currentInstrumentBarUserControl.DeleteMidiMappingGroupRequested += CurrentInstrumentBarBarUserControl_DeleteMidiMappingGroupRequested;
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
			documentTreeUserControl.DeleteRequested += DocumentTreeUserControl_DeleteRequested;
			documentTreeUserControl.LibrariesNodeSelected += documentTreeUserControl_LibrariesNodeSelected;
			documentTreeUserControl.LibraryMidiMappingGroupNodeSelected += DocumentTreeUserControl_LibraryMidiMappingGroupNodeSelected;
			documentTreeUserControl.LibraryMidiNodeSelected += documentTreeUserControl_LibraryMidiNodeSelected;
			documentTreeUserControl.LibraryNodeSelected += documentTreeUserControl_LibraryNodeSelected;
			documentTreeUserControl.LibraryPatchNodeSelected += documentTreeUserControl_LibraryPatchNodeSelected;
			documentTreeUserControl.LibraryPatchGroupNodeSelected += documentTreeUserControl_LibraryPatchGroupNodeSelected;
			documentTreeUserControl.LibraryScaleNodeSelected += documentTreeUserControl_LibraryScaleNodeSelected;
			documentTreeUserControl.LibraryScalesNodeSelected += documentTreeUserControl_LibraryScalesNodeSelected;
			documentTreeUserControl.MidiNodeSelected += documentTreeUserControl_MidiNodeSelected;
			documentTreeUserControl.MidiMappingGroupNodeSelected += DocumentTreeUserControl_MidiMappingGroupNodeSelected;
			documentTreeUserControl.NewRequested += documentTreeUserControl_NewRequested;
			documentTreeUserControl.PatchGroupNodeSelected += documentTreeUserControl_PatchGroupNodeSelected;
			documentTreeUserControl.PatchHovered += documentTreeUserControl_PatchHovered;
			documentTreeUserControl.PatchNodeSelected += documentTreeUserControl_PatchNodeSelected;
			documentTreeUserControl.PlayRequested += documentTreeUserControl_PlayRequested;
			documentTreeUserControl.OpenItemExternallyRequested += documentTreeUserControl_OpenItemExternallyRequested;
			documentTreeUserControl.RedoRequested += documentTreeUserControl_RedoRequested;
			documentTreeUserControl.RefreshRequested += documentTreeUserControl_RefreshRequested;
			documentTreeUserControl.SaveRequested += documentTreeUserControl_SaveRequested;
			documentTreeUserControl.ScalesNodeSelected += documentTreeUserControl_ScalesNodeSelected;
			documentTreeUserControl.ScaleNodeSelected += documentTreeUserControl_ScaleNodeSelected;
			documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
			documentTreeUserControl.ShowAudioOutputRequested += documentTreeUserControl_ShowAudioOutputRequested;
			documentTreeUserControl.ShowLibraryRequested += documentTreeUserControl_ShowLibraryRequested;
			documentTreeUserControl.ShowMidiMappingGroupRequested += DocumentTreeUserControl_ShowMidiMappingGroupRequested;
			documentTreeUserControl.ShowPatchRequested += documentTreeUserControl_ShowPatchRequested;
			documentTreeUserControl.ShowScaleRequested += documentTreeUserControl_ShowScaleRequested;
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

			midiMappingDetailsUserControl.AddToInstrumentRequested += MidiMappingGroupDetailsUserControl_AddToInstrumentRequested;
			midiMappingDetailsUserControl.CloseRequested += midiMappingGroupDetailsUserControl_CloseRequested;
			midiMappingDetailsUserControl.DeleteRequested += midiMappingGroupDetailsUserControl_DeleteRequested;
			midiMappingDetailsUserControl.ExpandMidiMappingRequested += MidiMappingGroupDetailsUserControl_ExpandMidiMappingRequested;
			midiMappingDetailsUserControl.MoveMidiMappingRequested += MidiMappingGroupDetailsUserControl_MoveMidiMappingRequested;
			midiMappingDetailsUserControl.NewRequested += midiMappingGroupDetailsUserControl_NewRequested;
			midiMappingDetailsUserControl.SelectMidiMappingRequested += MidiMappingGroupDetailsUserControl_SelectMidiMappingRequested;

			midiMappingPropertiesUserControl.MidiMappingTypeChanged += MidiMappingPropertiesUserControl_MidiMappingTypeChanged;
			midiMappingPropertiesUserControl.CloseRequested += MidiMappingPropertiesUserControl_CloseRequested;
			midiMappingPropertiesUserControl.DeleteRequested += MidiMappingPropertiesUserControl_DeleteRequested;
			midiMappingPropertiesUserControl.ExpandRequested += MidiMappingPropertiesUserControl_ExpandRequested;
			midiMappingPropertiesUserControl.LoseFocusRequested += MidiMappingPropertiesUserControl_LoseFocusRequested;

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

			scalePropertiesUserControl.AddToInstrumentRequested += ScalePropertiesUserControl_AddToInstrumentRequested;
			scalePropertiesUserControl.CloseRequested += scalePropertiesUserControl_CloseRequested;
			scalePropertiesUserControl.DeleteRequested += ScalePropertiesUserControl_DeleteRequested;
			scalePropertiesUserControl.LoseFocusRequested += scalePropertiesUserControl_LoseFocusRequested;

			toneGridEditUserControl.SetCurrrentInstrumentScaleRequested += ToneGridEditUserControl_SetCurrentInstrumentScaleRequested;
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

			_infrastructureFacade.ExceptionOnMidiThreadOcurred += _infrastructureFacade_ExceptionOnMidiThreadOcurred;
			_infrastructureFacade.MidiControllerValueChanged += _infrastructureFacade_MidiControllerValueChanged;
			_infrastructureFacade.MidiNoteOnOccurred += _infrastructureFacade_MidiNoteOnOccurred;
			_infrastructureFacade.MidiDimensionValuesChanged += InfrastructureFacade_MidiDimensionValuesChanged;

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

					UpdateInfrastructureIfSuccessful();
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

		private void CurrentInstrumentBarUserControl_ExpandMidiMappingGroupRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurrentInstrumentBar_ExpandMidiMappingGroup(e.Value));
		}

		private void CurrentInstrumentBarUserControl_ExpandPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.CurrentInstrumentBar_ExpandPatch(e.Value));
		}

		private void CurrentInstrumentBarUserControl_MoveMidiMappingGroupBackwardRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentBar_MoveMidiMappingGroupBackward(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void CurrentInstrumentBarBarUserControl_MoveMidiMappingGroupForwardRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentBar_MoveMidiMappingGroupForward(e.Value);
					UpdateInfrastructureIfSuccessful();
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

		private void CurrentInstrumentBarBarUserControl_DeleteMidiMappingGroupRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentBar_DeleteMidiMappingGroup(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void CurrentInstrumentBarBarUserControl_DeletePatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.CurrentInstrumentBar_RemovePatch(e.Value);
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
					UpdateInfrastructureIfSuccessful();
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

		private void documentTreeUserControl_LibraryScalesNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryScales(e.Value));
		}

		private void documentTreeUserControl_LibraryScaleNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryScale(e.Value));
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

		private void documentTreeUserControl_LibraryMidiNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryMidi(e.Value));
		}

		private void DocumentTreeUserControl_LibraryMidiMappingGroupNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryMidiMappingGroup(e.Value));
		}

		private void documentTreeUserControl_MidiNodeSelected(object sender, EventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectMidi());
		}

		private void DocumentTreeUserControl_MidiMappingGroupNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectMidiMappingGroup(e.Value));
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

		private void documentTreeUserControl_ScaleNodeSelected(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectScale(e.Value));
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

		private void DocumentTreeUserControl_ShowMidiMappingGroupRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowMidiMappingGroup(e.Value));
		}

		private void documentTreeUserControl_ShowPatchRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowPatch(e.Value));
		}

		private void documentTreeUserControl_ShowScaleRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowScale(e.Value));
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

		// Infrastructure 

		private void InfrastructureFacade_MidiDimensionValuesChanged(
			object sender,
			EventArgs<IList<(DimensionEnum dimensionEnum, string name, double value)>> e)
		{
			_infrastructureFacade_MidiDimensionValuesChanged_DelayedInvoker.InvokeWithDelay(
				() => TemplateActionHandler(() => _mainPresenter.Monitoring_DimensionValuesChanged(e.Value)));
		}

		private void _infrastructureFacade_MidiNoteOnOccurred(object sender, EventArgs<(int midiNoteNumber, int midiVelocity, int midiChannel)> e)
		{
			_infrastructureFacade_MidiNoteOnOccurred_DelayedInvoker.InvokeWithDelay(
				() => TemplateActionHandler(
					() => _mainPresenter.Monitoring_MidiNoteOnOccurred(e.Value.midiNoteNumber, e.Value.midiVelocity, e.Value.midiChannel)));
		}

		private void _infrastructureFacade_ExceptionOnMidiThreadOcurred(object sender, EventArgs<Exception> e)
		{
			_infrastructureFacade_ExceptionOnMidiThreadOcurred_DelayedInvoker.InvokeWithDelay(
				() => UnhandledExceptionMessageBoxShower.ShowMessageBox(e.Value));
		}

		private void _infrastructureFacade_MidiControllerValueChanged(
			object sender,
			EventArgs<(int midiControllerCode, int midiControllerValue, int midiChannel)> e)
		{
			_infrastructureFacade_MidiControllerValueChanged_DelayedInvoker.InvokeWithDelay(
				() =>
					TemplateActionHandler(
						() => _mainPresenter.Monitoring_MidiControllerValueChanged(
							e.Value.midiControllerCode,
							e.Value.midiControllerValue,
							e.Value.midiChannel)));
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

		private void MidiMappingGroupDetailsUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.MidiMappingGroupDetails_AddToInstrument(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void midiMappingGroupDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingGroupDetails_Close(e.Value));
		}

		private void MidiMappingGroupDetailsUserControl_MoveMidiMappingRequested(
			object sender,
			EventArgs<(int midiMappingGroupID, int midiMappingID, float x, float y)> e)
		{
			TemplateActionHandler(
				() => _mainPresenter.MidiMappingGroupDetails_MoveMidiMapping(e.Value.midiMappingGroupID, e.Value.midiMappingID, e.Value.x, e.Value.y));
		}

		private void midiMappingGroupDetailsUserControl_NewRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.MidiMappingGroupDetails_CreateMidiMapping(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void MidiMappingGroupDetailsUserControl_SelectMidiMappingRequested(
			object sender,
			EventArgs<(int midiMappingGroupID, int midiMappingID)> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMapping_Select(e.Value.midiMappingGroupID, e.Value.midiMappingID));
		}

		private void midiMappingGroupDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.MidiMappingGroupDetails_DeleteSelectedMidiMapping(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void MidiMappingGroupDetailsUserControl_ExpandMidiMappingRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingGroupDetails_ExpandMidiMapping(e.Value));
		}

		private void MidiMappingPropertiesUserControl_MidiMappingTypeChanged(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingProperties_ChangeMidiMappingType(e.Value));
		}

		private void MidiMappingPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.MidiMappingProperties_Close(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void MidiMappingPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.MidiMappingProperties_Delete(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void MidiMappingPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(() => _mainPresenter.MidiMappingProperties_Expand(e.Value));
		}

		private void MidiMappingPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.MidiMappingProperties_LoseFocus(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
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

		// Tone

		private void ToneGridEditUserControl_SetCurrentInstrumentScaleRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneGridEdit_SetInstrumentScale(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void toneGridEditUserControl_CloseRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneGridEdit_Close(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void toneGridEditUserControl_Edited(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneGridEdit_Edit(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ToneGridEdit_LoseFocus(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void toneGridEditUserControl_CreateToneRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Tone_Create(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void toneGridEditUserControl_DeleteToneRequested(object sender, ScaleAndToneEventArgs e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Tone_Delete(e.ScaleID, e.ToneID);
					UpdateInfrastructureIfSuccessful();
				});
		}

		private void toneGridEditUserControl_PlayToneRequested(object sender, ScaleAndToneEventArgs e)
		{
			TemplateActionHandler(() => _mainPresenter.Tone_Play(e.ScaleID, e.ToneID));
		}

		// Scale

		private void ScalePropertiesUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
		{
			TemplateActionHandler(
				() =>
				{
					_mainPresenter.ScaleProperties_SetInstrumentScale(e.Value);
					UpdateInfrastructureIfSuccessful();
				});
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
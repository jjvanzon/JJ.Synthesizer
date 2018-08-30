using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using MouseEventArgs = JJ.Framework.VectorGraphics.EventArg.MouseEventArgs;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm
    {
        private void BindEvents()
        {
            FormClosing += MainForm_FormClosing;

            audioFileOutputGridUserControl.CloseRequested += AudioFileOutputGridUserControl_CloseRequested;
            audioFileOutputGridUserControl.AddRequested += AudioFileOutputGridUserControl_AddRequested;
            audioFileOutputGridUserControl.DeleteRequested += AudioFileOutputGridUserControl_DeleteRequested;
            audioFileOutputGridUserControl.ShowItemRequested += AudioFileOutputGridUserControl_ShowItemRequested;

            audioFileOutputPropertiesUserControl.CloseRequested += AudioFileOutputPropertiesUserControl_CloseRequested;
            audioFileOutputPropertiesUserControl.LoseFocusRequested += AudioFileOutputPropertiesUserControl_LoseFocusRequested;
            audioFileOutputPropertiesUserControl.DeleteRequested += AudioFileOutputPropertiesUserControl_DeleteRequested;

            audioOutputPropertiesUserControl.CloseRequested += AudioOutputPropertiesUserControl_CloseRequested;
            audioOutputPropertiesUserControl.LoseFocusRequested += AudioOutputPropertiesUserControl_LoseFocusRequested;
            audioOutputPropertiesUserControl.PlayRequested += AudioOutputPropertiesUserControl_PlayRequested;

            curveDetailsListUserControl.ChangeInterpolationOfSelectedNodeRequested +=
                CurveDetailsListUserControl_ChangeInterpolationOfSelectedNodeRequested;

            curveDetailsListUserControl.CloseRequested += CurveDetailsListUserControl_CloseRequested;
            curveDetailsListUserControl.CreateNodeRequested += CurveDetailsListUserControl_CreateNodeRequested;
            curveDetailsListUserControl.DeleteSelectedNodeRequested += CurveDetailsListUserControl_DeleteSelectedNodeRequested;
            curveDetailsListUserControl.ExpandCurveRequested += CurveDetailsListUserControl_ExpandCurveRequested;
            curveDetailsListUserControl.LoseFocusRequested += CurveDetailsListUserControl_LoseFocusRequested;
            curveDetailsListUserControl.NodeMoving += CurveDetailsListUserControl_NodeMoving;
            curveDetailsListUserControl.NodeMoved += CurveDetailsListUserControl_NodeMoved;
            curveDetailsListUserControl.SelectCurveRequested += CurveDetailsListUserControl_SelectCurveRequested;
            curveDetailsListUserControl.SelectNodeRequested += CurveDetailsListUserControl_SelectNodeRequested;
            curveDetailsListUserControl.ExpandNodeRequested += CurveDetailsListUserControl_ExpandNodeRequested;

            documentDetailsUserControl.CloseRequested += DocumentDetailsUserControl_CloseRequested;
            documentDetailsUserControl.DeleteRequested += DocumentDetailsUserControl_DeleteRequested;
            documentDetailsUserControl.SaveRequested += DocumentDetailsUserControl_SaveRequested;

            documentGridUserControl.AddRequested += DocumentGridUserControl_AddRequested;
            documentGridUserControl.CloseRequested += DocumentGridUserControl_CloseRequested;
            documentGridUserControl.PlayRequested += DocumentGridUserControl_PlayRequested;
            documentGridUserControl.DeleteRequested += DocumentGridUserControl_DeleteRequested;
            documentGridUserControl.ShowItemRequested += DocumentGridUserControl_ShowItemRequested;

            documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
            documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;
            documentPropertiesUserControl.PlayRequested += documentPropertiesUserControl_PlayRequested;

            documentTreeUserControl.AudioFileOutputsNodeSelected += DocumentTreeUserControl_AudioFileOutputsNodeSelected;
            documentTreeUserControl.AudioOutputNodeSelected += DocumentTreeUserControl_AudioOutputNodeSelected;
            documentTreeUserControl.CreateRequested += DocumentTreeUserControl_CreateRequested;
            documentTreeUserControl.DeleteRequested += DocumentTreeUserControl_DeleteRequested;
            documentTreeUserControl.LibrariesNodeSelected += DocumentTreeUserControl_LibrariesNodeSelected;
            documentTreeUserControl.LibraryMidiMappingGroupNodeSelected += DocumentTreeUserControl_LibraryMidiMappingGroupNodeSelected;
            documentTreeUserControl.LibraryMidiNodeSelected += DocumentTreeUserControl_LibraryMidiNodeSelected;
            documentTreeUserControl.LibraryNodeSelected += DocumentTreeUserControl_LibraryNodeSelected;
            documentTreeUserControl.LibraryPatchNodeSelected += DocumentTreeUserControl_LibraryPatchNodeSelected;
            documentTreeUserControl.LibraryPatchGroupNodeSelected += DocumentTreeUserControl_LibraryPatchGroupNodeSelected;
            documentTreeUserControl.LibraryScaleNodeSelected += DocumentTreeUserControl_LibraryScaleNodeSelected;
            documentTreeUserControl.LibraryScalesNodeSelected += DocumentTreeUserControl_LibraryScalesNodeSelected;
            documentTreeUserControl.MidiNodeSelected += DocumentTreeUserControl_MidiNodeSelected;
            documentTreeUserControl.MidiMappingGroupNodeSelected += DocumentTreeUserControl_MidiMappingGroupNodeSelected;
            documentTreeUserControl.PatchGroupNodeSelected += DocumentTreeUserControl_PatchGroupNodeSelected;
            documentTreeUserControl.PatchHovered += DocumentTreeUserControl_PatchHovered;
            documentTreeUserControl.PatchNodeSelected += DocumentTreeUserControl_PatchNodeSelected;
            documentTreeUserControl.ScalesNodeSelected += DocumentTreeUserControl_ScalesNodeSelected;
            documentTreeUserControl.ScaleNodeSelected += DocumentTreeUserControl_ScaleNodeSelected;
            documentTreeUserControl.ShowAudioFileOutputsRequested += DocumentTreeUserControl_ShowAudioFileOutputsRequested;
            documentTreeUserControl.ShowAudioOutputRequested += DocumentTreeUserControl_ShowAudioOutputRequested;
            documentTreeUserControl.ShowLibraryRequested += DocumentTreeUserControl_ShowLibraryRequested;
            documentTreeUserControl.ShowMidiMappingGroupRequested += DocumentTreeUserControl_ShowMidiMappingGroupRequested;
            documentTreeUserControl.ShowPatchRequested += DocumentTreeUserControl_ShowPatchRequested;
            documentTreeUserControl.ShowScaleRequested += DocumentTreeUserControl_ShowScaleRequested;

            _instrumentBarElement.ExpandRequested += InstrumentBarElement_ExpandRequested;
            _instrumentBarElement.ExpandMidiMappingGroupRequested += InstrumentBarElement_ExpandMidiMappingGroupRequested;
            _instrumentBarElement.ExpandPatchRequested += InstrumentBarElement_ExpandPatchRequested;
            _instrumentBarElement.DeleteMidiMappingGroupRequested += InstrumentBarElement_DeleteMidiMappingGroupRequested;
            _instrumentBarElement.DeletePatchRequested += InstrumentBarElement_DeletePatchRequested;
            _instrumentBarElement.HeightChanged += InstrumentBarElement_HeightChanged;
            _instrumentBarElement.MoveMidiMappingGroupBackwardRequested += InstrumentBarElement_MoveMidiMappingGroupBackwardRequested;
            _instrumentBarElement.MoveMidiMappingGroupForwardRequested += InstrumentBarElement_MoveMidiMappingGroupForwardRequested;
            _instrumentBarElement.MovePatchBackwardRequested += InstrumentBarElement_MovePatchBackwardRequested;
            _instrumentBarElement.MovePatchForwardRequested += InstrumentBarElement_MovePatchForwardRequested;
            _instrumentBarElement.PlayRequested += InstrumentBarElement_PlayRequested;
            _instrumentBarElement.PlayPatchRequested += InstrumentBarElement_PlayPatchRequested;

            libraryPropertiesUserControl.CloseRequested += LibraryPropertiesUserControl_CloseRequested;
            libraryPropertiesUserControl.LoseFocusRequested += LibraryPropertiesUserControl_LoseFocusRequested;
            libraryPropertiesUserControl.PlayRequested += LibraryPropertiesUserControl_PlayRequested;
            libraryPropertiesUserControl.ExpandRequested += LibraryPropertiesUserControl_ExpandRequested;
            libraryPropertiesUserControl.DeleteRequested += LibraryPropertiesUserControl_DeleteRequested;

            midiMappingDetailsUserControl.AddToInstrumentRequested += MidiMappingGroupDetailsUserControl_AddToInstrumentRequested;
            midiMappingDetailsUserControl.CloseRequested += MidiMappingGroupDetailsUserControl_CloseRequested;
            midiMappingDetailsUserControl.DeleteRequested += MidiMappingGroupDetailsUserControl_DeleteRequested;
            midiMappingDetailsUserControl.ExpandMidiMappingRequested += MidiMappingGroupDetailsUserControl_ExpandMidiMappingRequested;
            midiMappingDetailsUserControl.MoveMidiMappingRequested += MidiMappingGroupDetailsUserControl_MoveMidiMappingRequested;
            midiMappingDetailsUserControl.NewRequested += MidiMappingGroupDetailsUserControl_NewRequested;
            midiMappingDetailsUserControl.SelectMidiMappingRequested += MidiMappingGroupDetailsUserControl_SelectMidiMappingRequested;

            midiMappingPropertiesUserControl.MidiMappingTypeChanged += MidiMappingPropertiesUserControl_MidiMappingTypeChanged;
            midiMappingPropertiesUserControl.CloseRequested += MidiMappingPropertiesUserControl_CloseRequested;
            midiMappingPropertiesUserControl.DeleteRequested += MidiMappingPropertiesUserControl_DeleteRequested;
            midiMappingPropertiesUserControl.ExpandRequested += MidiMappingPropertiesUserControl_ExpandRequested;
            midiMappingPropertiesUserControl.LoseFocusRequested += MidiMappingPropertiesUserControl_LoseFocusRequested;

            monitoringBarUserControl.HeightChanged += MonitoringBarUserControl_HeightChanged;

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

            patchDetailsUserControl.AddToInstrumentRequested += PatchDetailsUserControl_AddToInstrumentRequested;
            patchDetailsUserControl.ChangeInputOutletRequested += PatchDetailsUserControl_ChangeInputOutletRequested;
            patchDetailsUserControl.CloneRequested += PatchDetailsUserControl_CloneRequested;
            patchDetailsUserControl.CloseRequested += PatchDetailsUserControl_CloseRequested;
            patchDetailsUserControl.DeleteRequested += PatchDetailsUserControl_DeleteRequested;
            patchDetailsUserControl.MoveOperatorRequested += PatchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.PlayRequested += PatchDetailsUserControl_PlayRequested;
            patchDetailsUserControl.SelectOperatorRequested += PatchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.ExpandOperatorRequested += PatchDetailsUserControl_ExpandOperatorRequested;
            patchDetailsUserControl.ExpandPatchRequested += PatchDetailsUserControl_ExpandPatchRequested;
            patchDetailsUserControl.ExpandRequested += PatchDetailsUserControl_ExpandRequested;
            patchDetailsUserControl.SelectPatchRequested += PatchDetailsUserControl_SelectPatchRequested;

            patchPropertiesUserControl.AddToInstrumentRequested += PatchPropertiesUserControl_AddToInstrumentRequested;
            patchPropertiesUserControl.CloseRequested += PatchPropertiesUserControl_CloseRequested;
            patchPropertiesUserControl.ExpandRequested += PatchPropertiesUserControl_ExpandRequested;
            patchPropertiesUserControl.HasDimensionChanged += PatchPropertiesUserControl_HasDimensionChanged;
            patchPropertiesUserControl.LoseFocusRequested += PatchPropertiesUserControl_LoseFocusRequested;
            patchPropertiesUserControl.PlayRequested += PatchPropertiesUserControl_PlayRequested;
            patchPropertiesUserControl.DeleteRequested += PatchPropertiesUserControl_DeleteRequested;

            scalePropertiesUserControl.AddToInstrumentRequested += ScalePropertiesUserControl_AddToInstrumentRequested;
            scalePropertiesUserControl.CloseRequested += ScalePropertiesUserControl_CloseRequested;
            scalePropertiesUserControl.DeleteRequested += ScalePropertiesUserControl_DeleteRequested;
            scalePropertiesUserControl.LoseFocusRequested += ScalePropertiesUserControl_LoseFocusRequested;

            toneGridEditUserControl.SetInstrumentScaleRequested += ToneGridEditUserControl_SetInstrumentScaleRequested;
            toneGridEditUserControl.CloseRequested += ToneGridEditUserControl_CloseRequested;
            toneGridEditUserControl.CreateToneRequested += ToneGridEditUserControl_CreateToneRequested;
            toneGridEditUserControl.DeleteToneRequested += ToneGridEditUserControl_DeleteToneRequested;
            toneGridEditUserControl.Edited += ToneGridEditUserControl_Edited;
            toneGridEditUserControl.LoseFocusRequested += ToneGridEditUserControl_LoseFocusRequested;
            toneGridEditUserControl.PlayToneRequested += ToneGridEditUserControl_PlayToneRequested;

            topBarUserControl.TopBarElement.DocumentClosePictureButton.MouseDown += DocumentClosePictureButton_MouseDown;

            _topButtonBarElement.ButtonBarElement.AddToInstrumentClicked += TopButtonBarElement_AddToInstrumentClicked;
            _topButtonBarElement.ButtonBarElement.BrowseClicked += TopButtonBarElement_BrowseClicked;
            _topButtonBarElement.ButtonBarElement.CloneClicked += TopButtonBarElement_CloneClicked;
            _topButtonBarElement.ButtonBarElement.DeleteClicked += TopButtonBarElement_DeleteClicked;
            _topButtonBarElement.ButtonBarElement.RenameClicked += TopButtonBarElement_RenameClicked;
            _topButtonBarElement.ButtonBarElement.TreeStructureClicked += TopButtonBarElement_TreeStructureClicked;
            _topButtonBarElement.ButtonBarElement.NewClicked += TopButtonBarElement_NewClicked;
            _topButtonBarElement.ButtonBarElement.ExpandClicked += TopButtonBarElement_ExpandClicked;
            _topButtonBarElement.ButtonBarElement.PlayClicked += TopButtonBarElement_PlayClicked;
            _topButtonBarElement.ButtonBarElement.RedoClicked += TopButtonBarElement_RedoClicked;
            _topButtonBarElement.ButtonBarElement.RefreshClicked += TopButtonBarElement_RefreshClicked;
            _topButtonBarElement.ButtonBarElement.SaveClicked += TopButtonBarElement_SaveClicked;
            _topButtonBarElement.ButtonBarElement.UndoClicked += TopButtonBarElement_UndoClicked;

            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;

            _autoPatchPopupForm.CloseRequested += _autoPatchPopupForm_CloseRequested;
            _autoPatchPopupForm.SaveRequested += _autoPatchPopupForm_SaveRequested;

            _autoPatchPopupForm.patchDetailsUserControl.MoveOperatorRequested += PatchDetailsUserControl_MoveOperatorRequested;
            _autoPatchPopupForm.patchDetailsUserControl.SelectOperatorRequested += PatchDetailsUserControl_SelectOperatorRequested;
            _autoPatchPopupForm.patchDetailsUserControl.PlayRequested += PatchDetailsUserControl_PlayRequested;

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
            => TemplateActionHandler(
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

        private void AudioFileOutputGridUserControl_AddRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.AudioFileOutputGrid_Create);

        private void AudioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.AudioFileOutputGrid_Close);

        private void AudioFileOutputGridUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.AudioFileOutputGrid_Delete(e.Value));

        private void AudioFileOutputGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_Show(e.Value));

        private void AudioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_Close(e.Value));

        private void AudioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_LoseFocus(e.Value));

        private void AudioFileOutputPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.AudioFileOutputProperties_Delete(e.Value));

        // AudioOutput

        private void AudioOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.AudioOutputProperties_Close();

                    RecreatePatchCalculatorIfSuccessful();
                    SetAudioOutputIfNeeded();
                });

        private void AudioOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.AudioOutputProperties_LoseFocus();

                    RecreatePatchCalculatorIfSuccessful();
                    SetAudioOutputIfNeeded();
                });

        private void AudioOutputPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(_mainPresenter.AudioOutputProperties_Play);

        // AutoPatchPopup

        private void _autoPatchPopupForm_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.AutoPatchPopup_Close);

        private void _autoPatchPopupForm_SaveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(_mainPresenter.AutoPatchPopup_Save);

        // Curve

        private void CurveDetailsListUserControl_ChangeInterpolationOfSelectedNodeRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.Node_ChangeInterpolationOfSelectedNode(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void CurveDetailsListUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.CurveDetails_Close(e.Value));

        private void CurveDetailsListUserControl_CreateNodeRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.Node_Create(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void CurveDetailsListUserControl_DeleteSelectedNodeRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.Node_DeleteSelected(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void CurveDetailsListUserControl_ExpandCurveRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.CurveDetails_Expand(e.Value));

        private void CurveDetailsListUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.CurveDetails_LoseFocus(e.Value));

        private void CurveDetailsListUserControl_NodeMoving(object sender, MoveNodeEventArgs e)
            => TemplateActionHandler(() => _mainPresenter.Node_Moving(e.CurveID, e.NodeID, e.X, e.Y));

        private void CurveDetailsListUserControl_NodeMoved(object sender, MoveNodeEventArgs e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.Node_Moved(e.CurveID, e.NodeID, e.X, e.Y);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void CurveDetailsListUserControl_SelectCurveRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.Curve_Select(e.Value));

        private void CurveDetailsListUserControl_SelectNodeRequested(object sender, NodeEventArgs e)
            => TemplateActionHandler(() => _mainPresenter.Node_Select(e.CurveID, e.NodeID));

        private void CurveDetailsListUserControl_ExpandNodeRequested(object sender, NodeEventArgs e)
            => TemplateActionHandler(() => _mainPresenter.CurveDetails_ExpandNode(e.CurveID, e.NodeID));

        // DocumentClosePictureButton

        private void DocumentClosePictureButton_MouseDown(object sender, MouseEventArgs e)
            => TemplateActionHandler(
                () =>
                {
                    ForceLoseFocus();
                    _mainPresenter.Document_Close();
                });

        // Document Details

        private void DocumentDetailsUserControl_SaveRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentDetails_Save);

        private void DocumentDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentDelete_Show(e.Value));

        private void DocumentDetailsUserControl_CloseRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentDetails_Close);

        // Document Grid

        private void DocumentGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.Document_Create);

        private void DocumentGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentGrid_Close);

        private void DocumentGridUserControl_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentGrid_Play(e.Value));

        private void DocumentGridUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentDelete_Show(e.Value));

        private void DocumentGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    ForceLoseFocus();

                    _mainPresenter.Document_Open(e.Value);

                    SetAudioOutputIfNeeded();
                });

        // Document Tree

        private void DocumentTreeUserControl_AudioFileOutputsNodeSelected(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentTree_SelectAudioFileOutputs);

        private void DocumentTreeUserControl_AudioOutputNodeSelected(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentTree_SelectAudioOutput);

        private void DocumentTreeUserControl_CreateRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTree_Create);

        private void DocumentTreeUserControl_DeleteRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentTree_Delete);

        private void DocumentTreeUserControl_LibrariesNodeSelected(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentTree_SelectLibraries);

        private void DocumentTreeUserControl_LibraryScalesNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryScales(e.Value));

        private void DocumentTreeUserControl_LibraryScaleNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryScale(e.Value));

        private void DocumentTreeUserControl_LibraryNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibrary(e.Value));

        private void DocumentTreeUserControl_LibraryPatchNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryPatch(e.Value));

        private void DocumentTreeUserControl_LibraryPatchGroupNodeSelected(object sender, LibraryPatchGroupEventArgs e)
            => TemplateActionHandler(
                () => _mainPresenter.DocumentTree_SelectLibraryPatchGroup(e.LowerDocumentReferenceID, e.PatchGroup));

        private void DocumentTreeUserControl_LibraryMidiNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryMidi(e.Value));

        private void DocumentTreeUserControl_LibraryMidiMappingGroupNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectLibraryMidiMappingGroup(e.Value));

        private void DocumentTreeUserControl_MidiNodeSelected(object sender, EventArgs e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectMidi());

        private void DocumentTreeUserControl_MidiMappingGroupNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectMidiMappingGroup(e.Value));

        private void DocumentTreeUserControl_PatchGroupNodeSelected(object sender, EventArgs<string> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectPatchGroup(e.Value));

        private void DocumentTreeUserControl_PatchHovered(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_HoverPatch(e.Value));

        private void DocumentTreeUserControl_PatchNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectPatch(e.Value));

        private void DocumentTreeUserControl_ScalesNodeSelected(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentTree_SelectScales);

        private void DocumentTreeUserControl_ScaleNodeSelected(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_SelectScale(e.Value));

        private void DocumentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.AudioFileOutputGrid_Show);

        private void DocumentTreeUserControl_ShowAudioOutputRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentTree_ShowAudioOutput);

        private void DocumentTreeUserControl_ShowLibraryRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowLibrary(e.Value));

        private void DocumentTreeUserControl_ShowMidiMappingGroupRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowMidiMappingGroup(e.Value));

        private void DocumentTreeUserControl_ShowPatchRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowPatch(e.Value));

        private void DocumentTreeUserControl_ShowScaleRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.DocumentTree_ShowScale(e.Value));

        // Document Properties

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentProperties_Close);

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentProperties_LoseFocus);

        private void documentPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(_mainPresenter.DocumentProperties_Play);

        // Infrastructure 

        private void InfrastructureFacade_MidiDimensionValuesChanged(
            object sender,
            EventArgs<IList<(DimensionEnum dimensionEnum, string name, int? position, double value)>> e)
            => _infrastructureFacade_MidiDimensionValuesChanged_DelayedInvoker.InvokeWithDelay(
                () => TemplateActionHandler(() => _mainPresenter.Monitoring_DimensionValuesChanged(e.Value)));

        private void _infrastructureFacade_MidiNoteOnOccurred(
            object sender,
            EventArgs<(int midiNoteNumber, int midiVelocity, int midiChannel)> e)
            => _infrastructureFacade_MidiNoteOnOccurred_DelayedInvoker.InvokeWithDelay(
                () => TemplateActionHandler(
                    () => _mainPresenter.Monitoring_MidiNoteOnOccurred(
                        e.Value.midiNoteNumber,
                        e.Value.midiVelocity,
                        e.Value.midiChannel)));

        private void _infrastructureFacade_ExceptionOnMidiThreadOcurred(object sender, EventArgs<Exception> e)
            => _infrastructureFacade_ExceptionOnMidiThreadOccurred_DelayedInvoker.InvokeWithDelay(
                () => UnhandledExceptionMessageBoxShower.ShowMessageBox(e.Value));

        private void _infrastructureFacade_MidiControllerValueChanged(
            object sender,
            EventArgs<(int midiControllerCode, int absoluteMidiControllerValue, int relativeMidiControllerValue, int
                midiChannel)> e)
            => _infrastructureFacade_MidiControllerValueChanged_DelayedInvoker.InvokeWithDelay(
                () =>
                    TemplateActionHandler(
                        () => _mainPresenter.Monitoring_MidiControllerValueChanged(
                            e.Value.midiControllerCode,
                            e.Value.absoluteMidiControllerValue,
                            e.Value.relativeMidiControllerValue,
                            e.Value.midiChannel)));

        // InstrumentBar

        private void PatchPropertiesUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.PatchProperties_AddToInstrument(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void InstrumentBarElement_ExpandRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.InstrumentBar_Expand);

        private void InstrumentBarElement_ExpandMidiMappingGroupRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.InstrumentBar_ExpandMidiMappingGroup(e.Value));

        private void InstrumentBarElement_ExpandPatchRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.InstrumentBar_ExpandPatch(e.Value));

        private void InstrumentBarElement_MoveMidiMappingGroupBackwardRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.InstrumentBar_MoveMidiMappingGroupBackward(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void InstrumentBarElement_MoveMidiMappingGroupForwardRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.InstrumentBar_MoveMidiMappingGroupForward(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void InstrumentBarElement_MovePatchBackwardRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.InstrumentBar_MovePatchBackward(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void InstrumentBarElement_MovePatchForwardRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.InstrumentBar_MovePatchForward(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void InstrumentBarElement_PlayRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.InstrumentBar_Play);

        private void InstrumentBarElement_PlayPatchRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.InstrumentBar_PlayPatch(e.Value));

        private void InstrumentBarElement_DeleteMidiMappingGroupRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.InstrumentBar_DeleteMidiMappingGroup(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void InstrumentBarElement_DeletePatchRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.InstrumentBar_RemovePatch(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void InstrumentBarElement_HeightChanged(object sender, EventArgs e) => PositionControls();

        // Library

        private void LibraryPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.LibraryProperties_Close(e.Value));

        private void LibraryPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.LibraryProperties_LoseFocus(e.Value));

        private void LibraryPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.LibraryProperties_Play(e.Value));

        private void LibraryPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.LibraryProperties_OpenExternally(e.Value));

        private void LibraryPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.LibraryProperties_Remove(e.Value));

        private void _librarySelectionPopupForm_CancelRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.LibrarySelectionPopup_Cancel);

        private void _librarySelectionPopupForm_CloseRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.LibrarySelectionPopup_Close);

        private void _librarySelectionPopupForm_OKRequested(object sender, EventArgs<int?> e)
            => TemplateActionHandler(() => _mainPresenter.LibrarySelectionPopup_OK(e.Value));

        private void _librarySelectionPopupForm_OpenItemExternallyRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.LibrarySelectionPopup_OpenItemExternally(e.Value));

        private void _librarySelectionPopupForm_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.LibrarySelectionPopup_Play(e.Value));

        // MidiMapping

        private void MidiMappingGroupDetailsUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.MidiMappingGroupDetails_AddToInstrument(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void MidiMappingGroupDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.MidiMappingGroupDetails_Close(e.Value));

        private void MidiMappingGroupDetailsUserControl_MoveMidiMappingRequested(
            object sender,
            EventArgs<(int midiMappingGroupID, int midiMappingID, float x, float y)> e)
            => TemplateActionHandler(
                () => _mainPresenter.MidiMappingGroupDetails_MoveMidiMapping(
                    e.Value.midiMappingGroupID,
                    e.Value.midiMappingID,
                    e.Value.x,
                    e.Value.y));

        private void MidiMappingGroupDetailsUserControl_NewRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.MidiMappingGroupDetails_CreateMidiMapping(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void MidiMappingGroupDetailsUserControl_SelectMidiMappingRequested(
            object sender,
            EventArgs<(int midiMappingGroupID, int midiMappingID)> e)
            => TemplateActionHandler(
                () => _mainPresenter.MidiMapping_Select(e.Value.midiMappingGroupID, e.Value.midiMappingID));

        private void MidiMappingGroupDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.MidiMappingGroupDetails_DeleteSelectedMidiMapping(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void MidiMappingGroupDetailsUserControl_ExpandMidiMappingRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.MidiMappingGroupDetails_ExpandMidiMapping(e.Value));

        private void MidiMappingPropertiesUserControl_MidiMappingTypeChanged(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.MidiMappingProperties_ChangeMidiMappingType(e.Value));

        private void MidiMappingPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.MidiMappingProperties_Close(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void MidiMappingPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.MidiMappingProperties_Delete(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void MidiMappingPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.MidiMappingProperties_Expand(e.Value));

        private void MidiMappingPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.MidiMappingProperties_LoseFocus(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        // MonitoringBar

        private void MonitoringBarUserControl_HeightChanged(object sender, EventArgs e) => PositionControls();

        // Node

        private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.NodeProperties_Close(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void nodePropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.NodeProperties_Expand(e.Value));

        private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.NodeProperties_LoseFocus(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void NodePropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.NodeProperties_Delete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        // Operator

        private void operatorPropertiesUserControlBase_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.OperatorProperties_Play(e.Value));

        private void OperatorPropertiesUserControlBase_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Delete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControlBase_ExpandRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.OperatorProperties_Expand(e.Value));

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForCache_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_ForCache(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForCache_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_ForCache(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_ForCurve(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_ForCurve(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested(
            object sender,
            EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_ForInletsToDimension(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForInletsToDimension_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_ForInletsToDimension(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_ForNumber(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_ForNumber(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_ForPatchInlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_ForPatchInlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_ForPatchOutlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_ForPatchOutlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_ForSample(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_ForSample(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_WithInterpolation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_WithInterpolation_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_WithInterpolation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested(
            object sender,
            EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_LoseFocus_WithCollectionRecalculation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested(
            object sender,
            EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.OperatorProperties_Close_WithCollectionRecalculation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        // Patch

        private void PatchDetailsUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.PatchDetails_AddToInstrument(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void PatchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.Operator_ChangeInputOutlet(e.PatchID, e.InletID, e.InputOutletID);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void PatchDetailsUserControl_CloneRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchDetails_Clone(e.Value));

        private void PatchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchDetails_Close(e.Value));

        private void PatchDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.PatchDetails_DeleteSelectedOperator(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void PatchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
            => TemplateActionHandler(() => _mainPresenter.Operator_Move(e.PatchID, e.OperatorID, e.X, e.Y));

        private void PatchDetailsUserControl_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchDetails_Play(e.Value));

        private void PatchDetailsUserControl_SelectOperatorRequested(object sender, PatchAndOperatorEventArgs e)
            => TemplateActionHandler(() => _mainPresenter.Operator_Select(e.PatchID, e.OperatorID));

        private void PatchDetailsUserControl_ExpandOperatorRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.Operator_Expand(e.Value));

        /// <summary> This is for the background double click. </summary>
        private void PatchDetailsUserControl_ExpandPatchRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchDetails_Expand(e.Value));

        /// <summary> This is for the tool bar button click. </summary>
        private void PatchDetailsUserControl_ExpandRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchDetails_Expand(e.Value));

        private void PatchDetailsUserControl_SelectPatchRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchDetails_Select(e.Value));

        private void PatchPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchProperties_Close(e.Value));

        private void PatchPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchProperties_Expand(e.Value));

        private void PatchPropertiesUserControl_HasDimensionChanged(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchProperties_ChangeHasDimension(e.Value));

        private void PatchPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchProperties_LoseFocus(e.Value));

        private void PatchPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(() => _mainPresenter.PatchProperties_Play(e.Value));

        private void PatchPropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.PatchProperties_Delete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        // Tone

        private void ToneGridEditUserControl_SetInstrumentScaleRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ToneGridEdit_SetInstrumentScale(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void ToneGridEditUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ToneGridEdit_Close(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void ToneGridEditUserControl_Edited(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ToneGridEdit_Edit(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void ToneGridEditUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ToneGridEdit_LoseFocus(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void ToneGridEditUserControl_CreateToneRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.Tone_Create(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void ToneGridEditUserControl_DeleteToneRequested(object sender, ScaleAndToneEventArgs e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.Tone_Delete(e.ScaleID, e.ToneID);
                    UpdateInfrastructureIfSuccessful();
                });

        private void ToneGridEditUserControl_PlayToneRequested(object sender, ScaleAndToneEventArgs e)
            => TemplateActionHandler(() => _mainPresenter.Tone_Play(e.ScaleID, e.ToneID));


        // TopButtonBar

        private void TopButtonBarElement_AddToInstrumentClicked(object sender, EventArgs e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.TopButtonBar_AddToInstrument();
                    RecreatePatchCalculatorIfSuccessful();
                    UpdateInfrastructureIfSuccessful();
                });

        private void TopButtonBarElement_CloneClicked(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.TopButtonBar_Clone);

        private void TopButtonBarElement_DeleteClicked(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.TopButtonBar_Delete);

        private void TopButtonBarElement_TreeStructureClicked(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.TopButtonBar_ShowOrCloseDocumentTree);

        private void TopButtonBarElement_BrowseClicked(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.TopButtonBar_ShowDocumentGrid);

        private void TopButtonBarElement_RenameClicked(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.TopButtonBar_ShowDocumentProperties);

        private void TopButtonBarElement_NewClicked(object sender, EventArgs e) 
            => TemplateActionHandler(_mainPresenter.TopButtonBar_Create);

        private void TopButtonBarElement_ExpandClicked(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.TopButtonBar_OpenItemExternally);

        private void TopButtonBarElement_PlayClicked(object sender, EventArgs e)
            => TemplateActionHandler(() => _mainPresenter.TopButtonBar_Play());

        private void TopButtonBarElement_RefreshClicked(object sender, EventArgs e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.TopButtonBar_RefreshDocument();
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void TopButtonBarElement_RedoClicked(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.TopButtonBar_Redo);

        private void TopButtonBarElement_SaveClicked(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.TopButtonBar_Save);

        private void TopButtonBarElement_UndoClicked(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.TopButtonBar_Undo);

        // Scale

        private void ScalePropertiesUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ScaleProperties_SetInstrumentScale(e.Value);
                    UpdateInfrastructureIfSuccessful();
                });

        private void ScalePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ScaleProperties_Close(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void ScalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ScaleProperties_LoseFocus(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        private void ScalePropertiesUserControl_DeleteRequested(object sender, EventArgs<int> e)
            => TemplateActionHandler(
                () =>
                {
                    _mainPresenter.ScaleProperties_Delete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });

        // Message Boxes

        private void ModalPopupHelper_DocumentDeleteCanceled(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentDelete_Cancel);

        private void ModalPopupHelper_DocumentDeleteConfirmed(object sender, EventArgs<int> e)
            => TemplateActionHandler(_mainPresenter.DocumentDelete_Confirm);

        private void ModalPopupHelper_DocumentDeletedOKRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentDeleted_OK);

        private void ModalPopupHelper_DocumentOrPatchNotFoundOKRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.DocumentOrPatchNotFound_OK);

        private void ModalPopupHelper_PopupMessagesOKRequested(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.PopupMessages_OK);

        private void ModalPopupHelper_SampleFileBrowserCanceled(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.SampleFileBrowser_Cancel);

        private void ModalPopupHelper_SampleFileBrowserOKRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.SampleFileBrowser_OK);

        private void ModalPopupHelper_SaveChangesPopupYesRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.SaveChangesPopup_Yes);

        private void ModalPopupHelper_SaveChangesPopupNoRequested(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.SaveChangesPopup_No);

        private void ModalPopupHelper_SaveChangesPopupCanceled(object sender, EventArgs e)
            => TemplateActionHandler(_mainPresenter.SaveChangesPopup_Cancel);

        // DocumentCannotDeleteForm

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e) => TemplateActionHandler(_mainPresenter.DocumentCannotDelete_OK);

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
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }
    }
}
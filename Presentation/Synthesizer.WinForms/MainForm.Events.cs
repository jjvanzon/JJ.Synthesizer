using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Windows.Forms;
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
            currentInstrumentUserControl.MoveBackwardRequested += currentInstrumentUserControl_MoveBackwardRequested;
            currentInstrumentUserControl.MoveForwardRequested += currentInstrumentUserControl_MoveForwardRequested;
            currentInstrumentUserControl.PlayRequested += currentInstrumentUserControl_PlayRequested;
            currentInstrumentUserControl.RemoveRequested += currentInstrumentUserControl_RemoveRequested;
            curveDetailsListUserControl.ChangeSelectedNodeTypeRequested += curveDetailsUserControl_ChangeSelectedNodeTypeRequested;
            curveDetailsListUserControl.CloseRequested += curveDetailsUserControl_CloseRequested;
            curveDetailsListUserControl.CreateNodeRequested += curveDetailsUserControl_CreateNodeRequested;
            curveDetailsListUserControl.DeleteSelectedNodeRequested += curveDetailsUserControl_DeleteSelectedNodeRequested;
            curveDetailsListUserControl.ExpandCurveRequested += curveDetailsListUserControl_ExpandCurveRequested;
            curveDetailsListUserControl.LoseFocusRequested += curveDetailsUserControl_LoseFocusRequested;
            curveDetailsListUserControl.NodeMoving += curveDetailsUserControl_NodeMoving;
            curveDetailsListUserControl.NodeMoved += curveDetailsUserControl_NodeMoved;
            curveDetailsListUserControl.SelectNodeRequested += curveDetailsUserControl_SelectNodeRequested;
            curveDetailsListUserControl.ShowNodePropertiesRequested += curveDetailsUserControl_ShowNodePropertiesRequested;
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
            documentTreeUserControl.AddRequested += documentTreeUserControl_AddRequested;
            documentTreeUserControl.AddToInstrumentRequested += documentTreeUserControl_AddToInstrumentRequested;
            documentTreeUserControl.AudioFileOutputsNodeSelected += documentTreeUserControl_AudioFileOutputsNodeSelected;
            documentTreeUserControl.AudioOutputNodeSelected += documentTreeUserControl_AudioOutputNodeSelected;
            documentTreeUserControl.CloseRequested += documentTreeUserControl_CloseRequested;
            documentTreeUserControl.LibrariesNodeSelected += documentTreeUserControl_LibrariesNodeSelected;
            documentTreeUserControl.LibraryNodeSelected += documentTreeUserControl_LibraryNodeSelected;
            documentTreeUserControl.LibraryPatchNodeSelected += documentTreeUserControl_LibraryPatchNodeSelected;
            documentTreeUserControl.LibraryPatchGroupNodeSelected += documentTreeUserControl_LibraryPatchGroupNodeSelected;
            documentTreeUserControl.NewRequested += documentTreeUserControl_NewRequested;
            documentTreeUserControl.PatchGroupNodeSelected += documentTreeUserControl_PatchGroupNodeSelected;
            documentTreeUserControl.PatchHovered += documentTreeUserControl_PatchHovered;
            documentTreeUserControl.PatchNodeSelected += documentTreeUserControl_PatchNodeSelected;
            documentTreeUserControl.PlayRequested += documentTreeUserControl_PlayRequested;
            documentTreeUserControl.OpenItemExternallyRequested += documentTreeUserControl_OpenItemExternallyRequested;
            documentTreeUserControl.RefreshRequested += documentTreeUserControl_RefreshRequested;
            documentTreeUserControl.RemoveRequested += documentTreeUserControl_RemoveRequested;
            documentTreeUserControl.SaveRequested += documentTreeUserControl_SaveRequested;
            documentTreeUserControl.ScalesNodeSelected += documentTreeUserControl_ScalesNodeSelected;
            documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
            documentTreeUserControl.ShowAudioOutputRequested += documentTreeUserControl_ShowAudioOutputRequested;
            documentTreeUserControl.ShowLibraryPropertiesRequested += documentTreeUserControl_ShowLibraryPropertiesRequested;
            documentTreeUserControl.ShowPatchDetailsRequested += documentTreeUserControl_ShowPatchDetailsRequested;
            documentTreeUserControl.ShowScalesRequested += documentTreeUserControl_ShowScalesRequested;
            libraryPropertiesUserControl.CloseRequested += libraryPropertiesUserControl_CloseRequested;
            libraryPropertiesUserControl.LoseFocusRequested += libraryPropertiesUserControl_LoseFocusRequested;
            libraryPropertiesUserControl.PlayRequested += libraryPropertiesUserControl_PlayRequested;
            libraryPropertiesUserControl.ExpandRequested += libraryPropertiesUserControl_ExpandRequested;
            libraryPropertiesUserControl.RemoveRequested += libraryPropertiesUserControl_RemoveRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
            menuUserControl.ShowDocumentGridRequested += menuUserControl_ShowDocumentGridRequested;
            menuUserControl.ShowDocumentPropertiesRequested += MenuUserControl_ShowDocumentPropertiesRequested;
            nodePropertiesUserControl.CloseRequested += nodePropertiesUserControl_CloseRequested;
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
            patchDetailsUserControl.LoseFocusRequested += patchDetailsUserControl_LoseFocusRequested;
            patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
            patchDetailsUserControl.RemoveRequested += patchDetailsUserControl_RemoveRequested;
            patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.ExpandOperatorRequested += patchDetailsUserControl_ExpandOperatorRequested;
            patchDetailsUserControl.ShowPatchPropertiesRequested += patchDetailsUserControl_ShowPatchPropertiesRequested;
            patchPropertiesUserControl.AddToInstrumentRequested += patchPropertiesUserControl_AddToInstrumentRequested;
            patchPropertiesUserControl.CloseRequested += patchPropertiesUserControl_CloseRequested;
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
            ModalPopupHelper.DocumentDeletedOK += ModalPopupHelper_DocumentDeletedOK;
            ModalPopupHelper.DocumentOrPatchNotFoundOK += ModalPopupHelper_DocumentOrPatchNotFoundOK;
            ModalPopupHelper.PopupMessagesOK += ModalPopupHelper_PopupMessagesOK;
            ModalPopupHelper.SampleFileBrowserCancel += ModalPopupHelper_SampleFileBrowserCancel;
            ModalPopupHelper.SampleFileBrowserOK += ModalPopupHelper_SampleFileBrowserOK;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.RemoveMainWindow(this);
        }

        /// <summary>
        /// This will among other things reclaim ownership of the Midi device when switching between document windows.
        /// </summary>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!_mustHandleMainFormActivated) return;
            if (_presenter.MainViewModel == null) return;
            if (!_presenter.MainViewModel.Document.IsOpen) return;

            TemplateActionHandler(
                () =>
                {
                    _presenter.DocumentActivate();

                    if (_presenter.MainViewModel.Successful)
                    {
                        int audioOutputID = _presenter.MainViewModel.Document.AudioOutputProperties.Entity.ID;
                        AudioOutput audioOutput = _repositories.AudioOutputRepository.Get(audioOutputID);
                        Patch patch = GetCurrentInstrumentPatch();

                        _infrastructureFacade.UpdateInfrastructure(audioOutput, patch);
                    }
                });
        }

        // AudioFileOutput

        private void audioFileOutputGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(_presenter.AudioFileOutputCreate);
        }

        private void audioFileOutputGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.AudioFileOutputGridDelete(e.Value));
        }

        private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(_presenter.AudioFileOutputGridClose);
        }

        private void audioFileOutputGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.AudioFileOutputPropertiesShow(e.Value));
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.AudioFileOutputPropertiesClose(e.Value));
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.AudioFileOutputPropertiesLoseFocus(e.Value));
        }

        private void audioFileOutputPropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.AudioFileOutputPropertiesDelete(e.Value));
        }

        // AudioOutput

        private void audioOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.AudioOutputPropertiesClose();

                    RecreatePatchCalculatorIfSuccessful();
                    SetAudioOutputIfNeeded();
                });
        }

        private void audioOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.AudioOutputPropertiesLoseFocus();

                    RecreatePatchCalculatorIfSuccessful();
                    SetAudioOutputIfNeeded();
                });
        }

        private void audioOutputPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.AudioOutputPropertiesPlay();

                    PlayOutletIfNeeded();
                });
        }

        // CurrentInstrument

        private void patchPropertiesUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.PatchPropertiesAddToInstrument(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void currentInstrumentUserControl_ExpandRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.CurrentInstrumentExpand);

        private void currentInstrumentUserControl_MoveBackwardRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() =>
            {
                _presenter.CurrentInstrumentMoveBackward(e.Value);
                RecreatePatchCalculatorIfSuccessful();
            });
        }

        private void currentInstrumentUserControl_MoveForwardRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() =>
            {
                _presenter.CurrentInstrumentMoveForward(e.Value);
                RecreatePatchCalculatorIfSuccessful();
            });
        }

        private void currentInstrumentUserControl_PlayRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.CurrentInstrumentPlay();

                    PlayOutletIfNeeded();
                });
        }

        private void currentInstrumentUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.RemoveFromInstrument(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void _autoPatchPopupForm_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.AutoPatchPopupClose);

        private void _autoPatchPopupForm_SaveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(_presenter.AutoPatchPopupSave);

        // Curve

        private void curveDetailsUserControl_SelectNodeRequested(object sender, NodeEventArgs e)
        {
            TemplateActionHandler(() => _presenter.NodeSelect(e.CurveID, e.NodeID));
        }

        private void curveDetailsUserControl_CreateNodeRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodeCreate(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void curveDetailsUserControl_DeleteSelectedNodeRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodeDeleteSelected(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }


        private void curveDetailsListUserControl_ExpandCurveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.CurveDetailsExpand(e.Value));
        }

        private void curveDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.CurveDetailsClose(e.Value));
        }

        private void curveDetailsUserControl_NodeMoving(object sender, MoveNodeEventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodeMoving(e.CurveID, e.NodeID, e.X, e.Y);
                });
        }

        private void curveDetailsUserControl_NodeMoved(object sender, MoveNodeEventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodeMoved(e.CurveID, e.NodeID, e.X, e.Y);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void curveDetailsUserControl_ChangeSelectedNodeTypeRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodeChangeSelectedNodeType(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void curveDetailsUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.CurveDetailsLoseFocus(e.Value));
        }

        private void curveDetailsUserControl_ShowNodePropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.NodePropertiesShow(e.Value));
        }

        // Document Grid

        private void documentGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentDetailsCreate);

        private void documentGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentGridClose);

        private void documentGridUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.DocumentGridPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void documentGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.DocumentDeleteShow(e.Value));
        }

        private void documentGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    ForceLoseFocus();

                    _presenter.DocumentOpen(e.Value);

                    SetAudioOutputIfNeeded();
                });
        }

        // Document Details

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentDetailsSave);

        private void documentDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.DocumentDeleteShow(e.Value));
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentDetailsClose);

        // Document Tree

        private void documentTreeUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeAdd);

        private void documentTreeUserControl_AddToInstrumentRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.DocumentTreeAddToInstrument();
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void documentTreeUserControl_AudioFileOutputsNodeSelected(object sender, EventArgs e)
        {
            TemplateActionHandler(_presenter.DocumentTreeSelectAudioFileOutputs);
        }

        private void documentTreeUserControl_AudioOutputNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeSelectAudioOutput);

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeClose);

        private void documentTreeUserControl_LibrariesNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeSelectLibraries);

        private void documentTreeUserControl_LibraryNodeSelected(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.DocumentTreeSelectLibrary(e.Value));
        }

        private void documentTreeUserControl_LibraryPatchNodeSelected(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.DocumentTreeSelectLibraryPatch(e.Value));
        }

        private void documentTreeUserControl_LibraryPatchGroupNodeSelected(object sender, LibraryPatchGroupEventArgs e)
        {
            TemplateActionHandler(() => _presenter.DocumentTreeSelectLibraryPatchGroup(e.LowerDocumentReferenceID, e.PatchGroup));
        }

        private void documentTreeUserControl_NewRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(() => _presenter.DocumentTreeNew());
        }

        private void documentTreeUserControl_OpenItemExternallyRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.DocumentTreeOpenItemExternally();
                    OpenDocumentExternallyAndOptionallyPatchIfNeeded();
                });
        }

        private void documentTreeUserControl_PatchGroupNodeSelected(object sender, EventArgs<string> e)
        {
            TemplateActionHandler(() => _presenter.DocumentTreeSelectPatchGroup(e.Value));
        }

        private void documentTreeUserControl_PatchHovered(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.DocumentTreeHoverPatch(e.Value));
        }

        private void documentTreeUserControl_PatchNodeSelected(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.DocumentTreeSelectPatch(e.Value));
        }

        private void documentTreeUserControl_PlayRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.DocumentTreePlay();
                    PlayOutletIfNeeded();
                });
        }

        private void documentTreeUserControl_RefreshRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.DocumentRefresh();
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void documentTreeUserControl_RemoveRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeRemove);

        private void documentTreeUserControl_ScalesNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeSelectScales);

        private void documentTreeUserControl_SaveRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentSave);

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.AudioFileOutputGridShow);

        private void documentTreeUserControl_ShowAudioOutputRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.AudioOutputPropertiesShow);

        private void documentTreeUserControl_ShowLibraryPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPropertiesShow(e.Value);
                });
        }

        private void documentTreeUserControl_ShowPatchDetailsRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchDetailsShow(e.Value));
        }

        private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.ScaleGridShow);

        // Document Properties

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentPropertiesClose);

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentPropertiesLoseFocus);

        private void documentPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.DocumentPropertiesPlay();
                    PlayOutletIfNeeded();
                });
        }

        // Library

        private void libraryPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.LibraryPropertiesClose(e.Value));
        }

        private void libraryPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.LibraryPropertiesLoseFocus(e.Value));
        }

        private void libraryPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPropertiesPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void libraryPropertiesUserControl_ExpandRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPropertiesOpenExternally(e.Value);
                    OpenDocumentExternallyAndOptionallyPatchIfNeeded();
                });
        }

        private void libraryPropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.LibraryPropertiesRemove(e.Value));
        }

        private void _librarySelectionPopupForm_CancelRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.LibrarySelectionPopupCancel);

        private void _librarySelectionPopupForm_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.LibrarySelectionPopupClose);

        private void _librarySelectionPopupForm_OKRequested(object sender, EventArgs<int?> e)
        {
            TemplateActionHandler(() => _presenter.LibrarySelectionPopupOK(e.Value));
        }

        private void _librarySelectionPopupForm_OpenItemExternallyRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibrarySelectionPopupOpenItemExternally(e.Value);
                    OpenDocumentExternallyAndOptionallyPatchIfNeeded();
                });
        }

        private void _librarySelectionPopupForm_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibrarySelectionPopupPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        // Menu

        private void menuUserControl_ShowDocumentGridRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentGridShow);

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeShow);

        private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    ForceLoseFocus();
                    _presenter.DocumentClose();
                });
        }

        private void MenuUserControl_ShowDocumentPropertiesRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentPropertiesShow);

        // Node

        private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodePropertiesClose(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodePropertiesLoseFocus(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void nodePropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.NodePropertiesDelete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        // Operator

        private void operatorPropertiesUserControlBase_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void operatorPropertiesUserControlBase_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesDelete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControlBase_ExpandRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.OperatorPropertiesExpand(e.Value));
        }

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForCache_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_ForCache(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForCache_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_ForCache(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_ForCurve(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_ForCurve(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_ForInletsToDimension(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForInletsToDimension_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_ForInletsToDimension(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_ForNumber(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_ForNumber(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_ForPatchInlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_ForPatchInlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_ForPatchOutlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_ForPatchOutlet(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_ForSample(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_ForSample(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_WithInterpolation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_WithInterpolation_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_WithInterpolation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_WithCollectionRecalculation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_WithCollectionRecalculation(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        // Patch

        private void patchDetailsUserControl_AddToInstrumentRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.PatchDetailsAddToInstrument(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorChangeInputOutlet(e.PatchID, e.InletID, e.InputOutletID);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.PatchDetailsClose(e.Value));

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorDeleteSelected(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchDetailsLoseFocus(e.Value));
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
        {
            TemplateActionHandler(() => _presenter.OperatorMove(e.PatchID, e.OperatorID, e.X, e.Y));
        }

        private void patchDetailsUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.PatchDetailsPlay(e.Value);

                    PlayOutletIfNeeded();
                });
        }

        private void patchDetailsUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            // PatchDetails Delete action deletes the Selected Operator, not the Patch.
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorDeleteSelected(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, PatchAndOperatorEventArgs e)
        {
            TemplateActionHandler(() => _presenter.OperatorSelect(e.PatchID, e.OperatorID));
        }

        private void patchDetailsUserControl_ExpandOperatorRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.OperatorExpand(e.Value));
        }

        private void patchDetailsUserControl_ShowPatchPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchPropertiesShow(e.Value));
        }

        private void patchPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchPropertiesClose(e.Value));
        }

        private void patchPropertiesUserControl_HasDimensionChanged(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchPropertiesChangeHasDimension(e.Value));
        }

        private void patchPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchPropertiesLoseFocus(e.Value));
        }

        private void patchPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.PatchPropertiesPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void patchPropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.PatchPropertiesDelete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        // Scale

        private void scaleGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.ScaleGridCreate);

        private void scaleGridUserControl_RemoveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.ScaleGridDelete(e.Value));

        private void scaleGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.ScaleGridClose);

        private void scaleGridUserControl_ShowItemRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.ScaleShow(e.Value));

        private void toneGridEditUserControl_CloseRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.ToneGridEditClose(e.Value));

        private void toneGridEditUserControl_Edited(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.ToneGridEditEdit(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.ToneGridEditLoseFocus(e.Value));
        }

        private void toneGridEditUserControl_CreateToneRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.ToneCreate(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void toneGridEditUserControl_DeleteToneRequested(object sender, ScaleAndToneEventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.ToneDelete(e.ScaleID, e.ToneID);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void toneGridEditUserControl_PlayToneRequested(object sender, ScaleAndToneEventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.TonePlay(e.ScaleID, e.ToneID);
                    PlayOutletIfNeeded();
                });
        }

        private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.ScalePropertiesClose(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.ScalePropertiesLoseFocus(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void scalePropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.ScalePropertiesDelete(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        // Message Boxes

        private void ModalPopupHelper_DocumentDeleteCanceled(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentDeleteCancel);

        private void ModalPopupHelper_DocumentDeleteConfirmed(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(_presenter.DocumentDeleteConfirm);
        }

        private void ModalPopupHelper_DocumentDeletedOK(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentDeletedOK);
        private void ModalPopupHelper_DocumentOrPatchNotFoundOK(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentOrPatchNotFoundOK);
        private void ModalPopupHelper_PopupMessagesOK(object sender, EventArgs e) => TemplateActionHandler(_presenter.PopupMessagesOK);
        private void ModalPopupHelper_SampleFileBrowserCancel(object sender, EventArgs e) => TemplateActionHandler(_presenter.SampleFileBrowserCancel);
        private void ModalPopupHelper_SampleFileBrowserOK(object sender, EventArgs e) => TemplateActionHandler(_presenter.SampleFileBrowserOK);

        // DocumentCannotDeleteForm

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentCannotDeleteOK);

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
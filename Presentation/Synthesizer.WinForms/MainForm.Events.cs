using System;
using System.Windows.Forms;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

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
            currentInstrumentUserControl.CloseRequested += currentInstrumentUserControl_CloseRequested;
            currentInstrumentUserControl.PlayRequested += currentInstrumentUserControl_PlayRequested;
            currentInstrumentUserControl.RemoveRequested += currentInstrumentUserControl_RemoveRequested;
            currentInstrumentUserControl.ShowAutoPatchRequested += currentInstrumentUserControl_ShowAutoPatchRequested;
            curveDetailsListUserControl.ChangeSelectedNodeTypeRequested += curveDetailsUserControl_ChangeSelectedNodeTypeRequested;
            curveDetailsListUserControl.CloseRequested += curveDetailsUserControl_CloseRequested;
            curveDetailsListUserControl.CreateNodeRequested += curveDetailsUserControl_CreateNodeRequested;
            curveDetailsListUserControl.DeleteRequested += curveDetailsUserControl_DeleteSelectedNodeRequested;
            curveDetailsListUserControl.LoseFocusRequested += curveDetailsUserControl_LoseFocusRequested;
            curveDetailsListUserControl.NodeMoving += curveDetailsUserControl_NodeMoving;
            curveDetailsListUserControl.NodeMoved += curveDetailsUserControl_NodeMoved;
            curveDetailsListUserControl.SelectNodeRequested += curveDetailsUserControl_SelectNodeRequested;
            curveDetailsListUserControl.ShowCurvePropertiesRequested += curveDetailsUserControl_ShowCurvePropertiesRequested;
            curveDetailsListUserControl.ShowNodePropertiesRequested += curveDetailsUserControl_ShowNodePropertiesRequested;
            curveGridUserControl.CloseRequested += curveGridUserControl_CloseRequested;
            curveGridUserControl.AddRequested += curveGridUserControl_AddRequested;
            curveGridUserControl.RemoveRequested += curveGridUserControl_RemoveRequested;
            curveGridUserControl.ShowItemRequested += curveGridUserControl_ShowItemRequested;
            curvePropertiesUserControl.CloseRequested += curvePropertiesUserControl_CloseRequested;
            curvePropertiesUserControl.LoseFocusRequested += curvePropertiesUserControl_LoseFocusRequested;
            curvePropertiesUserControl.RemoveRequested += curvePropertiesUserControl_RemoveRequested;
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
            documentTreeUserControl.AudioFileOutputsNodeSelected += documentTreeUserControl_AudioFileOutputsNodeSelected;
            documentTreeUserControl.AudioOutputNodeSelected += documentTreeUserControl_AudioOutputNodeSelected;
            documentTreeUserControl.CloseRequested += documentTreeUserControl_CloseRequested;
            documentTreeUserControl.CurvesNodeSelected += documentTreeUserControl_CurvesNodeSelected;
            documentTreeUserControl.LibrariesNodeSelected += documentTreeUserControl_LibrariesNodeSelected;
            documentTreeUserControl.LibraryNodeSelected += documentTreeUserControl_LibraryNodeSelected;
            documentTreeUserControl.LibraryPatchNodeSelected += documentTreeUserControl_LibraryPatchNodeSelected;
            documentTreeUserControl.LibraryPatchGroupNodeSelected += documentTreeUserControl_LibraryPatchGroupNodeSelected;
            documentTreeUserControl.PatchGroupNodeSelected += documentTreeUserControl_PatchGroupNodeSelected;
            documentTreeUserControl.PatchNodeSelected += documentTreeUserControl_PatchNodeSelected;
            documentTreeUserControl.PlayRequested += documentTreeUserControl_PlayRequested;
            documentTreeUserControl.OpenItemExternallyRequested += documentTreeUserControl_OpenItemExternallyRequested;
            documentTreeUserControl.RefreshRequested += documentTreeUserControl_RefreshRequested;
            documentTreeUserControl.SamplesNodeSelected += documentTreeUserControl_SamplesNodeSelected;
            documentTreeUserControl.SaveRequested += documentTreeUserControl_SaveRequested;
            documentTreeUserControl.ScalesNodeSelected += documentTreeUserControl_ScalesNodeSelected;
            documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
            documentTreeUserControl.ShowAudioOutputRequested += documentTreeUserControl_ShowAudioOutputRequested;
            documentTreeUserControl.ShowCurvesRequested += documentTreeUserControl_ShowCurvesRequested;
            documentTreeUserControl.ShowLibrariesRequested += documentTreeUserControl_ShowLibrariesRequested;
            documentTreeUserControl.ShowLibraryPatchGridRequested += documentTreeUserControl_ShowLibraryPatchGridRequested;
            documentTreeUserControl.ShowLibraryPatchPropertiesRequested += documentTreeUserControl_ShowLibraryPatchPropertiesRequested;
            documentTreeUserControl.ShowLibraryPropertiesRequested += documentTreeUserControl_ShowLibraryPropertiesRequested;
            documentTreeUserControl.ShowPatchDetailsRequested += documentTreeUserControl_ShowPatchDetailsRequested;
            documentTreeUserControl.ShowPatchGridRequested += documentTreeUserControl_ShowPatchGridRequested;
            documentTreeUserControl.ShowSamplesRequested += documentTreeUserControl_ShowSamplesRequested;
            documentTreeUserControl.ShowScalesRequested += documentTreeUserControl_ShowScalesRequested;
            libraryGridUserControl.AddRequested += libraryGridUserControl_AddRequested;
            libraryGridUserControl.CloseRequested += libraryGridUserControl_CloseRequested;
            libraryGridUserControl.PlayRequested += libraryGridUserControl_PlayRequested;
            libraryGridUserControl.OpenItemExternallyRequested += libraryGridUserControl_OpenItemExternallyRequested;
            libraryGridUserControl.RemoveRequested += libraryGridUserControl_RemoveRequested;
            libraryGridUserControl.ShowItemRequested += libraryGridUserControl_ShowItemRequested;
            libraryPatchGridUserControl.CloseRequested += libraryPatchGridUserControl_CloseRequested;
            libraryPatchGridUserControl.PlayRequested += libraryPatchGridUserControl_PlayRequested;
            libraryPatchGridUserControl.ShowItemRequested += libraryPatchGridUserControl_ShowItemRequested;
            libraryPatchGridUserControl.OpenItemExternallyRequested += libraryPatchGridUserControl_OpenItemExternallyRequested;
            libraryPatchPropertiesUserControl.AddToInstrumentRequested += libraryPatchPropertiesUserControl_AddToInstrument;
            libraryPatchPropertiesUserControl.CloseRequested += libraryPatchPropertiesUserControl_CloseRequested;
            libraryPatchPropertiesUserControl.OpenExternallyRequested += libraryPatchPropertiesUserControl_OpenExternallyRequested;
            libraryPatchPropertiesUserControl.PlayRequested += libraryPatchPropertiesUserControl_PlayRequested;
            libraryPropertiesUserControl.CloseRequested += libraryPropertiesUserControl_CloseRequested;
            libraryPropertiesUserControl.LoseFocusRequested += libraryPropertiesUserControl_LoseFocusRequested;
            libraryPropertiesUserControl.PlayRequested += libraryPropertiesUserControl_PlayRequested;
            libraryPropertiesUserControl.OpenExternallyRequested += libraryPropertiesUserControl_OpenExternallyRequested;
            libraryPropertiesUserControl.RemoveRequested += libraryPropertiesUserControl_RemoveRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            menuUserControl.ShowCurrentInstrumentRequested += menuUserControl_ShowCurrentInstrumentRequested;
            menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
            menuUserControl.ShowDocumentGridRequested += menuUserControl_ShowDocumentGridRequested;
            menuUserControl.ShowDocumentPropertiesRequested += MenuUserControl_ShowDocumentPropertiesRequested;
            nodePropertiesUserControl.CloseRequested += nodePropertiesUserControl_CloseRequested;
            nodePropertiesUserControl.LoseFocusRequested += nodePropertiesUserControl_LoseFocusRequested;
            nodePropertiesUserControl.RemoveRequested += nodePropertiesUserControl_RemoveRequested;
            operatorPropertiesUserControl.CloseRequested += operatorPropertiesUserControl_CloseRequested;
            operatorPropertiesUserControl.LoseFocusRequested += operatorPropertiesUserControl_LoseFocusRequested;
            operatorPropertiesUserControl.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_ForCache.CloseRequested += operatorPropertiesUserControl_ForCache_CloseRequested;
            operatorPropertiesUserControl_ForCache.LoseFocusRequested += operatorPropertiesUserControl_ForCache_LoseFocusRequested;
            operatorPropertiesUserControl_ForCache.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_ForCache.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_ForCurve.CloseRequested += operatorPropertiesUserControl_ForCurve_CloseRequested;
            operatorPropertiesUserControl_ForCurve.LoseFocusRequested += operatorPropertiesUserControl_ForCurve_LoseFocusRequested;
            operatorPropertiesUserControl_ForCurve.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_ForCurve.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_ForInletsToDimension.CloseRequested += operatorPropertiesUserControl_ForInletsToDimension_CloseRequested;
            operatorPropertiesUserControl_ForInletsToDimension.LoseFocusRequested += operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested;
            operatorPropertiesUserControl_ForInletsToDimension.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_ForInletsToDimension.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_ForNumber.CloseRequested += operatorPropertiesUserControl_ForNumber_CloseRequested;
            operatorPropertiesUserControl_ForNumber.LoseFocusRequested += operatorPropertiesUserControl_ForNumber_LoseFocusRequested;
            operatorPropertiesUserControl_ForNumber.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_ForNumber.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_ForPatchInlet.CloseRequested += operatorPropertiesUserControl_ForPatchInlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchInlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchInlet.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_ForPatchInlet.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_ForPatchOutlet.CloseRequested += operatorPropertiesUserControl_ForPatchOutlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchOutlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchOutlet.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_ForPatchOutlet.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_ForSample.CloseRequested += operatorPropertiesUserControl_ForSample_CloseRequested;
            operatorPropertiesUserControl_ForSample.LoseFocusRequested += operatorPropertiesUserControl_ForSample_LoseFocusRequested;
            operatorPropertiesUserControl_ForSample.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_ForSample.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_WithInterpolation.CloseRequested += operatorPropertiesUserControl_WithInterpolation_CloseRequested;
            operatorPropertiesUserControl_WithInterpolation.LoseFocusRequested += operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested;
            operatorPropertiesUserControl_WithInterpolation.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_WithInterpolation.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_WithCollectionRecalculation.CloseRequested += operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested;
            operatorPropertiesUserControl_WithCollectionRecalculation.LoseFocusRequested += operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested;
            operatorPropertiesUserControl_WithCollectionRecalculation.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_WithCollectionRecalculation.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_WithOutletCount.CloseRequested += operatorPropertiesUserControl_WithOutletCount_CloseRequested;
            operatorPropertiesUserControl_WithOutletCount.LoseFocusRequested += operatorPropertiesUserControl_WithOutletCount_LoseFocusRequested;
            operatorPropertiesUserControl_WithOutletCount.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_WithOutletCount.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            operatorPropertiesUserControl_WithInletCount.CloseRequested += operatorPropertiesUserControl_WithInletCount_CloseRequested;
            operatorPropertiesUserControl_WithInletCount.LoseFocusRequested += operatorPropertiesUserControl_WithInletCount_LoseFocusRequested;
            operatorPropertiesUserControl_WithInletCount.PlayRequested += operatorPropertiesUserControlBase_PlayRequested;
            operatorPropertiesUserControl_WithInletCount.RemoveRequested += operatorPropertiesUserControlBase_RemoveRequested;
            patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
            patchDetailsUserControl.CreateOperatorRequested += patchDetailsUserControl_CreateOperatorRequested;
            patchDetailsUserControl.DeleteOperatorRequested += patchDetailsUserControl_DeleteOperatorRequested;
            patchDetailsUserControl.LoseFocusRequested += patchDetailsUserControl_LoseFocusRequested;
            patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
            patchDetailsUserControl.RemoveRequested += patchDetailsUserControl_RemoveRequested;
            patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.ShowOperatorPropertiesRequested += patchDetailsUserControl_ShowOperatorPropertiesRequested;
            patchDetailsUserControl.ShowPatchPropertiesRequested += patchDetailsUserControl_ShowPatchPropertiesRequested;
            patchGridUserControl.CloseRequested += patchGridUserControl_CloseRequested;
            patchGridUserControl.AddRequested += patchGridUserControl_AddRequested;
            patchGridUserControl.PlayRequested += patchGridUserControl_PlayRequested;
            patchGridUserControl.RemoveRequested += patchGridUserControl_RemoveRequested;
            patchGridUserControl.ShowItemRequested += patchGridUserControl_ShowItemRequested;
            patchPropertiesUserControl.AddToInstrumentRequested += patchPropertiesUserControl_AddToInstrumentRequested;
            patchPropertiesUserControl.CloseRequested += patchPropertiesUserControl_CloseRequested;
            patchPropertiesUserControl.HasDimensionChanged += patchPropertiesUserControl_HasDimensionChanged;
            patchPropertiesUserControl.LoseFocusRequested += patchPropertiesUserControl_LoseFocusRequested;
            patchPropertiesUserControl.PlayRequested += patchPropertiesUserControl_PlayRequested;
            patchPropertiesUserControl.RemoveRequested += patchPropertiesUserControl_RemoveRequested;
            sampleGridUserControl.CloseRequested += sampleGridUserControl_CloseRequested;
            sampleGridUserControl.AddRequested += sampleGridUserControl_AddRequested;
            sampleGridUserControl.PlayRequested += sampleGridUserControl_PlayRequested;
            sampleGridUserControl.RemoveRequested += sampleGridUserControl_RemoveRequested;
            sampleGridUserControl.ShowItemRequested += sampleGridUserControl_ShowItemRequested;
            samplePropertiesUserControl.CloseRequested += samplePropertiesUserControl_CloseRequested;
            samplePropertiesUserControl.LoseFocusRequested += samplePropertiesUserControl_LoseFocusRequested;
            samplePropertiesUserControl.PlayRequested += samplePropertiesUserControl_PlayRequested;
            samplePropertiesUserControl.RemoveRequested += samplePropertiesUserControl_RemoveRequested;
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
            _librarySelectionPopupForm.OKRequested += _librarySelectionPopupForm_OKRequested;
            _librarySelectionPopupForm.OpenItemExternallyRequested += _librarySelectionPopupForm_OpenItemExternallyRequested;
            _librarySelectionPopupForm.PlayRequested += _librarySelectionPopupForm_PlayRequested;

            MessageBoxHelper.DocumentDeleteConfirmed += MessageBoxHelper_DocumentDeleteConfirmed;
            MessageBoxHelper.DocumentDeleteCanceled += MessageBoxHelper_DocumentDeleteCanceled;
            MessageBoxHelper.DocumentDeletedOK += MessageBoxHelper_DocumentDeletedOK;
            MessageBoxHelper.DocumentOrPatchNotFoundOK += MessageBoxHelper_DocumentOrPatchNotFoundOK;
            MessageBoxHelper.PopupMessagesOK += MessageBoxHelper_PopupMessagesOK;
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
                    _presenter.AddToInstrument(e.Value);

                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void currentInstrumentUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.CurrentInstrumentClose);

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

        private void currentInstrumentUserControl_ShowAutoPatchRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.AutoPatchPopupShow);

        private void _autoPatchPopupForm_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.AutoPatchPopupClose);

        private void _autoPatchPopupForm_SaveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(_presenter.AutoPatchPopupSave);

        // Curve

        private void curveGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.CurveCreate);

        private void curveGridUserControl_RemoveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.CurveGridDelete(e.Value));

        private void curveGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.CurveGridClose);

        private void curveGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.CurveDetailsShow(e.Value);
                    _presenter.CurvePropertiesShow(e.Value);
                });
        }

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

        private void curveDetailsUserControl_ShowCurvePropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.CurvePropertiesShow(e.Value));
        }

        private void curveDetailsUserControl_ShowNodePropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.NodePropertiesShow(e.Value));
        }

        private void curvePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.CurvePropertiesLoseFocus(e.Value));
        }

        private void curvePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.CurvePropertiesClose(e.Value));
        }

        private void curvePropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.CurvePropertiesDelete(e.Value));
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

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeClose);

        private void documentTreeUserControl_SaveRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentSave);

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.AudioFileOutputGridShow);

        private void documentTreeUserControl_ShowAudioOutputRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.AudioOutputPropertiesShow);

        private void documentTreeUserControl_ShowCurvesRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.CurveGridShow);

        private void documentTreeUserControl_ShowLibrariesRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.LibraryGridShow);

        private void documentTreeUserControl_ShowLibraryPatchGridRequested(object sender, LibraryPatchGroupEventArgs e)
        {
            TemplateActionHandler(() => _presenter.LibraryPatchGridShow(e.LowerDocumentReferenceID, e.PatchGroup));
        }

        private void documentTreeUserControl_ShowLibraryPatchPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.LibraryPatchPropertiesShow(e.Value));
        }

        private void documentTreeUserControl_ShowLibraryPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPropertiesShow(e.Value);
                    _presenter.LibraryPatchGridShow(e.Value, group: null);
                });
        }

        private void documentTreeUserControl_ShowPatchGridRequested(object sender, EventArgs<string> e)
        {
            TemplateActionHandler(() => _presenter.PatchGridShow(e.Value));
        }

        private void documentTreeUserControl_ShowPatchDetailsRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchDetailsShow(e.Value));
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.SampleGridShow);

        private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.ScaleGridShow);

        private void documentTreeUserControl_AudioFileOutputsNodeSelected(object sender, EventArgs e)
        {
            TemplateActionHandler(_presenter.DocumentTreeSelectAudioFileOutputs);
        }

        private void documentTreeUserControl_AudioOutputNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeSelectAudioOutput);

        private void documentTreeUserControl_CurvesNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeSelectCurves);

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

        private void documentTreeUserControl_SamplesNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeSelectSamples);

        private void documentTreeUserControl_ScalesNodeSelected(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentTreeSelectScales);

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

        private void libraryGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.LibraryAdd);

        private void libraryGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.LibraryGridClose);

        private void libraryGridUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryGridPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void libraryGridUserControl_OpenItemExternallyRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryGridOpenItemExternally(e.Value);
                    OpenDocumentExternallyAndOptionallyPatchIfNeeded();
                });
        }

        private void libraryGridUserControl_RemoveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.LibraryGridRemove(e.Value));

        private void libraryGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPropertiesShow(e.Value);
                    _presenter.LibraryPatchGridShow(e.Value, group: null);
                });
        }

        private void libraryPatchGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPatchGridClose(libraryPatchGridUserControl.ViewModel.LowerDocumentReferenceID, libraryPatchGridUserControl.ViewModel.Group);
                });
        }

        private void libraryPatchGridUserControl_OpenItemExternallyRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPatchGridOpenItemExternally(
                        libraryPatchGridUserControl.ViewModel.LowerDocumentReferenceID,
                        libraryPatchGridUserControl.ViewModel.Group,
                        e.Value);
                    OpenDocumentExternallyAndOptionallyPatchIfNeeded();
                });
        }

        private void libraryPatchGridUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPatchPropertiesPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void libraryPatchGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.LibraryPatchPropertiesShow(e.Value));
        }

        private void libraryPatchPropertiesUserControl_AddToInstrument(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.AddToInstrument(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void libraryPatchPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.LibraryPatchPropertiesClose(e.Value));
        }

        private void libraryPatchPropertiesUserControl_OpenExternallyRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPatchPropertiesOpenExternally(e.Value);
                    OpenDocumentExternallyAndOptionallyPatchIfNeeded();
                });
        }

        private void libraryPatchPropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.LibraryPatchPropertiesPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

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

        private void libraryPropertiesUserControl_OpenExternallyRequested(object sender, EventArgs<int> e)
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

        private void menuUserControl_ShowCurrentInstrumentRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.CurrentInstrumentShow);

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

        private void operatorPropertiesUserControl_WithOutletCount_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_WithOutletCount(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_WithOutletCount_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_WithOutletCount(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_WithInletCount_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesLoseFocus_WithInletCount(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void operatorPropertiesUserControl_WithInletCount_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorPropertiesClose_WithInletCount(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        // Patch

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

        private void patchDetailsUserControl_CreateOperatorRequested(object sender, CreateOperatorEventArgs e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.OperatorCreate(e.PatchID, e.OperatorTypeID);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

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

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, SelectOperatorEventArgs e)
        {
            TemplateActionHandler(() => _presenter.OperatorSelect(e.PatchID, e.OperatorID));
        }

        private void patchDetailsUserControl_ShowOperatorPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.OperatorPropertiesShow(e.Value));
        }

        private void patchDetailsUserControl_ShowPatchPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchPropertiesShow(e.Value));
        }

        private void patchGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(() => _presenter.PatchCreate(patchGridUserControl.ViewModel.Group));
        }

        private void patchGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateActionHandler(() => _presenter.PatchGridClose(patchGridUserControl.ViewModel.Group));
        }

        private void patchGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.PatchGridDelete(patchGridUserControl.ViewModel.Group, e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void patchGridUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.PatchGridPlay(patchGridUserControl.ViewModel.Group, e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void patchGridUserControl_ShowItemRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.PatchDetailsShow(e.Value));

        private void patchPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchPropertiesClose(e.Value));
        }

        private void patchPropertiesUserControl_HasDimensionChanged(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.PatchPropertiesHasDimensionChanged(e.Value));
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

        // Sample

        private void sampleGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.SampleCreate);

        private void sampleGridUserControl_CloseRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.SampleGridClose);

        private void sampleGridUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.SampleGridPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void sampleGridUserControl_RemoveRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.SampleGridDelete(e.Value));

        private void sampleGridUserControl_ShowItemRequested(object sender, EventArgs<int> e) => TemplateActionHandler(() => _presenter.SamplePropertiesShow(e.Value));

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.SamplePropertiesLoseFocus(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.SamplePropertiesClose(e.Value);
                    RecreatePatchCalculatorIfSuccessful();
                });
        }

        private void samplePropertiesUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(
                () =>
                {
                    _presenter.SamplePropertiesPlay(e.Value);
                    PlayOutletIfNeeded();
                });
        }

        private void samplePropertiesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.SamplePropertiesDelete(e.Value));
        }

        // Scale

        private void scaleGridUserControl_AddRequested(object sender, EventArgs e) => TemplateActionHandler(_presenter.ScaleCreate);

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

        private void MessageBoxHelper_DocumentDeleteCanceled(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentDeleteCancel);

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, EventArgs<int> e)
        {
            TemplateActionHandler(() => _presenter.DocumentDeleteConfirm(e.Value));
        }

        private void MessageBoxHelper_DocumentDeletedOK(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentDeletedOK);

        private void MessageBoxHelper_DocumentOrPatchNotFoundOK(object sender, EventArgs e) => TemplateActionHandler(_presenter.DocumentOrPatchNotFoundOK);

        private void MessageBoxHelper_PopupMessagesOK(object sender, EventArgs e) => TemplateActionHandler(_presenter.PopupMessagesOK);

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
using System;
using System.Media;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm
    {
        private void BindEvents()
        {
            audioFileOutputGridUserControl.CloseRequested += audioFileOutputGridUserControl_CloseRequested;
            audioFileOutputGridUserControl.AddRequested += audioFileOutputGridUserControl_AddRequested;
            audioFileOutputGridUserControl.RemoveRequested += audioFileOutputGridUserControl_RemoveRequested;
            audioFileOutputGridUserControl.ShowItemRequested += audioFileOutputGridUserControl_ShowItemRequested;
            audioFileOutputPropertiesUserControl.CloseRequested += audioFileOutputPropertiesUserControl_CloseRequested;
            audioFileOutputPropertiesUserControl.LoseFocusRequested += audioFileOutputPropertiesUserControl_LoseFocusRequested;
            audioOutputPropertiesUserControl.CloseRequested += audioOutputPropertiesUserControl_CloseRequested;
            audioOutputPropertiesUserControl.LoseFocusRequested += audioOutputPropertiesUserControl_LoseFocusRequested;
            currentPatchesUserControl.CloseRequested += currentPatchesUserControl_CloseRequested;
            currentPatchesUserControl.RemoveRequested += currentPatchesUserControl_RemoveRequested;
            currentPatchesUserControl.ShowAutoPatchRequested += currentPatchesUserControl_ShowAutoPatchRequested;
            curveDetailsUserControl.ChangeSelectedNodeTypeRequested += curveDetailsUserControl_ChangeSelectedNodeTypeRequested;
            curveDetailsUserControl.CloseRequested += curveDetailsUserControl_CloseRequested;
            curveDetailsUserControl.CreateNodeRequested += curveDetailsUserControl_CreateNodeRequested;
            curveDetailsUserControl.DeleteSelectedNodeRequested += curveDetailsUserControl_DeleteSelectedNodeRequested;
            curveDetailsUserControl.LoseFocusRequested += curveDetailsUserControl_LoseFocusRequested;
            curveDetailsUserControl.MoveNodeRequested += curveDetailsUserControl_MoveNodeRequested;
            curveDetailsUserControl.SelectNodeRequested += curveDetailsUserControl_SelectNodeRequested;
            curveDetailsUserControl.ShowCurvePropertiesRequested += curveDetailsUserControl_ShowCurvePropertiesRequested;
            curveDetailsUserControl.ShowNodePropertiesRequested += curveDetailsUserControl_ShowNodePropertiesRequested;
            curveGridUserControl.CloseRequested += curveGridUserControl_CloseRequested;
            curveGridUserControl.AddRequested += curveGridUserControl_AddRequested;
            curveGridUserControl.RemoveRequested += curveGridUserControl_RemoveRequested;
            curveGridUserControl.ShowItemRequested += curveGridUserControl_ShowItemRequested;
            curvePropertiesUserControl.CloseRequested += curvePropertiesUserControl_CloseRequested;
            curvePropertiesUserControl.LoseFocusRequested += curvePropertiesUserControl_LoseFocusRequested;
            documentDetailsUserControl.CloseRequested += documentDetailsUserControl_CloseRequested;
            documentDetailsUserControl.DeleteRequested += documentDetailsUserControl_DeleteRequested;
            documentDetailsUserControl.SaveRequested += documentDetailsUserControl_SaveRequested;
            documentGridUserControl.CloseRequested += documentGridUserControl_CloseRequested;
            documentGridUserControl.AddRequested += documentGridUserControl_AddRequested;
            documentGridUserControl.RemoveRequested += documentGridUserControl_RemoveRequested;
            documentGridUserControl.ShowItemRequested += documentGridUserControl_ShowItemRequested;
            documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
            documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;
            documentTreeUserControl.CloseRequested += documentTreeUserControl_CloseRequested;
            documentTreeUserControl.ShowAudioOutputRequested += documentTreeUserControl_ShowAudioOutputRequested;
            documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
            documentTreeUserControl.ShowCurvesRequested += documentTreeUserControl_ShowCurvesRequested;
            documentTreeUserControl.ShowLibrariesRequested += documentTreeUserControl_ShowLibrariesRequested;
            documentTreeUserControl.ShowLibraryPropertiesRequested += documentTreeUserControl_ShowLibraryPropertiesRequested;
            documentTreeUserControl.ShowPatchDetailsRequested += documentTreeUserControl_ShowPatchDetailsRequested;
            documentTreeUserControl.ShowPatchGridRequested += documentTreeUserControl_ShowPatchGridRequested;
            documentTreeUserControl.ShowSamplesRequested += documentTreeUserControl_ShowSamplesRequested;
            documentTreeUserControl.ShowScalesRequested += documentTreeUserControl_ShowScalesRequested;
            libraryGridUserControl.CloseRequested += libraryGridUserControl_CloseRequested;
            libraryGridUserControl.AddRequested += libraryGridUserControl_AddRequested;
            libraryGridUserControl.RemoveRequested += libraryGridUserControl_RemoveRequested;
            libraryGridUserControl.ShowItemRequested += libraryGridUserControl_ShowItemRequested;
            libraryPropertiesUserControl.CloseRequested += libraryPropertiesUserControl_CloseRequested;
            libraryPropertiesUserControl.LoseFocusRequested += libraryPropertiesUserControl_LoseFocusRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            menuUserControl.ShowCurrentPatchesRequested += menuUserControl_ShowCurrentPatchesRequested;
            menuUserControl.DocumentSaveRequested += menuUserControl_DocumentSaveRequested;
            menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
            menuUserControl.ShowDocumentGridRequested += menuUserControl_ShowDocumentGridRequested;
            menuUserControl.ShowDocumentPropertiesRequested += MenuUserControl_ShowDocumentPropertiesRequested;
            nodePropertiesUserControl.CloseRequested += nodePropertiesUserControl_CloseRequested;
            nodePropertiesUserControl.LoseFocusRequested += nodePropertiesUserControl_LoseFocusRequested;
            operatorPropertiesUserControl.CloseRequested += operatorPropertiesUserControl_CloseRequested;
            operatorPropertiesUserControl.LoseFocusRequested += operatorPropertiesUserControl_LoseFocusRequested;
            operatorPropertiesUserControl_ForCache.CloseRequested += operatorPropertiesUserControl_ForCache_CloseRequested;
            operatorPropertiesUserControl_ForCache.LoseFocusRequested += operatorPropertiesUserControl_ForCache_LoseFocusRequested;
            operatorPropertiesUserControl_ForCurve.CloseRequested += operatorPropertiesUserControl_ForCurve_CloseRequested;
            operatorPropertiesUserControl_ForCurve.LoseFocusRequested += operatorPropertiesUserControl_ForCurve_LoseFocusRequested;
            operatorPropertiesUserControl_ForCustomOperator.CloseRequested += operatorPropertiesUserControl_ForCustomOperator_CloseRequested;
            operatorPropertiesUserControl_ForCustomOperator.LoseFocusRequested += operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested;
            operatorPropertiesUserControl_ForInletsToDimension.CloseRequested += operatorPropertiesUserControl_ForInletsToDimension_CloseRequested;
            operatorPropertiesUserControl_ForInletsToDimension.LoseFocusRequested += operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested;
            operatorPropertiesUserControl_ForNumber.CloseRequested += operatorPropertiesUserControl_ForNumber_CloseRequested;
            operatorPropertiesUserControl_ForNumber.LoseFocusRequested += operatorPropertiesUserControl_ForNumber_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchInlet.CloseRequested += operatorPropertiesUserControl_ForPatchInlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchInlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchOutlet.CloseRequested += operatorPropertiesUserControl_ForPatchOutlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchOutlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForSample.CloseRequested += operatorPropertiesUserControl_ForSample_CloseRequested;
            operatorPropertiesUserControl_ForSample.LoseFocusRequested += operatorPropertiesUserControl_ForSample_LoseFocusRequested;
            operatorPropertiesUserControl_WithInterpolation.CloseRequested += operatorPropertiesUserControl_WithInterpolation_CloseRequested;
            operatorPropertiesUserControl_WithInterpolation.LoseFocusRequested += operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested;
            operatorPropertiesUserControl_WithCollectionRecalculation.CloseRequested += operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested;
            operatorPropertiesUserControl_WithCollectionRecalculation.LoseFocusRequested += operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested;
            operatorPropertiesUserControl_WithOutletCount.CloseRequested += operatorPropertiesUserControl_WithOutletCount_CloseRequested;
            operatorPropertiesUserControl_WithOutletCount.LoseFocusRequested += operatorPropertiesUserControl_WithOutletCount_LoseFocusRequested;
            operatorPropertiesUserControl_WithInletCount.CloseRequested += operatorPropertiesUserControl_WithInletCount_CloseRequested;
            operatorPropertiesUserControl_WithInletCount.LoseFocusRequested += operatorPropertiesUserControl_WithInletCount_LoseFocusRequested;
            patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
            patchDetailsUserControl.CreateOperatorRequested += patchDetailsUserControl_CreateOperatorRequested;
            patchDetailsUserControl.DeleteOperatorRequested += patchDetailsUserControl_DeleteOperatorRequested;
            patchDetailsUserControl.LoseFocusRequested += patchDetailsUserControl_LoseFocusRequested;
            patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
            patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.ShowOperatorPropertiesRequested += patchDetailsUserControl_ShowOperatorPropertiesRequested;
            patchDetailsUserControl.ShowPatchPropertiesRequested += patchDetailsUserControl_ShowPatchPropertiesRequested;
            patchGridUserControl.CloseRequested += patchGridUserControl_CloseRequested;
            patchGridUserControl.AddRequested += patchGridUserControl_AddRequested;
            patchGridUserControl.RemoveRequested += patchGridUserControl_RemoveRequested;
            patchGridUserControl.ShowItemRequested += patchGridUserControl_ShowItemRequested;
            patchPropertiesUserControl.AddCurrentPatchRequested += patchPropertiesUserControl_AddCurrentPatchRequested;
            patchPropertiesUserControl.CloseRequested += patchPropertiesUserControl_CloseRequested;
            patchPropertiesUserControl.LoseFocusRequested += patchPropertiesUserControl_LoseFocusRequested;
            sampleGridUserControl.CloseRequested += sampleGridUserControl_CloseRequested;
            sampleGridUserControl.AddRequested += sampleGridUserControl_AddRequested;
            sampleGridUserControl.RemoveRequested += sampleGridUserControl_RemoveRequested;
            sampleGridUserControl.ShowItemRequested += sampleGridUserControl_ShowItemRequested;
            samplePropertiesUserControl.CloseRequested += samplePropertiesUserControl_CloseRequested;
            samplePropertiesUserControl.LoseFocusRequested += samplePropertiesUserControl_LoseFocusRequested;
            scaleGridUserControl.CloseRequested += scaleGridUserControl_CloseRequested;
            scaleGridUserControl.AddRequested += scaleGridUserControl_AddRequested;
            scaleGridUserControl.RemoveRequested += scaleGridUserControl_RemoveRequested;
            scaleGridUserControl.ShowItemRequested += scaleGridUserControl_ShowItemRequested;
            scalePropertiesUserControl.CloseRequested += scalePropertiesUserControl_CloseRequested;
            scalePropertiesUserControl.LoseFocusRequested += scalePropertiesUserControl_LoseFocusRequested;
            toneGridEditUserControl.CloseRequested += toneGridEditUserControl_CloseRequested;
            toneGridEditUserControl.CreateToneRequested += toneGridEditUserControl_CreateToneRequested;
            toneGridEditUserControl.DeleteToneRequested += toneGridEditUserControl_DeleteToneRequested;
            toneGridEditUserControl.Edited += toneGridEditUserControl_Edited;
            toneGridEditUserControl.LoseFocusRequested += toneGridEditUserControl_LoseFocusRequested;
            toneGridEditUserControl.PlayToneRequested += toneGridEditUserControl_PlayToneRequested;

            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;
            _autoPatchDetailsForm.CloseRequested += _autoPatchDetailsForm_CloseRequested;
            _librarySelectionPopupForm.CancelRequested += _librarySelectionPopupForm_CancelRequested;
            _librarySelectionPopupForm.OKRequested += _librarySelectionPopupForm_OKRequested;

            MessageBoxHelper.DocumentDeleteConfirmed += MessageBoxHelper_DocumentDeleteConfirmed;
            MessageBoxHelper.DocumentDeleteCanceled += MessageBoxHelper_DocumentDeleteCanceled;
            MessageBoxHelper.DocumentDeletedOK += MessageBoxHelper_DocumentDeletedOK;
            MessageBoxHelper.PopupMessagesOK += MessageBoxHelper_PopupMessagesOK;
        }

        // AudioFileOutput

        private void audioFileOutputGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputCreate);
        }

        private void audioFileOutputGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.AudioFileOutputDelete(e.Value));
        }

        private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputGridClose);
        }

        private void audioFileOutputGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.AudioFileOutputPropertiesShow(e.Value));
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.AudioFileOutputPropertiesClose(e.Value));
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.AudioFileOutputPropertiesLoseFocus(e.Value));
        }

        // AudioOutput

        private void audioOutputPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() =>
            {
                _presenter.AudioOutputPropertiesClose();
                SetAudioOutputIfNeeded();
            });
        }

        private void audioOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() =>
            {
                _presenter.AudioOutputPropertiesLoseFocus();
                SetAudioOutputIfNeeded();
            });
        }

        // CurrentPatches

        private void patchPropertiesUserControl_AddCurrentPatchRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() =>
            {
                _presenter.CurrentPatchAdd(e.Value);
                RecreatePatchCalculator();
            });
        }

        private void currentPatchesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurrentPatchesClose);
        }

        private void currentPatchesUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() =>
            {
                _presenter.CurrentPatchRemove(e.Value);
                RecreatePatchCalculator();
            });
        }

        private void currentPatchesUserControl_ShowAutoPatchRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurrentPatchesShowAutoPatch);
        }

        private void _autoPatchDetailsForm_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AutoPatchDetailsClose);
        }

        // Curve

        private void curveGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurveCreate);
        }

        private void curveGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.CurveDelete(e.Value));
        }

        private void curveGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurveGridClose);
        }

        private void curveGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.CurveDetailsShow(e.Value));
        }

        private void curveDetailsUserControl_SelectNodeRequested(object sender, NodeEventArgs e)
        {
            TemplateEventHandler(() => _presenter.NodeSelect(e.CurveID, e.NodeID));
        }

        private void curveDetailsUserControl_CreateNodeRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.NodeCreate(e.Value));
        }

        private void curveDetailsUserControl_DeleteSelectedNodeRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.NodeDeleteSelected(e.Value));
        }

        private void curveDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.CurveDetailsClose(e.Value));
        }

        private void curveDetailsUserControl_MoveNodeRequested(object sender, MoveNodeEventArgs e)
        {
            TemplateEventHandler(() => _presenter.NodeMove(e.CurveID, e.NodeID, e.X, e.Y));
        }

        private void curveDetailsUserControl_ChangeSelectedNodeTypeRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.NodeChangeSelectedNodeType(e.Value));
        }

        private void curveDetailsUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.CurveDetailsLoseFocus(e.Value));
        }

        private void curveDetailsUserControl_ShowCurvePropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.CurvePropertiesShow(e.Value));
        }

        private void curveDetailsUserControl_ShowNodePropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.NodePropertiesShow(e.Value));
        }

        private void curvePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.CurvePropertiesLoseFocus(e.Value));
        }

        private void curvePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.CurvePropertiesClose(e.Value));
        }

        // Document Grid

        private void documentGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentDetailsCreate);
        }

        private void documentGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() =>
            {
                ForceLoseFocus();

                _presenter.DocumentOpen(e.Value);

                SetAudioOutputIfNeeded();
            });
        }

        private void documentGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.DocumentDeleteShow(e.Value));
        }

        private void documentGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentGridClose);
        }

        // Document Details

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentDetailsSave);
        }

        private void documentDetailsUserControl_DeleteRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.DocumentDeleteShow(e.Value));
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentDetailsClose);
        }

        // Document Tree

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentTreeClose);
        }

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputGridShow);
        }

        private void documentTreeUserControl_ShowAudioOutputRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioOutputPropertiesShow);
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurveGridShow);
        }

        private void documentTreeUserControl_ShowLibrariesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.LibraryGridShow);
        }

        private void documentTreeUserControl_ShowLibraryPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.LibraryPropertiesShow(e.Value));
        }

        private void documentTreeUserControl_ShowPatchGridRequested(object sender, EventArgs<string> e)
        {
            TemplateEventHandler(() => _presenter.PatchGridShow(e.Value));
        }

        private void documentTreeUserControl_ShowPatchDetailsRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchDetailsShow(e.Value));
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SampleGridShow);
        }

        private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ScaleGridShow);
        }

        // Document Properties

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentPropertiesClose);
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentPropertiesLoseFocus);
        }

        // Library

        private void libraryGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.LibraryGridClose);
        }

        private void libraryGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.LibraryAdd);
        }

        private void libraryGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.LibraryRemove(e.Value));
        }

        private void libraryGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.LibraryPropertiesShow(e.Value));
        }

        private void libraryPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.LibraryPropertiesLoseFocus(e.Value));
        }

        private void libraryPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.LibraryPropertiesClose(e.Value));
        }

        private void _librarySelectionPopupForm_CancelRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.LibrarySelectionPopupCancel);
        }

        private void _librarySelectionPopupForm_OKRequested(object sender, EventArgs<int?> e)
        {
            TemplateEventHandler(() => _presenter.LibrarySelectionPopupOK(e.Value));
        }

        // Menu

        private void menuUserControl_ShowDocumentGridRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentGridShow);
        }

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentTreeShow);
        }

        private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(() =>
            {
                ForceLoseFocus();
                _presenter.DocumentClose();
            });
        }

        private void menuUserControl_DocumentSaveRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(() =>
            {
                ForceLoseFocus();
                _presenter.DocumentSave();
            });
        }

        private void menuUserControl_ShowCurrentPatchesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurrentPatchesShow);
        }

        private void MenuUserControl_ShowDocumentPropertiesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentPropertiesShow);
        }

        // Node

        private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.NodePropertiesLoseFocus(e.Value));
        }

        private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.NodePropertiesClose(e.Value));
        }

        // Operator

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus(e.Value));
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose(e.Value));
        }

        private void operatorPropertiesUserControl_ForCache_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForCache(e.Value));
        }

        private void operatorPropertiesUserControl_ForCache_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForCache(e.Value));
        }

        private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForCurve(e.Value));
        }

        private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForCurve(e.Value));
        }

        private void operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForCustomOperator(e.Value));
        }

        private void operatorPropertiesUserControl_ForCustomOperator_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForCustomOperator(e.Value));
        }

        private void operatorPropertiesUserControl_ForInletsToDimension_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForInletsToDimension(e.Value));
        }

        private void operatorPropertiesUserControl_ForInletsToDimension_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForInletsToDimension(e.Value));
        }

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForNumber(e.Value));
        }

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForNumber(e.Value));
        }

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForPatchInlet(e.Value));
        }

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForPatchInlet(e.Value));
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForPatchOutlet(e.Value));
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForPatchOutlet(e.Value));
        }

        private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_ForSample(e.Value));
        }

        private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_ForSample(e.Value));
        }

        private void operatorPropertiesUserControl_WithInterpolation_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_WithInterpolation(e.Value));
        }

        private void operatorPropertiesUserControl_WithInterpolation_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_WithInterpolation(e.Value));
        }

        private void operatorPropertiesUserControl_WithCollectionRecalculation_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_WithCollectionRecalculation(e.Value));
        }

        private void operatorPropertiesUserControl_WithCollectionRecalculation_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_WithCollectionRecalculation(e.Value));
        }

        private void operatorPropertiesUserControl_WithOutletCount_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_WithOutletCount(e.Value));
        }

        private void operatorPropertiesUserControl_WithOutletCount_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_WithOutletCount(e.Value));
        }

        private void operatorPropertiesUserControl_WithInletCount_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesLoseFocus_WithInletCount(e.Value));
        }

        private void operatorPropertiesUserControl_WithInletCount_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesClose_WithInletCount(e.Value));
        }

        // Patch

        private void patchGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(() => _presenter.PatchCreate(patchGridUserControl.ViewModel.Group));
        }

        private void patchGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchDelete(patchGridUserControl.ViewModel.Group, e.Value));
        }

        private void patchGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(() => _presenter.PatchGridClose(patchGridUserControl.ViewModel.Group));
        }

        private void patchGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchDetailsShow(e.Value));
        }

        private void patchDetailsUserControl_PlayRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() =>
            {
                string outputFilePath = _presenter.PatchPlay(e.Value);
                if (outputFilePath == null)
                {
                    return;
                }

                var soundPlayer = new SoundPlayer(outputFilePath);
                soundPlayer.Play();
            });
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, SelectOperatorEventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorSelect(e.PatchID, e.OperatorID));
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorChangeInputOutlet(e.PatchID, e.InletID, e.InputOutletID));
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorMove(e.PatchID, e.OperatorID, e.X, e.Y));
        }

        private void patchDetailsUserControl_CreateOperatorRequested(object sender, CreateOperatorEventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorCreate(e.PatchID, e.OperatorTypeID));
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorDeleteSelected(e.Value));
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchDetailsLoseFocus(e.Value));
        }

        private void patchDetailsUserControl_ShowOperatorPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesShow(e.Value));
        }

        private void patchDetailsUserControl_ShowPatchPropertiesRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchPropertiesShow(e.Value));
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchDetailsClose(e.Value));
        }

        private void patchPropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchPropertiesLoseFocus(e.Value));
        }

        private void patchPropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.PatchPropertiesClose(e.Value));
        }

        // Sample

        private void sampleGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SampleCreate);
        }

        private void sampleGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.SampleDelete(e.Value));
        }

        private void sampleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SampleGridClose);
        }

        private void sampleGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.SamplePropertiesShow(e.Value));
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.SamplePropertiesLoseFocus(e.Value));
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.SamplePropertiesClose(e.Value));
        }

        // Scale

        private void scaleGridUserControl_AddRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ScaleCreate);
        }

        private void scaleGridUserControl_RemoveRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ScaleDelete(e.Value));
        }

        private void scaleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ScaleGridClose);
        }

        private void scaleGridUserControl_ShowItemRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ScaleShow(e.Value));
        }

        private void toneGridEditUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ToneGridEditClose(e.Value));
        }

        private void toneGridEditUserControl_Edited(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ToneGridEditEdit(e.Value));
        }

        private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ToneGridEditLoseFocus(e.Value));
        }

        private void toneGridEditUserControl_CreateToneRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ToneCreate(e.Value));
        }

        private void toneGridEditUserControl_DeleteToneRequested(object sender, ScaleAndToneEventArgs e)
        {
            TemplateEventHandler(() => _presenter.ToneDelete(e.ScaleID, e.ToneID));
        }

        private void toneGridEditUserControl_PlayToneRequested(object sender, ScaleAndToneEventArgs e)
        {
            TemplateEventHandler(() =>
            {
                string outputFilePath = _presenter.TonePlay(e.ScaleID, e.ToneID);
                if (string.IsNullOrEmpty(outputFilePath))
                {
                    return;
                }

                var soundPlayer = new SoundPlayer(outputFilePath);
                soundPlayer.Play();
            });
        }

        private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ScalePropertiesClose(e.Value));
        }

        private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.ScalePropertiesLoseFocus(e.Value));
        }

        // Message Boxes

        private void MessageBoxHelper_DocumentDeleteCanceled(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentDeleteCancel);
        }

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, EventArgs<int> e)
        {
            TemplateEventHandler(() => _presenter.DocumentDeleteConfirm(e.Value));
        }

        private void MessageBoxHelper_DocumentDeletedOK(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentDeletedOK);
        }

        private void MessageBoxHelper_PopupMessagesOK(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.PopupMessagesOK);
        }

        // DocumentCannotDeleteForm

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentCannotDeleteOK);
        }

        // Template Method

        /// <summary> Surrounds a call to a presenter action with rollback and ApplyViewModel. </summary>
        private void TemplateEventHandler(Action action)
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
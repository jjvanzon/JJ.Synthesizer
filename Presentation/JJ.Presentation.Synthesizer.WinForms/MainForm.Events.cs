using System;
using System.Collections.Generic;
using System.Linq;
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
            audioFileOutputGridUserControl.CreateRequested += audioFileOutputGridUserControl_CreateRequested;
            audioFileOutputGridUserControl.DeleteRequested += audioFileOutputGridUserControl_DeleteRequested;
            audioFileOutputGridUserControl.ShowPropertiesRequested += audioFileOutputGridUserControl_ShowPropertiesRequested;
            audioFileOutputPropertiesUserControl.CloseRequested += audioFileOutputPropertiesUserControl_CloseRequested;
            audioFileOutputPropertiesUserControl.LoseFocusRequested += audioFileOutputPropertiesUserControl_LoseFocusRequested;
            currentPatchesUserControl.CloseRequested += currentPatchesUserControl_CloseRequested;
            currentPatchesUserControl.RemoveRequested += currentPatchesUserControl_RemoveRequested;
            currentPatchesUserControl.PreviewAutoPatchRequested += currentPatchesUserControl_PreviewAutoPatchRequested;
            curveDetailsUserControl.ChangeNodeTypeRequested += curveDetailsUserControl_ChangeNodeTypeRequested;
            curveDetailsUserControl.CloseRequested += curveDetailsUserControl_CloseRequested;
            curveDetailsUserControl.CreateNodeRequested += curveDetailsUserControl_CreateNodeRequested;
            curveDetailsUserControl.DeleteNodeRequested += curveDetailsUserControl_DeleteNodeRequested;
            curveDetailsUserControl.LoseFocusRequested += curveDetailsUserControl_LoseFocusRequested;
            curveDetailsUserControl.MoveNodeRequested += curveDetailsUserControl_MoveNodeRequested;
            curveDetailsUserControl.SelectNodeRequested += curveDetailsUserControl_SelectNodeRequested;
            curveDetailsUserControl.ShowCurvePropertiesRequested += curveDetailsUserControl_ShowCurvePropertiesRequested;
            curveDetailsUserControl.ShowNodePropertiesRequested += curveDetailsUserControl_ShowNodePropertiesRequested;
            curveDetailsUserControl.ShowSelectedNodePropertiesRequested += curveDetailsUserControl_ShowSelectedNodePropertiesRequested;
            curveGridUserControl.CloseRequested += curveGridUserControl_CloseRequested;
            curveGridUserControl.CreateRequested += curveGridUserControl_CreateRequested;
            curveGridUserControl.DeleteRequested += curveGridUserControl_DeleteRequested;
            curveGridUserControl.ShowDetailsRequested += curveGridUserControl_ShowDetailsRequested;
            curvePropertiesUserControl.CloseRequested += curvePropertiesUserControl_CloseRequested;
            curvePropertiesUserControl.LoseFocusRequested += curvePropertiesUserControl_LoseFocusRequested;
            documentDetailsUserControl.CloseRequested += documentDetailsUserControl_CloseRequested;
            documentDetailsUserControl.DeleteRequested += documentDetailsUserControl_DeleteRequested;
            documentDetailsUserControl.SaveRequested += documentDetailsUserControl_SaveRequested;
            documentGridUserControl.CloseRequested += documentGridUserControl_CloseRequested;
            documentGridUserControl.CreateRequested += documentGridUserControl_CreateRequested;
            documentGridUserControl.DeleteRequested += documentGridUserControl_DeleteRequested;
            documentGridUserControl.OpenRequested += documentGridUserControl_OpenRequested;
            documentPropertiesUserControl.CloseRequested += documentPropertiesUserControl_CloseRequested;
            documentPropertiesUserControl.LoseFocusRequested += documentPropertiesUserControl_LoseFocusRequested;
            documentTreeUserControl.CloseRequested += documentTreeUserControl_CloseRequested;
            documentTreeUserControl.CollapseNodeRequested += documentTreeUserControl_CollapseNodeRequested;
            documentTreeUserControl.ExpandNodeRequested += documentTreeUserControl_ExpandNodeRequested;
            documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
            documentTreeUserControl.ShowCurvesRequested += documentTreeUserControl_ShowCurvesRequested;
            documentTreeUserControl.ShowDocumentPropertiesRequested += documentTreeUserControl_ShowDocumentPropertiesRequested;
            documentTreeUserControl.ShowPatchDetailsRequested += documentTreeUserControl_ShowPatchDetailsRequested;
            documentTreeUserControl.ShowPatchGridRequested += documentTreeUserControl_ShowPatchGridRequested;
            documentTreeUserControl.ShowSamplesRequested += documentTreeUserControl_ShowSamplesRequested;
            documentTreeUserControl.ShowScalesRequested += documentTreeUserControl_ShowScalesRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            menuUserControl.ShowCurrentPatchesRequested += menuUserControl_ShowCurrentPatchesRequested;
            menuUserControl.DocumentSaveRequested += menuUserControl_DocumentSaveRequested;
            menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
            menuUserControl.ShowDocumentGridRequested += menuUserControl_ShowDocumentGridRequested;
            nodePropertiesUserControl.CloseRequested += nodePropertiesUserControl_CloseRequested;
            nodePropertiesUserControl.LoseFocusRequested += nodePropertiesUserControl_LoseFocusRequested;
            operatorPropertiesUserControl.CloseRequested += operatorPropertiesUserControl_CloseRequested;
            operatorPropertiesUserControl.LoseFocusRequested += operatorPropertiesUserControl_LoseFocusRequested;
            operatorPropertiesUserControl_ForAggregate.CloseRequested += operatorPropertiesUserControl_ForAggregate_CloseRequested;
            operatorPropertiesUserControl_ForAggregate.LoseFocusRequested += operatorPropertiesUserControl_ForAggregate_LoseFocusRequested;
            operatorPropertiesUserControl_ForBundle.CloseRequested += operatorPropertiesUserControl_ForBundle_CloseRequested;
            operatorPropertiesUserControl_ForBundle.LoseFocusRequested += operatorPropertiesUserControl_ForBundle_LoseFocusRequested;
            operatorPropertiesUserControl_ForCache.CloseRequested += operatorPropertiesUserControl_ForCache_CloseRequested;
            operatorPropertiesUserControl_ForCache.LoseFocusRequested += operatorPropertiesUserControl_ForCache_LoseFocusRequested;
            operatorPropertiesUserControl_ForCurve.CloseRequested += operatorPropertiesUserControl_ForCurve_CloseRequested;
            operatorPropertiesUserControl_ForCurve.LoseFocusRequested += operatorPropertiesUserControl_ForCurve_LoseFocusRequested;
            operatorPropertiesUserControl_ForCustomOperator.CloseRequested += operatorPropertiesUserControl_ForCustomOperator_CloseRequested;
            operatorPropertiesUserControl_ForCustomOperator.LoseFocusRequested += operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested;
            operatorPropertiesUserControl_ForFilter.CloseRequested += operatorPropertiesUserControl_ForFilter_CloseRequested;
            operatorPropertiesUserControl_ForFilter.LoseFocusRequested += operatorPropertiesUserControl_ForFilter_LoseFocusRequested;
            operatorPropertiesUserControl_ForNumber.CloseRequested += operatorPropertiesUserControl_ForNumber_CloseRequested;
            operatorPropertiesUserControl_ForNumber.LoseFocusRequested += operatorPropertiesUserControl_ForNumber_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchInlet.CloseRequested += operatorPropertiesUserControl_ForPatchInlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchInlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchOutlet.CloseRequested += operatorPropertiesUserControl_ForPatchOutlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchOutlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForRandom.CloseRequested += operatorPropertiesUserControl_ForRandom_CloseRequested;
            operatorPropertiesUserControl_ForRandom.LoseFocusRequested += operatorPropertiesUserControl_ForRandom_LoseFocusRequested;
            operatorPropertiesUserControl_ForResample.CloseRequested += operatorPropertiesUserControl_ForResample_CloseRequested;
            operatorPropertiesUserControl_ForResample.LoseFocusRequested += operatorPropertiesUserControl_ForResample_LoseFocusRequested;
            operatorPropertiesUserControl_ForSample.CloseRequested += operatorPropertiesUserControl_ForSample_CloseRequested;
            operatorPropertiesUserControl_ForSample.LoseFocusRequested += operatorPropertiesUserControl_ForSample_LoseFocusRequested;
            operatorPropertiesUserControl_ForSpectrum.CloseRequested += operatorPropertiesUserControl_ForSpectrum_CloseRequested;
            operatorPropertiesUserControl_ForSpectrum.LoseFocusRequested += operatorPropertiesUserControl_ForSpectrum_LoseFocusRequested;
            operatorPropertiesUserControl_ForUnbundle.CloseRequested += operatorPropertiesUserControl_ForUnbundle_CloseRequested;
            operatorPropertiesUserControl_ForUnbundle.LoseFocusRequested += operatorPropertiesUserControl_ForUnbundle_LoseFocusRequested;
            patchDetailsUserControl.ChangeInputOutletRequested += patchDetailsUserControl_ChangeInputOutletRequested;
            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
            patchDetailsUserControl.CreateOperatorRequested += patchDetailsUserControl_CreateOperatorRequested;
            patchDetailsUserControl.DeleteOperatorRequested += patchDetailsUserControl_DeleteOperatorRequested;
            patchDetailsUserControl.LoseFocusRequested += patchDetailsUserControl_LoseFocusRequested;
            patchDetailsUserControl.MoveOperatorRequested += patchDetailsUserControl_MoveOperatorRequested;
            patchDetailsUserControl.OperatorPropertiesRequested += patchDetailsUserControl_OperatorPropertiesRequested;
            patchDetailsUserControl.PlayRequested += patchDetailsUserControl_PlayRequested;
            patchDetailsUserControl.SelectOperatorRequested += patchDetailsUserControl_SelectOperatorRequested;
            patchDetailsUserControl.SelectedOperatorPropertiesRequested += patchDetailsUserControl_SelectedOperatorPropertiesRequested;
            patchGridUserControl.CloseRequested += patchGridUserControl_CloseRequested;
            patchGridUserControl.CreateRequested += patchGridUserControl_CreateRequested;
            patchGridUserControl.DeleteRequested += patchGridUserControl_DeleteRequested;
            patchGridUserControl.ShowDetailsRequested += patchGridUserControl_ShowDetailsRequested;
            patchPropertiesUserControl.AddCurrentPatchRequested += patchPropertiesUserControl_AddCurrentPatchRequested;
            patchPropertiesUserControl.CloseRequested += patchPropertiesUserControl_CloseRequested;
            patchPropertiesUserControl.LoseFocusRequested += patchPropertiesUserControl_LoseFocusRequested;
            sampleGridUserControl.CloseRequested += sampleGridUserControl_CloseRequested;
            sampleGridUserControl.CreateRequested += sampleGridUserControl_CreateRequested;
            sampleGridUserControl.DeleteRequested += sampleGridUserControl_DeleteRequested;
            sampleGridUserControl.ShowPropertiesRequested += sampleGridUserControl_ShowPropertiesRequested;
            samplePropertiesUserControl.CloseRequested += samplePropertiesUserControl_CloseRequested;
            samplePropertiesUserControl.LoseFocusRequested += samplePropertiesUserControl_LoseFocusRequested;
            scaleGridUserControl.CloseRequested += scaleGridUserControl_CloseRequested;
            scaleGridUserControl.CreateRequested += scaleGridUserControl_CreateRequested;
            scaleGridUserControl.DeleteRequested += scaleGridUserControl_DeleteRequested;
            scaleGridUserControl.ShowDetailsRequested += scaleGridUserControl_ShowDetailsRequested;
            scalePropertiesUserControl.CloseRequested += scalePropertiesUserControl_CloseRequested;
            scalePropertiesUserControl.LoseFocusRequested += scalePropertiesUserControl_LoseFocusRequested;
            toneGridEditUserControl.CloseRequested += toneGridEditUserControl_CloseRequested;
            toneGridEditUserControl.CreateToneRequested += toneGridEditUserControl_CreateToneRequested;
            toneGridEditUserControl.DeleteToneRequested += toneGridEditUserControl_DeleteToneRequested;
            toneGridEditUserControl.LoseFocusRequested += toneGridEditUserControl_LoseFocusRequested;
            toneGridEditUserControl.PlayToneRequested += toneGridEditUserControl_PlayToneRequested;

            _documentCannotDeleteForm.OKClicked += _documentCannotDeleteForm_OKClicked;

            MessageBoxHelper.DocumentDeleteConfirmed += MessageBoxHelper_DocumentDeleteConfirmed;
            MessageBoxHelper.DocumentDeleteCanceled += MessageBoxHelper_DocumentDeleteCanceled;
            MessageBoxHelper.DocumentDeletedOK += MessageBoxHelper_DocumentDeletedOK;
            MessageBoxHelper.PopupMessagesOK += MessageBoxHelper_PopupMessagesOK;
        }

        // AudioFileOutput

        private void audioFileOutputGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputCreate);
        }

        private void audioFileOutputGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.AudioFileOutputDelete(e.Value));
        }

        private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputGridClose);
        }

        private void audioFileOutputGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.AudioFileOutputPropertiesShow(e.Value));
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputPropertiesClose);
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputPropertiesLoseFocus);
        }

        // CurrentPatches

        private void patchPropertiesUserControl_AddCurrentPatchRequested(object sender, Int32EventArgs e)
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

        private void currentPatchesUserControl_RemoveRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() =>
            {
                _presenter.CurrentPatchRemove(e.Value);
                RecreatePatchCalculator();
            });
        }

        private void currentPatchesUserControl_PreviewAutoPatchRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurrentPatchesPreviewAutoPatch);

            // For debugging, use this code line instead to preview the polyponic auto-patch.
            //TemplateEventHandler(_presenter.CurrentPatchesPreviewAutoPatchPolyphonic);
        }

        // Curve

        private void curveGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.CurveCreate(e.Value));
        }

        private void curveGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.CurveDelete(e.Value));
        }

        private void curveGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurveGridClose);
        }

        private void curveGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.CurveDetailsShow(e.Value));
        }

        private void curveDetailsUserControl_SelectNodeRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.NodeSelect(e.Value));
        }

        private void curveDetailsUserControl_CreateNodeRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.NodeCreate);
        }

        private void curveDetailsUserControl_DeleteNodeRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.NodeDelete);
        }

        private void curveDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurveDetailsClose);
        }

        private void curveDetailsUserControl_MoveNodeRequested(object sender, MoveEntityEventArgs e)
        {
            TemplateEventHandler(() => _presenter.NodeMove(e.EntityID, e.X, e.Y));
        }

        private void curveDetailsUserControl_ChangeNodeTypeRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.NodeChangeNodeType);
        }

        private void curveDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurveDetailsLoseFocus);
        }

        private void curveDetailsUserControl_ShowCurvePropertiesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(() => _presenter.CurvePropertiesShow(curveDetailsUserControl.ViewModel.ID));
        }

        private void curveDetailsUserControl_ShowNodePropertiesRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.NodePropertiesShow(e.Value));
        }

        private void curveDetailsUserControl_ShowSelectedNodePropertiesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SelectedNodePropertiesShow);
        }

        private void curvePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurvePropertiesLoseFocus);
        }

        private void curvePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.CurvePropertiesClose);
        }

        // Document Grid

        private void documentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentDetailsCreate);
        }

        private void documentGridUserControl_OpenRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() =>
            {
                ForceLoseFocus();
                _presenter.DocumentOpen(e.Value);
            });
        }

        private void documentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.DocumentDelete(e.Value));
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

        private void documentDetailsUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.DocumentDelete(e.Value));
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

        private void documentTreeUserControl_CollapseNodeRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.DocumentTreeCollapseNode(e.Value));
        }

        private void documentTreeUserControl_ExpandNodeRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.DocumentTreeExpandNode(e.Value));
        }

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.AudioFileOutputGridShow);
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.CurveGridShow(e.Value));
        }

        private void documentTreeUserControl_ShowDocumentPropertiesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentPropertiesShow);
        }

        private void documentTreeUserControl_ShowPatchGridRequested(object sender, StringEventArgs e)
        {
            TemplateEventHandler(() => _presenter.PatchGridShow(e.Value));
        }

        private void documentTreeUserControl_ShowPatchDetailsRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.PatchDetailsShow(e.Value));
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.SampleGridShow(e.Value));
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

        // Node

        private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.NodePropertiesLoseFocus);
        }

        private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.NodePropertiesClose);
        }

        // Operator

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus);
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose);
        }

        private void operatorPropertiesUserControl_ForAggregate_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForAggregate);
        }

        private void operatorPropertiesUserControl_ForAggregate_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForAggregate);
        }

        private void operatorPropertiesUserControl_ForBundle_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForBundle);
        }

        private void operatorPropertiesUserControl_ForBundle_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForBundle);
        }

        private void operatorPropertiesUserControl_ForCache_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForCache);
        }

        private void operatorPropertiesUserControl_ForCache_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForCache);
        }

        private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForCurve);
        }

        private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForCurve);
        }

        private void operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForCustomOperator);
        }

        private void operatorPropertiesUserControl_ForCustomOperator_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForCustomOperator);
        }

        private void operatorPropertiesUserControl_ForFilter_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForFilter);
        }

        private void operatorPropertiesUserControl_ForFilter_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForFilter);
        }

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForNumber);
        }

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForNumber);
        }

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForPatchInlet);
        }

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForPatchInlet);
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForPatchOutlet);
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForPatchOutlet);
        }

        private void operatorPropertiesUserControl_ForRandom_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForRandom);
        }

        private void operatorPropertiesUserControl_ForRandom_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForRandom);
        }

        private void operatorPropertiesUserControl_ForResample_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForResample);
        }

        private void operatorPropertiesUserControl_ForResample_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForResample);
        }

        private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForSample);
        }

        private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForSample);
        }

        private void operatorPropertiesUserControl_ForSpectrum_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForSpectrum);
        }

        private void operatorPropertiesUserControl_ForSpectrum_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForSpectrum);
        }

        private void operatorPropertiesUserControl_ForUnbundle_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesLoseFocus_ForUnbundle);
        }

        private void operatorPropertiesUserControl_ForUnbundle_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorPropertiesClose_ForUnbundle);
        }

        // Patch

        private void patchGridUserControl_CreateRequested(object sender, StringEventArgs e)
        {
            TemplateEventHandler(() => _presenter.PatchCreate(e.Value));
        }

        private void patchGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.PatchDelete(e.Value));
        }

        private void patchGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.PatchGridClose);
        }

        private void patchGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.PatchDetailsShow(e.Value));
        }

        private void patchDetailsUserControl_PlayRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(() =>
            {
                string outputFilePath = _presenter.PatchPlay();

                SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                soundPlayer.Play();
            });
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorSelect(e.Value));
        }

        private void patchDetailsUserControl_OperatorPropertiesRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorPropertiesShow(e.Value));
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorChangeInputOutlet(e.InletID, e.InputOutletID));
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveEntityEventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorMove(e.EntityID, e.X, e.Y));
        }

        private void patchDetailsUserControl_CreateOperatorRequested(object sender, CreateOperatorEventArgs e)
        {
            TemplateEventHandler(() => _presenter.OperatorCreate(e.OperatorTypeID));
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.OperatorDelete);
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.PatchDetailsLoseFocus);
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.PatchDetailsClose);
        }

        private void patchPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.PatchPropertiesLoseFocus);
        }

        private void patchPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.PatchPropertiesClose);
        }

        private void patchDetailsUserControl_SelectedOperatorPropertiesRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SelectedOperatorPropertiesShow);
        }

        // Sample

        private void sampleGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.SampleCreate(e.Value));
        }

        private void sampleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.SampleDelete(e.Value));
        }

        private void sampleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SampleGridClose);
        }

        private void sampleGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.SamplePropertiesShow(e.Value));
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SamplePropertiesLoseFocus);
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.SamplePropertiesClose);
        }

        // Scale

        private void scaleGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ScaleCreate);
        }

        private void scaleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.ScaleDelete(e.Value));
        }

        private void scaleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ScaleGridClose);
        }

        private void scaleGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.ScaleShow(e.Value));
        }

        private void toneGridEditUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ToneGridEditClose);
        }

        private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ToneGridEditLoseFocus);
        }

        private void toneGridEditUserControl_CreateToneRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.ToneCreate(e.Value));
        }

        private void toneGridEditUserControl_DeleteToneRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.ToneDelete(e.Value));
        }

        private void toneGridEditUserControl_PlayToneRequested(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() =>
            {
                string outputFilePath = _presenter.TonePlay(e.Value);

                if (!String.IsNullOrEmpty(outputFilePath))
                {
                    SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                    soundPlayer.Play();
                }
            });
        }

        private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ScalePropertiesClose);
        }

        private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.ScalePropertiesLoseFocus);
        }

        // Message Boxes

        private void MessageBoxHelper_DocumentDeleteCanceled(object sender, EventArgs e)
        {
            TemplateEventHandler(_presenter.DocumentCancelDelete);
        }

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, Int32EventArgs e)
        {
            TemplateEventHandler(() => _presenter.DocumentConfirmDelete(e.Value));
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
                ApplyViewModel();
            }
        }
    }
}
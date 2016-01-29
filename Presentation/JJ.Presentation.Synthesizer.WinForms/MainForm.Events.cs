using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.NAudio;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Configuration;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;

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
            documentGridUserControl.ShowRequested += documentGridUserControl_ShowRequested;
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
            operatorPropertiesUserControl_ForBundle.CloseRequested += operatorPropertiesUserControl_ForBundle_CloseRequested;
            operatorPropertiesUserControl_ForBundle.LoseFocusRequested += operatorPropertiesUserControl_ForBundle_LoseFocusRequested;
            operatorPropertiesUserControl_ForCurve.CloseRequested += operatorPropertiesUserControl_ForCurve_CloseRequested;
            operatorPropertiesUserControl_ForCurve.LoseFocusRequested += operatorPropertiesUserControl_ForCurve_LoseFocusRequested;
            operatorPropertiesUserControl_ForCustomOperator.CloseRequested += operatorPropertiesUserControl_ForCustomOperator_CloseRequested;
            operatorPropertiesUserControl_ForCustomOperator.LoseFocusRequested += operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested;
            operatorPropertiesUserControl_ForNumber.CloseRequested += operatorPropertiesUserControl_ForNumber_CloseRequested;
            operatorPropertiesUserControl_ForNumber.LoseFocusRequested += operatorPropertiesUserControl_ForNumber_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchInlet.CloseRequested += operatorPropertiesUserControl_ForPatchInlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchInlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested;
            operatorPropertiesUserControl_ForPatchOutlet.CloseRequested += operatorPropertiesUserControl_ForPatchOutlet_CloseRequested;
            operatorPropertiesUserControl_ForPatchOutlet.LoseFocusRequested += operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested;
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

            MessageBoxHelper.NotFoundOK += MessageBoxHelper_NotFoundOK;
            MessageBoxHelper.DocumentDeleteConfirmed += MessageBoxHelper_DocumentDeleteConfirmed;
            MessageBoxHelper.DocumentDeleteCanceled += MessageBoxHelper_DocumentDeleteCanceled;
            MessageBoxHelper.DocumentDeletedOK += MessageBoxHelper_DocumentDeletedOK;
            MessageBoxHelper.PopupMessagesOK += MessageBoxHelper_PopupMessagesOK;
        }

        // AudioFileOutput

        private void audioFileOutputGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.AudioFileOutputCreate();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void audioFileOutputGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.AudioFileOutputDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.AudioFileOutputGridClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void audioFileOutputGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.AudioFileOutputPropertiesShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.AudioFileOutputPropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.AudioFileOutputPropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // CurrentPatches

        private void patchPropertiesUserControl_AddCurrentPatchRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.CurrentPatchAdd(e.Value);
                ApplyViewModel();

                RecreatePatchCalculator();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void currentPatchesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.CurrentPatchesClose();
            ApplyViewModel();
        }

        private void currentPatchesUserControl_RemoveRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.CurrentPatchRemove(e.Value);
                ApplyViewModel();

                RecreatePatchCalculator();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void currentPatchesUserControl_PreviewAutoPatchRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurrentPatchesPreviewAutoPatch();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Curve

        private void curveGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.CurveCreate(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.CurveDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurveGridClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.CurveDetailsShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_SelectNodeRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.NodeSelect(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_CreateNodeRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.NodeCreate();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_DeleteNodeRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.NodeDelete();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurveDetailsClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_MoveNodeRequested(object sender, MoveEntityEventArgs e)
        {
            try
            {
                _presenter.NodeMove(e.EntityID, e.X, e.Y);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_ChangeNodeTypeRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.NodeChangeNodeType();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurveDetailsLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_ShowCurvePropertiesRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurvePropertiesShow(curveDetailsUserControl.ViewModel.ID);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curveDetailsUserControl_ShowNodePropertiesRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.NodePropertiesShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curvePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurvePropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void curvePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurvePropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Document Grid

        private void documentGridUserControl_ShowRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.DocumentGridShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentDetailsCreate();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentGridUserControl_OpenRequested(object sender, Int32EventArgs e)
        {
            try
            {
                ForceLoseFocus();

                _presenter.DocumentOpen(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.DocumentDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentGridClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Document Details

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentDetailsSave();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }

        }

        private void documentDetailsUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.DocumentDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentDetailsClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Document Tree

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentTreeClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_CollapseNodeRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.DocumentTreeCollapseNode(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ExpandNodeRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.DocumentTreeExpandNode(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.AudioFileOutputGridShow();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.CurveGridShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ShowDocumentPropertiesRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentPropertiesShow();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ShowPatchGridRequested(object sender, StringEventArgs e)
        {
            try
            {
                _presenter.PatchGridShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ShowPatchDetailsRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.PatchDetailsShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.SampleGridShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.ScaleGridShow();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Document Properties

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentPropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentPropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Menu

        private void menuUserControl_ShowDocumentGridRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentGridShow(pageNumber: 1);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentTreeShow();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
        {
            try
            {
                ForceLoseFocus();

                _presenter.DocumentClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void menuUserControl_DocumentSaveRequested(object sender, EventArgs e)
        {
            try
            {
                ForceLoseFocus();

                _presenter.DocumentSave();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void menuUserControl_ShowCurrentPatchesRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.CurrentPatchesShow();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Node

        private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.NodePropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.NodePropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Operator

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForBundle_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForBundle();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForBundle_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForBundle();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForCurve();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForCurve();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForCustomOperator();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForCustomOperator_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForCustomOperator();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForNumber();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForNumber();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForPatchInlet();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForPatchInlet();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForPatchOutlet();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForPatchOutlet();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForSample();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForSample();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForSpectrum_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForSpectrum();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForSpectrum_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForSpectrum();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForUnbundle_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesLoseFocus_ForUnbundle();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void operatorPropertiesUserControl_ForUnbundle_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesClose_ForUnbundle();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Patch

        private void patchGridUserControl_CreateRequested(object sender, StringEventArgs e)
        {
            try
            {
                _presenter.PatchCreate(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.PatchDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.PatchGridClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.PatchDetailsShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_PlayRequested(object sender, EventArgs e)
        {
            try
            {
                string outputFilePath = _presenter.PatchPlay();

                ApplyViewModel();

                if (_presenter.MainViewModel.Successful)
                {
                    SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                    soundPlayer.Play();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.OperatorSelect(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_OperatorPropertiesRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.OperatorPropertiesShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            try
            {
                _presenter.OperatorChangeInputOutlet(e.InletID, e.InputOutletID);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveEntityEventArgs e)
        {
            try
            {
                _presenter.OperatorMove(e.EntityID, e.X, e.Y);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_CreateOperatorRequested(object sender, CreateOperatorEventArgs e)
        {
            try
            {
                _presenter.OperatorCreate(e.OperatorTypeID);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.OperatorDelete();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.PatchDetailsLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.PatchDetailsClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.PatchPropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void patchPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.PatchPropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Sample

        private void sampleGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.SampleCreate(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void sampleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.SampleDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void sampleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.SampleGridClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void sampleGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.SamplePropertiesShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.SamplePropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.SamplePropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Scale

        private void scaleGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.ScaleCreate();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void scaleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.ScaleDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void scaleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.ScaleGridClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void scaleGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.ScaleShow(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void toneGridEditUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.ToneGridEditClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.ToneGridEditLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void toneGridEditUserControl_CreateToneRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.ToneCreate(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void toneGridEditUserControl_DeleteToneRequested(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.ToneDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void toneGridEditUserControl_PlayToneRequested(object sender, Int32EventArgs e)
        {
            try
            {
                string outputFilePath = _presenter.TonePlay(e.Value);

                if (!String.IsNullOrEmpty(outputFilePath))
                {
                    SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                    soundPlayer.Play();
                }

                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.ScalePropertiesClose();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            try
            {
                _presenter.ScalePropertiesLoseFocus();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Message Boxes

        private void MessageBoxHelper_NotFoundOK(object sender, EventArgs e)
        {
            try
            {
                _presenter.NotFoundOK();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void MessageBoxHelper_DocumentDeleteCanceled(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentCancelDelete();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, Int32EventArgs e)
        {
            try
            {
                _presenter.DocumentConfirmDelete(e.Value);
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void MessageBoxHelper_DocumentDeletedOK(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentDeletedOK();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void MessageBoxHelper_PopupMessagesOK(object sender, EventArgs e)
        {
            try
            {
                _presenter.PopupMessagesOK();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // DocumentCannotDeleteForm

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
        {
            try
            {
                _presenter.DocumentCannotDeleteOK();
                ApplyViewModel();
            }
            finally
            {
                _repositories.Rollback();
            }
        }
    }
}
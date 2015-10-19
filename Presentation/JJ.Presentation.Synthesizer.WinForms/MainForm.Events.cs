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
            childDocumentPropertiesUserControl.CloseRequested += childDocumentPropertiesUserControl_CloseRequested;
            childDocumentPropertiesUserControl.LoseFocusRequested += childDocumentPropertiesUserControl_LoseFocusRequested;
            curveGridUserControl.CloseRequested += curveGridUserControl_CloseRequested;
            curveGridUserControl.CreateRequested += curveGridUserControl_CreateRequested;
            curveGridUserControl.DeleteRequested += curveGridUserControl_DeleteRequested;
            curveGridUserControl.ShowDetailsRequested += curveGridUserControl_ShowDetailsRequested;
            curveDetailsUserControl.ChangeNodeTypeRequested += curveDetailsUserControl_ChangeNodeTypeRequested;
            curveDetailsUserControl.CloseRequested += curveDetailsUserControl_CloseRequested;
            curveDetailsUserControl.CreateNodeRequested += curveDetailsUserControl_CreateNodeRequested;
            curveDetailsUserControl.DeleteNodeRequested += curveDetailsUserControl_DeleteNodeRequested;
            curveDetailsUserControl.LoseFocusRequested += curveDetailsUserControl_LoseFocusRequested;
            curveDetailsUserControl.MoveNodeRequested += curveDetailsUserControl_MoveNodeRequested;
            curveDetailsUserControl.SelectNodeRequested += curveDetailsUserControl_SelectNodeRequested;
            curveDetailsUserControl.ShowCurvePropertiesRequested += curveDetailsUserControl_ShowCurvePropertiesRequested;
            curveDetailsUserControl.ShowNodePropertiesRequested += curveDetailsUserControl_ShowNodePropertiesRequested;
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
            documentTreeUserControl.DocumentPropertiesRequested += documentTreeUserControl_DocumentPropertiesRequested;
            documentTreeUserControl.ExpandNodeRequested += documentTreeUserControl_ExpandNodeRequested;
            documentTreeUserControl.ShowAudioFileOutputsRequested += documentTreeUserControl_ShowAudioFileOutputsRequested;
            documentTreeUserControl.ShowChildDocumentPropertiesRequested += documentTreeUserControl_ShowChildDocumentPropertiesRequested;
            documentTreeUserControl.ShowCurvesRequested += documentTreeUserControl_ShowCurvesRequested;
            documentTreeUserControl.ShowEffectsRequested += documentTreeUserControl_ShowEffectsRequested;
            documentTreeUserControl.ShowInstrumentsRequested += documentTreeUserControl_ShowInstrumentsRequested;
            documentTreeUserControl.ShowPatchesRequested += documentTreeUserControl_ShowPatchesRequested;
            documentTreeUserControl.ShowSamplesRequested += documentTreeUserControl_ShowSamplesRequested;
            documentTreeUserControl.ShowScalesRequested += documentTreeUserControl_ShowScalesRequested;
            effectGridUserControl.CloseRequested += effectGridUserControl_CloseRequested;
            effectGridUserControl.CreateRequested += effectGridUserControl_CreateRequested;
            effectGridUserControl.DeleteRequested += effectGridUserControl_DeleteRequested;
            effectGridUserControl.ShowPropertiesRequested += effectGridUserControl_ShowPropertiesRequested;
            instrumentGridUserControl.CloseRequested += instrumentGridUserControl_CloseRequested;
            instrumentGridUserControl.CreateRequested += instrumentGridUserControl_CreateRequested;
            instrumentGridUserControl.DeleteRequested += instrumentGridUserControl_DeleteRequested;
            instrumentGridUserControl.ShowPropertiesRequested += instrumentGridUserControl_ShowPropertiesRequested;
            menuUserControl.DocumentCloseRequested += menuUserControl_DocumentCloseRequested;
            menuUserControl.DocumentSaveRequested += menuUserControl_DocumentSaveRequested;
            menuUserControl.ShowDocumentGridRequested += menuUserControl_ShowDocumentGridRequested;
            menuUserControl.ShowDocumentTreeRequested += menuUserControl_ShowDocumentTreeRequested;
            nodePropertiesUserControl.CloseRequested += nodePropertiesUserControl_CloseRequested;
            nodePropertiesUserControl.LoseFocusRequested += nodePropertiesUserControl_LoseFocusRequested;
            operatorPropertiesUserControl.CloseRequested += operatorPropertiesUserControl_CloseRequested;
            operatorPropertiesUserControl.LoseFocusRequested += operatorPropertiesUserControl_LoseFocusRequested;
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
            sampleGridUserControl.CloseRequested += sampleGridUserControl_CloseRequested;
            sampleGridUserControl.CreateRequested += sampleGridUserControl_CreateRequested;
            sampleGridUserControl.DeleteRequested += sampleGridUserControl_DeleteRequested;
            sampleGridUserControl.ShowPropertiesRequested += sampleGridUserControl_ShowPropertiesRequested;
            samplePropertiesUserControl.CloseRequested += samplePropertiesUserControl_CloseRequested;
            samplePropertiesUserControl.LoseFocusRequested += samplePropertiesUserControl_LoseFocusRequested;
            toneGridEditUserControl.CloseRequested += toneGridEditUserControl_CloseRequested;
            toneGridEditUserControl.CreateToneRequested += toneGridEditUserControl_CreateToneRequested;
            toneGridEditUserControl.DeleteToneRequested += toneGridEditUserControl_DeleteToneRequested;
            toneGridEditUserControl.LoseFocusRequested += toneGridEditUserControl_LoseFocusRequested;
            toneGridEditUserControl.PlayToneRequested += toneGridEditUserControl_PlayToneRequested;
            scaleGridUserControl.CloseRequested += scaleGridUserControl_CloseRequested;
            scaleGridUserControl.CreateRequested += scaleGridUserControl_CreateRequested;
            scaleGridUserControl.DeleteRequested += scaleGridUserControl_DeleteRequested;
            scaleGridUserControl.ShowDetailsRequested += scaleGridUserControl_ShowDetailsRequested;
            scalePropertiesUserControl.CloseRequested += scalePropertiesUserControl_CloseRequested;
            scalePropertiesUserControl.LoseFocusRequested += scalePropertiesUserControl_LoseFocusRequested;

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
            AudioFileOutputCreate();
        }

        private void audioFileOutputGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputDelete(e.Value);
        }

        private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputGridClose();
        }

        private void audioFileOutputGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            AudioFileOutputPropertiesShow(e.Value);
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            AudioFileOutputPropertiesClose();
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            AudioFileOutputPropertiesLoseFocus();
        }

        // Child Document

        private void childDocumentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            ChildDocumentPropertiesLoseFocus();
        }

        private void childDocumentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            ChildDocumentPropertiesClose();
        }

        // Curve

        private void curveGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            CurveCreate(e.Value);
        }

        private void curveGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            CurveDelete(e.Value);
        }

        private void curveGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            CurveGridClose();
        }

        private void curveGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            CurveDetailsShow(e.Value);
        }

        private void curveDetailsUserControl_SelectNodeRequested(object sender, Int32EventArgs e)
        {
            NodeSelect(e.Value);
        }

        private void curveDetailsUserControl_CreateNodeRequested(object sender, EventArgs e)
        {
            NodeCreate();
        }

        private void curveDetailsUserControl_DeleteNodeRequested(object sender, EventArgs e)
        {
            NodeDelete();
        }

        private void curveDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            CurveDetailsClose();
        }

        private void curveDetailsUserControl_MoveNodeRequested(object sender, MoveEntityEventArgs e)
        {
            _presenter.NodeMove(e.EntityID, e.X, e.Y);
            ApplyViewModel();
        }

        private void curveDetailsUserControl_ChangeNodeTypeRequested(object sender, EventArgs e)
        {
            _presenter.NodeChangeNodeType();
            ApplyViewModel();
        }

        private void curveDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.CurveDetailsLoseFocus();
            ApplyViewModel();
        }

        private void curveDetailsUserControl_ShowCurvePropertiesRequested(object sender, EventArgs e)
        {
            _presenter.CurvePropertiesShow(curveDetailsUserControl.ViewModel.Entity.ID);
            ApplyViewModel();
        }

        private void curveDetailsUserControl_ShowNodePropertiesRequested(object sender, Int32EventArgs e)
        {
            _presenter.NodePropertiesShow(e.Value);
            ApplyViewModel();
        }

        private void curvePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.CurvePropertiesLoseFocus();
            ApplyViewModel();
        }

        private void curvePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.CurvePropertiesClose();
            ApplyViewModel();
        }

        // Document Grid

        private void documentGridUserControl_ShowRequested(object sender, Int32EventArgs e)
        {
            DocumentGridShow(e.Value);
        }

        private void documentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            DocumentDetailsCreate();
        }

        private void documentGridUserControl_OpenRequested(object sender, Int32EventArgs e)
        {
            DocumentOpen(e.Value);
        }

        private void documentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            DocumentDelete(e.Value);
        }

        private void documentGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentGridClose();
        }

        // Document Details

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            DocumentDetailsSave(documentDetailsUserControl.ViewModel);
        }

        private void documentDetailsUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            DocumentDelete(e.Value);
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentDetailsClose();
        }

        // Document Tree

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
        {
            AudioFileOutputGridShow();
        }

        private void documentTreeUserControl_ShowChildDocumentPropertiesRequested(object sender, Int32EventArgs e)
        {
            ChildDocumentPropertiesShow(e.Value);
        }

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentTreeClose();
        }

        private void documentTreeUserControl_CollapseNodeRequested(object sender, Int32EventArgs e)
        {
            DocumentTreeCollapseNode(e.Value);
        }

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, EventArgs e)
        {
            DocumentPropertiesShow();
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, Int32EventArgs e)
        {
            CurveGridShow(e.Value);
        }

        private void documentTreeUserControl_ShowEffectsRequested(object sender, EventArgs e)
        {
            EffectGridShow();
        }

        private void documentTreeUserControl_ShowInstrumentsRequested(object sender, EventArgs e)
        {
            InstrumentGridShow();
        }

        private void documentTreeUserControl_ExpandNodeRequested(object sender, Int32EventArgs e)
        {
            DocumentTreeExpandNode(e.Value);
        }

        private void documentTreeUserControl_ShowPatchesRequested(object sender, Int32EventArgs e)
        {
            PatchGridShow(e.Value);
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, Int32EventArgs e)
        {
            SampleGridShow(e.Value);
        }

        private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e)
        {
            ScaleGridShow();
        }

        // Document Properties

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentPropertiesClose();
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            DocumentPropertiesLoseFocus();
        }

        // Effect

        private void effectGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            EffectCreate();
        }

        private void effectGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            EffectDelete(e.Value);
        }

        private void effectGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            EffectGridClose();
        }

        private void effectGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            ChildDocumentPropertiesShow(e.Value);
        }

        // Instrument

        private void instrumentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            InstrumentCreate();
        }

        private void instrumentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            InstrumentDelete(e.Value);
        }

        private void instrumentGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            InstrumentGridClose();
        }

        private void instrumentGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            ChildDocumentPropertiesShow(e.Value);
        }

        // Menu

        private void menuUserControl_ShowDocumentGridRequested(object sender, EventArgs e)
        {
            DocumentGridShow();
        }

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            DocumentTreeShow();
        }

        private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
        {
            DocumentClose();
        }

        private void menuUserControl_DocumentSaveRequested(object sender, EventArgs e)
        {
            DocumentSave();
        }

        // Node

        private void nodePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.NodePropertiesLoseFocus();
            ApplyViewModel();
        }

        private void nodePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.NodePropertiesClose();
            ApplyViewModel();
        }

        // Operator

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus();
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose();
        }

        private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForCurve();
        }

        private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForCurve();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForCustomOperator();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForCustomOperator();
        }

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForNumber();
        }

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForNumber();
        }

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForPatchInlet();
        }

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForPatchInlet();
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForPatchOutlet();
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForPatchOutlet();
        }

        private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForSample();
        }

        private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForSample();
        }

        // Patch

        private void patchGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            PatchCreate(e.Value);
        }

        private void patchGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            PatchDelete(e.Value);
        }

        private void patchGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            PatchGridClose();
        }

        private void patchGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            PatchDetailsShow(e.Value);
        }

        private void patchDetailsUserControl_PlayRequested(object sender, EventArgs e)
        {
            PatchPlay();
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, Int32EventArgs e)
        {
            OperatorSelect(e.Value);
        }

        private void patchDetailsUserControl_OperatorPropertiesRequested(object sender, Int32EventArgs e)
        {
            OperatorPropertiesShow(e.Value);
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            OperatorChangeInputOutlet(e.InletID, e.InputOutletID);
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveEntityEventArgs e)
        {
            OperatorMove(e.EntityID, e.X, e.Y);
        }

        private void patchDetailsUserControl_CreateOperatorRequested(object sender, CreateOperatorEventArgs e)
        {
            OperatorCreate(e.OperatorTypeID);
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs e)
        {
            OperatorDelete();
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            PatchDetailsLoseFocus();
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            PatchDetailsClose();
        }

        // Sample

        private void sampleGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            SampleCreate(e.Value);
        }

        private void sampleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            SampleDelete(e.Value);
        }

        private void sampleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            SampleGridClose();
        }

        private void sampleGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            SamplePropertiesShow(e.Value);
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            SamplePropertiesLoseFocus();
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            SamplePropertiesClose();
        }

        // Scale

        private void scaleGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            ScaleCreate();
        }

        private void scaleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            ScaleDelete(e.Value);
        }

        private void scaleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            ScaleGridClose();
        }

        private void scaleGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            ScaleShow(e.Value);
        }

        private void toneGridEditUserControl_CloseRequested(object sender, EventArgs e)
        {
            ToneGridEditClose();
        }

        private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            ToneGridEditLoseFocus();
        }

        private void toneGridEditUserControl_CreateToneRequested(object sender, Int32EventArgs e)
        {
            _presenter.ToneCreate(e.Value);
            ApplyViewModel();
        }

        private void toneGridEditUserControl_DeleteToneRequested(object sender, Int32EventArgs e)
        {
            _presenter.ToneDelete(e.Value);
            ApplyViewModel();
        }

        private void toneGridEditUserControl_PlayToneRequested(object sender, Int32EventArgs e)
        {
            string outputFilePath = _presenter.TonePlay(e.Value);

            if (!String.IsNullOrEmpty(outputFilePath))
            {
                SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                soundPlayer.Play();
            }

            ApplyViewModel();
        }

        private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            ScalePropertiesClose();
        }

        private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            ScalePropertiesLoseFocus();
        }

        // Message Boxes

        private void MessageBoxHelper_NotFoundOK(object sender, EventArgs e)
        {
            NotFoundOK();
        }

        private void MessageBoxHelper_DocumentDeleteCanceled(object sender, EventArgs e)
        {
            DocumentCancelDelete();
        }

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, Int32EventArgs e)
        {
            DocumentConfirmDelete(e.Value);
        }

        private void MessageBoxHelper_DocumentDeletedOK(object sender, EventArgs e)
        {
            DocumentDeletedOK();
        }

        private void MessageBoxHelper_PopupMessagesOK(object sender, EventArgs e)
        {
            PopupMessagesOK();
        }

        // DocumentCannotDeleteForm

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
        {
            DocumentCannotDeleteOK();
        }
    }
}

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
            _presenter.AudioFileOutputCreate();
            ApplyViewModel();
        }

        private void audioFileOutputGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.AudioFileOutputDelete(e.Value);
            ApplyViewModel();
        }

        private void audioFileOutputGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.AudioFileOutputGridClose();
            ApplyViewModel();
        }

        private void audioFileOutputGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            _presenter.AudioFileOutputPropertiesShow(e.Value);
            ApplyViewModel();
        }

        private void audioFileOutputPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.AudioFileOutputPropertiesClose();
            ApplyViewModel();
        }

        private void audioFileOutputPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.AudioFileOutputPropertiesLoseFocus();
            ApplyViewModel();
        }

        // Child Document

        private void childDocumentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.ChildDocumentPropertiesLoseFocus();
            ApplyViewModel();
        }

        private void childDocumentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.ChildDocumentPropertiesClose();
            ApplyViewModel();
        }

        // Curve

        private void curveGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            _presenter.CurveCreate(e.Value);
            ApplyViewModel();
        }

        private void curveGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.CurveDelete(e.Value);
            ApplyViewModel();
        }

        private void curveGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.CurveGridClose();
            ApplyViewModel();
        }

        private void curveGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            _presenter.CurveDetailsShow(e.Value);
            ApplyViewModel();
        }

        private void curveDetailsUserControl_SelectNodeRequested(object sender, Int32EventArgs e)
        {
            _presenter.NodeSelect(e.Value);
            ApplyViewModel();
        }

        private void curveDetailsUserControl_CreateNodeRequested(object sender, EventArgs e)
        {
            _presenter.NodeCreate();
            ApplyViewModel();
        }

        private void curveDetailsUserControl_DeleteNodeRequested(object sender, EventArgs e)
        {
            _presenter.NodeDelete();
            ApplyViewModel();
        }

        private void curveDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.CurveDetailsClose();
            ApplyViewModel();
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
            _presenter.DocumentGridShow(e.Value);
            ApplyViewModel();
        }

        private void documentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            _presenter.DocumentDetailsCreate();
            ApplyViewModel();
        }

        private void documentGridUserControl_OpenRequested(object sender, Int32EventArgs e)
        {
            ForceLoseFocus();

            _presenter.DocumentOpen(e.Value);
            ApplyViewModel();
        }

        private void documentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.DocumentDelete(e.Value);
            ApplyViewModel();
        }

        private void documentGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.DocumentGridClose();
            ApplyViewModel();
        }

        // Document Details

        private void documentDetailsUserControl_SaveRequested(object sender, EventArgs e)
        {
            // TODO: This code line is probably not necessary. But check before your remove it.
            _presenter.ViewModel.DocumentDetails = documentDetailsUserControl.ViewModel;

            _presenter.DocumentDetailsSave();
            ApplyViewModel();
        }

        private void documentDetailsUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.DocumentDelete(e.Value);
            ApplyViewModel();
        }

        private void documentDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.DocumentDetailsClose();
            ApplyViewModel();
        }

        // Document Tree

        private void documentTreeUserControl_ShowAudioFileOutputsRequested(object sender, EventArgs e)
        {
            _presenter.AudioFileOutputGridShow();
            ApplyViewModel();
        }

        private void documentTreeUserControl_ShowChildDocumentPropertiesRequested(object sender, Int32EventArgs e)
        {
            _presenter.ChildDocumentPropertiesShow(e.Value);
            ApplyViewModel();
        }

        private void documentTreeUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.DocumentTreeClose();
            ApplyViewModel();
        }

        private void documentTreeUserControl_CollapseNodeRequested(object sender, Int32EventArgs e)
        {
            _presenter.DocumentTreeCollapseNode(e.Value);
            ApplyViewModel();
        }

        private void documentTreeUserControl_DocumentPropertiesRequested(object sender, EventArgs e)
        {
            _presenter.DocumentPropertiesShow();
            ApplyViewModel();
        }

        private void documentTreeUserControl_ShowCurvesRequested(object sender, Int32EventArgs e)
        {
            _presenter.CurveGridShow(e.Value);
            ApplyViewModel();
        }

        private void documentTreeUserControl_ShowEffectsRequested(object sender, EventArgs e)
        {
            _presenter.EffectGridShow();
            ApplyViewModel();
        }

        private void documentTreeUserControl_ShowInstrumentsRequested(object sender, EventArgs e)
        {
            _presenter.InstrumentGridShow();
            ApplyViewModel();
        }

        private void documentTreeUserControl_ExpandNodeRequested(object sender, Int32EventArgs e)
        {
            _presenter.DocumentTreeExpandNode(e.Value);
            ApplyViewModel();
        }

        private void documentTreeUserControl_ShowSamplesRequested(object sender, Int32EventArgs e)
        {
            _presenter.SampleGridShow(e.Value);
            ApplyViewModel();
        }

        private void documentTreeUserControl_ShowScalesRequested(object sender, EventArgs e)
        {
            _presenter.ScaleGridShow();
            ApplyViewModel();
        }

        // Document Properties

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.DocumentPropertiesClose();
            ApplyViewModel();
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.DocumentPropertiesLoseFocus();
            ApplyViewModel();
        }

        // Effect

        private void effectGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            _presenter.EffectCreate();
            ApplyViewModel();
        }

        private void effectGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.EffectDelete(e.Value);
            ApplyViewModel();
        }

        private void effectGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.EffectGridClose();
            ApplyViewModel();
        }

        private void effectGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            _presenter.ChildDocumentPropertiesShow(e.Value);
            ApplyViewModel();
        }

        // Instrument

        private void instrumentGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            _presenter.InstrumentCreate();
            ApplyViewModel();
        }

        private void instrumentGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.InstrumentDelete(e.Value);
            ApplyViewModel();
        }

        private void instrumentGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.InstrumentGridClose();
            ApplyViewModel();
        }

        private void instrumentGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            _presenter.ChildDocumentPropertiesShow(e.Value);
            ApplyViewModel();
        }

        // Menu

        private void menuUserControl_ShowDocumentGridRequested(object sender, EventArgs e)
        {
            _presenter.DocumentGridShow(pageNumber: 1);
            ApplyViewModel();
        }

        private void menuUserControl_ShowDocumentTreeRequested(object sender, EventArgs e)
        {
            _presenter.DocumentTreeShow();
            ApplyViewModel();
        }

        private void menuUserControl_DocumentCloseRequested(object sender, EventArgs e)
        {
            ForceLoseFocus();

            _presenter.DocumentClose();
            ApplyViewModel();
        }

        private void menuUserControl_DocumentSaveRequested(object sender, EventArgs e)
        {
            ForceLoseFocus();

            _presenter.DocumentSave();
            ApplyViewModel();
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
            _presenter.OperatorPropertiesLoseFocus();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForBundle_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForBundle();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForBundle_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForBundle();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForCurve_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForCurve();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForCurve_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForCurve();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForCustomOperator();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForCustomOperator();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForNumber();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForNumber();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForPatchInlet_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForPatchInlet();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForPatchInlet_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForPatchInlet();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForPatchOutlet();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForPatchOutlet_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForPatchOutlet();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForSample_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForSample();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForSample_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForSample();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForUnbundle_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesLoseFocus_ForUnbundle();
            ApplyViewModel();
        }

        private void operatorPropertiesUserControl_ForUnbundle_CloseRequested(object sender, EventArgs e)
        {
            _presenter.OperatorPropertiesClose_ForUnbundle();
            ApplyViewModel();
        }

        // Patch

        private void patchDetailsUserControl_PlayRequested(object sender, EventArgs e)
        {
            string outputFilePath = _presenter.PatchPlay();

            ApplyViewModel();

            if (_presenter.ViewModel.Successful)
            {
                SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                soundPlayer.Play();
            }
        }

        private void patchDetailsUserControl_SelectOperatorRequested(object sender, Int32EventArgs e)
        {
            _presenter.OperatorSelect(e.Value);
            ApplyViewModel();
        }

        private void patchDetailsUserControl_OperatorPropertiesRequested(object sender, Int32EventArgs e)
        {
            _presenter.OperatorPropertiesShow(e.Value);
            ApplyViewModel();
        }

        private void patchDetailsUserControl_ChangeInputOutletRequested(object sender, ChangeInputOutletEventArgs e)
        {
            _presenter.OperatorChangeInputOutlet(e.InletID, e.InputOutletID);
            ApplyViewModel();
        }

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveEntityEventArgs e)
        {
            _presenter.OperatorMove(e.EntityID, e.X, e.Y);
            ApplyViewModel();
        }

        private void patchDetailsUserControl_CreateOperatorRequested(object sender, CreateOperatorEventArgs e)
        {
            _presenter.OperatorCreate(e.OperatorTypeID);
            ApplyViewModel();
        }

        private void patchDetailsUserControl_DeleteOperatorRequested(object sender, EventArgs e)
        {
            _presenter.OperatorDelete();
            ApplyViewModel();
        }

        private void patchDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.PatchDetailsLoseFocus();
            ApplyViewModel();
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.PatchDetailsClose();
            ApplyViewModel();
        }

        // Sample

        private void sampleGridUserControl_CreateRequested(object sender, Int32EventArgs e)
        {
            _presenter.SampleCreate(e.Value);
            ApplyViewModel();
        }

        private void sampleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.SampleDelete(e.Value);
            ApplyViewModel();
        }

        private void sampleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.SampleGridClose();
            ApplyViewModel();
        }

        private void sampleGridUserControl_ShowPropertiesRequested(object sender, Int32EventArgs e)
        {
            _presenter.SamplePropertiesShow(e.Value);
            ApplyViewModel();
        }

        private void samplePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.SamplePropertiesLoseFocus();
            ApplyViewModel();
        }

        private void samplePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.SamplePropertiesClose();
            ApplyViewModel();
        }

        // Scale

        private void scaleGridUserControl_CreateRequested(object sender, EventArgs e)
        {
            _presenter.ScaleCreate();
            ApplyViewModel();
        }

        private void scaleGridUserControl_DeleteRequested(object sender, Int32EventArgs e)
        {
            _presenter.ScaleDelete(e.Value);
            ApplyViewModel();
        }

        private void scaleGridUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.ScaleGridClose();
            ApplyViewModel();
        }

        private void scaleGridUserControl_ShowDetailsRequested(object sender, Int32EventArgs e)
        {
            _presenter.ScaleShow(e.Value);
            ApplyViewModel();
        }

        private void toneGridEditUserControl_CloseRequested(object sender, EventArgs e)
        {
            _presenter.ToneGridEditClose();
            ApplyViewModel();
        }

        private void toneGridEditUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.ToneGridEditLoseFocus();
            ApplyViewModel();
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
            _presenter.ScalePropertiesClose();
            ApplyViewModel();
        }

        private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            _presenter.ScalePropertiesLoseFocus();
            ApplyViewModel();
        }

        // Message Boxes

        private void MessageBoxHelper_NotFoundOK(object sender, EventArgs e)
        {
            _presenter.NotFoundOK();
            ApplyViewModel();
        }

        private void MessageBoxHelper_DocumentDeleteCanceled(object sender, EventArgs e)
        {
            _presenter.DocumentCancelDelete();
            ApplyViewModel();
        }

        private void MessageBoxHelper_DocumentDeleteConfirmed(object sender, Int32EventArgs e)
        {
            _presenter.DocumentConfirmDelete(e.Value);
            ApplyViewModel();
        }

        private void MessageBoxHelper_DocumentDeletedOK(object sender, EventArgs e)
        {
            _presenter.DocumentDeletedOK();
            ApplyViewModel();
        }

        private void MessageBoxHelper_PopupMessagesOK(object sender, EventArgs e)
        {
            _presenter.PopupMessagesOK();
            ApplyViewModel();
        }

        // DocumentCannotDeleteForm

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
        {
            _presenter.DocumentCannotDeleteOK();
            ApplyViewModel();
        }
    }
}

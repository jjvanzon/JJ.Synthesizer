using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm
    {
        // AudioFileOutput Events

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

        // Child Document Events

        private void childDocumentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            ChildDocumentPropertiesLoseFocus();
        }

        private void childDocumentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            ChildDocumentPropertiesClose();
        }

        // Curve Events

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

        // Document Grid Events

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

        // Document Details Events

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

        // Document Tree Events

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

        // Document Properties Events

        private void documentPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            DocumentPropertiesClose();
        }

        private void documentPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            DocumentPropertiesLoseFocus();
        }

        // Effect Events

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

        // Instrument Events

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

        // Menu Events

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

        // Operator Events

        private void operatorPropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus();
        }

        private void operatorPropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForCustomOperator();
        }

        private void operatorPropertiesUserControl_ForCustomOperator_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForCustomOperator();
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

        private void operatorPropertiesUserControl_ForNumber_LoseFocusRequested(object sender, EventArgs e)
        {
            OperatorPropertiesLoseFocus_ForNumber();
        }

        private void operatorPropertiesUserControl_ForNumber_CloseRequested(object sender, EventArgs e)
        {
            OperatorPropertiesClose_ForNumber();
        }

        // Patch Events

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

        private void patchDetailsUserControl_MoveOperatorRequested(object sender, MoveOperatorEventArgs e)
        {
            OperatorMove(e.OperatorID, e.CenterX, e.CenterY);
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

        // Sample Events

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

        // Scale Events

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

        private void scaleDetailsUserControl_CloseRequested(object sender, EventArgs e)
        {
            ScaleDetailsClose();
        }

        private void scaleDetailsUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            ScaleDetailsLoseFocus();
        }

        private void scalePropertiesUserControl_CloseRequested(object sender, EventArgs e)
        {
            ScalePropertiesClose();
        }

        private void scalePropertiesUserControl_LoseFocusRequested(object sender, EventArgs e)
        {
            ScalePropertiesLoseFocus();
        }

        // Message Box Events

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

        // DocumentCannotDeleteForm Events

        private void _documentCannotDeleteForm_OKClicked(object sender, EventArgs e)
        {
            DocumentCannotDeleteOK();
        }
    }
}

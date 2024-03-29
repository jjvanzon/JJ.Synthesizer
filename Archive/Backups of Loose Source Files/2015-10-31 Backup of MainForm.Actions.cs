﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm
    {
        // General

        private void Open()
        {
            _presenter.Show();
            ApplyViewModel();
        }

        private void NotFoundOK()
        {
            _presenter.NotFoundOK();
            ApplyViewModel();
        }

        // AudioFileOutput

        private void AudioFileOutputGridShow()
        {
            _presenter.AudioFileOutputGridShow();
            ApplyViewModel();
        }

        private void AudioFileOutputGridClose()
        {
            _presenter.AudioFileOutputGridClose();
            ApplyViewModel();
        }

        private void AudioFileOutputDelete(int id)
        {
            _presenter.AudioFileOutputDelete(id);
            ApplyViewModel();
        }

        private void AudioFileOutputCreate()
        {
            _presenter.AudioFileOutputCreate();
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesShow(int id)
        {
            _presenter.AudioFileOutputPropertiesShow(id);
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesClose()
        {
            _presenter.AudioFileOutputPropertiesClose();
            ApplyViewModel();
        }

        private void AudioFileOutputPropertiesLoseFocus()
        {
            _presenter.AudioFileOutputPropertiesLoseFocus();
            ApplyViewModel();
        }

        // ChildDocument

        private void ChildDocumentPropertiesShow(int childDocumentID)
        {
            _presenter.ChildDocumentPropertiesShow(childDocumentID);
            ApplyViewModel();
        }

        private void ChildDocumentPropertiesClose()
        {
            _presenter.ChildDocumentPropertiesClose();
            ApplyViewModel();
        }

        private void ChildDocumentPropertiesLoseFocus()
        {
            _presenter.ChildDocumentPropertiesLoseFocus();
            ApplyViewModel();
        }

        // Curve

        private void CurveGridShow(int documentID)
        {
            _presenter.CurveGridShow(documentID);
            ApplyViewModel();
        }

        private void CurveGridClose()
        {
            _presenter.CurveGridClose();
            ApplyViewModel();
        }

        private void CurveCreate(int documentID)
        {
            _presenter.CurveCreate(documentID);
            ApplyViewModel();
        }

        private void CurveDelete(int curveID)
        {
            _presenter.CurveDelete(curveID);
            ApplyViewModel();
        }

        private void CurveDetailsShow(int curveID)
        {
            _presenter.CurveDetailsShow(curveID);
            ApplyViewModel();
        }

        private void CurveDetailsClose()
        {
            _presenter.CurveDetailsClose();
            ApplyViewModel();
        }

        // Document Grid

        private void DocumentGridShow(int pageNumber = 1)
        {
            _presenter.DocumentGridShow(pageNumber);
            ApplyViewModel();
        }

        private void DocumentGridClose()
        {
            _presenter.DocumentGridClose();
            ApplyViewModel();
        }

        // Document Details

        private void DocumentDetailsCreate()
        {
            _presenter.DocumentDetailsCreate();
            ApplyViewModel();
        }

        private void DocumentDetailsSave(DocumentDetailsViewModel viewModel)
        {
            _presenter.ViewModel.DocumentDetails = viewModel;

            _presenter.DocumentDetailsSave();
            ApplyViewModel();
        }

        private void DocumentDetailsClose()
        {
            _presenter.DocumentDetailsClose();
            ApplyViewModel();
        }

        private void DocumentCannotDeleteOK()
        {
            _presenter.DocumentCannotDeleteOK();
            ApplyViewModel();
        }

        // Document

        private void DocumentOpen(int id)
        {
            ForceLoseFocus();

            _presenter.DocumentOpen(id);
            ApplyViewModel();
        }

        private void DocumentClose()
        {
            ForceLoseFocus();

            _presenter.DocumentClose();
            ApplyViewModel();
        }

        private void DocumentDelete(int id)
        {
            _presenter.DocumentDelete(id);
            ApplyViewModel();
        }

        private void DocumentConfirmDelete(int id)
        {
            _presenter.DocumentConfirmDelete(id);
            ApplyViewModel();
        }

        private void DocumentDeletedOK()
        {
            _presenter.DocumentDeletedOK();
            ApplyViewModel();
        }

        private void PopupMessagesOK()
        {
            _presenter.PopupMessagesOK();
            ApplyViewModel();
        }

        private void DocumentCancelDelete()
        {
            _presenter.DocumentCancelDelete();
            ApplyViewModel();
        }

        private void DocumentSave()
        {
            ForceLoseFocus();

            _presenter.DocumentSave();
            ApplyViewModel();
        }

        // Document Tree

        private void DocumentTreeShow()
        {
            _presenter.DocumentTreeShow();
            ApplyViewModel();
        }

        private void DocumentTreeClose()
        {
            _presenter.DocumentTreeClose();
            ApplyViewModel();
        }

        private void DocumentTreeExpandNode(int nodeIndex)
        {
            _presenter.DocumentTreeExpandNode(nodeIndex);
            ApplyViewModel();
        }

        private void DocumentTreeCollapseNode(int nodeIndex)
        {
            _presenter.DocumentTreeCollapseNode(nodeIndex);
            ApplyViewModel();
        }

        // Document Properties

        private void DocumentPropertiesShow()
        {
            _presenter.DocumentPropertiesShow();
            ApplyViewModel();
        }

        private void DocumentPropertiesClose()
        {
            _presenter.DocumentPropertiesClose();
            ApplyViewModel();
        }

        private void DocumentPropertiesLoseFocus()
        {
            _presenter.DocumentPropertiesLoseFocus();
            ApplyViewModel();
        }

        // Effect

        private void EffectGridShow()
        {
            _presenter.EffectGridShow();
            ApplyViewModel();
        }

        private void EffectGridClose()
        {
            _presenter.EffectGridClose();
            ApplyViewModel();
        }

        private void EffectCreate()
        {
            _presenter.EffectCreate();
            ApplyViewModel();
        }

        private void EffectDelete(int listIndex)
        {
            _presenter.EffectDelete(listIndex);
            ApplyViewModel();
        }

        // Instrument

        private void InstrumentGridShow()
        {
            _presenter.InstrumentGridShow();
            ApplyViewModel();
        }

        private void InstrumentGridClose()
        {
            _presenter.InstrumentGridClose();
            ApplyViewModel();
        }

        private void InstrumentCreate()
        {
            _presenter.InstrumentCreate();
            ApplyViewModel();
        }

        private void InstrumentDelete(int id)
        {
            _presenter.InstrumentDelete(id);
            ApplyViewModel();
        }

        // Node

        private void NodeSelect(int nodeID)
        {
            _presenter.NodeSelect(nodeID);
            ApplyViewModel();
        }

        private void NodeCreate()
        {
            _presenter.NodeCreate();
            ApplyViewModel();
        }

        private void NodeDelete()
        {
            _presenter.NodeDelete();
            ApplyViewModel();
        }

        // Operator

        private void OperatorPropertiesShow(int operatorID)
        {
            _presenter.OperatorPropertiesShow(operatorID);
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForCurve()
        {
            _presenter.OperatorPropertiesClose_ForCurve();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForCustomOperator()
        {
            _presenter.OperatorPropertiesClose_ForCustomOperator();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForPatchInlet()
        {
            _presenter.OperatorPropertiesClose_ForPatchInlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForPatchOutlet()
        {
            _presenter.OperatorPropertiesClose_ForPatchOutlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesClose_ForNumber()
        {
            _presenter.OperatorPropertiesClose_ForNumber();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForCurve()
        {
            _presenter.OperatorPropertiesLoseFocus_ForCurve();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForCustomOperator()
        {
            _presenter.OperatorPropertiesLoseFocus_ForCustomOperator();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForPatchInlet()
        {
            _presenter.OperatorPropertiesLoseFocus_ForPatchInlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForPatchOutlet()
        {
            _presenter.OperatorPropertiesLoseFocus_ForPatchOutlet();
            ApplyViewModel();
        }

        private void OperatorPropertiesLoseFocus_ForNumber()
        {
            _presenter.OperatorPropertiesLoseFocus_ForNumber();
            ApplyViewModel();
        }

        private void OperatorCreate(int operatorTypeID)
        {
            _presenter.OperatorCreate(operatorTypeID);
            ApplyViewModel();
        }

        private void OperatorDelete()
        {
            _presenter.OperatorDelete();
            ApplyViewModel();
        }

        private void OperatorMove(int operatorID, float centerX, float centerY)
        {
            _presenter.OperatorMove(operatorID, centerX, centerY);
            ApplyViewModel();
        }

        private void OperatorChangeInputOutlet(int inletID, int inputOutletID)
        {
            _presenter.OperatorChangeInputOutlet(inletID, inputOutletID);
            ApplyViewModel();
        }

        private void OperatorSelect(int operatorID)
        {
            _presenter.OperatorSelect(operatorID);
            ApplyViewModel();
        }

        // Patch

        private void PatchGridShow(int documentID)
        {
            _presenter.PatchGridShow(documentID);
            ApplyViewModel();
        }

        private void PatchGridClose()
        {
            _presenter.PatchGridClose();
            ApplyViewModel();
        }

        private void PatchCreate(int documentID)
        {
            _presenter.PatchCreate(documentID);
            ApplyViewModel();
        }

        private void PatchDelete(int patchID)
        {
            _presenter.PatchDelete(patchID);
            ApplyViewModel();
        }

        private void PatchDetailsShow(int patchID)
        {
            _presenter.PatchDetailsShow(patchID);
            ApplyViewModel();
        }

        private void PatchDetailsClose()
        {
            _presenter.PatchDetailsClose();
            ApplyViewModel();
        }

        private void PatchDetailsLoseFocus()
        {
            _presenter.PatchDetailsLoseFocus();
            ApplyViewModel();
        }

        private void PatchPlay()
        {
            string outputFilePath = _presenter.PatchPlay();

            ApplyViewModel();

            if (_presenter.ViewModel.Successful)
            {
                SoundPlayer soundPlayer = new SoundPlayer(outputFilePath);
                soundPlayer.Play();
            }
        }

        // Sample

        private void SampleGridShow(int documentID)
        {
            _presenter.SampleGridShow(documentID);
            ApplyViewModel();
        }

        private void SampleGridClose()
        {
            _presenter.SampleGridClose();
            ApplyViewModel();
        }

        private void SampleCreate(int documentID)
        {
            _presenter.SampleCreate(documentID);
            ApplyViewModel();
        }

        private void SampleDelete(int sampleID)
        {
            _presenter.SampleDelete(sampleID);
            ApplyViewModel();
        }

        private void SamplePropertiesShow(int sampleID)
        {
            _presenter.SamplePropertiesShow(sampleID);
            ApplyViewModel();
        }

        private void SamplePropertiesClose()
        {
            _presenter.SamplePropertiesClose();
            ApplyViewModel();
        }

        private void SamplePropertiesLoseFocus()
        {
            _presenter.SamplePropertiesLoseFocus();
            ApplyViewModel();
        }

        // Scale

        private void ScaleGridShow()
        {
            _presenter.ScaleGridShow();
            ApplyViewModel();
        }

        private void ScaleGridClose()
        {
            _presenter.ScaleGridClose();
            ApplyViewModel();
        }

        private void ScaleDelete(int id)
        {
            _presenter.ScaleDelete(id);
            ApplyViewModel();
        }

        private void ScaleCreate()
        {
            _presenter.ScaleCreate();
            ApplyViewModel();
        }

        private void ScaleShow(int id)
        {
            _presenter.ScaleShow(id);
            ApplyViewModel();
        }

        private void ToneGridEditClose()
        {
            _presenter.ToneGridEditClose();
            ApplyViewModel();
        }

        private void ToneGridEditLoseFocus()
        {
            _presenter.ToneGridEditLoseFocus();
            ApplyViewModel();
        }

        private void ScalePropertiesClose()
        {
            _presenter.ScalePropertiesClose();
            ApplyViewModel();
        }

        private void ScalePropertiesLoseFocus()
        {
            _presenter.ScalePropertiesLoseFocus();
            ApplyViewModel();
        }
    }
}

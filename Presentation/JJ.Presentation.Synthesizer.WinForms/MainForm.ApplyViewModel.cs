using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm
    {
        private void ApplyViewModel()
        {
            SuspendLayout();

            try
            {
                Text = _presenter.MainViewModel.WindowTitle + _titleBarExtraText;

                menuUserControl.Show(_presenter.MainViewModel.Menu);

                // NOTE: Actually making controls visible is postponed till last, to do it in a way that does not flash as much.

                audioFileOutputGridUserControl.ViewModel = _presenter.MainViewModel.Document.AudioFileOutputGrid;
                audioFileOutputPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.AudioFileOutputPropertiesList
                                                                                              .Where(x => x.Visible)
                                                                                              .FirstOrDefault();

                // AutoPatch
                _autoPatchDetailsForm.ViewModel = _presenter.MainViewModel.Document.AutoPatchDetails;
                _autoPatchDetailsForm.Visible = _presenter.MainViewModel.Document.AutoPatchDetails.Visible;

                // CurrentPatches
                currentPatchesUserControl.ViewModel = _presenter.MainViewModel.Document.CurrentPatches;
                currentPatchesUserControl.Visible = currentPatchesUserControl.ViewModel.Visible;

                // CurveDetails
                curveDetailsUserControl.ViewModel =
                    Enumerable.Union(
                        _presenter.MainViewModel.Document.CurveDetailsList,
                        _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.CurveDetailsList))
                   .Where(x => x.Visible)
                   .FirstOrDefault();

                // CurveGrid
                if (_presenter.MainViewModel.Document.CurveGrid.Visible)
                {
                    curveGridUserControl.ViewModel = _presenter.MainViewModel.Document.CurveGrid;
                }
                else
                {
                    curveGridUserControl.ViewModel = _presenter.MainViewModel.Document.PatchDocumentList
                                                                                  .Select(x => x.CurveGrid)
                                                                                  .Where(x => x.Visible)
                                                                                  .FirstOrDefault();
                }

                // CurveProperties
                curvePropertiesUserControl.ViewModel =
                    Enumerable.Union(
                        _presenter.MainViewModel.Document.CurvePropertiesList,
                        _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.CurvePropertiesList))
                   .Where(x => x.Visible)
                   .FirstOrDefault();

                // Document
                documentDetailsUserControl.ViewModel = _presenter.MainViewModel.DocumentDetails;
                documentGridUserControl.ViewModel = _presenter.MainViewModel.DocumentGrid;
                documentPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.DocumentProperties;
                documentTreeUserControl.ViewModel = _presenter.MainViewModel.Document.DocumentTree;

                // NodeProperties
                nodePropertiesUserControl.ViewModel =
                    Enumerable.Union(
                        _presenter.MainViewModel.Document.NodePropertiesList,
                        _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.NodePropertiesList))
                   .Where(x => x.Visible)
                   .FirstOrDefault();

                // OperatorProperties
                operatorPropertiesUserControl.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForBundle
                operatorPropertiesUserControl_ForBundle.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForBundles)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForCurve
                // (Needs slightly different code, because the CurveLookup is different for root documents and child documents.
                operatorPropertiesUserControl_ForCurve.ViewModel = null;
                OperatorPropertiesViewModel_ForCurve visibleOperatorPropertiesViewModel_ForCurve = null;
                foreach (PatchDocumentViewModel patchDocumentViewModel in _presenter.MainViewModel.Document.PatchDocumentList)
                {
                    visibleOperatorPropertiesViewModel_ForCurve = patchDocumentViewModel.OperatorPropertiesList_ForCurves.Where(x => x.Visible).FirstOrDefault();
                    if (visibleOperatorPropertiesViewModel_ForCurve != null)
                    {
                        operatorPropertiesUserControl_ForCurve.SetCurveLookup(patchDocumentViewModel.CurveLookup);
                        operatorPropertiesUserControl_ForCurve.ViewModel = visibleOperatorPropertiesViewModel_ForCurve;
                        break;
                    }
                }

                // OperatorProperties_ForCustomOperator
                operatorPropertiesUserControl_ForCustomOperator.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators)
                    .Where(x => x.Visible)
                    .FirstOrDefault();
                operatorPropertiesUserControl_ForCustomOperator.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);

                // OperatorProperties_ForNumber
                operatorPropertiesUserControl_ForNumber.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForNumbers)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForPatchInlet
                operatorPropertiesUserControl_ForPatchInlet.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchInlets)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForPatchOutlet
                operatorPropertiesUserControl_ForPatchOutlet.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchOutlets)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForSample
                // (Needs slightly different code, because the SampleLookup is different for root documents and child documents.
                operatorPropertiesUserControl_ForSample.ViewModel = null;
                OperatorPropertiesViewModel_ForSample visibleOperatorPropertiesViewModel_ForSample = null;
                foreach (PatchDocumentViewModel patchDocumentViewModel in _presenter.MainViewModel.Document.PatchDocumentList)
                {
                    visibleOperatorPropertiesViewModel_ForSample = patchDocumentViewModel.OperatorPropertiesList_ForSamples.Where(x => x.Visible).FirstOrDefault();
                    if (visibleOperatorPropertiesViewModel_ForSample != null)
                    {
                        operatorPropertiesUserControl_ForSample.SetSampleLookup(patchDocumentViewModel.SampleLookup);
                        operatorPropertiesUserControl_ForSample.ViewModel = visibleOperatorPropertiesViewModel_ForSample;
                        break;
                    }
                }

                // OperatorProperties_ForSpectrum
                operatorPropertiesUserControl_ForSpectrum.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForSpectrums)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForUnbundle
                operatorPropertiesUserControl_ForUnbundle.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForUnbundles)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // PatchDetails
                patchDetailsUserControl.ViewModel =
                    _presenter.MainViewModel.Document.PatchDocumentList.Select(x => x.PatchDetails)
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // PatchGrid
                patchGridUserControl.ViewModel = _presenter.MainViewModel.Document.PatchGridList
                                                                              .Where(x => x.Visible)
                                                                              .FirstOrDefault();

                // PatchProperties
                patchPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.PatchDocumentList
                                                                                    .Select(x => x.PatchProperties)
                                                                                    .Where(x => x.Visible)
                                                                                    .FirstOrDefault();
                // SampleGrid
                if (_presenter.MainViewModel.Document.SampleGrid.Visible)
                {
                    sampleGridUserControl.ViewModel = _presenter.MainViewModel.Document.SampleGrid;
                }
                else
                {
                    sampleGridUserControl.ViewModel = _presenter.MainViewModel.Document.PatchDocumentList
                                                                                   .Select(x => x.SampleGrid)
                                                                                   .Where(x => x.Visible)
                                                                                   .FirstOrDefault();
                }

                // SampleProperties
                samplePropertiesUserControl.ViewModel =
                    Enumerable.Union(
                        _presenter.MainViewModel.Document.SamplePropertiesList,
                        _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.SamplePropertiesList))
                   .Where(x => x.Visible)
                   .FirstOrDefault();

                // Scale
                scaleGridUserControl.ViewModel = _presenter.MainViewModel.Document.ScaleGrid;
                toneGridEditUserControl.ViewModel = _presenter.MainViewModel.Document.ToneGridEditList
                                                                                 .Where(x => x.Visible)
                                                                                 .FirstOrDefault();
                scalePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.ScalePropertiesList
                                                                                    .Where(x => x.Visible)
                                                                                    .FirstOrDefault();
                // Set Visible Properties
                bool audioFileOutputGridVisible = audioFileOutputGridUserControl.ViewModel != null &&
                                                  audioFileOutputGridUserControl.ViewModel.Visible;
                bool audioFileOutputPropertiesVisible = audioFileOutputPropertiesUserControl.ViewModel != null &&
                                                        audioFileOutputPropertiesUserControl.ViewModel.Visible;
                bool curveDetailsVisible = curveDetailsUserControl.ViewModel != null &&
                                           curveDetailsUserControl.ViewModel.Visible;
                bool curveGridVisible = curveGridUserControl.ViewModel != null &&
                                        curveGridUserControl.ViewModel.Visible;
                bool curvePropertiesVisible = curvePropertiesUserControl.ViewModel != null &&
                                              curvePropertiesUserControl.ViewModel.Visible;
                bool documentDetailsVisible = documentDetailsUserControl.ViewModel != null &&
                                              documentDetailsUserControl.ViewModel.Visible;
                bool documentGridVisible = documentGridUserControl.ViewModel != null &&
                                           documentGridUserControl.ViewModel.Visible;
                bool documentPropertiesVisible = documentPropertiesUserControl.ViewModel != null &&
                                                 documentPropertiesUserControl.ViewModel.Visible;
                bool documentTreeVisible = documentTreeUserControl.ViewModel != null &&
                                           documentTreeUserControl.ViewModel.Visible;
                bool nodePropertiesVisible = nodePropertiesUserControl.ViewModel != null &&
                                             nodePropertiesUserControl.ViewModel.Visible;
                bool operatorPropertiesVisible = operatorPropertiesUserControl.ViewModel != null &&
                                                 operatorPropertiesUserControl.ViewModel.Visible;
                bool operatorPropertiesVisible_ForBundle = operatorPropertiesUserControl_ForBundle.ViewModel != null &&
                                                           operatorPropertiesUserControl_ForBundle.ViewModel.Visible;
                bool operatorPropertiesVisible_ForCurve = operatorPropertiesUserControl_ForCurve.ViewModel != null &&
                                                          operatorPropertiesUserControl_ForCurve.ViewModel.Visible;
                bool operatorPropertiesVisible_ForCustomOperator = operatorPropertiesUserControl_ForCustomOperator.ViewModel != null &&
                                                                   operatorPropertiesUserControl_ForCustomOperator.ViewModel.Visible;
                bool operatorPropertiesVisible_ForNumber = operatorPropertiesUserControl_ForNumber.ViewModel != null &&
                                                           operatorPropertiesUserControl_ForNumber.ViewModel.Visible;
                bool operatorPropertiesVisible_ForPatchInlet = operatorPropertiesUserControl_ForPatchInlet.ViewModel != null &&
                                                               operatorPropertiesUserControl_ForPatchInlet.ViewModel.Visible;
                bool operatorPropertiesVisible_ForPatchOutlet = operatorPropertiesUserControl_ForPatchOutlet.ViewModel != null &&
                                                                operatorPropertiesUserControl_ForPatchOutlet.ViewModel.Visible;
                bool operatorPropertiesVisible_ForSample = operatorPropertiesUserControl_ForSample.ViewModel != null &&
                                                           operatorPropertiesUserControl_ForSample.ViewModel.Visible;
                bool operatorPropertiesVisible_ForSpectrum = operatorPropertiesUserControl_ForSpectrum.ViewModel != null &&
                                                             operatorPropertiesUserControl_ForSpectrum.ViewModel.Visible;
                bool operatorPropertiesVisible_ForUnbundle = operatorPropertiesUserControl_ForUnbundle.ViewModel != null &&
                                                             operatorPropertiesUserControl_ForUnbundle.ViewModel.Visible;
                bool patchGridVisible = patchGridUserControl.ViewModel != null &&
                                        patchGridUserControl.ViewModel.Visible;
                bool patchDetailsVisible = patchDetailsUserControl.ViewModel != null &&
                                           patchDetailsUserControl.ViewModel.Visible;
                bool patchPropertiesVisible = patchPropertiesUserControl.ViewModel != null &&
                                              patchPropertiesUserControl.ViewModel.Visible;
                bool sampleGridVisible = sampleGridUserControl.ViewModel != null &&
                                         sampleGridUserControl.ViewModel.Visible;
                bool samplePropertiesVisible = samplePropertiesUserControl.ViewModel != null &&
                                               samplePropertiesUserControl.ViewModel.Visible;
                bool scaleGridVisible = scaleGridUserControl.ViewModel != null &&
                                        scaleGridUserControl.ViewModel.Visible;
                bool toneGridEditVisible = toneGridEditUserControl.ViewModel != null &&
                                           toneGridEditUserControl.ViewModel.Visible;
                bool scalePropertiesVisible = scalePropertiesUserControl.ViewModel != null &&
                                              scalePropertiesUserControl.ViewModel.Visible;

                // Applying Visible = true first and then Visible = false prevents flickering.
                if (audioFileOutputGridVisible) audioFileOutputGridUserControl.Visible = true;
                if (audioFileOutputPropertiesVisible) audioFileOutputPropertiesUserControl.Visible = true;
                if (curveDetailsVisible) curveDetailsUserControl.Visible = true;
                if (curveGridVisible) curveGridUserControl.Visible = true;
                if (curvePropertiesVisible) curvePropertiesUserControl.Visible = true;
                if (documentDetailsVisible) documentDetailsUserControl.Visible = true;
                if (documentGridVisible) documentGridUserControl.Visible = true;
                if (documentPropertiesVisible) documentPropertiesUserControl.Visible = true;
                if (documentTreeVisible) documentTreeUserControl.Visible = true;
                if (nodePropertiesVisible) nodePropertiesUserControl.Visible = true;
                if (operatorPropertiesVisible) operatorPropertiesUserControl.Visible = true;
                if (operatorPropertiesVisible_ForBundle) operatorPropertiesUserControl_ForBundle.Visible = true;
                if (operatorPropertiesVisible_ForCurve) operatorPropertiesUserControl_ForCurve.Visible = true;
                if (operatorPropertiesVisible_ForCustomOperator) operatorPropertiesUserControl_ForCustomOperator.Visible = true;
                if (operatorPropertiesVisible_ForNumber) operatorPropertiesUserControl_ForNumber.Visible = true;
                if (operatorPropertiesVisible_ForPatchInlet) operatorPropertiesUserControl_ForPatchInlet.Visible = true;
                if (operatorPropertiesVisible_ForPatchOutlet) operatorPropertiesUserControl_ForPatchOutlet.Visible = true;
                if (operatorPropertiesVisible_ForSample) operatorPropertiesUserControl_ForSample.Visible = true;
                if (operatorPropertiesVisible_ForSpectrum) operatorPropertiesUserControl_ForSpectrum.Visible = true;
                if (operatorPropertiesVisible_ForUnbundle) operatorPropertiesUserControl_ForUnbundle.Visible = true;
                if (patchGridVisible) patchGridUserControl.Visible = true;
                if (patchDetailsVisible) patchDetailsUserControl.Visible = true;
                if (patchPropertiesVisible) patchPropertiesUserControl.Visible = true;
                if (sampleGridVisible) sampleGridUserControl.Visible = true;
                if (samplePropertiesVisible) samplePropertiesUserControl.Visible = true;
                if (scaleGridVisible) scaleGridUserControl.Visible = true;
                if (toneGridEditVisible) toneGridEditUserControl.Visible = true;
                if (scalePropertiesVisible) scalePropertiesUserControl.Visible = true;

                if (!audioFileOutputGridVisible) audioFileOutputGridUserControl.Visible = false;
                if (!audioFileOutputPropertiesVisible) audioFileOutputPropertiesUserControl.Visible = false;
                if (!curveDetailsVisible) curveDetailsUserControl.Visible = false;
                if (!curveGridVisible) curveGridUserControl.Visible = false;
                if (!curvePropertiesVisible) curvePropertiesUserControl.Visible = false;
                if (!documentDetailsVisible) documentDetailsUserControl.Visible = false;
                if (!documentGridVisible) documentGridUserControl.Visible = false;
                if (!documentPropertiesVisible) documentPropertiesUserControl.Visible = false;
                if (!documentTreeVisible) documentTreeUserControl.Visible = false;
                if (!nodePropertiesVisible) nodePropertiesUserControl.Visible = false;
                if (!operatorPropertiesVisible) operatorPropertiesUserControl.Visible = false;
                if (!operatorPropertiesVisible_ForBundle) operatorPropertiesUserControl_ForBundle.Visible = false;
                if (!operatorPropertiesVisible_ForCurve) operatorPropertiesUserControl_ForCurve.Visible = false;
                if (!operatorPropertiesVisible_ForCustomOperator) operatorPropertiesUserControl_ForCustomOperator.Visible = false;
                if (!operatorPropertiesVisible_ForNumber) operatorPropertiesUserControl_ForNumber.Visible = false;
                if (!operatorPropertiesVisible_ForPatchInlet) operatorPropertiesUserControl_ForPatchInlet.Visible = false;
                if (!operatorPropertiesVisible_ForPatchOutlet) operatorPropertiesUserControl_ForPatchOutlet.Visible = false;
                if (!operatorPropertiesVisible_ForSample) operatorPropertiesUserControl_ForSample.Visible = false;
                if (!operatorPropertiesVisible_ForSpectrum) operatorPropertiesUserControl_ForSpectrum.Visible = false;
                if (!operatorPropertiesVisible_ForUnbundle) operatorPropertiesUserControl_ForUnbundle.Visible = false;
                if (!patchDetailsVisible) patchDetailsUserControl.Visible = false;
                if (!patchGridVisible) patchGridUserControl.Visible = false;
                if (!patchPropertiesVisible) patchPropertiesUserControl.Visible = false;
                if (!sampleGridVisible) sampleGridUserControl.Visible = false;
                if (!samplePropertiesVisible) samplePropertiesUserControl.Visible = false;
                if (!scaleGridVisible) scaleGridUserControl.Visible = false;
                if (!toneGridEditVisible) toneGridEditUserControl.Visible = false;
                if (!scalePropertiesVisible) scalePropertiesUserControl.Visible = false;

                // Panel Visibility
                bool treePanelMustBeVisible = documentTreeVisible;
                SetTreePanelVisible(treePanelMustBeVisible);

                bool propertiesPanelMustBeVisible = documentPropertiesVisible ||
                                                    audioFileOutputPropertiesVisible ||
                                                    curvePropertiesVisible ||
                                                    nodePropertiesVisible ||
                                                    operatorPropertiesVisible ||
                                                    operatorPropertiesVisible_ForBundle ||
                                                    operatorPropertiesVisible_ForCurve ||
                                                    operatorPropertiesVisible_ForCustomOperator ||
                                                    operatorPropertiesVisible_ForNumber ||
                                                    operatorPropertiesVisible_ForPatchInlet ||
                                                    operatorPropertiesVisible_ForPatchOutlet ||
                                                    operatorPropertiesVisible_ForSample ||
                                                    operatorPropertiesVisible_ForSpectrum ||
                                                    operatorPropertiesVisible_ForUnbundle ||
                                                    samplePropertiesVisible ||
                                                    patchPropertiesVisible ||
                                                    scalePropertiesVisible;

                SetPropertiesPanelVisible(propertiesPanelMustBeVisible);
            }
            finally
            {
                ResumeLayout();
            }

            if (_presenter.MainViewModel.NotFound.Visible)
            {
                MessageBoxHelper.ShowNotFound(_presenter.MainViewModel.NotFound);
            }

            if (_presenter.MainViewModel.DocumentDelete.Visible)
            {
                MessageBoxHelper.ShowDocumentConfirmDelete(_presenter.MainViewModel.DocumentDelete);
            }

            if (_presenter.MainViewModel.DocumentDeleted.Visible)
            {
                MessageBoxHelper.ShowDocumentIsDeleted();
            }

            if (_presenter.MainViewModel.DocumentCannotDelete.Visible)
            {
                _documentCannotDeleteForm.ShowDialog(_presenter.MainViewModel.DocumentCannotDelete);
            }

            if (_presenter.MainViewModel.ValidationMessages.Count != 0)
            {
                // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                MessageBox.Show(String.Join(Environment.NewLine, _presenter.MainViewModel.ValidationMessages.Select(x => x.Text)));

                // Clear them so the next time the message box is not shown (message box is a temporary solution).
                _presenter.MainViewModel.ValidationMessages.Clear();
            }

            if (_presenter.MainViewModel.PopupMessages.Count != 0)
            {
                MessageBoxHelper.ShowPopupMessages(_presenter.MainViewModel.PopupMessages);
            }

            // Focus control if not valid.
            bool mustFocusAudioFileOutputPropertiesUserControl = audioFileOutputPropertiesUserControl.Visible &&
                                                                !audioFileOutputPropertiesUserControl.ViewModel.Successful;
            if (mustFocusAudioFileOutputPropertiesUserControl)
            {
                audioFileOutputPropertiesUserControl.Focus();
            }

            bool mustFocusCurveDetailsUserControl = curveDetailsUserControl.Visible &&
                                                   !curveDetailsUserControl.ViewModel.Successful;
            if (mustFocusCurveDetailsUserControl)
            {
                curveDetailsUserControl.Focus();
            }

            bool mustFocusCurvePropertiesUserControl = curvePropertiesUserControl.Visible &&
                                                      !curvePropertiesUserControl.ViewModel.Successful;
            if (mustFocusCurvePropertiesUserControl)
            {
                curvePropertiesUserControl.Focus();
            }

            bool mustFocusDocumentPropertiesUserControl = documentPropertiesUserControl.Visible &&
                                                         !documentPropertiesUserControl.ViewModel.Successful;
            if (mustFocusDocumentPropertiesUserControl)
            {
                documentPropertiesUserControl.Focus();
            }

            bool mustFocusNodePropertiesUserControl = nodePropertiesUserControl.Visible &&
                                                     !nodePropertiesUserControl.ViewModel.Successful;
            if (mustFocusNodePropertiesUserControl)
            {
                nodePropertiesUserControl.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl = operatorPropertiesUserControl.Visible &&
                                                         !operatorPropertiesUserControl.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl)
            {
                operatorPropertiesUserControl.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForBundle = operatorPropertiesUserControl_ForBundle.Visible &&
                                                                   !operatorPropertiesUserControl_ForBundle.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForBundle)
            {
                operatorPropertiesUserControl_ForBundle.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForCurve = operatorPropertiesUserControl_ForCurve.Visible &&
                                                                  !operatorPropertiesUserControl_ForCurve.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForCurve)
            {
                operatorPropertiesUserControl_ForCurve.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForCustomOperator = operatorPropertiesUserControl_ForCustomOperator.Visible &&
                                                                           !operatorPropertiesUserControl_ForCustomOperator.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForCustomOperator)
            {
                operatorPropertiesUserControl_ForCustomOperator.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForNumber = operatorPropertiesUserControl_ForNumber.Visible &&
                                                                   !operatorPropertiesUserControl_ForNumber.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForNumber)
            {
                operatorPropertiesUserControl_ForNumber.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForPatchInlet = operatorPropertiesUserControl_ForPatchInlet.Visible &&
                                                                       !operatorPropertiesUserControl_ForPatchInlet.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForPatchInlet)
            {
                operatorPropertiesUserControl_ForPatchInlet.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForPatchOutlet = operatorPropertiesUserControl_ForPatchOutlet.Visible &&
                                                                        !operatorPropertiesUserControl_ForPatchOutlet.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForPatchOutlet)
            {
                operatorPropertiesUserControl_ForPatchOutlet.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForSample = operatorPropertiesUserControl_ForSample.Visible &&
                                                                   !operatorPropertiesUserControl_ForSample.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForSample)
            {
                operatorPropertiesUserControl_ForSample.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForSpectrum = operatorPropertiesUserControl_ForSpectrum.Visible &&
                                                                   !operatorPropertiesUserControl_ForSpectrum.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForSpectrum)
            {
                operatorPropertiesUserControl_ForSpectrum.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForUnbundle = operatorPropertiesUserControl_ForUnbundle.Visible &&
                                                                     !operatorPropertiesUserControl_ForUnbundle.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForUnbundle)
            {
                operatorPropertiesUserControl_ForUnbundle.Focus();
            }

            bool mustFocusPatchDetailsUserControl = patchDetailsUserControl.Visible &&
                                                   !patchDetailsUserControl.ViewModel.Successful;
            if (mustFocusPatchDetailsUserControl)
            {
                patchDetailsUserControl.Focus();
            }

            bool mustFocusPatchPropertiesUserControl = patchPropertiesUserControl.Visible &&
                                                      !patchPropertiesUserControl.ViewModel.Successful;
            if (mustFocusPatchPropertiesUserControl)
            {
                patchPropertiesUserControl.Focus();
            }

            bool mustFocusSamplePropertiesUserControl = samplePropertiesUserControl.Visible &&
                                                       !samplePropertiesUserControl.ViewModel.Successful;
            if (mustFocusSamplePropertiesUserControl)
            {
                samplePropertiesUserControl.Focus();
            }

            bool mustFocusToneGridEditUserControl = toneGridEditUserControl.Visible &&
                                                   !toneGridEditUserControl.ViewModel.Successful;
            if (mustFocusToneGridEditUserControl)
            {
                toneGridEditUserControl.Focus();
            }

            bool mustFocusScalePropertiesUserControl = scalePropertiesUserControl.Visible &&
                                                      !scalePropertiesUserControl.ViewModel.Successful;
            if (mustFocusScalePropertiesUserControl)
            {
                scalePropertiesUserControl.Focus();
            }
        }
    }
}

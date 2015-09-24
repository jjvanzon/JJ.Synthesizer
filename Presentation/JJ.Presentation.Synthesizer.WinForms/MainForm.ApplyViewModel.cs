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
                Text = _presenter.ViewModel.WindowTitle + _titleBarExtraText;

                menuUserControl.Show(_presenter.ViewModel.Menu);

                // NOTE: Actually making controls visible is postponed till last, to do it in a way that does not flash as much.

                audioFileOutputGridUserControl.ViewModel = _presenter.ViewModel.Document.AudioFileOutputGrid;
                audioFileOutputPropertiesUserControl.ViewModel = _presenter.ViewModel.Document.AudioFileOutputPropertiesList
                                                                                              .Where(x => x.Visible)
                                                                                              .FirstOrDefault();

                childDocumentPropertiesUserControl.ViewModel = _presenter.ViewModel.Document.ChildDocumentPropertiesList
                                                                                            .Where(x => x.Visible)
                                                                                            .FirstOrDefault();
                // CurveGrid
                if (_presenter.ViewModel.Document.CurveGrid.Visible)
                {
                    curveGridUserControl.ViewModel = _presenter.ViewModel.Document.CurveGrid;
                }
                else
                {
                    curveGridUserControl.ViewModel = _presenter.ViewModel.Document.ChildDocumentList
                                                                                  .Select(x => x.CurveGrid)
                                                                                  .Where(x => x.Visible)
                                                                                  .FirstOrDefault();
                }

                documentDetailsUserControl.ViewModel = _presenter.ViewModel.DocumentDetails;
                documentGridUserControl.ViewModel = _presenter.ViewModel.DocumentGrid;
                documentPropertiesUserControl.ViewModel = _presenter.ViewModel.Document.DocumentProperties;
                documentTreeUserControl.ViewModel = _presenter.ViewModel.Document.DocumentTree;
                instrumentGridUserControl.ViewModel = _presenter.ViewModel.Document.InstrumentGrid;
                effectGridUserControl.ViewModel = _presenter.ViewModel.Document.EffectGrid;

                // OperatorProperties
                operatorPropertiesUserControl.ViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList))
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForCustomOperator
                operatorPropertiesUserControl_ForCustomOperator.ViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForCustomOperators,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators))
                    .Where(x => x.Visible)
                    .FirstOrDefault();
                operatorPropertiesUserControl_ForCustomOperator.SetUnderlyingDocumentLookup(_presenter.ViewModel.Document.UnderlyingDocumentLookup);

                // OperatorProperties_ForPatchInlet
                operatorPropertiesUserControl_ForPatchInlet.ViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForPatchInlets,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchInlets))
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForPatchOutlet
                operatorPropertiesUserControl_ForPatchOutlet.ViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForPatchOutlets,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchOutlets))
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // OperatorProperties_ForSample
                // (Needs slightly different code, because the SampleLookup is different for root documents and child documents.
                operatorPropertiesUserControl_ForSample.ViewModel = null;
                OperatorPropertiesViewModel_ForSample visibleOperatorPropertiesViewModel_ForSample =
                    _presenter.ViewModel.Document.OperatorPropertiesList_ForSamples.Where(x => x.Visible).FirstOrDefault();
                if (visibleOperatorPropertiesViewModel_ForSample != null)
                {
                    operatorPropertiesUserControl_ForSample.SetSampleLookup(_presenter.ViewModel.Document.SampleLookup);
                    operatorPropertiesUserControl_ForSample.ViewModel = visibleOperatorPropertiesViewModel_ForSample;
                }
                else
                {
                    foreach (ChildDocumentViewModel childDocumentViewModel in _presenter.ViewModel.Document.ChildDocumentList)
                    {
                        visibleOperatorPropertiesViewModel_ForSample =
                            childDocumentViewModel.OperatorPropertiesList_ForSamples.Where(x => x.Visible).FirstOrDefault();
                        if (visibleOperatorPropertiesViewModel_ForSample != null)
                        {
                            operatorPropertiesUserControl_ForSample.SetSampleLookup(childDocumentViewModel.SampleLookup);
                            operatorPropertiesUserControl_ForSample.ViewModel = visibleOperatorPropertiesViewModel_ForSample;
                            break;
                        }
                    }
                }

                // OperatorProperties_ForNumber
                operatorPropertiesUserControl_ForNumber.ViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.OperatorPropertiesList_ForNumbers,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForNumbers))
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // PatchGrid
                if (_presenter.ViewModel.Document.PatchGrid.Visible)
                {
                    patchGridUserControl.ViewModel = _presenter.ViewModel.Document.PatchGrid;
                }
                else
                {
                    patchGridUserControl.ViewModel = _presenter.ViewModel.Document.ChildDocumentList
                                                                                  .Select(x => x.PatchGrid)
                                                                                  .Where(x => x.Visible)
                                                                                  .FirstOrDefault();
                }

                // PatchDetails
                patchDetailsUserControl.ViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.PatchDetailsList,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.PatchDetailsList))
                    .Where(x => x.Visible)
                    .FirstOrDefault();

                // SampleGrid
                if (_presenter.ViewModel.Document.SampleGrid.Visible)
                {
                    sampleGridUserControl.ViewModel = _presenter.ViewModel.Document.SampleGrid;
                }
                else
                {
                    sampleGridUserControl.ViewModel = _presenter.ViewModel.Document.ChildDocumentList
                                                                                   .Select(x => x.SampleGrid)
                                                                                   .Where(x => x.Visible)
                                                                                   .FirstOrDefault();
                }

                // SampleProperties
                samplePropertiesUserControl.ViewModel =
                    Enumerable.Union(
                        _presenter.ViewModel.Document.SamplePropertiesList,
                        _presenter.ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList))
                   .Where(x => x.Visible)
                   .FirstOrDefault();

                // Scale
                scaleGridUserControl.ViewModel = _presenter.ViewModel.Document.ScaleGrid;
                scaleDetailsUserControl.ViewModel = _presenter.ViewModel.Document.ScaleDetailsList
                                                                                 .Where(x => x.Visible)
                                                                                 .FirstOrDefault();
                // Set Visible Properties
                bool audioFileOutputGridVisible = audioFileOutputGridUserControl.ViewModel != null &&
                                                  audioFileOutputGridUserControl.ViewModel.Visible;
                bool audioFileOutputPropertiesVisible = audioFileOutputPropertiesUserControl.ViewModel != null &&
                                                        audioFileOutputPropertiesUserControl.ViewModel.Visible;
                bool childDocumentPropertiesVisible = childDocumentPropertiesUserControl.ViewModel != null &&
                                                      childDocumentPropertiesUserControl.ViewModel.Visible;
                bool curveGridVisible = curveGridUserControl.ViewModel != null &&
                                        curveGridUserControl.ViewModel.Visible;
                bool documentDetailsVisible = documentDetailsUserControl.ViewModel != null &&
                                              documentDetailsUserControl.ViewModel.Visible;
                bool documentGridVisible = documentGridUserControl.ViewModel != null &&
                                           documentGridUserControl.ViewModel.Visible;
                bool documentPropertiesVisible = documentPropertiesUserControl.ViewModel != null &&
                                                 documentPropertiesUserControl.ViewModel.Visible;
                bool documentTreeVisible = documentTreeUserControl.ViewModel != null &&
                                           documentTreeUserControl.ViewModel.Visible;
                bool instrumentGridVisible = instrumentGridUserControl.ViewModel != null &&
                                             instrumentGridUserControl.ViewModel.Visible;
                bool effectGridVisible = effectGridUserControl.ViewModel != null &&
                                         effectGridUserControl.ViewModel.Visible;
                bool operatorPropertiesVisible = operatorPropertiesUserControl.ViewModel != null &&
                                                 operatorPropertiesUserControl.ViewModel.Visible;
                bool operatorPropertiesVisible_ForCustomOperator = operatorPropertiesUserControl_ForCustomOperator.ViewModel != null &&
                                                                   operatorPropertiesUserControl_ForCustomOperator.ViewModel.Visible;
                bool operatorPropertiesVisible_ForPatchInlet = operatorPropertiesUserControl_ForPatchInlet.ViewModel != null &&
                                                               operatorPropertiesUserControl_ForPatchInlet.ViewModel.Visible;
                bool operatorPropertiesVisible_ForPatchOutlet = operatorPropertiesUserControl_ForPatchOutlet.ViewModel != null &&
                                                                operatorPropertiesUserControl_ForPatchOutlet.ViewModel.Visible;
                bool operatorPropertiesVisible_ForSample = operatorPropertiesUserControl_ForSample.ViewModel != null &&
                                                           operatorPropertiesUserControl_ForSample.ViewModel.Visible;
                bool operatorPropertiesVisible_ForNumber = operatorPropertiesUserControl_ForNumber.ViewModel != null &&
                                                           operatorPropertiesUserControl_ForNumber.ViewModel.Visible;
                bool patchGridVisible = patchGridUserControl.ViewModel != null &&
                                        patchGridUserControl.ViewModel.Visible;
                bool patchDetailsVisible = patchDetailsUserControl.ViewModel != null &&
                                           patchDetailsUserControl.ViewModel.Visible;
                bool sampleGridVisible = sampleGridUserControl.ViewModel != null &&
                                         sampleGridUserControl.ViewModel.Visible;
                bool samplePropertiesVisible = samplePropertiesUserControl.ViewModel != null &&
                                               samplePropertiesUserControl.ViewModel.Visible;
                bool scaleGridVisible = scaleGridUserControl.ViewModel != null &&
                                        scaleGridUserControl.ViewModel.Visible;
                bool scaleDetailsVisible = scaleDetailsUserControl.ViewModel != null &&
                                           scaleDetailsUserControl.ViewModel.Visible;

                // Applying Visible = true first and then Visible = false prevents flickering.
                if (audioFileOutputGridVisible) audioFileOutputGridUserControl.Visible = true;
                if (audioFileOutputPropertiesVisible) audioFileOutputPropertiesUserControl.Visible = true;
                if (childDocumentPropertiesVisible) childDocumentPropertiesUserControl.Visible = true;
                if (curveGridVisible) curveGridUserControl.Visible = true;
                if (documentDetailsVisible) documentDetailsUserControl.Visible = true;
                if (documentGridVisible) documentGridUserControl.Visible = true;
                if (documentPropertiesVisible) documentPropertiesUserControl.Visible = true;
                if (documentTreeVisible) documentTreeUserControl.Visible = true;
                if (instrumentGridVisible) instrumentGridUserControl.Visible = true;
                if (effectGridVisible) effectGridUserControl.Visible = true;
                if (operatorPropertiesVisible) operatorPropertiesUserControl.Visible = true;
                if (operatorPropertiesVisible_ForCustomOperator) operatorPropertiesUserControl_ForCustomOperator.Visible = true;
                if (operatorPropertiesVisible_ForPatchInlet) operatorPropertiesUserControl_ForPatchInlet.Visible = true;
                if (operatorPropertiesVisible_ForPatchOutlet) operatorPropertiesUserControl_ForPatchOutlet.Visible = true;
                if (operatorPropertiesVisible_ForSample) operatorPropertiesUserControl_ForSample.Visible = true;
                if (operatorPropertiesVisible_ForNumber) operatorPropertiesUserControl_ForNumber.Visible = true;
                if (patchGridVisible) patchGridUserControl.Visible = true;
                if (patchDetailsVisible) patchDetailsUserControl.Visible = true;
                if (sampleGridVisible) sampleGridUserControl.Visible = true;
                if (samplePropertiesVisible) samplePropertiesUserControl.Visible = true;
                if (scaleGridVisible) scaleGridUserControl.Visible = true;
                if (scaleDetailsVisible) scaleDetailsUserControl.Visible = true;

                if (!audioFileOutputGridVisible) audioFileOutputGridUserControl.Visible = false;
                if (!audioFileOutputPropertiesVisible) audioFileOutputPropertiesUserControl.Visible = false;
                if (!childDocumentPropertiesVisible) childDocumentPropertiesUserControl.Visible = false;
                if (!curveGridVisible) curveGridUserControl.Visible = false;
                if (!documentDetailsVisible) documentDetailsUserControl.Visible = false;
                if (!documentGridVisible) documentGridUserControl.Visible = false;
                if (!documentPropertiesVisible) documentPropertiesUserControl.Visible = false;
                if (!documentTreeVisible) documentTreeUserControl.Visible = false;
                if (!instrumentGridVisible) instrumentGridUserControl.Visible = false;
                if (!effectGridVisible) effectGridUserControl.Visible = false;
                if (!operatorPropertiesVisible) operatorPropertiesUserControl.Visible = false;
                if (!operatorPropertiesVisible_ForCustomOperator) operatorPropertiesUserControl_ForCustomOperator.Visible = false;
                if (!operatorPropertiesVisible_ForPatchInlet) operatorPropertiesUserControl_ForPatchInlet.Visible = false;
                if (!operatorPropertiesVisible_ForPatchOutlet) operatorPropertiesUserControl_ForPatchOutlet.Visible = false;
                if (!operatorPropertiesVisible_ForSample) operatorPropertiesUserControl_ForSample.Visible = false;
                if (!operatorPropertiesVisible_ForNumber) operatorPropertiesUserControl_ForNumber.Visible = false;
                if (!patchGridVisible) patchGridUserControl.Visible = false;
                if (!patchDetailsVisible) patchDetailsUserControl.Visible = false;
                if (!sampleGridVisible) sampleGridUserControl.Visible = false;
                if (!samplePropertiesVisible) samplePropertiesUserControl.Visible = false;
                if (!scaleGridVisible) scaleGridUserControl.Visible = false;
                if (!scaleDetailsVisible) scaleDetailsUserControl.Visible = false;

                // Panel Visibility
                bool treePanelMustBeVisible = documentTreeVisible;
                SetTreePanelVisible(treePanelMustBeVisible);

                bool propertiesPanelMustBeVisible = documentPropertiesVisible ||
                                                    audioFileOutputPropertiesVisible ||
                                                    childDocumentPropertiesVisible ||
                                                    operatorPropertiesVisible ||
                                                    operatorPropertiesVisible_ForCustomOperator ||
                                                    operatorPropertiesVisible_ForPatchInlet ||
                                                    operatorPropertiesVisible_ForPatchOutlet ||
                                                    operatorPropertiesVisible_ForSample ||
                                                    operatorPropertiesVisible_ForNumber ||
                                                    samplePropertiesVisible;

                SetPropertiesPanelVisible(propertiesPanelMustBeVisible);
            }
            finally
            {
                ResumeLayout();
            }

            if (_presenter.ViewModel.NotFound.Visible)
            {
                MessageBoxHelper.ShowNotFound(_presenter.ViewModel.NotFound);
            }

            if (_presenter.ViewModel.DocumentDelete.Visible)
            {
                MessageBoxHelper.ShowDocumentConfirmDelete(_presenter.ViewModel.DocumentDelete);
            }

            if (_presenter.ViewModel.DocumentDeleted.Visible)
            {
                MessageBoxHelper.ShowDocumentIsDeleted();
            }

            if (_presenter.ViewModel.DocumentCannotDelete.Visible)
            {
                _documentCannotDeleteForm.ShowDialog(_presenter.ViewModel.DocumentCannotDelete);
            }

            if (_presenter.ViewModel.ValidationMessages.Count != 0)
            {
                // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                MessageBox.Show(String.Join(Environment.NewLine, _presenter.ViewModel.ValidationMessages.Select(x => x.Text)));

                // Clear them so the next time the message box is not shown (message box is a temporary solution).
                _presenter.ViewModel.ValidationMessages.Clear();
            }

            if (_presenter.ViewModel.PopupMessages.Count != 0)
            {
                MessageBoxHelper.ShowPopupMessages(_presenter.ViewModel.PopupMessages);
            }

            // Focus control if not valid.
            bool mustFocusAudioFileOutputPropertiesUserControl = audioFileOutputPropertiesUserControl.Visible &&
                                                                !audioFileOutputPropertiesUserControl.ViewModel.Successful;
            if (mustFocusAudioFileOutputPropertiesUserControl)
            {
                audioFileOutputPropertiesUserControl.Focus();
            }

            bool mustFocusChildDocumentPropertiesUserControl = childDocumentPropertiesUserControl.Visible &&
                                                              !childDocumentPropertiesUserControl.ViewModel.Successful;
            if (mustFocusChildDocumentPropertiesUserControl)
            {
                childDocumentPropertiesUserControl.Focus();
            }

            bool mustFocusDocumentPropertiesUserControl = documentPropertiesUserControl.Visible &&
                                                         !documentPropertiesUserControl.ViewModel.Successful;
            if (mustFocusDocumentPropertiesUserControl)
            {
                documentPropertiesUserControl.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl = operatorPropertiesUserControl.Visible &&
                                                         !operatorPropertiesUserControl.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl)
            {
                operatorPropertiesUserControl.Focus();
            }

            bool mustFocusOperatorPropertiesUserControl_ForCustomOperator = operatorPropertiesUserControl_ForCustomOperator.Visible &&
                                                                           !operatorPropertiesUserControl_ForCustomOperator.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForCustomOperator)
            {
                operatorPropertiesUserControl_ForCustomOperator.Focus();
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

            bool mustFocusOperatorPropertiesUserControl_ForNumber = operatorPropertiesUserControl_ForNumber.Visible &&
                                                                   !operatorPropertiesUserControl_ForNumber.ViewModel.Successful;
            if (mustFocusOperatorPropertiesUserControl_ForNumber)
            {
                operatorPropertiesUserControl_ForNumber.Focus();
            }

            bool mustFocusSamplePropertiesUserControl = samplePropertiesUserControl.Visible &&
                                                       !samplePropertiesUserControl.ViewModel.Successful;
            if (mustFocusSamplePropertiesUserControl)
            {
                samplePropertiesUserControl.Focus();
            }

            // TODO: Uncomment code when the control is finished.
            //bool mustFocusScaleDetailsUserControl = scaleDetailsUserControl.Visible &&
            //                                        !scaleDetailsUserControl.ViewModel.Successful;
            //if (mustFocusScaleDetailsUserControl)
            //{
            //    scaleDetailsUserControl.Focus();
            //}
        }
    }
}

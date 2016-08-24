using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using Message = JJ.Data.Canonical.Message;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm
    {
        private void ApplyViewModel()
        {
            // NOTE: Actually making controls visible is postponed till last, to do it in a way that does not flash as much.

            Text = _presenter.MainViewModel.TitleBar;

            menuUserControl.Show(_presenter.MainViewModel.Menu);

            // AudioFileOutput
            audioFileOutputGridUserControl.ViewModel = _presenter.MainViewModel.Document.AudioFileOutputGrid;
            audioFileOutputPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.AudioFileOutputPropertiesDictionary.Values
                                                                                              .Where(x => x.Visible)
                                                                                              .FirstOrDefault();

            // AudioOutput
            audioOutputPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.AudioOutputProperties;

            // AutoPatch
            _autoPatchDetailsForm.ViewModel = _presenter.MainViewModel.Document.AutoPatchDetails;
            _autoPatchDetailsForm.Visible = _presenter.MainViewModel.Document.AutoPatchDetails.Visible;

            // CurrentPatches
            currentPatchesUserControl.ViewModel = _presenter.MainViewModel.Document.CurrentPatches;
            currentPatchesUserControl.Visible = currentPatchesUserControl.ViewModel.Visible;

            // CurveDetails
            curveDetailsUserControl.ViewModel =
                Enumerable.Union(
                    _presenter.MainViewModel.Document.CurveDetailsDictionary.Values,
                    _presenter.MainViewModel.Document.PatchDocumentDictionary.Values.SelectMany(x => x.CurveDetailsDictionary.Values))
               .Where(x => x.Visible)
               .FirstOrDefault();

            // CurveGrid
            if (_presenter.MainViewModel.Document.CurveGrid.Visible)
            {
                curveGridUserControl.ViewModel = _presenter.MainViewModel.Document.CurveGrid;
            }
            else
            {
                curveGridUserControl.ViewModel = _presenter.MainViewModel.Document.PatchDocumentDictionary.Values
                                                                                  .Select(x => x.CurveGrid)
                                                                                  .Where(x => x.Visible)
                                                                                  .FirstOrDefault();
            }

            // CurveProperties
            curvePropertiesUserControl.ViewModel =
                Enumerable.Union(
                    _presenter.MainViewModel.Document.CurvePropertiesDictionary.Values,
                    _presenter.MainViewModel.Document.PatchDocumentDictionary.Values.SelectMany(x => x.CurvePropertiesDictionary.Values))
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
                    _presenter.MainViewModel.Document.NodePropertiesDictionary.Values,
                    _presenter.MainViewModel.Document.PatchDocumentDictionary.Values.SelectMany(x => x.NodePropertiesDictionary.Values))
               .Where(x => x.Visible)
               .FirstOrDefault();

            // OperatorProperties
            operatorPropertiesUserControl.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForBundle
            operatorPropertiesUserControl_ForBundle.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForBundles.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForCache
            operatorPropertiesUserControl_ForCache.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForCaches.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForCurve
            // (Needs slightly different code, because the CurveLookup is different for root documents and child documents.
            OperatorPropertiesViewModel_ForCurve visibleOperatorPropertiesViewModel_ForCurve = 
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForCurves.Values
                .Where(x => x.Visible)
                .FirstOrDefault();
            if (visibleOperatorPropertiesViewModel_ForCurve != null)
            {
                operatorPropertiesUserControl_ForCurve.ViewModel = visibleOperatorPropertiesViewModel_ForCurve;

                // Set Curve Lookup
                int childDocumentID = visibleOperatorPropertiesViewModel_ForCurve.ChildDocumentID;
                PatchDocumentViewModel patchDocumentViewModel = _presenter.MainViewModel.Document.PatchDocumentDictionary[childDocumentID];
                operatorPropertiesUserControl_ForCurve.SetCurveLookup(patchDocumentViewModel.CurveLookup);
            }

            // OperatorProperties_ForCustomOperator
            operatorPropertiesUserControl_ForCustomOperator.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators.Values
                .Where(x => x.Visible)
                .FirstOrDefault();
            operatorPropertiesUserControl_ForCustomOperator.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);

            // OperatorProperties_ForMakeContinuous
            operatorPropertiesUserControl_ForMakeContinuous.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForMakeContinuous.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForNumber
            operatorPropertiesUserControl_ForNumber.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForPatchInlet
            operatorPropertiesUserControl_ForPatchInlet.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForPatchOutlet
            operatorPropertiesUserControl_ForPatchOutlet.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForSample
            // (Needs slightly different code, because the SampleLookup is different for root documents and child documents.
            operatorPropertiesUserControl_ForSample.ViewModel = null;
            OperatorPropertiesViewModel_ForSample visibleOperatorPropertiesViewModel_ForSample =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_ForSamples.Values
                .Where(x => x.Visible)
                .FirstOrDefault();
            if (visibleOperatorPropertiesViewModel_ForSample != null)
            {
                operatorPropertiesUserControl_ForSample.ViewModel = visibleOperatorPropertiesViewModel_ForSample;

                int childDocumentID = visibleOperatorPropertiesViewModel_ForSample.ChildDocumentID;
                PatchDocumentViewModel patchDocumentViewModel = _presenter.MainViewModel.Document.PatchDocumentDictionary[childDocumentID];
                operatorPropertiesUserControl_ForSample.SetSampleLookup(patchDocumentViewModel.SampleLookup);
            }

            // OperatorProperties_WithDimension
            operatorPropertiesUserControl_WithDimension.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_WithDimension.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_WithDimensionAndInterpolation
            operatorPropertiesUserControl_WithDimensionAndInterpolation.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndInterpolation.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_WithDimensionAndCollectionRecalculation
            operatorPropertiesUserControl_WithDimensionAndCollectionRecalculation.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_WithDimensionAndOutletCount
            operatorPropertiesUserControl_WithDimensionAndOutletCount.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_WithDimensionAndOutletCount.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_WithInletCount
            operatorPropertiesUserControl_WithInletCount.ViewModel =
                _presenter.MainViewModel.Document.OperatorPropertiesDictionary_WithInletCount.Values
                .Where(x => x.Visible)
                .FirstOrDefault();

            // PatchDetails
            patchDetailsUserControl.ViewModel =
                _presenter.MainViewModel.Document.PatchDocumentDictionary.Values.Select(x => x.PatchDetails)
                .Where(x => x.Visible)
                .FirstOrDefault();

            // PatchGrid
            patchGridUserControl.ViewModel = _presenter.MainViewModel.Document.PatchGridDictionary.Values
                                                                              .Where(x => x.Visible)
                                                                              .FirstOrDefault();

            // PatchProperties
            patchPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.PatchDocumentDictionary.Values
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
                sampleGridUserControl.ViewModel = _presenter.MainViewModel.Document.PatchDocumentDictionary.Values
                                                                                   .Select(x => x.SampleGrid)
                                                                                   .Where(x => x.Visible)
                                                                                   .FirstOrDefault();
            }

            // SampleProperties
            samplePropertiesUserControl.ViewModel =
                Enumerable.Union(
                    _presenter.MainViewModel.Document.SamplePropertiesDictionary.Values,
                    _presenter.MainViewModel.Document.PatchDocumentDictionary.Values.SelectMany(x => x.SamplePropertiesDictionary.Values))
               .Where(x => x.Visible)
               .FirstOrDefault();

            // Scale
            scaleGridUserControl.ViewModel = _presenter.MainViewModel.Document.ScaleGrid;
            toneGridEditUserControl.ViewModel = _presenter.MainViewModel.Document.ToneGridEditDictionary.Values
                                                                                 .Where(x => x.Visible)
                                                                                 .FirstOrDefault();
            scalePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.ScalePropertiesDictionary.Values
                                                                                    .Where(x => x.Visible)
                                                                                    .FirstOrDefault();

            // Applying Visible = true first and then Visible = false prevents flickering.
            foreach (UserControlBase userControl in _userControls)
            {
                if (MustBecomeVisible(userControl))
                {
                    userControl.Visible = true;
                }
            }

            foreach (UserControlBase userControl in _userControls)
            {
                if (!MustBecomeVisible(userControl))
                {
                    userControl.Visible = false;
                }
            }

            // Panel Visibility
            bool treePanelMustBeVisible = MustBecomeVisible(documentTreeUserControl);
            SetTreePanelVisible(treePanelMustBeVisible);

            bool propertiesPanelMustBeVisible = _userControls.Where(x => MustBecomeVisible(x) && x is PropertiesUserControlBase).Any();
            SetPropertiesPanelVisible(propertiesPanelMustBeVisible);

            if (_presenter.MainViewModel.DocumentDelete.Visible)
            {
                MessageBoxHelper.ShowDocumentConfirmDelete(this, _presenter.MainViewModel.DocumentDelete);
            }

            if (_presenter.MainViewModel.DocumentDeleted.Visible)
            {
                MessageBoxHelper.ShowDocumentIsDeleted(this);
            }

            if (_presenter.MainViewModel.DocumentCannotDelete.Visible)
            {
                _documentCannotDeleteForm.ShowDialog(_presenter.MainViewModel.DocumentCannotDelete);
            }

            if (_presenter.MainViewModel.ValidationMessages.Count != 0)
            {
                // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                MessageBoxHelper.ShowMessageBox(this, String.Join(Environment.NewLine, _presenter.MainViewModel.ValidationMessages.Select(x => x.Text)));

                // Clear them so the next time the message box is not shown (message box is a temporary solution).
                _presenter.MainViewModel.ValidationMessages.Clear();
            }

            if (_presenter.MainViewModel.PopupMessages.Count != 0)
            {
                // TODO: I get an infinite loop either because of LoseFocus events going off,
                // or otherwise ActionIsBusy boolean making PopupMessageOK not handled...
                // I just cannot hack it right now.
                IList<Message> popupMessages = _presenter.MainViewModel.PopupMessages;
                _presenter.MainViewModel.PopupMessages = new List<Message>();

                MessageBoxHelper.ShowPopupMessages(this, popupMessages);
            }

            // Focus control if not valid.
            foreach (UserControlBase userControl in _userControls)
            {
                if (userControl.ViewModel != null)
                {
                    bool mustFocus = MustBecomeVisible(userControl) && 
                                     !userControl.ViewModel.Successful;
                    if (mustFocus)
                    {
                        userControl.Focus();
                        break;
                    }
                }
            }
        }
        private bool MustBecomeVisible(UserControlBase userControl)
        {
            return userControl.ViewModel != null &&
                   userControl.ViewModel.Visible;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm
    {
        private class UserControlTuple
        {
            public UserControlTuple(UserControl userControl, ViewModelBase viewModel, bool isPropertiesView = false)
            {
                UserControl = userControl;
                ViewModel = viewModel;
                IsPropertiesView = isPropertiesView;
            }

            public UserControl UserControl { get; private set; }
            public ViewModelBase ViewModel { get; private set; }
            public bool IsPropertiesView { get; private set; }

            public bool ViewMustBecomeVisible { get; set; }
        }

        private void ApplyViewModel()
        {
            Text = _presenter.MainViewModel.TitleBar;

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

            // OperatorProperties_ForAggregate
            operatorPropertiesUserControl_ForAggregate.ViewModel =
                _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForAggregates)
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForBundle
            operatorPropertiesUserControl_ForBundle.ViewModel =
                _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForBundles)
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForCache
            operatorPropertiesUserControl_ForCache.ViewModel =
                _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCaches)
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

            // OperatorProperties_ForRandom
            operatorPropertiesUserControl_ForRandom.ViewModel =
                _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForRandoms)
                .Where(x => x.Visible)
                .FirstOrDefault();

            // OperatorProperties_ForResample
            operatorPropertiesUserControl_ForResample.ViewModel =
                _presenter.MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForResamples)
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
            UserControlTuple[] tuples = CreateUserControlTuples();

            // Determine Visible Values
            foreach (UserControlTuple tuple in tuples)
            {
                tuple.ViewMustBecomeVisible = tuple.ViewModel != null &&
                                              tuple.ViewModel.Visible;
            }

            // Applying Visible = true first and then Visible = false prevents flickering.
            foreach (UserControlTuple tuple in tuples)
            {
                if (tuple.ViewMustBecomeVisible)
                {
                    tuple.UserControl.Visible = true;
                }
            }

            foreach (UserControlTuple tuple in tuples)
            {
                if (!tuple.ViewMustBecomeVisible)
                {
                    tuple.UserControl.Visible = false;
                }
            }

            // Panel Visibility
            bool treePanelMustBeVisible = tuples.Where(x => x.UserControl is DocumentTreeUserControl).Single().ViewMustBecomeVisible;
            SetTreePanelVisible(treePanelMustBeVisible);

            bool propertiesPanelMustBeVisible = tuples.Where(x => x.ViewMustBecomeVisible && x.IsPropertiesView).Any();
            SetPropertiesPanelVisible(propertiesPanelMustBeVisible);

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
            foreach (UserControlTuple tuple in tuples)
            {
                bool mustFocus = tuple.ViewMustBecomeVisible && !tuple.ViewModel.Successful;

                if (mustFocus)
                {
                    tuple.UserControl.Focus();
                    break;
                }
            }
        }

        // TODO: It is a shame I have to recreate this array every time I do ApplyViewModel.
        // You could prevent this by making a UserControl base class and working with that polymorphically.
        private UserControlTuple[] CreateUserControlTuples()
        {
            return new UserControlTuple[]
            {
                new UserControlTuple(
                    audioFileOutputGridUserControl,
                    audioFileOutputGridUserControl.ViewModel),
                new UserControlTuple(
                    audioFileOutputPropertiesUserControl,
                    audioFileOutputPropertiesUserControl.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    curveDetailsUserControl,
                    curveDetailsUserControl.ViewModel),
                new UserControlTuple(
                    curveGridUserControl,
                    curveGridUserControl.ViewModel),
                new UserControlTuple(
                    curvePropertiesUserControl,
                    curvePropertiesUserControl.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    documentDetailsUserControl,
                    documentDetailsUserControl.ViewModel),
                new UserControlTuple(
                    documentGridUserControl,
                    documentGridUserControl.ViewModel),
                new UserControlTuple(
                    documentPropertiesUserControl,
                    documentPropertiesUserControl.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    documentTreeUserControl,
                    documentTreeUserControl.ViewModel),
                new UserControlTuple(
                    nodePropertiesUserControl,
                    nodePropertiesUserControl.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl,
                    operatorPropertiesUserControl.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForAggregate,
                    operatorPropertiesUserControl_ForAggregate.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForBundle,
                    operatorPropertiesUserControl_ForBundle.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForCache,
                    operatorPropertiesUserControl_ForCache.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForCurve,
                    operatorPropertiesUserControl_ForCurve.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForCustomOperator,
                    operatorPropertiesUserControl_ForCustomOperator.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForNumber,
                    operatorPropertiesUserControl_ForNumber.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForPatchInlet,
                    operatorPropertiesUserControl_ForPatchInlet.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForPatchOutlet,
                    operatorPropertiesUserControl_ForPatchOutlet.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForRandom,
                    operatorPropertiesUserControl_ForRandom.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForResample,
                    operatorPropertiesUserControl_ForResample.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForSample,
                    operatorPropertiesUserControl_ForSample.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForSpectrum,
                    operatorPropertiesUserControl_ForSpectrum.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    operatorPropertiesUserControl_ForUnbundle,
                    operatorPropertiesUserControl_ForUnbundle.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    patchGridUserControl,
                    patchGridUserControl.ViewModel),
                new UserControlTuple(
                    patchDetailsUserControl,
                    patchDetailsUserControl.ViewModel),
                new UserControlTuple(
                    patchPropertiesUserControl,
                    patchPropertiesUserControl.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    sampleGridUserControl,
                    sampleGridUserControl.ViewModel),
                new UserControlTuple(
                    samplePropertiesUserControl,
                    samplePropertiesUserControl.ViewModel,
                    isPropertiesView: true),
                new UserControlTuple(
                    scaleGridUserControl,
                    scaleGridUserControl.ViewModel),
                new UserControlTuple(
                    toneGridEditUserControl,
                    toneGridEditUserControl.ViewModel),
                new UserControlTuple(
                    scalePropertiesUserControl,
                    scalePropertiesUserControl.ViewModel,
                    isPropertiesView: true)
            };
        }
    }
}
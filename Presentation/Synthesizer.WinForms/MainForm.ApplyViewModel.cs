using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
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

            _autoPatchPopupForm.ViewModel = _presenter.MainViewModel.Document.AutoPatchPopup;
            audioFileOutputGridUserControl.ViewModel = _presenter.MainViewModel.Document.AudioFileOutputGrid;
            audioFileOutputPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleAudioFileOutputProperties;
            audioOutputPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.AudioOutputProperties;
            currentInstrumentUserControl.ViewModel = _presenter.MainViewModel.Document.CurrentInstrument;
            currentInstrumentUserControl.Visible = currentInstrumentUserControl.ViewModel.Visible;
            curveDetailsUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleCurveDetails;
            curveGridUserControl.ViewModel = _presenter.MainViewModel.Document.CurveGrid;
            curvePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleCurveProperties;
            documentDetailsUserControl.ViewModel = _presenter.MainViewModel.DocumentDetails;
            documentGridUserControl.ViewModel = _presenter.MainViewModel.DocumentGrid;
            documentPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.DocumentProperties;
            documentTreeUserControl.ViewModel = _presenter.MainViewModel.Document.DocumentTree;
            libraryGridUserControl.ViewModel = _presenter.MainViewModel.Document.LibraryGrid;
            libraryPatchPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleLibraryPatchProperties;
            libraryPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleLibraryProperties;
            _librarySelectionPopupForm.ViewModel = _presenter.MainViewModel.Document.LibrarySelectionPopup;
            nodePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleNodeProperties;
            operatorPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties;
            operatorPropertiesUserControl_ForCache.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForCache;
            operatorPropertiesUserControl_ForCurve.SetCurveLookup(_presenter.MainViewModel.Document.CurveLookup);
            operatorPropertiesUserControl_ForCurve.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForCurve;
            operatorPropertiesUserControl_ForCustomOperator.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForCustomOperator;
            operatorPropertiesUserControl_ForCustomOperator.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForInletsToDimension.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension;
            operatorPropertiesUserControl_ForNumber.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForNumber;
            operatorPropertiesUserControl_ForPatchInlet.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet;
            operatorPropertiesUserControl_ForPatchOutlet.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet;
            operatorPropertiesUserControl_ForSample.SetSampleLookup(_presenter.MainViewModel.Document.SampleLookup);
            operatorPropertiesUserControl_ForSample.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForSample;
            operatorPropertiesUserControl_WithCollectionRecalculation.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation;
            operatorPropertiesUserControl_WithInletCount.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_WithInletCount;
            operatorPropertiesUserControl_WithInterpolation.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_WithInterpolation;
            operatorPropertiesUserControl_WithOutletCount.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_WithOutletCount;
            patchDetailsUserControl.ViewModel = _presenter.MainViewModel.Document.VisiblePatchDetails;
            patchGridUserControl.ViewModel = _presenter.MainViewModel.Document.VisiblePatchGrid;
            patchPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisiblePatchProperties;
            sampleGridUserControl.ViewModel = _presenter.MainViewModel.Document.SampleGrid;
            samplePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleSampleProperties;
            scaleGridUserControl.ViewModel = _presenter.MainViewModel.Document.ScaleGrid;
            scalePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleScaleProperties;
            toneGridEditUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleToneGridEdit;

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

            _autoPatchPopupForm.Visible = _presenter.MainViewModel.Document.AutoPatchPopup.Visible;

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

            if (_presenter.MainViewModel.Document.LibrarySelectionPopup.Visible)
            {
                if (!_librarySelectionPopupForm.Visible)
                {
                    _librarySelectionPopupForm.ShowDialog();
                }
            }
            else
            {
                _librarySelectionPopupForm.Visible = false;
            }

            if (_presenter.MainViewModel.ValidationMessages.Count != 0)
            {
                // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                MessageBoxHelper.ShowMessageBox(this, string.Join(Environment.NewLine, _presenter.MainViewModel.ValidationMessages.Select(x => x.Text)));

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
                if (userControl.ViewModel == null)
                {
                    continue;
                }

                bool mustFocus = MustBecomeVisible(userControl) && 
                                 !userControl.ViewModel.Successful;
                if (!mustFocus)
                {
                    continue;
                }

                userControl.Focus();
                break;
            }
        }

        private bool MustBecomeVisible(UserControlBase userControl)
        {
            return userControl.ViewModel != null &&
                   userControl.ViewModel.Visible;
        }
    }
}

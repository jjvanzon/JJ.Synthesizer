using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

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
            curveGridUserControl.ViewModel = _presenter.MainViewModel.Document.CurveGrid;
            curvePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleCurveProperties;
            curveDetailsListUserControl.ViewModels = _presenter.MainViewModel.Document.CurveDetailsDictionary.Values.OrderBy(x => x.Curve.Name).ToArray();
            documentDetailsUserControl.ViewModel = _presenter.MainViewModel.DocumentDetails;
            documentGridUserControl.ViewModel = _presenter.MainViewModel.DocumentGrid;
            documentPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.DocumentProperties;
            documentTreeUserControl.ViewModel = _presenter.MainViewModel.Document.DocumentTree;
            libraryPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleLibraryProperties;
            _librarySelectionPopupForm.ViewModel = _presenter.MainViewModel.Document.LibrarySelectionPopup;
            nodePropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleNodeProperties;
            operatorPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties;
            operatorPropertiesUserControl.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForCache.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForCache;
            operatorPropertiesUserControl_ForCache.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForCurve.SetCurveLookup(_presenter.MainViewModel.Document.CurveLookup);
            operatorPropertiesUserControl_ForCurve.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForCurve;
            operatorPropertiesUserControl_ForCurve.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForInletsToDimension.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension;
            operatorPropertiesUserControl_ForInletsToDimension.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForNumber.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForNumber;
            operatorPropertiesUserControl_ForNumber.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForPatchInlet.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet;
            operatorPropertiesUserControl_ForPatchInlet.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForPatchOutlet.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet;
            operatorPropertiesUserControl_ForPatchOutlet.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForSample.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_ForSample.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_ForSample;
            operatorPropertiesUserControl_WithCollectionRecalculation.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation;
            operatorPropertiesUserControl_WithCollectionRecalculation.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            operatorPropertiesUserControl_WithInterpolation.ViewModel = _presenter.MainViewModel.Document.VisibleOperatorProperties_WithInterpolation;
            operatorPropertiesUserControl_WithInterpolation.SetUnderlyingPatchLookup(_presenter.MainViewModel.Document.UnderlyingPatchLookup);
            patchDetailsUserControl.ViewModel = _presenter.MainViewModel.Document.VisiblePatchDetails;
            patchPropertiesUserControl.ViewModel = _presenter.MainViewModel.Document.VisiblePatchProperties;
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

            bool curveDetailsPanelMustBeVisible = _presenter.MainViewModel.Document.CurveDetailsDictionary.Values.Any(x => x.Visible);
            SetCurveDetailsPanelVisible(curveDetailsPanelMustBeVisible);

            // Modal Windows
            if (_presenter.MainViewModel.DocumentDelete.Visible)
            {
                ModalPopupHelper.ShowDocumentConfirmDelete(this, _presenter.MainViewModel.DocumentDelete);
            }

            if (_presenter.MainViewModel.DocumentDeleted.Visible)
            {
                ModalPopupHelper.ShowDocumentIsDeleted(this);
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

            if (_presenter.MainViewModel.Document.SampleFileBrowser.Visible)
            {
                ModalPopupHelper.ShowSampleFileBrowser(this, _presenter.MainViewModel.Document.SampleFileBrowser);
            }

            if (_presenter.MainViewModel.ValidationMessages.Count != 0)
            {
                // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                ModalPopupHelper.ShowMessageBox(this, string.Join(Environment.NewLine, _presenter.MainViewModel.ValidationMessages));

                // Clear them so the next time the message box is not shown (message box is a temporary solution).
                _presenter.MainViewModel.ValidationMessages.Clear();
            }

            if (_presenter.MainViewModel.PopupMessages.Count != 0)
            {
                IList<string> messages = _presenter.MainViewModel.PopupMessages;
                _presenter.MainViewModel.PopupMessages = new List<string>();

                ModalPopupHelper.ShowPopupMessages(this, messages);
            }

            if (_presenter.MainViewModel.WarningMessages.Count != 0)
            {
                // TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
                IList<string> messages = _presenter.MainViewModel.WarningMessages;
                _presenter.MainViewModel.WarningMessages = new List<string>();
                ModalPopupHelper.ShowMessageBox(
                    this,
                    ResourceFormatter.Warnings + ":" + Environment.NewLine + 
                    string.Join(Environment.NewLine, messages));
            }

            if (_presenter.MainViewModel.DocumentOrPatchNotFound.Visible)
            {
                ModalPopupHelper.ShowDocumentOrPatchNotFoundPopup(this, _presenter.MainViewModel.DocumentOrPatchNotFound);
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

            // Close MainForm if needed.
            if (_presenter.MainViewModel.DocumentOrPatchNotFound.MustCloseMainView)
            {
                Close();
            }
        }

        private bool MustBecomeVisible(UserControlBase userControl)
        {
            return userControl.ViewModel != null &&
                   userControl.ViewModel.Visible;
        }
    }
}

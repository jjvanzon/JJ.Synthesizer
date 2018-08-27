using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms
{
	internal partial class MainForm
	{
		private void ApplyViewModel()
		{
			MainViewModel mainViewModel = _mainPresenter.MainViewModel;
			DocumentViewModel documentViewModel = mainViewModel.Document;

			// NOTE: Actually making controls visible is postponed till last, to do it in a way that does not flash as much.

			Text = mainViewModel.TitleBar;

			_autoPatchPopupForm.ViewModel = documentViewModel.AutoPatchPopup;
			audioFileOutputGridUserControl.ViewModel = documentViewModel.AudioFileOutputGrid;
			audioFileOutputPropertiesUserControl.ViewModel = documentViewModel.VisibleAudioFileOutputProperties;
			audioOutputPropertiesUserControl.ViewModel = documentViewModel.AudioOutputProperties;
            curveDetailsListUserControl.ViewModels = documentViewModel.CurveDetailsDictionary.Values.OrderBy(x => x.Curve.Name).ToArray();
			documentDetailsUserControl.ViewModel = mainViewModel.DocumentDetails;
			documentGridUserControl.ViewModel = mainViewModel.DocumentGrid;
			documentPropertiesUserControl.ViewModel = documentViewModel.DocumentProperties;
			documentTreeUserControl.ViewModel = documentViewModel.DocumentTree;
			libraryPropertiesUserControl.ViewModel = documentViewModel.VisibleLibraryProperties;
			_librarySelectionPopupForm.ViewModel = documentViewModel.LibrarySelectionPopup;
			midiMappingDetailsUserControl.ViewModel = documentViewModel.VisibleMidiMappingGroupDetails;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			midiMappingPropertiesUserControl.ViewModel = documentViewModel.VisibleMidiMappingProperties;
			monitoringBarUserControl.ViewModel = mainViewModel.MonitoringBar;
			nodePropertiesUserControl.ViewModel = documentViewModel.VisibleNodeProperties;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl.ViewModel = documentViewModel.VisibleOperatorProperties;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_ForCache.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForCache.ViewModel = documentViewModel.VisibleOperatorProperties_ForCache;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_ForCurve.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForCurve.ViewModel = documentViewModel.VisibleOperatorProperties_ForCurve;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_ForInletsToDimension.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForInletsToDimension.ViewModel = documentViewModel.VisibleOperatorProperties_ForInletsToDimension;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_ForNumber.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForNumber.ViewModel = documentViewModel.VisibleOperatorProperties_ForNumber;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_ForPatchInlet.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForPatchInlet.ViewModel = documentViewModel.VisibleOperatorProperties_ForPatchInlet;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_ForPatchOutlet.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForPatchOutlet.ViewModel = documentViewModel.VisibleOperatorProperties_ForPatchOutlet;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_ForSample.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForSample.ViewModel = documentViewModel.VisibleOperatorProperties_ForSample;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_WithCollectionRecalculation.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_WithCollectionRecalculation.ViewModel = documentViewModel.VisibleOperatorProperties_WithCollectionRecalculation;
			// Order-Dependence: Always set lookups before setting view model, otherwise the selected value from the lookup isn't there.
			operatorPropertiesUserControl_WithInterpolation.SetUnderlyingPatchLookup(documentViewModel.UnderlyingPatchLookup);
			operatorPropertiesUserControl_WithInterpolation.ViewModel = documentViewModel.VisibleOperatorProperties_WithInterpolation;
			patchDetailsUserControl.ViewModel = documentViewModel.VisiblePatchDetails;
			patchPropertiesUserControl.ViewModel = documentViewModel.VisiblePatchProperties;
			scalePropertiesUserControl.ViewModel = documentViewModel.VisibleScaleProperties;
			toneGridEditUserControl.ViewModel = documentViewModel.VisibleToneGridEdit;
		    topBarUserControl.TopBarElement.InstrumentBarElement.ViewModel = documentViewModel.InstrumentBar;
		    topBarUserControl.TopBarElement.TopButtonBarElement.ViewModel = documentViewModel.TopButtonBar;
		    topBarUserControl.Refresh();

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

			_autoPatchPopupForm.Visible = documentViewModel.AutoPatchPopup.Visible;

			// Panel Visibility
			SetTreePanelVisible(documentViewModel.DocumentTree.Visible);
			SetPropertiesPanelVisible(mainViewModel.PropertiesPanelVisible);
			SetCurveDetailsPanelVisible(mainViewModel.CurveDetailsPanelVisible);

			// Modal Windows
			if (mainViewModel.DocumentDelete.Visible)
			{
				ModalPopupHelper.ShowDocumentConfirmDelete(this, mainViewModel.DocumentDelete);
			}

			if (mainViewModel.DocumentDeleted.Visible)
			{
				ModalPopupHelper.ShowDocumentIsDeleted(this);
			}

			DocumentCannotDeleteViewModel documentCannotDeleteViewModel = mainViewModel.DocumentCannotDelete;
			if (documentCannotDeleteViewModel.Visible)
			{
				_documentCannotDeleteForm.ShowDialog(documentCannotDeleteViewModel);
			}

			if (documentViewModel.LibrarySelectionPopup.Visible)
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

			SampleFileBrowserViewModel sampleFileBrowserViewModel = documentViewModel.SampleFileBrowser;
			if (sampleFileBrowserViewModel.Visible)
			{
				ModalPopupHelper.ShowSampleFileBrowser(this, sampleFileBrowserViewModel);
			}

			SaveChangesPopupViewModel saveChangesPopupViewModel = documentViewModel.SaveChangesPopup;
			if (saveChangesPopupViewModel.Visible)
			{
				ModalPopupHelper.ShowSaveChangesPopup(this, saveChangesPopupViewModel);
			}

			if (mainViewModel.ValidationMessages.Count != 0)
			{
				// TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
				ModalPopupHelper.ShowMessageBox(this, string.Join(Environment.NewLine, mainViewModel.ValidationMessages));

				// Clear them so the next time the message box is not shown (message box is a temporary solution).
				mainViewModel.ValidationMessages.Clear();
			}

			if (mainViewModel.PopupMessages.Count != 0)
			{
				IList<string> messages = mainViewModel.PopupMessages;
				mainViewModel.PopupMessages = new List<string>();

				ModalPopupHelper.ShowPopupMessages(this, messages);
			}

			if (mainViewModel.WarningMessages.Count != 0)
			{
				// TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
				IList<string> messages = mainViewModel.WarningMessages;
				mainViewModel.WarningMessages = new List<string>();
				ModalPopupHelper.ShowMessageBox(
					this,
					ResourceFormatter.Warnings + ":" + Environment.NewLine + 
					string.Join(Environment.NewLine, messages));
			}

			DocumentOrPatchNotFoundPopupViewModel documentOrPatchNotFoundPopupViewModel = mainViewModel.DocumentOrPatchNotFound;
			if (documentOrPatchNotFoundPopupViewModel.Visible)
			{
				ModalPopupHelper.ShowDocumentOrPatchNotFoundPopup(this, documentOrPatchNotFoundPopupViewModel);
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
			if (mainViewModel.DocumentOrPatchNotFound.MustCloseMainView)
			{
				Close();
			}
		}

		private bool MustBecomeVisible(UserControlBase userControl) => userControl.ViewModel?.Visible ?? false;
	}
}

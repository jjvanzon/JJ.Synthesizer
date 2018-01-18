using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms
{
	internal partial class MainForm
	{
		private void ApplyViewModel()
		{
			// NOTE: Actually making controls visible is postponed till last, to do it in a way that does not flash as much.

			Text = _mainPresenter.MainViewModel.TitleBar;

			menuUserControl.Show(_mainPresenter.MainViewModel.Menu);

			_autoPatchPopupForm.ViewModel = _mainPresenter.MainViewModel.Document.AutoPatchPopup;
			audioFileOutputGridUserControl.ViewModel = _mainPresenter.MainViewModel.Document.AudioFileOutputGrid;
			audioFileOutputPropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleAudioFileOutputProperties;
			audioOutputPropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.AudioOutputProperties;
			currentInstrumentUserControl.ViewModel = _mainPresenter.MainViewModel.Document.CurrentInstrument;
			curveDetailsListUserControl.ViewModels = _mainPresenter.MainViewModel.Document.CurveDetailsDictionary.Values.OrderBy(x => x.Curve.Name).ToArray();
			documentDetailsUserControl.ViewModel = _mainPresenter.MainViewModel.DocumentDetails;
			documentGridUserControl.ViewModel = _mainPresenter.MainViewModel.DocumentGrid;
			documentPropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.DocumentProperties;
			documentTreeUserControl.ViewModel = _mainPresenter.MainViewModel.Document.DocumentTree;
			libraryPropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleLibraryProperties;
			_librarySelectionPopupForm.ViewModel = _mainPresenter.MainViewModel.Document.LibrarySelectionPopup;
			midiMappingDetailsUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleMidiMappingDetails;
			midiMappingElementPropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleMidiMappingElementProperties;
			nodePropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleNodeProperties;
			operatorPropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties;
			operatorPropertiesUserControl.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForCache.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_ForCache;
			operatorPropertiesUserControl_ForCache.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForCurve.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_ForCurve;
			operatorPropertiesUserControl_ForCurve.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForInletsToDimension.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension;
			operatorPropertiesUserControl_ForInletsToDimension.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForNumber.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_ForNumber;
			operatorPropertiesUserControl_ForNumber.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForPatchInlet.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet;
			operatorPropertiesUserControl_ForPatchInlet.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForPatchOutlet.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet;
			operatorPropertiesUserControl_ForPatchOutlet.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForSample.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_ForSample.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_ForSample;
			operatorPropertiesUserControl_WithCollectionRecalculation.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation;
			operatorPropertiesUserControl_WithCollectionRecalculation.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			operatorPropertiesUserControl_WithInterpolation.ViewModel = _mainPresenter.MainViewModel.Document.VisibleOperatorProperties_WithInterpolation;
			operatorPropertiesUserControl_WithInterpolation.SetUnderlyingPatchLookup(_mainPresenter.MainViewModel.Document.UnderlyingPatchLookup);
			patchDetailsUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisiblePatchDetails;
			patchPropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisiblePatchProperties;
			scaleGridUserControl.ViewModel = _mainPresenter.MainViewModel.Document.ScaleGrid;
			scalePropertiesUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleScaleProperties;
			toneGridEditUserControl.ViewModel = _mainPresenter.MainViewModel.Document.VisibleToneGridEdit;

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

			_autoPatchPopupForm.Visible = _mainPresenter.MainViewModel.Document.AutoPatchPopup.Visible;

			// Panel Visibility
			SetTreePanelVisible(_mainPresenter.MainViewModel.Document.DocumentTree.Visible);
			SetPropertiesPanelVisible(_mainPresenter.MainViewModel.PropertiesPanelVisible);
			SetCurveDetailsPanelVisible(_mainPresenter.MainViewModel.CurveDetailsPanelVisible);

			// Modal Windows
			if (_mainPresenter.MainViewModel.DocumentDelete.Visible)
			{
				ModalPopupHelper.ShowDocumentConfirmDelete(this, _mainPresenter.MainViewModel.DocumentDelete);
			}

			if (_mainPresenter.MainViewModel.DocumentDeleted.Visible)
			{
				ModalPopupHelper.ShowDocumentIsDeleted(this);
			}

			DocumentCannotDeleteViewModel documentCannotDeleteViewModel = _mainPresenter.MainViewModel.DocumentCannotDelete;
			if (documentCannotDeleteViewModel.Visible)
			{
				_documentCannotDeleteForm.ShowDialog(documentCannotDeleteViewModel);
			}

			if (_mainPresenter.MainViewModel.Document.LibrarySelectionPopup.Visible)
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

			SampleFileBrowserViewModel sampleFileBrowserViewModel = _mainPresenter.MainViewModel.Document.SampleFileBrowser;
			if (sampleFileBrowserViewModel.Visible)
			{
				ModalPopupHelper.ShowSampleFileBrowser(this, sampleFileBrowserViewModel);
			}

			SaveChangesPopupViewModel saveChangesPopupViewModel = _mainPresenter.MainViewModel.Document.SaveChangesPopup;
			if (saveChangesPopupViewModel.Visible)
			{
				ModalPopupHelper.ShowSaveChangesPopup(this, saveChangesPopupViewModel);
			}

			if (_mainPresenter.MainViewModel.ValidationMessages.Count != 0)
			{
				// TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
				ModalPopupHelper.ShowMessageBox(this, string.Join(Environment.NewLine, _mainPresenter.MainViewModel.ValidationMessages));

				// Clear them so the next time the message box is not shown (message box is a temporary solution).
				_mainPresenter.MainViewModel.ValidationMessages.Clear();
			}

			if (_mainPresenter.MainViewModel.PopupMessages.Count != 0)
			{
				IList<string> messages = _mainPresenter.MainViewModel.PopupMessages;
				_mainPresenter.MainViewModel.PopupMessages = new List<string>();

				ModalPopupHelper.ShowPopupMessages(this, messages);
			}

			if (_mainPresenter.MainViewModel.WarningMessages.Count != 0)
			{
				// TODO: Lower priorty: This is a temporary dispatching of the validation messages. Later it will be shown in a separate Panel.
				IList<string> messages = _mainPresenter.MainViewModel.WarningMessages;
				_mainPresenter.MainViewModel.WarningMessages = new List<string>();
				ModalPopupHelper.ShowMessageBox(
					this,
					ResourceFormatter.Warnings + ":" + Environment.NewLine + 
					string.Join(Environment.NewLine, messages));
			}

			DocumentOrPatchNotFoundPopupViewModel documentOrPatchNotFoundPopupViewModel = _mainPresenter.MainViewModel.DocumentOrPatchNotFound;
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
			if (_mainPresenter.MainViewModel.DocumentOrPatchNotFound.MustCloseMainView)
			{
				Close();
			}
		}

		private bool MustBecomeVisible(UserControlBase userControl) => userControl.ViewModel?.Visible ?? false;
	}
}

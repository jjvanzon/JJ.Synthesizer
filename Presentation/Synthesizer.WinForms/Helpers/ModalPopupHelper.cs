using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Exceptions;
using JJ.Framework.IO;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
	/// <summary>
	/// Handles the trouble that comes with executing actions after the modal popup is closed.
	/// One of the problems it that the event is might be during the MainViewModelApplyViewModel,
	/// since the modal dialog is show there, which pauses the procedure.
	/// That's why the actual showing of the modal is done with a BeginInvoke,
	/// which queues it to after the call is done.
	/// </summary>
	internal static class ModalPopupHelper
	{
		private static readonly object _dummySender = new object();
		private static readonly OpenFileDialog _openFileDialog = new OpenFileDialog();

		public static event EventHandler<EventArgs<int>> DocumentDeleteConfirmed;
		public static event EventHandler DocumentDeleteCanceled;
		public static event EventHandler DocumentDeletedOKRequested;
		public static event EventHandler DocumentOrPatchNotFoundOKRequested;
		public static event EventHandler PopupMessagesOKRequested;
		public static event EventHandler SampleFileBrowserOKRequested;
		public static event EventHandler SampleFileBrowserCanceled;
		public static event EventHandler SaveChangesPopupCanceled;
		public static event EventHandler SaveChangesPopupNoRequested;
		public static event EventHandler SaveChangesPopupYesRequested;

		public static void ShowDocumentConfirmDelete(Form parentForm, DocumentDeleteViewModel viewModel)
		{
			if (parentForm == null) throw new NullException(() => parentForm);
			if (viewModel == null) throw new NullException(() => viewModel);

			parentForm.BeginInvoke(
				new Action(
					() =>
					{
						string message = CommonResourceFormatter.AreYouSureYouWishToDelete_WithType_AndName(ResourceFormatter.Document, viewModel.Document.Name);

						DialogResult dialogResult = MessageBox.Show(message, ResourceFormatter.ApplicationName, MessageBoxButtons.YesNo);
						switch (dialogResult)
						{
							case DialogResult.Yes:
								if (DocumentDeleteConfirmed != null)
								{
									var e = new EventArgs<int>(viewModel.Document.ID);
									DocumentDeleteConfirmed(_dummySender, e);
								}
								break;

							case DialogResult.No:

								DocumentDeleteCanceled(_dummySender, EventArgs.Empty);
								break;

							default:
								throw new ValueNotSupportedException(dialogResult);
						}
					}));
		}

		public static void ShowDocumentIsDeleted(Form parentForm)
		{
			if (parentForm == null) throw new NullException(() => parentForm);

			parentForm.BeginInvoke(
				new Action(
					() =>
					{
						MessageBox.Show(CommonResourceFormatter.IsDeleted_WithName(ResourceFormatter.Document));

						DocumentDeletedOKRequested(_dummySender, EventArgs.Empty);
					}));
		}

		public static void ShowDocumentOrPatchNotFoundPopup(Form parentForm, DocumentOrPatchNotFoundPopupViewModel viewModel)
		{
			if (parentForm == null) throw new NullException(() => parentForm);
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			parentForm.BeginInvoke(
				new Action(
					() =>
					{
						MessageBox.Show(string.Join(Environment.NewLine, viewModel.NotFoundMessage));

						DocumentOrPatchNotFoundOKRequested(_dummySender, EventArgs.Empty);
					}));
		}

		public static void ShowMessageBox(Form parentForm, string text)
		{
			if (parentForm == null) throw new NullException(() => parentForm);

			parentForm.BeginInvoke(
				new Action(
					() =>
					{
						MessageBox.Show(text);
					}));
		}

		public static void ShowPopupMessages(Form parentForm, IList<string> popupMessages)
		{
			if (parentForm == null) throw new NullException(() => parentForm);
			if (popupMessages == null) throw new NullException(() => popupMessages);

			parentForm.BeginInvoke(
				new Action(
					() =>
					{
						MessageBox.Show(string.Join(Environment.NewLine, popupMessages));

						PopupMessagesOKRequested(_dummySender, EventArgs.Empty);
					}));
		}

		public static void ShowSaveChangesPopup(Form parentForm, SaveChangesPopupViewModel viewModel)
		{
			if (parentForm == null) throw new NullException(() => parentForm);
			if (viewModel == null) throw new NullException(() => viewModel);

			parentForm.BeginInvoke(
				new Action(
					() =>
					{
						DialogResult dialogResult = MessageBox.Show(
							CommonResourceFormatter.WantToSaveChanges,
							ResourceFormatter.ApplicationName,
							MessageBoxButtons.YesNoCancel);

						switch (dialogResult)
						{
							case DialogResult.Yes:
								SaveChangesPopupYesRequested(_dummySender, EventArgs.Empty);
								break;

							case DialogResult.No:
								SaveChangesPopupNoRequested(_dummySender, EventArgs.Empty);
								break;

							case DialogResult.Cancel:
								SaveChangesPopupCanceled(_dummySender, EventArgs.Empty);
								break;

							default:
								throw new ValueNotSupportedException(dialogResult);
						}
					}));
		}

		public static void ShowSampleFileBrowser(Form parentForm, SampleFileBrowserViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			parentForm.BeginInvoke(
				new Action(
					() =>
					{
						if (_openFileDialog.ShowDialog(parentForm) == DialogResult.OK)
						{
							using (Stream stream = new FileStream(_openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
							{
								viewModel.Bytes = StreamHelper.StreamToBytes(stream);
								viewModel.FilePath = _openFileDialog.FileName;
							}

							SampleFileBrowserOKRequested(_dummySender, EventArgs.Empty);
						}
						else
						{
							SampleFileBrowserCanceled(_dummySender, EventArgs.Empty);
						}
					}));
		}
	}
}
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using CanonicalModel = JJ.Data.Canonical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class MessageBoxHelper
    {
        private static readonly object _dummySender = new object();

        public static event EventHandler<EventArgs<int>> DocumentDeleteConfirmed;
        public static event EventHandler DocumentDeleteCanceled;
        public static event EventHandler DocumentDeletedOK;
        public static event EventHandler PopupMessagesOK;

        public static void ShowDocumentConfirmDelete(Form parentForm, DocumentDeleteViewModel viewModel)
        {
            if (parentForm == null) throw new NullException(() => parentForm);
            if (viewModel == null) throw new NullException(() => viewModel);

            parentForm.BeginInvoke(new Action(() =>
            {
                string message = CommonResourceFormatter.AreYouSureYouWishToDelete_WithTypeName_AndName(ResourceFormatter.Document, viewModel.Document.Name);

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

                        DocumentDeleteCanceled?.Invoke(_dummySender, EventArgs.Empty);
                        break;

                    default:
                        throw new ValueNotSupportedException(dialogResult);
                }
            }));
        }

        public static void ShowDocumentIsDeleted(Form parentForm)
        {
            if (parentForm == null) throw new NullException(() => parentForm);

            parentForm.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(CommonResourceFormatter.IsDeleted_WithName(ResourceFormatter.Document));

                DocumentDeletedOK?.Invoke(_dummySender, EventArgs.Empty);
            }));
        }

        public static void ShowPopupMessages(Form parentForm, IList<CanonicalModel.Message> popupMessages)
        {
            if (parentForm == null) throw new NullException(() => parentForm);
            if (popupMessages == null) throw new NullException(() => popupMessages);

            parentForm.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(string.Join(Environment.NewLine, popupMessages.Select(x => x.Text)));

                PopupMessagesOK?.Invoke(_dummySender, EventArgs.Empty);
            }));
        }

        public static void ShowMessageBox(Form parentForm, string text)
        {
            if (parentForm == null) throw new NullException(() => parentForm);

            parentForm.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(text);
            }));
        }
    }
}
